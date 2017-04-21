using System;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompareTool.Tests
{
    [TestClass]
    public class TextFilesComparatorTests
    {
        [TestMethod]
        public void CompareTwoStrings_ShouldReturn_AssertTrue()
        {
            var dataOne = "Test string ZeroZeroOne";

            TextFilesComparator comparator = new TextFilesComparator();
           
            comparator.CompareFilesAsStrings(dataOne, dataOne);
            var compareResult = comparator.IsEquals;

            Assert.IsTrue(compareResult);
        }

        [TestMethod]
        public void CompareTwoStrings_ShouldReturn_AssertFalse()
        {
            var dataOne = "Test string ZeroZeroOne";
            var dataTwo = "Test string ZeroZeroTwo";

            TextFilesComparator comparator = new TextFilesComparator();
           
            comparator.CompareFilesAsStrings(dataOne, dataTwo);
            var compareResult = comparator.IsEquals;

            Assert.IsFalse(compareResult);
        }
    }
}
