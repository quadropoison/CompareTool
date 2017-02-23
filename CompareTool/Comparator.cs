namespace CompareTool
{
    public static class Comparator
    {
        public static void CompareFilesAsText(string fileFirst, string fileSecond)
        {
            if (!CompareTwoObjects(fileFirst, fileSecond))
                ConsoleOutput.ShowDiscrepancyFound();
            else
                ConsoleOutput.ShowDataMatch();
        }

        private static bool CompareTwoObjects(object itemExpected, object itemActual)
        {
            return itemExpected.Equals(itemActual);
        }
    }
}