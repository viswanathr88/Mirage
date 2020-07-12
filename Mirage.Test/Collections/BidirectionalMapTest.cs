using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mirage.Collections;

namespace Mirage_Test
{
    [TestClass]
    public class BidirectionalMapTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var bmap = new BidirectionalMap<int, int>();
            Assert.IsNotNull(bmap);
            Assert.AreEqual(bmap.Count, 0);
            Assert.IsFalse(bmap.IsFixedSize);
            Assert.IsFalse(bmap.IsReadOnly);
        }

        [TestMethod]
        public void AddTest()
        {
            var bmp = new BidirectionalMap<int, int>();
            bmp.Add(5, 10);
            Assert.IsTrue(bmp.Contains(5));
            Assert.AreEqual(bmp.Count, 1);
        }

        [TestMethod]
        public void ClearTest()
        {
            var bmp = new BidirectionalMap<int, int>();
            bmp.Add(5, 10);
            bmp.Add(15, 20);
            Assert.IsTrue(bmp.Contains(5));
            Assert.IsTrue(bmp.Contains(15));
            Assert.AreEqual(bmp.Count, 2);

            bmp.Clear();
            Assert.AreEqual(bmp.Count, 0);
            Assert.IsFalse(bmp.Contains(5));
            Assert.IsFalse(bmp.Contains(15));
        }

        [TestMethod]
        public void ContainsTest()
        {
            var bmp = new BidirectionalMap<int, string>();
            bmp.Add(10, "This is a test");
            Assert.IsTrue(bmp.Contains(10));
            Assert.IsTrue(bmp.Contains("This is a test"));
        }

        [TestMethod]
        public void AccessTest()
        {
            var bmp = new BidirectionalMap<int, double>();
            bmp.Add(25, 10.0);

            Assert.AreEqual(bmp[10.0], 25);
            Assert.AreEqual(bmp[25], 10.0);
        }

        [TestMethod]
        public void RemoveTest()
        {
            var bmp = new BidirectionalMap<int, double>();
            bmp.Add(25, 10.0);
            Assert.AreEqual(bmp.Count, 1);
            Assert.AreEqual(bmp[25], 10.0);

            bmp.Remove(10.0);
            Assert.AreEqual(bmp.Count, 0);
            Assert.IsFalse(bmp.Contains(25));
            Assert.IsFalse(bmp.Contains(10.0));
        }
    }
}
