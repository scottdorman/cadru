using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Cadru.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Core.Extensions.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class EnumerableExtensionsTests
    {
        [TestMethod]
        public void IsEmpty()
        {
            Assert.IsTrue(Enumerable.Empty<string>().IsEmpty());
            Assert.IsFalse(new[] { "test", "test2" }.IsEmpty());
            Assert.IsTrue(new ArrayList().IsEmpty());
            Assert.IsTrue(new List<int>().IsEmpty());

            Assert.IsTrue(String.Empty.IsEmpty());
            Assert.IsFalse("test".IsEmpty());
        }

        [TestMethod]
        public void IsNull()
        {
            Assert.IsFalse(Enumerable.Empty<string>().IsNull());
            Assert.IsFalse(new[] { "test", "test2" }.IsNull());
            Assert.IsTrue(((IEnumerable)null).IsNull());

            Assert.IsFalse(new ArrayList().IsNull());
            Assert.IsFalse(new List<int>().IsNull());
        }

        [TestMethod]
        public void IsNullOrEmpty()
        {
            Assert.IsTrue(Enumerable.Empty<string>().IsNullOrEmpty());
            Assert.IsFalse(new[] { "test", "test2" }.IsNullOrEmpty());
            Assert.IsTrue(((IEnumerable)null).IsNullOrEmpty());
            Assert.IsTrue(Enumerable.Empty<string>().IsNullOrEmpty());

            Assert.IsTrue(new ArrayList().IsNullOrEmpty());
            Assert.IsTrue(new List<int>().IsNullOrEmpty());
        }

        [TestMethod]
        public void Join()
        {
            string[] test = { "this", "is", "a", "test" };
            Assert.AreEqual("this,is,a,test", test.Join());
            Assert.AreEqual("this, is, a, test", test.Join(", "));
            Assert.AreEqual("this, is, a, test", test.Join(", ", 0, 4));
            Assert.AreEqual("this,is,a,test", test.Join(0, 4));

            var test1 = new List<string>()
            {
                "this", "is", "a", "test"
            };

            Assert.AreEqual("this,is,a,test", test1.Join());
            Assert.AreEqual("this, is, a, test", test1.Join(", "));
            Assert.AreEqual("this, is, a, test", test1.Join(", ", 0, 4));
            Assert.AreEqual("this,is,a,test", test1.Join(0, 4));

            var test2 = test1.Select(s => s);
            Assert.AreEqual("this,is,a,test", test2.Join());
            Assert.AreEqual("this, is, a, test", test2.Join(", "));

            var test3 = new List<int>()
            {
                1, 2, 3
            }.Select(i => i);

            Assert.AreEqual("1,2,3", test3.Join());
            Assert.AreEqual("1, 2, 3", test3.Join(", "));
        }

        [TestMethod]
        public void Slice()
        {
            var test = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CollectionAssert.AreEqual(new[] { 0, 1, 2, 3 }, test.Slice(0, 3).ToArray());
            CollectionAssert.AreEqual(new[] { 4, 5, 6, 7, 8, 9 }, test.Slice(4, 9).ToArray());
            CollectionAssert.AreEqual(new[] { 9 }, test.Slice(9, 9).ToArray());

            var test2 = new[] { 'a', 'b', 'c', 'd' };
            CollectionAssert.AreEqual(new[] { 'a', 'b', 'c', 'd' }, test2.Slice(0, 3).ToArray());
            CollectionAssert.AreEqual(new[] { 'b', 'c' }, test2.Slice(1, 2).ToArray());
        }

        [TestMethod]
        public void WhereIf()
        {
            int[] numbers = { 0, 30, 20, 15, 90, 85, 40, 75 };

            CollectionAssert.AreEqual(numbers, numbers.WhereIf(false, c => c < 40).ToArray());
            CollectionAssert.AreEqual(new[] { 0, 30, 20, 15 }, numbers.WhereIf(true, c => c < 40).ToArray());

            CollectionAssert.AreEqual(numbers, numbers.WhereIf(false, (number, index) => number <= index * 10).ToArray());
            CollectionAssert.AreEqual(new[] { 0, 20, 15, 40 }, numbers.WhereIf(true, (number, index) => number <= index * 10).ToArray());
        }

        [TestMethod]
        public void Partition()
        {
            int[] numbers = { 0, 30, 20, 15, 90, 85, 40, 75 };
            var partitions = numbers.Partition(3).ToArray();
            Assert.AreEqual(3, partitions.Length);
            CollectionAssert.AreEqual(new[] { 0, 30, 20 }, partitions[0].ToArray());
            CollectionAssert.AreEqual(new[] { 15, 90, 85 }, partitions[1].ToArray());
            CollectionAssert.AreEqual(new[] { 40, 75 }, partitions[2].ToArray());

            partitions = numbers.Partition(10).ToArray();
            Assert.AreEqual(1, partitions.Length);
            CollectionAssert.AreEqual(numbers, partitions[0].ToArray());

            numbers = new int[] { };
            partitions = numbers.Partition(3).ToArray();
            Assert.AreEqual(0, partitions.Length);
        }
    }
}
