using System;
using System.Collections.Generic;
using System.Text;
using WordCounter.File;
using WordCounter.Interfaces;

namespace WordCounter.Counter
{
    public class SimpleCounter : ICounter
    {
        public IReader Reader { get; set; }

        public SimpleCounter(IReader reader)
        {
            Reader = reader;
        }

        public Dictionary<string, int> Count()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (string word in Reader.GetWords())
            {
                if (!result.ContainsKey(word))
                {
                    result.Add(word, 1);
                }
                else
                {
                    result[word]++;
                }
            }

            return result;
        }
    }
}
