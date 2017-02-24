﻿using System;
using System.Collections.Generic;

namespace CompareTool
{
    public static class ConsoleOutput
    {
        public static void ShowToConsoleSingleString(this object data)
        {
            Console.WriteLine($"\n{data}\n");
        }

        public static void ShowToConsoleStringsList(this List<string> data)
        {
            int row = 1;
            foreach (var item in data)
            {
                if (data.Count < 2)
                {
                    Console.WriteLine($"{item}");
                }
                else
                {
                    Console.WriteLine($"{row}. {item}");
                    row++;
                }
            }
        }

        public static void ShowInstructionsForFileWithNumber(int number)
        {
            Console.WriteLine("{1}Type in name of the {0} file to compare:{1}", number, "\n");
        }

        public static void ShowDataMatch()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("All data match");
            Console.ResetColor();
        }

        public static void ShowDiscrepancyFound()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Discrepancy found");
            Console.ResetColor();
        }

        public static void OnComparisonSuccessfullyFinished(Object source, ComparatorEventArgs e)
        {
            if (e.isSuccess)
            {
                FileWriter.OutputData = "All data match";
                ShowDataMatch();
                FileWriter.WriteTxtOutput();
                return;
            }

            FileWriter.OutputData = "Discrepancy found";
            ShowDiscrepancyFound();
            FileWriter.WriteTxtOutput();
        }

        public static void ShowInstructionsWhenNoFilesFound(string fileName)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{1}There is no such file {0} in The Test Folder, try to put this file in appropriate folder and Try again.{1}", fileName, "\n");
            Console.ResetColor();
        }
    }
}