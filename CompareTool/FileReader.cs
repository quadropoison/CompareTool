using System.Collections.Generic;
using System.IO;

namespace CompareTool
{
    public static class FileReader
    {
        public static string GetFileContentAsText(string filePath)
        {            
            var fileInfo = DirectoryObserver.TakeOneFileFromTestFolder(filePath);
            var fileContentInText = GetFileText(fileInfo.FullName);
            return fileContentInText;
        }

        private static string GetFileText(string name)
        {
            string fileContents = string.Empty;
            if (File.Exists(name))
            {
                fileContents = File.ReadAllText(name);
            }

            return fileContents;
        }

        public static List<string> GetFileTextAsLinesToList(string name)

        {
            var fileInfo = DirectoryObserver.TakeOneFileFromTestFolder(name);
            List<string> fileContents = new List<string>();
            if (File.Exists(fileInfo.FullName))
            {
                var temp = File.ReadAllLines(fileInfo.FullName);
                foreach (var s in temp) fileContents.Add(s);
            }
                        
            return fileContents;
        }
    }
}