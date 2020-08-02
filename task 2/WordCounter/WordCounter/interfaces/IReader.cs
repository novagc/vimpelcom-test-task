using System;
using System.Collections.Generic;
using System.Text;

namespace WordCounter.Interfaces
{
    interface IReader
    {
        IEnumerable<string> GetWords(ISource source);
    }
}
