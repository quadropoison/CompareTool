using System;
using static CompareTool.ConsoleOutput;
using static CompareTool.InputCollector;

namespace CompareTool
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var avaliableFilesInfo = DirectoryObserver.TakeAllFilesFromTestDataFolder();
            var namesOfAvaliableFiles = DirectoryObserver.GetAllFileNames(avaliableFilesInfo);

            Console.WriteLine("Avaliable files list :\n");
            namesOfAvaliableFiles.ShowToConsoleStringsList();

            ShowInstructionsForFileWithNumber(1);
            var fileFirst = SetFileName();
            ShowInstructionsForFileWithNumber(2);
            var fileSecond = SetFileName();
            
            var fileBundle = CollectFileNamesToCompare(fileFirst, fileSecond);                                    
            Console.WriteLine("{0}Selected files :{0}", "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;                        
            fileBundle.ShowToConsoleStringsList();
            Console.ResetColor();
            Console.WriteLine("{0}Press any key to compare ...{0}", "\n");
            Console.ReadLine();
                    
            var fileFirstData = FileReader.GetFileContentAsText(fileFirst);
            var fileSecondData = FileReader.GetFileContentAsText(fileSecond);         
            
            Comparator comparator = new Comparator();            
            comparator.ComparisonFinished += OnComparisonFinished;
            comparator.ComparisonFinished += FileWriter.OnComparisonFinished;
            
            comparator.CompareFilesAsText(fileFirstData, fileSecondData);
            Console.ReadLine();         
        }
    }
}
