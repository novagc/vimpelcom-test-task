using System;
using System.Collections.Generic;
using System.Text;

namespace WordCounter.Interfaces
{
    public interface IReader
    {
        public ISource Source { get; set; }
        public IEnumerable<string> GetWords(int minLen);
    }
}
