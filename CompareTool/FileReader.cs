using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;

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

        public static void ReadDataFromFileInIsolatedStorage()
        {
            IsolatedStorageFile isolatedFile = IsolatedStorageFile.GetUserStoreForAssembly();

            ReadIsolatedCompareToolStatusData(isolatedFile);

            ReadIsolatedCompareToolFilesList(isolatedFile);
        }

        private static void ReadIsolatedCompareToolStatusData(IsolatedStorageFile isolatedFile)
        {
            using (
            IsolatedStorageFileStream stream = new IsolatedStorageFileStream(
            "CompareToolStatusData", 
            FileMode.OpenOrCreate,
            isolatedFile))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    var data = sr.ReadLine();

                    FileWriter.DataSelectionStatus = data;

                    if (FileWriter.DataSelectionStatus == "Yes")
                        FileWriter.IsDataSelected = true;
                    else
                        FileWriter.IsDataSelected = false;
                }
            }
        }

        private static void ReadIsolatedCompareToolFilesList(IsolatedStorageFile isolatedFile)
        {
            using (
            IsolatedStorageFileStream stream = new IsolatedStorageFileStream(
            "CompareToolFilesList",
            FileMode.OpenOrCreate,
            isolatedFile))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    var fileName = sr.ReadLine();
                    var itemsList = new List<string>();
                    while (fileName != null)
                    {
                        itemsList.Add(fileName);
                        fileName = sr.ReadLine();
                    }

                    InputCollector.FirstFileName = itemsList[0];
                    InputCollector.SecondFileName = itemsList[1];
                }
            }
        }
    }
}