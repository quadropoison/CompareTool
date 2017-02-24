using System;
using System.Collections.Generic;
using static CompareTool.Extentions;

namespace CompareTool
{
    public static class InputCollector
    {
        public static List<string> CollectFileNamesToCompare(string fileOne, string fileTwo)
        {
            List<string> files = new List<string>() { fileOne, fileTwo };
            return files;                      
        }

        public static string SetFileName()
        {         
            var folder = DirectoryObserver.GetTestDataFolderPath();            

            string fileName = null;            
            int attempt = 0;

            while (attempt != 3)
            {
                fileName = Console.ReadLine();                
                var filePath = $"{folder}\\{fileName}";
                bool isfileExist = IsFileExist(filePath);

                if (isfileExist)
                    return fileName;

                ConsoleOutput.ShowInstructionsWhenShouldTypeinAnotherFileName(fileName);

                attempt++;
            }

            ConsoleOutput.ShowInstructionsWhenNoFilesFound(fileName);

            return null;
        }
    }
}