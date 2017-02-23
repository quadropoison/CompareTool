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
            var folder = FolderReader.GetTestDataFolderPath();            

            string fileName = null;            
            int attempt = 0;

            while (attempt != 3)
            {
                fileName = Console.ReadLine();                
                var filePath = $"{folder}\\{fileName}";
                bool isfileExist = IsFileExist(filePath);

                if (isfileExist)
                    return fileName;

                Console.WriteLine("{1}There is no such file '{0}' in The Test Folder, try another one{1}", fileName, "\n");

                attempt++;
            }

            ConsoleOutput.ShowInstructionsWhenNoFilesFound(fileName);

            return null;
        }
    }
}