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
    }
}