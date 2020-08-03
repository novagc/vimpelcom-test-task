using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WordCounter.Other
{
    static class ToolsIO
    {
        public static string[] GetTextFilesPaths(string path) => Directory.GetFiles(path, "*.txt");
    }
}
