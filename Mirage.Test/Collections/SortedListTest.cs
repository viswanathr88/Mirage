using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mirage.Collections;
using System;
using System.Collections.Generic;

namespace Mirage_Test.Collections
{
    [TestClass]
    public class SortedListTest
    {
        [TestMethod]
        public void Constructor_Validate_Null_Comparer()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new SortedList<int>(null));
        }

        [TestMethod]
        public void Constructor_Default()
        {
            var list = new SortedList<int>();
            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count, 0);
            Assert.IsFalse(list.IsReadOnly);
        }

        [TestMethod]
        public void Constructor_Valid_Comparer()
        {
            var list = new SortedList<int>(Comparer<int>.Default);
            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count, 0);
        }

        [TestMethod]
        public void Ensure_Sorting()
        {
            var list = new SortedList<int>();
            list.Add(5);
            list.Add(2);
            list.Add(26);
            list.Add(-17);
            list.Add(18);

            Assert.AreEqual(list.Count, 5);
            Assert.AreEqual(list[0], -17);
            Assert.AreEqual(list[1], 2);
            Assert.AreEqual(list[2], 5);
            Assert.AreEqual(list[3], 18);
            Assert.AreEqual(list[4], 26);
        }

        [TestMethod]
        public void Validate_Clear()
        {
            var list = new SortedList<int>();
            list.Add(20);
            list.Add(16);
            list.Add(18);
            Assert.AreEqual(list.Count, 3);

            list.Clear();
            Assert.AreEqual(list.Count, 0);
        }

        [TestMethod]
        public void Validate_Contains()
        {
            var list = new SortedList<int>();
            list.Add(85);
            list.Add(-16);
            list.Add(77);
            list.Add(285);
            Assert.AreEqual(list.Count, 4);

            Assert.IsTrue(list.Contains(-16));
            Assert.IsTrue(list.Contains(285));
            Assert.IsFalse(list.Contains(284));
        }

        [TestMethod]
        public void Validate_Copy_To()
        {
            var list = new SortedList<int>();
            list.Add(85);
            list.Add(-16);
            list.Add(77);
            list.Add(285);

            var listArr = new int[5];
            list.CopyTo(listArr, 1);

            Assert.AreEqual(listArr[1], -16);
            Assert.AreEqual(listArr[2], 77);
            Assert.AreEqual(listArr[3], 85);
            Assert.AreEqual(listArr[4], 285);
        }

        [TestMethod]
        public void Validate_IndexOf()
        {
            var list = new SortedList<int>();
            list.Add(18);
            list.Add(-88);
            list.Add(17);
            Assert.AreEqual(list.Count, 3);

            Assert.AreEqual(list.IndexOf(-88), 0);
            Assert.AreEqual(list.IndexOf(18), 2);
            Assert.AreEqual(list.IndexOf(16), -1);
        }

        [TestMethod]
        public void Validate_Array_Accessor()
        {
            var list = new SortedList<int>();
            list.Add(18);
            list.Add(-88);
            list.Add(17);
            Assert.AreEqual(list.Count, 3);

            Assert.AreEqual(list[0], -88);
            Assert.AreEqual(list[2], 18);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => list[3]);
        }

        [TestMethod]
        public void Validate_Remove()
        {
            var list = new SortedList<int>();
            list.Add(18);
            list.Add(-88);
            list.Add(17);
            Assert.AreEqual(list.Count, 3);
            Assert.IsTrue(list.Contains(17));
            Assert.AreEqual(list[1], 17);
            Assert.AreEqual(list.IndexOf(17), 1);

            list.Remove(17);
            Assert.AreEqual(list.Count, 2);
            Assert.IsFalse(list.Contains(17));
            Assert.AreEqual(list.IndexOf(17), -1);
            Assert.AreEqual(list[1], 18);
        }

        [TestMethod]
        public void Validate_Custom_Comparer()
        {
            var list = new SortedList<int>(new CustomIntComparer());
            Assert.IsNotNull(list);

            list.Add(16);
            list.Add(-24);
            list.Add(8);
            Assert.AreEqual(list.Count, 3);

            // Validate in reverse order
            Assert.AreEqual(list[0], 16);
            Assert.AreEqual(list[1], 8);
            Assert.AreEqual(list[2], -24);
        }
    }

    sealed class CustomIntComparer : Comparer<int>
    {
        public override int Compare(int x, int y)
        {
            return x == y ? 0 : (x < y ? 1 : -1);
        }
    }
}
