using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static CompareTool.Extentions;

namespace CompareTool
{
    public static class InputCollector
    {
        public static string FirstFileName { get; set; }
        public static string SecondFileName { get; set; }

        public static List<string> CollectFileNamesToCompare()
        {
            List<string> files = new List<string>() { FirstFileName, SecondFileName };
            return files;                      
        }

        public static string SetFileName()
        {         
            var folder = DirectoryObserver.GetTestDataFolderPath();
                                 
            int attempt = 0;

            string fileName = null;

            while (attempt != 3)
            {
                fileName = Console.ReadLine();

                int fileIndex;
                bool successfullyParsed = int.TryParse(fileName, out fileIndex);
                if (successfullyParsed)
                {
                   fileName = DirectoryObserver.GetFileNameByIndex(fileIndex);
                }

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