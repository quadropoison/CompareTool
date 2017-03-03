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
            InputCollector.FirstFileName = SetFileName();
            ShowInstructionsForFileWithNumber(2);
            InputCollector.SecondFileName = SetFileName();
            
            var fileBundle = CollectFileNamesToCompare();                                    
            Console.WriteLine("{0}Selected files :{0}", "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;                        
            fileBundle.ShowToConsoleStringsList();
            Console.ResetColor();
            Console.WriteLine("{0}Press any key to compare ...{0}", "\n");
            Console.ReadLine();
                    
            var fileFirstData = FileReader.GetFileContentAsText(InputCollector.FirstFileName);
            var fileSecondData = FileReader.GetFileContentAsText(InputCollector.SecondFileName);         
            
            Comparator comparator = new Comparator();            
            comparator.ComparisonFinished += OnComparisonFinished;
            comparator.ComparisonFinished += FileWriter.OnComparisonFinished;   
                     
            comparator.CompareFilesAsStrings(fileFirstData, fileSecondData);

            // *
            Console.WriteLine("{0}Press any key to show differences ...{0}", "\n");
            Console.ReadLine();
            var fileFirstList = FileReader.GetFileTextAsLinesToList(InputCollector.FirstFileName);
            var fileSecondList = FileReader.GetFileTextAsLinesToList(InputCollector.SecondFileName);
            var diff = comparator.PutDiscrepanciesToList(fileFirstList, fileSecondList);
            diff.ShowToConsoleStringsList();
            // *
            
            Console.ReadLine();         
        }
    }
}
