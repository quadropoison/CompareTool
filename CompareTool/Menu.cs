using System;

namespace CompareTool
{
    public static class Menu
    {
        private static string Menuchoice { get; set; }

        private static bool IsMenuVisible { get; set; }

        public static void MenuProcess()
        {
            IsMenuVisible = true;

            while (Menuchoice != "9")
            {
                if (IsMenuVisible == true)
                {
                    Console.Clear();
                    Console.WriteLine("> MENU <\n");
                    Console.WriteLine("*** Please enter the number that you want to do:\n");
                    Console.WriteLine("1. Show avaiable files list\n");
                    Console.WriteLine("2. Select files\n");
                    Console.WriteLine("3. Show selected file names\n");
                    Console.WriteLine("4. Compare files\n");
                    Console.WriteLine("5. Show Discrepancies\n");
                    Console.WriteLine("9. \"Quit\". Exit\n");

                    Menuchoice = Console.ReadLine();

                    TextFilesComparator textFilesComparator = new TextFilesComparator();

                    switch (Menuchoice)
                    {
                        case "1":
                            ShowUserChoice(Menuchoice);
                            ShowAvailableFiles();
                            break;

                        case "2":
                            ShowUserChoice(Menuchoice);
                            SetFilesForComparing();
                            break;

                        case "3":
                            ShowUserChoice(Menuchoice);
                            ShowSelectedFiles();
                            break;

                        case "4":
                            ShowUserChoice(Menuchoice);
                            CompareFiles(textFilesComparator);
                            break;

                        case "5":
                            ShowUserChoice(Menuchoice);
                            ShowDiscrepancies(textFilesComparator);
                            break;

                        case "9":
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Sorry, invalid selection\n");                            
                            Console.ResetColor();
                            Console.WriteLine("Press any key to continue\n");
                            break;
                    }
                }
            }
        }

        private static void ShowUserChoice(string choice)
        {
            IsMenuVisible = false;

            string text = string.Empty;

            switch (choice)
            {
                case "1":
                    text = "Show available files list";
                    break;

                case "2":
                    text = "Select files";
                    break;

                case "3":
                    text = "Show selected file names";
                    break;

                case "4":
                    text = "Compare files";
                    break;

                case "5":
                    text = "Show Discrepancies";
                    break;

                default:
                    break;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Your choice is \"{text}\"");
            Console.ResetColor();
        }

        private static void CompareFiles(TextFilesComparator textFilesComparator)
        {
            var fileFirstData = FileReader.GetFileContentAsText(InputCollector.FirstFileName);
            var fileSecondData = FileReader.GetFileContentAsText(InputCollector.SecondFileName);

            var fileFirstList = FileReader.GetFileTextAsLinesToList(InputCollector.FirstFileName);
            var fileSecondList = FileReader.GetFileTextAsLinesToList(InputCollector.SecondFileName);

            textFilesComparator.ComparisonFinished += ConsoleOutput.OnComparisonFinished;
            textFilesComparator.ComparisonFinished += FileWriter.OnComparisonFinished;
            textFilesComparator.DiscrepanciesFound += FileWriter.OnDiscrepanciesFound;

            textFilesComparator.CompareFilesAsStrings(fileFirstData, fileSecondData);

            textFilesComparator.PutDiscrepanciesToList(fileFirstList, fileSecondList);

            MakeMenuVisible();
        }

        private static void ShowDiscrepancies(TextFilesComparator textFilesComparator)
        {
            var fileFirstList = FileReader.GetFileTextAsLinesToList(InputCollector.FirstFileName);
            var fileSecondList = FileReader.GetFileTextAsLinesToList(InputCollector.SecondFileName);

            var diff = textFilesComparator.PutDiscrepanciesToList(fileFirstList, fileSecondList);

            diff.ShowToConsoleStringsList();

            MakeMenuVisible();
        }

        private static void ShowSelectedFiles()
        {
            var fileBundle = InputCollector.CollectFileNamesToCompare();
            Console.WriteLine("{0}Selected files :{0}", "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            fileBundle.ShowToConsoleStringsList();
            Console.ResetColor();

            MakeMenuVisible();
        }

        private static void SetFilesForComparing()
        {
            ConsoleOutput.ShowInstructionsForFileWithNumber(1);
            InputCollector.FirstFileName = InputCollector.SetFileName();
            ConsoleOutput.ShowInstructionsForFileWithNumber(2);
            InputCollector.SecondFileName = InputCollector.SetFileName();

            MakeMenuVisible();
        }

        private static void ShowAvailableFiles()
        {
            var availableFilesInfo = DirectoryObserver.TakeAllFilesFromTestDataFolder();
            var namesOfAvailableFiles = DirectoryObserver.GetAllFileNames(availableFilesInfo);
            namesOfAvailableFiles.ShowToConsoleStringsList();

            MakeMenuVisible();
        }

        private static void MakeMenuVisible()
        {
            ConsoleKeyInfo k;

            do
            {
              Console.WriteLine("Press Enter to continue\n");
              k = Console.ReadKey();
            } while (k.Key != ConsoleKey.Enter);
                   
            IsMenuVisible = true;
        }
    }
}