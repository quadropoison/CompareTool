using System.IO;

namespace CompareTool
{
    public class FileReader
    {
        public string GetFileContentAsText(string testDataFolder, string filePath)
        {
            FolderReader folderReader = new FolderReader();
            var fileInfo = folderReader.TakeOneFileFromTestFolder(testDataFolder, filePath);
            var fileContentInText = GetFileText(fileInfo.FullName);
            return fileContentInText;
        }

        private string GetFileText(string name)
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