using System;
using System.Collections.Generic;
using System.Text;
using WordCounter.File;
using WordCounter.Interfaces;

namespace WordCounter.Counter
{
    public class SimpleCounter : ICounter
    {
        private int minLen;
        public IReader Reader { get; set; }

        public SimpleCounter(IReader reader, int minLen)
        {
            Reader = reader;
            this.minLen = minLen;
        }

        public Dictionary<string, int> Count(Dictionary<string, int> res = null)
        {
            Dictionary<string, int> result = res == null ? new Dictionary<string, int>() : res;

            foreach (string word in Reader.GetWords(minLen))
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
