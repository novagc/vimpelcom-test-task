using System;
using System.Collections.Generic;
using System.Text;

namespace WordCounter.Interfaces
{
    public interface ISource
    {
        string Path { get; set; }
        public string Read();
    }
}
