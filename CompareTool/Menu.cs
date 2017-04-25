﻿using System;

namespace CompareTool
{
    public static class Menu
    {
        private static string Menuchoice { get; set; }

        private static bool IsDataSelected { get; set; }

        private static bool IsDataCompared { get; set; }

        private static bool IsMenuVisible { get; set; }

        public static void MenuProcess()
        {
            IsMenuVisible = true;

            while (Menuchoice != "9")
            {
                if (IsMenuVisible == true)
                {
                    Console.WriteLine("- - > Compare Tool v. 0.1 < - -");
                    Console.WriteLine(" ");
                    Console.WriteLine("> MENU <\n");
                    Console.WriteLine("*** Please enter the number that you want to do:\n");
                    Console.WriteLine("1. Show avaiable files list\n");
                    Console.WriteLine("2. Select files\n");
                    Console.WriteLine("3. Show selected file names\n");
                    Console.WriteLine("4. Compare files\n");
                    Console.WriteLine("5. Show Discrepancies\n");
                    Console.WriteLine("9. \"Quit\". Exit\n");
                    Console.WriteLine("- - - - - - - - - - - - - - - -");
                    CheckIfDataReadyToCompare();
                    Console.WriteLine("- - - - - - - - - - - - - - - -");

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
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Sorry, invalid selection\n");                            
                            Console.ResetColor();
                            MakeMenuVisible();
                            break;
                    }
                }
            }
        }

        private static void CheckIfDataReadyToCompare()
        {
            var dataSelected = string.Empty;

            if (IsDataSelected == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                dataSelected = "Yes";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                dataSelected = "No";
            }

            Console.WriteLine($"Data selected : {dataSelected}\n");                

            Console.ResetColor();
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
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine($"Your choice is \"{text}\"");
            Console.ResetColor();
        }

        private static void CompareFiles(TextFilesComparator textFilesComparator)
        {
            string fileFirst = InputCollector.FirstFileName;
            string fileSecond = InputCollector.SecondFileName;

            if (fileFirst == null || fileSecond == null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine("\nPlease select at least two files to start comparing\n");
                Console.ResetColor();
                MakeMenuVisible();
                return;
            }

            var fileFirstData = FileReader.GetFileContentAsText(fileFirst);
            var fileSecondData = FileReader.GetFileContentAsText(fileSecond);

            var fileFirstList = FileReader.GetFileTextAsLinesToList(fileFirst);
            var fileSecondList = FileReader.GetFileTextAsLinesToList(fileSecond);

            textFilesComparator.ComparisonFinished += ConsoleOutput.OnComparisonFinished;
            textFilesComparator.ComparisonFinished += FileWriter.OnComparisonFinished;
            textFilesComparator.DiscrepanciesFound += FileWriter.OnDiscrepanciesFound;

            textFilesComparator.CompareFilesAsStrings(fileFirstData, fileSecondData);

            textFilesComparator.PutDiscrepanciesToList(fileFirstList, fileSecondList);

            IsDataCompared = true;

            MakeMenuVisible();
        }

        private static void ShowDiscrepancies(TextFilesComparator textFilesComparator)
        {
            if (!IsDataCompared)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nData was not compared\n");
                Console.ResetColor();
                MakeMenuVisible();
                return;
            }
                
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
            Console.WriteLine();

            if (InputCollector.FirstFileName == null || InputCollector.SecondFileName == null)
            {
                MakeMenuVisible();
                IsDataSelected = false;
                return;
            }

            MakeMenuVisible();

            IsDataSelected = true;
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
            ConsoleKeyInfo keyInfo;

            do
            {
              Console.WriteLine("Press Enter to continue\n");
              keyInfo = Console.ReadKey();

                if (keyInfo.Key != ConsoleKey.Enter)
                {
                    Console.SetCursorPosition(0, Console.CursorTop);
                }

            } while (keyInfo.Key != ConsoleKey.Enter);
                   
            IsMenuVisible = true;
            Console.Clear();
        }
    }
}