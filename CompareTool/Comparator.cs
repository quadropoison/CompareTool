using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CompareTool
{
    public class ComparatorEventArgs : EventArgs
    {
        public bool IsSuccess { get; set; }
    }

    public class Comparator: IComparator
    {
        public bool IsEquals { get; set; }

        public event EventHandler<ComparatorEventArgs> ComparisonFinished;

        public void CompareFilesAsStrings(string fileFirst, string fileSecond)
        {
                CompareTwoObjects(fileFirst, fileSecond);
                OnComparisonFinished(IsEquals);
        }

        public void CompareTwoObjects(object itemExpected, object itemActual)
        {
            IsEquals = itemExpected.Equals(itemActual);
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

    public interface IComparator
    {
        bool IsEquals { get; set; }
        void CompareTwoObjects(object itemExpected, object itemActual); 
    }
}