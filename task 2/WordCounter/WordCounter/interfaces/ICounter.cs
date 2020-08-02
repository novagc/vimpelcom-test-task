using System;
using System.Collections.Generic;
using System.Text;

namespace WordCounter.Interfaces
{
    interface ICounter
    {
        Dictionary<string, int> Count(IReader reader);
    }
}
