﻿using System;
using System.Collections.Generic;

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
            var fileInspector = new Extentions();
            var folderReader = new FolderReader();
            var folder = folderReader.FindTestDataFolder();

            string fileName = null;            
            int attempt = 0;

            while (attempt != 3)
            {
                fileName = Console.ReadLine();                
                var filePath = string.Format("{0}\\{1}", folder, fileName);
                bool isfileExist = fileInspector.IsFileExist(filePath);

                if (isfileExist)
                    return fileName;                    
                else
                    Console.WriteLine(string.Format("{1}There is no such file '{0}' in The Test Folder, try another one{1}", fileName, "\n"));

                attempt++;
            }

            ConsoleOutput.ShowInstructionsWhenNoFilesFound(fileName);

            return null;
        }
    }
}