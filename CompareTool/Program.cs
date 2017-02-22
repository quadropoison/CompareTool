using System;
using static CompareTool.InputCollector;

namespace CompareTool
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            FolderReader folderReader = new FolderReader();
            FileReader fileReader = new FileReader();

            var testDataFolder = folderReader.FindTestDataFolder();
            var avaliableFilesInfo = folderReader.TakeAllFilesFromTestFolder(testDataFolder);
            var namesOfAvaliableFiles = folderReader.GetAllFileNames(avaliableFilesInfo);

            Console.WriteLine("Avaliable files list :\n");
            namesOfAvaliableFiles.ShowToConsoleStringsList();

            ConsoleOutput.ShowInstructionsForFileWithNumber(1);
            var fileFirst = SetFileName();
            ConsoleOutput.ShowInstructionsForFileWithNumber(2);
            var fileSecond = SetFileName();
            
            var fileBundle = CollectFileNamesToCompare(fileFirst, fileSecond);                                    
            Console.WriteLine("{0}Selected files :{0}", "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;                        
            fileBundle.ShowToConsoleStringsList();
            Console.ResetColor();
            Console.WriteLine("{0}Press any key to compare ...{0}", "\n");
            Console.ReadLine();
                    
            var fileFirstData = fileReader.GetFileContentAsText(testDataFolder, fileFirst);
            var fileSecondData = fileReader.GetFileContentAsText(testDataFolder, fileSecond);         
            
            Comparator.CompareFilesAsText(fileFirstData, fileSecondData);
            Console.ReadLine();
        }
    }
}
