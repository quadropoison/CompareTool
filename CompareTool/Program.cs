using System;
using static CompareTool.ConsoleOutput;
using static CompareTool.InputCollector;

namespace CompareTool
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            Menu.MenuProcess();                
        }
    }

    public static class Menu
    {
        private static string Menuchoice { get; set; }

        public static void MenuProcess()
        {
            while (Menuchoice != "6")
            {
                Console.WriteLine("{0}", "\n");
                Console.WriteLine("MENU");
                Console.WriteLine("{0}", "\n");
                Console.WriteLine("Please enter the number that you want to do:");
                Console.WriteLine("{0}", "\n");
                Console.WriteLine("1. Show avaliable files list\n");
                Console.WriteLine("2. Select files\n");
                Console.WriteLine("3. Show selected file names\n");
                Console.WriteLine("4. Compare files\n");
                Console.WriteLine("5. Show Discrepancies\n");
                Console.WriteLine("6. \"Quit\". Exit");
                Console.WriteLine("{0}", "\n");                

                Menuchoice = Console.ReadLine();

                Comparator comparator = new Comparator();

                switch (Menuchoice)
                {
                    case "1":

                        Console.WriteLine("{0}", "\n");
                        var avaliableFilesInfo = DirectoryObserver.TakeAllFilesFromTestDataFolder();
                        var namesOfAvaliableFiles = DirectoryObserver.GetAllFileNames(avaliableFilesInfo);
                        namesOfAvaliableFiles.ShowToConsoleStringsList();

                        break;

                    case "2":

                        ShowInstructionsForFileWithNumber(1);
                        InputCollector.FirstFileName = SetFileName();
                        ShowInstructionsForFileWithNumber(2);
                        InputCollector.SecondFileName = SetFileName();

                        break;

                    case "3":

                        Console.WriteLine("{0}", "\n");
                        var fileBundle = CollectFileNamesToCompare();
                        Console.WriteLine("{0}Selected files :{0}", "\n");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        fileBundle.ShowToConsoleStringsList();
                        Console.ResetColor();

                        break;

                    case "4":

                        Console.WriteLine("{0}", "\n");
                        var fileFirstData = FileReader.GetFileContentAsText(InputCollector.FirstFileName);
                        var fileSecondData = FileReader.GetFileContentAsText(InputCollector.SecondFileName);

                        comparator.ComparisonFinished += OnComparisonFinished;
                        comparator.ComparisonFinished += FileWriter.OnComparisonFinished;

                        comparator.CompareFilesAsStrings(fileFirstData, fileSecondData);

                        break;

                    case "5":

                        Console.WriteLine("{0}", "\n");
                        var fileFirstList = FileReader.GetFileTextAsLinesToList(InputCollector.FirstFileName);
                        var fileSecondList = FileReader.GetFileTextAsLinesToList(InputCollector.SecondFileName);
                        var diff = comparator.PutDiscrepanciesToList(fileFirstList, fileSecondList);
                        diff.ShowToConsoleStringsList();

                        break;

                    case "6":

                        break;

                    default:

                        Console.WriteLine("{0}", "\n");
                        Console.WriteLine("Sorry, invalid selection");

                        break;
                }
            }
        }
    }
}
