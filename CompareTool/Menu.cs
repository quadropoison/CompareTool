using System;

namespace CompareTool
{
    public static class Menu
    {
        private static string Menuchoice { get; set; }

        public static void MenuProcess()
        {
            while (Menuchoice != "9")
            {
                Console.WriteLine("> MENU <\n");
                Console.WriteLine("*** Please enter the number that you want to do:\n");
                Console.WriteLine("1. Show avaiable files list\n");
                Console.WriteLine("2. Select files\n");
                Console.WriteLine("3. Show selected file names\n");
                Console.WriteLine("4. Compare files\n");
                Console.WriteLine("5. Show Discrepancies\n");
                Console.WriteLine("9. \"Quit\". Exit\n");

                Menuchoice = Console.ReadLine();

                Comparator comparator = new Comparator();

                switch (Menuchoice)
                {
                    case "1":
                        ShowAvailableFiles();
                        break;

                    case "2":
                        SetFilesForComparing();
                        break;

                    case "3":
                        ShowSelectedFiles();
                        break;

                    case "4":
                        CompareFiles(comparator);
                        break;

                    case "5":
                        ShowDiscrepancies(comparator);
                        break;

                    case "9":
                        break;

                    default:
                        Console.WriteLine("Sorry, invalid selection");
                        break;
                }
            }
        }

        private static void CompareFiles(Comparator comparator)
        {
            var fileFirstData = FileReader.GetFileContentAsText(InputCollector.FirstFileName);
            var fileSecondData = FileReader.GetFileContentAsText(InputCollector.SecondFileName);

            var fileFirstList = FileReader.GetFileTextAsLinesToList(InputCollector.FirstFileName);
            var fileSecondList = FileReader.GetFileTextAsLinesToList(InputCollector.SecondFileName);

            comparator.ComparisonFinished += ConsoleOutput.OnComparisonFinished;
            comparator.ComparisonFinished += FileWriter.OnComparisonFinished;
            comparator.DiscrepanciesFound += FileWriter.OnDiscrepanciesFound;            

            comparator.CompareFilesAsStrings(fileFirstData, fileSecondData);

            comparator.PutDiscrepanciesToList(fileFirstList, fileSecondList);
        }

        private static void ShowDiscrepancies(Comparator comparator)
        {
            var fileFirstList = FileReader.GetFileTextAsLinesToList(InputCollector.FirstFileName);
            var fileSecondList = FileReader.GetFileTextAsLinesToList(InputCollector.SecondFileName);

            var diff = comparator.PutDiscrepanciesToList(fileFirstList, fileSecondList);

            diff.ShowToConsoleStringsList();
        }

        private static void ShowSelectedFiles()
        {
            var fileBundle = InputCollector.CollectFileNamesToCompare();
            Console.WriteLine("{0}Selected files :{0}", "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            fileBundle.ShowToConsoleStringsList();
            Console.ResetColor();
        }

        private static void SetFilesForComparing()
        {
            ConsoleOutput.ShowInstructionsForFileWithNumber(1);
            InputCollector.FirstFileName = InputCollector.SetFileName();
            ConsoleOutput.ShowInstructionsForFileWithNumber(2);
            InputCollector.SecondFileName = InputCollector.SetFileName();
        }

        private static void ShowAvailableFiles()
        {
            var availableFilesInfo = DirectoryObserver.TakeAllFilesFromTestDataFolder();
            var namesOfAvailableFiles = DirectoryObserver.GetAllFileNames(availableFilesInfo);
            namesOfAvailableFiles.ShowToConsoleStringsList();
        }
    }
}