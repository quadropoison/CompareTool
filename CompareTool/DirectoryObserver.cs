using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CompareTool
{
    public static class DirectoryObserver
    {
        private const string OutputFolderName = "Output";
        private const string TestDataFolderName = "TestData";

        private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();

        public static List<string> NamesOfAvaliableFiles = GetNamesOfAvaliableFiles();

        private static List<string> GetNamesOfAvaliableFiles()
        {
            var avaliableFilesInfo = TakeAllFilesFromTestDataFolder();
            return NamesOfAvaliableFiles = GetAllFileNames(avaliableFilesInfo);
        }

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

        private static Dictionary<int, string> PutFileNamesListToDictionary()
        {
            int key = 1;
            Dictionary<int, string> result = new Dictionary<int, string>();

            foreach (var item in NamesOfAvaliableFiles)
            {
                if (!result.ContainsKey(key))
                {
                    result.Add(key, item);
                    key++;
                }
            }

            return result;
        }

        public static string GetFileNameByIndex(int index)
        {
            var fileDictionary = PutFileNamesListToDictionary();
            return fileDictionary.Where(pair => pair.Key == index)
                                 .Select(pair => pair.Value)
                                 .FirstOrDefault();
        }
    }
}