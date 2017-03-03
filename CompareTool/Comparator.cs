using System;
using System.Collections.Generic;
using System.Linq;

namespace CompareTool
{
    public class ComparatorEventArgs : EventArgs
    {
        public bool IsSuccess { get; set; }

    }

    public class Comparator
    {
        public event EventHandler<ComparatorEventArgs> ComparisonFinished;

        public void CompareFilesAsStrings(string fileFirst, string fileSecond)
        {
            if (!CompareTwoObjects(fileFirst, fileSecond))
                OnComparisonFinished(false);
            else
                OnComparisonFinished(true);
        }

        private static bool CompareTwoObjects(object itemExpected, object itemActual)
        {
            return itemExpected.Equals(itemActual);
        }

        public List<string> PutDiscrepanciesToList(List<string> fileFirst, List<string> fileSecond)
        {
            var result = GetUniqueRowsFromFilesData(fileFirst, fileSecond);

            return result;
        }

        private static List<string> GetUniqueRowsFromFilesData(List<string> fileFirstData, List<string> fileSecondData)
        {
            List<string> uniqueValues = new List<string>();

            var uniqueRowsInFirstFile = fileFirstData.Except(fileSecondData);
            var uniqueRowsInSecondFile = fileSecondData.Except(fileFirstData);
            var fileFirstName = InputCollector.FirstFileName;
            var fileSecondName = InputCollector.SecondFileName;

            foreach (var row in uniqueRowsInFirstFile)
            {
                uniqueValues.Add(row + " || " + $"[{fileFirstName}]");
            }

            foreach (var row in uniqueRowsInSecondFile)
            {
                uniqueValues.Add(row + " || " + $"[{fileSecondName}]");
            }

            return uniqueValues;
        }

        private void OnComparisonFinished(bool result)
        {
            ComparisonFinished?.Invoke(this, new ComparatorEventArgs() {IsSuccess = result});
        }
    }
}