using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Cadru.Collections;
using System.Net;
using System.Diagnostics.CodeAnalysis;
using Cadru.UnitTest.Framework;
using System.Linq;

namespace Cadru.UnitTest.Framework.UnitTests.Collections
{
    struct EqualityTester
    {
        public readonly int Index;
        public readonly DateTime Date;

        public EqualityTester(int index, DateTime date) : this()
        {
            Index = index;
            Date = date;
        }
    }

    [TestClass, ExcludeFromCodeCoverage]
    public class EqualityOfTTests
    {
        [TestMethod]
        public void Create()
        {
            var comparer = Equality<DateTime>.CreateComparer(x => x.Day);
            Assert.IsNotNull(comparer);
            Assert.IsInstanceOfType(comparer, typeof(IEqualityComparer<DateTime>));
        }

        [TestMethod]
        public void DistinctByTest()
        {
            var list = Enumerable.Range(0, 200).Select(i => new EqualityTester(i, DateTime.Today.AddDays(i % 4))).ToList();

            var distinctList = list.Distinct(Equality<EqualityTester>.CreateComparer(x => x.Date)).ToList();
            Assert.AreEqual(4, distinctList.Count);

            Assert.AreEqual(0, distinctList[0].Index);
            Assert.AreEqual(1, distinctList[1].Index);
            Assert.AreEqual(2, distinctList[2].Index);
            Assert.AreEqual(3, distinctList[3].Index);

            Assert.AreEqual(DateTime.Today, distinctList[0].Date);
            Assert.AreEqual(DateTime.Today.AddDays(1), distinctList[1].Date);
            Assert.AreEqual(DateTime.Today.AddDays(2), distinctList[2].Date);
            Assert.AreEqual(DateTime.Today.AddDays(3), distinctList[3].Date);

            Assert.AreEqual(200, list.Count);
        }

        [TestMethod]
        public void DistinctByWithComparerTest()
        {
            var list = Enumerable.Range(0, 200).Select(i => new EqualityTester(i, DateTime.Today.AddDays(i % 4))).ToList();

            var distinctList = list.Distinct(Equality<EqualityTester>.CreateComparer(x => x.Date, EqualityComparer<DateTime>.Default)).ToList();
            Assert.AreEqual(4, distinctList.Count);

            Assert.AreEqual(0, distinctList[0].Index);
            Assert.AreEqual(1, distinctList[1].Index);
            Assert.AreEqual(2, distinctList[2].Index);
            Assert.AreEqual(3, distinctList[3].Index);

            Assert.AreEqual(DateTime.Today, distinctList[0].Date);
            Assert.AreEqual(DateTime.Today.AddDays(1), distinctList[1].Date);
            Assert.AreEqual(DateTime.Today.AddDays(2), distinctList[2].Date);
            Assert.AreEqual(DateTime.Today.AddDays(3), distinctList[3].Date);

            Assert.AreEqual(200, list.Count);
        }
    }
}
