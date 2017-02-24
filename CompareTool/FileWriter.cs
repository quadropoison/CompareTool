using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace CompareTool
{
    public static class FileWriter
    {
        public static string OutputData { get; set; }

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
            File.WriteAllText(outputPath, OutputData);
            Console.WriteLine("Output saved");            
        }

        public static void WriteTestResult(bool isSuccess)
        {
            if (!isSuccess)
            {
                OutputData = "Discrepancy found";
                WriteTxtOutput();
                return;
            }

            OutputData = "All data match";
            WriteTxtOutput();
        }

        public static void OnComparisonFinished(Object source, ComparatorEventArgs e)
        {
           WriteTestResult(e.IsSuccess);
        }
    }
}