using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using libcommon.Sorting;

namespace Testy
{
    /// <summary>
    /// Summary description for SortsTest
    /// </summary>
    [TestClass]
    public class SortsTest
    {
        public SortsTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        
        private bool IsSorted(int[] arr)
        {
            for (int i = 0; i < arr.Length-1; i++)
            {
                if (arr[i] > arr[i + 1])
                    return false;
            }
            return true;
        }

        private bool AreElementsIdentical(int[] a, int[] b)
        {
            if (a.Length != b.Length)
                return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }
            return true;
        }

        [TestMethod]
        public void MergeSort1()
        {
            int[] testArr = new int[] { 8,3,5,7 };
            Sorts.MergeSort(testArr);
            Assert.IsTrue(AreElementsIdentical(testArr, new int[] { 3, 5, 7, 8 }));
        }

        [TestMethod]
        public void MergeSort2()
        {
            int[] testArr = new int[] { 1, 7, 5, 3, 8, 6, 4, 1, 2, 9 };
            Sorts.MergeSort(testArr);
            Assert.IsTrue(IsSorted(testArr));
        }

        [TestMethod]
        public void MergeSort3()
        {
            int[] testArr = new int[] { -8, 3, 5, 7,2 };
            Sorts.MergeSort(testArr);
            Assert.IsTrue(AreElementsIdentical(testArr, new int[] { -8,2,3,5,7 }));
        }

        [TestMethod]
        public void MergeSort4()
        {
            int[] testArr = new int[] { 1,2,3,4,5 };
            Sorts.MergeSort(testArr);
            Assert.IsTrue(AreElementsIdentical(testArr, new int[] { 1,2,3,4,5 }));
        }

        [TestMethod]
        public void MergeSort5()
        {
            int[] testArr = new int[] { 5,4,3,2,1 };
            Sorts.MergeSort(testArr);
            Assert.IsTrue(AreElementsIdentical(testArr, new int[] { 1, 2, 3, 4, 5 }));
        }

    }
}
