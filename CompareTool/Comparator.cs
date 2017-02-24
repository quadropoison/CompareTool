using System;

namespace CompareTool
{
    public class ComparatorEventArgs : EventArgs
    {
        public bool IsSuccess { get; set; }

    }

    public class Comparator
    {
        public event EventHandler<ComparatorEventArgs> ComparisonFinished;        

        public void CompareFilesAsText(string fileFirst, string fileSecond)
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

        private void OnComparisonFinished(bool result)
        {
            ComparisonFinished?.Invoke(this, new ComparatorEventArgs() {IsSuccess = result});
        }
    }
}