namespace CompareTool
{
    public static class Comparator
    {
        public static void CompareFilesAsText(string file1, string file2)
        {
            if (!CompareTwoObjects(file1, file2))
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