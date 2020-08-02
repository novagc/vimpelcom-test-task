using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WordCounter.Interfaces;

namespace WordCounter.File
{
    public class FileReader : IReader
    {
        public ISource Source { get; set; }

        public FileReader(ISource source)
        {
            Source = source;
        }

        public IEnumerable<string> GetWords() => (IEnumerable<string>)new Regex(@"\w{10,}").Matches(Source.Read());
    }
}
