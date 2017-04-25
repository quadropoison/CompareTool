using System;
using System.Collections.Generic;
using System.Linq;
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

        [TestMethod]
        public void GetDiscrepanciesFromFilesData_ShouldReturn_UniqueRowFromFirstList()
        {
         List<string> firstListWithDataRows = new List<string> {"zero", "zero", "one", "two"};
         List<string> secondListWithDataRows = new List<string> {"zero", "zero", "one"};

         TextFilesComparator comparator = new TextFilesComparator();

         List<string> resultList = comparator.PutDiscrepanciesToList(firstListWithDataRows, secondListWithDataRows);
         var result = resultList[0];

         Assert.IsTrue(result != null);            
         Assert.IsTrue(result.Contains("two"));
        }

        [TestMethod]
        public void GetDiscrepanciesFromFilesData_ShouldReturn_UniqueRowFromSecondList()
        {
            List<string> firstListWithDataRows = new List<string> { "zero", "zero" };
            List<string> secondListWithDataRows = new List<string> { "zero", "zero", "one" };

            TextFilesComparator comparator = new TextFilesComparator();

            List<string> resultList = comparator.PutDiscrepanciesToList(firstListWithDataRows, secondListWithDataRows);
            var result = resultList[0];

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Contains("one"));
        }
    }
}
