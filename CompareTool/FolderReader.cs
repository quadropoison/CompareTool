using System.Collections.Generic;
using System.IO;

namespace CompareTool
{
    public class FolderReader
    {
        public string FindTestDataFolder()
        {
            var testFolderName = "TestData";
            var currentDirectory = Directory.GetCurrentDirectory();
            var testDataFolder = Path.Combine(currentDirectory, testFolderName);
            return testDataFolder;
        }

        public IEnumerable<FileInfo> TakeAllFilesFromTestFolder(string testDataFolder)
        {
            DirectoryInfo testDataFolderData = new DirectoryInfo(testDataFolder);

            IEnumerable<FileInfo> fileList = testDataFolderData.GetFiles("*.*", SearchOption.AllDirectories);

            return fileList;
        }

        public FileInfo TakeOneFileFromTestFolder(string testDataFolder, string fileName)
        {
            DirectoryInfo testDataFolderData = new DirectoryInfo(testDataFolder);

            FileInfo[] fileArray = testDataFolderData.GetFiles(fileName, SearchOption.AllDirectories);

            FileInfo file = fileArray[0];

            return file;
        }

        public List<string> GetAllFileNames(IEnumerable<FileInfo> files)
        {
            List<string> names = new List<string> { };

            foreach (var file in files)
            {
                var name = file.Name;
                names.Add(name);
            }

            return names;
        }
    }
}