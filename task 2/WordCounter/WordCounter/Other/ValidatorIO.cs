using System.IO;

namespace WordCounter.Other
{
    public static class ValidatorIO
    {
        public static bool VerifyDirectoryPath(string path) => Directory.Exists(path);

        public static bool CheckTextFilesExistence(string path) => ToolsIO.GetTextFilesPaths(path).Length > 0;
    }
}
