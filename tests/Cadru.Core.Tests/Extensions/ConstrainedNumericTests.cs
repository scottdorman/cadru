using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Core.Extensions.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ConstrainedNumericTests
    {
        [TestMethod]
        public void IntegerConstrained()
        {
            var i = new ConstrainedNumeric<int>(5, 10);
            Assert.AreEqual(5, i.LowerBound);
            Assert.AreEqual(10, i.UpperBound);

            i.Value = 6;
            Assert.AreEqual(6, i.Value);

            i.Value = 4;
            Assert.AreEqual(5, i.Value);

            i.Value = 11;
            Assert.AreEqual(10, i.Value);

            i.Value = 5;
            Assert.AreEqual(5, i.Value);

            i.Value = 10;
            Assert.AreEqual(10, i.Value);

            Assert.AreEqual(10, i);

            var a = new ConstrainedNumeric<int>(4, 6) { Value = 5 };
            i.Value = 5;
            Assert.AreEqual(a, i);

            i.Value = 6;
            Assert.AreNotEqual(a, i);
            Assert.IsTrue(a < i);
            Assert.IsTrue(i > a);
        }

        [TestMethod]
        public void DateTimeConstrained()
        {
            var i = new ConstrainedNumeric<DateTime>(new DateTime(2020, 9, 11), new DateTime(2020, 9, 15));
            Assert.AreEqual(new DateTime(2020, 9, 11), i.LowerBound);
            Assert.AreEqual(new DateTime(2020, 9, 15), i.UpperBound);

            i.Value = new DateTime(2020, 9, 12);
            Assert.AreEqual(new DateTime(2020, 9, 12), i.Value);

            i.Value = new DateTime(2020, 9, 10);
            Assert.AreEqual(new DateTime(2020, 9, 11), i.Value);

            i.Value = new DateTime(2020, 9, 16);
            Assert.AreEqual(new DateTime(2020, 9, 15), i.Value);

            i.Value = new DateTime(2020, 9, 11);
            Assert.AreEqual(new DateTime(2020, 9, 11), i.Value);

            i.Value = new DateTime(2020, 9, 15);
            Assert.AreEqual(new DateTime(2020, 9, 15), i.Value);
            Assert.AreEqual(new DateTime(2020, 9, 15), i);

            var a = new ConstrainedNumeric<DateTime>(new DateTime(2020, 8, 11), new DateTime(2020, 8, 15)) { Value = new DateTime(2020, 8, 13) };
            var b = new ConstrainedNumeric<DateTime>(new DateTime(2020, 8, 11), new DateTime(2020, 8, 15)) { Value = new DateTime(2020, 8, 13) };

            Assert.IsTrue(a == b);

            i.Value = new DateTime(2020, 9, 13);
            Assert.AreNotEqual(a, i);
            Assert.IsTrue(a < i);
            Assert.IsTrue(i > a);
        }

        [TestMethod]
        public void CharConstrained()
        {
            var i = new ConstrainedNumeric<char>('b', 'd');
            Assert.AreEqual('b', i.LowerBound);
            Assert.AreEqual('d', i.UpperBound);
            i.Value = 'c';
            Assert.AreEqual('c', i.Value);
            i.Value = 'a';
            Assert.AreEqual('b', i.Value);
            i.Value = 'e';
            Assert.AreEqual('d', i.Value);
            i.Value = 'b';
            Assert.AreEqual('b', i.Value);
            i.Value = 'd';
            Assert.AreEqual('d', i.Value);
            Assert.AreEqual('d', i);
            var a = new ConstrainedNumeric<char>('c', 'd') { Value = 'c' };
            i.Value = 'c';
            Assert.AreEqual(a, i);
            i.Value = 'd';
            Assert.AreNotEqual(a, i);
            Assert.IsTrue(a < i);
            Assert.IsTrue(i > a);
        }

        [TestMethod]
        public void EnumConstrained()
        {
            var i = new ConstrainedNumeric<int>((int)DayOfWeek.Monday, (int)DayOfWeek.Saturday);
            Assert.AreEqual((int)DayOfWeek.Monday, i.LowerBound);
            Assert.AreEqual((int)DayOfWeek.Saturday, i.UpperBound);
            i.Value = (int)DayOfWeek.Tuesday;
            Assert.AreEqual((int)DayOfWeek.Tuesday, i.Value);
        }
    }
}
