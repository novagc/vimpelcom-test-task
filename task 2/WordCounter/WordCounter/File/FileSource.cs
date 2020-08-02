using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using WordCounter.Interfaces;

namespace WordCounter.File
{
    public class FileSource : ISource
    {
        private readonly string path;

        public FileSource(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                throw new FileNotFoundException($"File {path} not found");
            }

            this.path = path;
        }

        public string Read() => System.IO.File.ReadAllText(path);
    }
}
