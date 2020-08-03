using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Resources;
using System.Threading;
using WordCounter.Counter;
using WordCounter.File;
using WordCounter.Interfaces;
using WordCounter.Other;

namespace WordCounter
{
    class Program
    {
        private static Thread[] threads;

        public static int minLen;
        public static Queue<string> filesQueue { get; private set; }

        static void Main(string[] args)
        {
            minLen = RequestMinLen();

            string path = RequestPath();
            string[] files = ToolsIO.GetTextFilesPaths(path);
            
            if (files.Length == 0)
            {
                FilesNotFoundMessage();
                Environment.Exit(1);
            }

            threads = new Thread[files.Length > 8 ? 8 : files.Length]
                .Select(x => new Thread(StartParsing))
                .ToArray();

            filesQueue = new Queue<string>(files);

            var results = Enumerable
                .Range(0, files.Length)
                .Select(x => new Dictionary<string, int>())
                .ToList();

            Dictionary<string, int> result = new Dictionary<string, int>();

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start(results[i]);
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
                foreach (var pair in results[i])
                {
                    if (result.ContainsKey(pair.Key))
                    {
                        result[pair.Key] += pair.Value;
                    }
                    else
                    {
                        result.Add(pair.Key, pair.Value);
                    }
                }
            }

            Console.Clear();

            Console.WriteLine(String.Join("\n", 
                result
                    .OrderByDescending(x => x.Value)
                    .Select(x => $"{x.Key}: {x.Value}")
                    .Take(10))
            );
        }

        static void StartParsing(object rateDictionary)
        {
            FileSource fs;

            lock (filesQueue)
            {
                 fs = new FileSource(filesQueue.Peek());
            }

            FileReader fr = new FileReader(fs);
            SimpleCounter sc = new SimpleCounter(fr, minLen);


            while (ParseNewFile((Dictionary<string, int>)rateDictionary, sc, fr, fs)) { }
        }

        static bool ParseNewFile(Dictionary<string, int> rateDictionary, ICounter counter, IReader reader, ISource source)
        {
            string temp;

            lock (filesQueue)
            {
                if (filesQueue.Count == 0)
                {
                    return false;
                }

                temp = filesQueue.Dequeue();
            }

            if (reader.Source == null || reader.Source != source)
            {
                reader.Source = source;
            }

            source.Path = temp;
            counter.Count(rateDictionary);

            return true;
        }

        static int RequestMinLen()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("MinLen: ");

                string temp = Console.ReadLine();
                int res;

                if (int.TryParse(temp, out res))
                    return res;
            }
        }

        static string RequestPath()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Path: ");

                string path = Console.ReadLine();

                if (ValidatorIO.VerifyDirectoryPath(path))
                    return path;
            }
        }

        static void FilesNotFoundMessage()
        {
            Console.Clear();
            Console.WriteLine("Text files were not found...");
            Console.WriteLine("Click any button to continue");
            Console.ReadKey();
        }
    }
}
