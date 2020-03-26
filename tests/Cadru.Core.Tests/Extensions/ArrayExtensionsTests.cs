using System.Diagnostics.CodeAnalysis;

using Cadru.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Core.Extensions.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ArrayExtensionsTests
    {
        [TestMethod]
        public void BytesToBinaryString()
        {
            Assert.AreEqual("[100]", new byte[] { 4 }.BytesToBinaryString());
            Assert.AreEqual("[1001011]", new byte[] { 75 }.BytesToBinaryString());
            Assert.AreEqual("[100][1001011]", new byte[] { 4, 75 }.BytesToBinaryString());
        }

        [TestMethod]
        public void BytesToString()
        {
            Assert.AreEqual("04", new byte[] { 4 }.BytesToString());
            Assert.AreEqual("4b", new byte[] { 75 }.BytesToString());
            Assert.AreEqual("044b", new byte[] { 4, 75 }.BytesToString());
        }

        [TestMethod]
        public void ReverseArray()
        {
            var array = new byte[] { 4, 75 };
            var reversed = array.ReverseArray();

            CollectionAssert.AreEqual(new byte[] { 75, 4 }, reversed);
            CollectionAssert.AreEqual(new byte[] { 4, 75 }, array);
            CollectionAssert.AreNotEqual(array, reversed);
        }

        [TestMethod]
        public void ReverseArrayInPlace()
        {
            var array = new byte[] { 4, 75 };
            array.ReverseArrayInPlace();

            CollectionAssert.AreEqual(new byte[] { 75, 4 }, array);
            CollectionAssert.AreNotEqual(new byte[] { 4, 75 }, array);
        }
    }
}
