using System;

namespace CompareTool
{
    public class ComparatorEventArgs : EventArgs
    {
        public bool isSuccess { get; set; }

    }

    public class Comparator
    {
        public event EventHandler<ComparatorEventArgs> ComparisonSuccessfullyFinished;        

        public void CompareFilesAsText(string fileFirst, string fileSecond)
        {
            if (!CompareTwoObjects(fileFirst, fileSecond))
                OnComparisonSuccessfullyFinished(false);
            else
                OnComparisonSuccessfullyFinished(true);
        }

        private static bool CompareTwoObjects(object itemExpected, object itemActual)
        {
            return itemExpected.Equals(itemActual);
        }

        private void OnComparisonSuccessfullyFinished(bool result)
        {
            ComparisonSuccessfullyFinished?.Invoke(this, new ComparatorEventArgs() {isSuccess = result});
        }
    }
}