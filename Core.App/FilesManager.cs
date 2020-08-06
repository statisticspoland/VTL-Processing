namespace Core.App
{
    using System;
    using System.IO;

    public static class FilesManager
    {
        public static void ResultToFile(string result, string fullFileName, string path = null)
        {
            string directoryPath = GetDirectoryPath(path);

            string combinedPath = Path.Combine(directoryPath, "result", fullFileName);
            Directory.CreateDirectory(Path.GetDirectoryName(combinedPath));
            File.WriteAllText(combinedPath, result);

            Console.WriteLine(combinedPath);
        }

        public static string SourceFromFile(string fullFileName, string path = null)
        {
            string directoryPath = GetDirectoryPath(path);
            string combinedPath = Path.Combine(directoryPath, fullFileName);

            if (File.Exists(combinedPath))
            {
                return File.ReadAllText(combinedPath);
            }
            return string.Empty;
        }

        private static string GetDirectoryPath(string path)
        {
            if (path != null)
            {
                return path;
            }
            else
            {
                return Directory.GetCurrentDirectory();
            }
        }
    }
}
