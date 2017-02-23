using System;

namespace CompareTool
{
    public class ComparatorEventArgs : EventArgs
    {
        public string Output { get; set; }
    }

    public class Comparator
    {
        public event EventHandler<ComparatorEventArgs> ComparisonSuccessfullyFinished;        
        public event EventHandler<ComparatorEventArgs> ComparisonUnsuccessfullyFinished;

        public void CompareFilesAsText(string fileFirst, string fileSecond)
        {
            if (!CompareTwoObjects(fileFirst, fileSecond))
                OnComparisonSuccessfullyFinished();
            else
                OnComparisonUnsuccessfullyFinished();
        }

        private static bool CompareTwoObjects(object itemExpected, object itemActual)
        {
            return itemExpected.Equals(itemActual);
        }

        private void OnComparisonSuccessfullyFinished()
        {
            ComparisonSuccessfullyFinished?.Invoke(null, new ComparatorEventArgs());
        }

        private void OnComparisonUnsuccessfullyFinished()
        {
            ComparisonUnsuccessfullyFinished?.Invoke(null, new ComparatorEventArgs());
        }
    }
}