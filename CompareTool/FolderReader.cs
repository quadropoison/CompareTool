using System.Collections.Generic;
using System.IO;

namespace CompareTool
{
    public static class FolderReader
    {
        private const string OutputFolderName = "Output";
        private const string TestDataFolderName = "TestData";

        private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();

        public static string GetOutputFolderPath()
        {            
            var outputFolder = Path.Combine(CurrentDirectory, OutputFolderName);

            if (Extentions.IsDirectoryExist(outputFolder))
            {
                return outputFolder;
            }

            Directory.CreateDirectory(outputFolder);
            return outputFolder;            
        }

        public static string GetTestDataFolderPath()
        {
            return Path.Combine(CurrentDirectory, TestDataFolderName);
        }

        public static IEnumerable<FileInfo> TakeAllFilesFromTestDataFolder()
        {
            var testDataFolderPath = GetTestDataFolderPath();
            DirectoryInfo testDataFolderInfo = new DirectoryInfo(testDataFolderPath);
            IEnumerable<FileInfo> fileList = testDataFolderInfo.GetFiles("*.*", SearchOption.AllDirectories);
            return fileList;
        }

        public static FileInfo TakeOneFileFromTestFolder(string fileName)
        {
            var testDataFolderPath = GetTestDataFolderPath();
            DirectoryInfo testDataFolderInfo = new DirectoryInfo(testDataFolderPath);
            FileInfo[] fileArray = testDataFolderInfo.GetFiles(fileName, SearchOption.AllDirectories);
            FileInfo file = fileArray[0];
            return file;
        }

        public static List<string> GetAllFileNames(IEnumerable<FileInfo> files)
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