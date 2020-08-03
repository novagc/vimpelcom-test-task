using System;
using System.Collections.Generic;
using System.Text;

namespace WordCounter.Interfaces
{
    public interface ICounter
    {
        public Dictionary<string, int> Count(Dictionary<string, int> res = null);
    }
}
