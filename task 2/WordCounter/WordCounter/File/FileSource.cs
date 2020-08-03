using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using WordCounter.Interfaces;

namespace WordCounter.File
{
    public class FileSource : ISource
    {
        private string path;

        public string Path
        {
            get => path;
            set
            {
                if (!System.IO.File.Exists(value))
                {
                    throw new FileNotFoundException($"File {path} not found");
                }

                path = value;
            }
        }

        public FileSource(string path)
        {
            Path = path;
        }

        public string Read() => System.IO.File.ReadAllText(path);
    }
}
