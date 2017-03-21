using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace CompareTool
{
    public static class FileWriter
    {
        public static List<string> OutputData { get; set; }

        public static void WriteTxtOutput()
        {
            int fileNumber = 0;
            string fileExtention = "txt";
            string fileName = $"TestOutput-{fileNumber}.{fileExtention}";
            string outputPath = $"{DirectoryObserver.GetOutputFolderPath()}\\{fileName}";
            var isCreated = false;

            while (!isCreated)
            {
                if (Extentions.IsFileExist(outputPath))
                {
                    fileNumber++;
                    fileName = $"TestOutput-{fileNumber}.{fileExtention}";
                    outputPath = $"{DirectoryObserver.GetOutputFolderPath()}\\{fileName}";
                }
                else
                {
                    isCreated = true;
                }                
            }

            Console.WriteLine($"File {fileName} created");

            using (StreamWriter outputFile = new StreamWriter(outputPath, true))
            {
                foreach (var line in OutputData)
                {
                    outputFile.WriteLine(line);
                }                
            }

            ////File.WriteAllText(outputPath, OutputData);
            Console.WriteLine("Output saved");            
        }

        public static void WriteTestResult(bool isSuccess)
        {
            var ls = new List<string>();

            if (!isSuccess)
            {
                ls.Add("Discrepancy found");
                OutputData = ls;
                WriteTxtOutput();
                return;
            }

            ls.Add("All data match");
            OutputData = ls; 
            WriteTxtOutput();
        }

        public static void WriteTestResultLine(List<string> dis)
        {
            var data = dis;

            if (data.Count != 0)
            {
                foreach (var line in data)
                {
                    OutputData.Add(line);
                }

                WriteTxtOutput();
                return;
            }
            
            Console.WriteLine("No Discrepancies found");           
        }

        public static void OnComparisonFinished(Object source, ComparatorEventArgs e)
        {
           WriteTestResult(e.IsSuccess);
        }

        public static void OnDiscrepanciesFound(Object source, ComparatorEventArgs e)
        {
            WriteTestResultLine(e.Dis);
        }
    }
}