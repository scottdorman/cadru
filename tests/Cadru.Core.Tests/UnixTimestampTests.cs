using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cadru.UnitTest.Framework;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.Core.UnitTests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class UnixTimestampTests
    {
        private static DateTime date = new DateTime(2014, 03, 18, 21, 0, 0, 0);
        private static long rawTimestamp = 1395176400L;

        [TestMethod]
        public void Constructors()
        {
            UnixTimestamp timestamp = rawTimestamp;
            Assert.AreEqual(date.Ticks, timestamp.DateTime.Ticks);

            Assert.AreEqual(date.Ticks, ((UnixTimestamp)1395176400L).DateTime.Ticks);
            Assert.AreEqual(date.Ticks, new UnixTimestamp(date).DateTime.Ticks);
            Assert.AreEqual(date.Ticks, new UnixTimestamp(1395176400L).DateTime.Ticks);

            Assert.AreEqual(date.Ticks, new UnixTimestamp(2014, 03, 18, 21, 0, 0).DateTime.Ticks);
            Assert.AreEqual(date.AddHours(-21).Ticks, new UnixTimestamp(2014, 03, 18).DateTime.Ticks);

            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => new UnixTimestamp(UnixTimestamp.MaxValue.Seconds + 1));
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => new UnixTimestamp(UnixTimestamp.MinValue.Seconds - 1));
        }

        [TestMethod]
        public void Operators()
        {
            ConditionAssert.IsTrue(UnixTimestamp.MaxValue > UnixTimestamp.MinValue);
            ConditionAssert.IsFalse(UnixTimestamp.MinValue >= UnixTimestamp.MaxValue);
            ConditionAssert.IsFalse(UnixTimestamp.MaxValue <= UnixTimestamp.MinValue);
            ConditionAssert.IsTrue(UnixTimestamp.MinValue < UnixTimestamp.MaxValue);
            ConditionAssert.IsTrue(((UnixTimestamp)1395176400L) == ((UnixTimestamp)1395176400L));
            ConditionAssert.IsTrue(UnixTimestamp.MinValue != UnixTimestamp.MaxValue);
        }

        //[TestMethod]
        //public void Conversions()
        //{
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToBoolean(null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToByte(null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToChar(null));
        //    ExceptionAssert.DoesNotThrow(() => ((IConvertible)UnixTimestamp.MaxValue).ToDateTime(null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToDecimal(null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToDouble(null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToInt16(null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToInt32(null));
        //    ExceptionAssert.DoesNotThrow(() => ((IConvertible)UnixTimestamp.MaxValue).ToInt64(null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToSByte(null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToSingle(null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToString(null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToUInt16(null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToUInt32(null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToUInt64(null));

        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToType(typeof(bool), null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToType(typeof(byte), null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToType(typeof(char), null));
        //    ExceptionAssert.DoesNotThrow(() => ((IConvertible)UnixTimestamp.MaxValue).ToType(typeof(DateTime), null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToType(typeof(decimal), null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToType(typeof(double), null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToType(typeof(short), null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToType(typeof(int), null));
        //    ExceptionAssert.DoesNotThrow(() => ((IConvertible)UnixTimestamp.MaxValue).ToType(typeof(long), null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToType(typeof(sbyte), null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToType(typeof(Single), null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToType(typeof(string), null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToType(typeof(ushort), null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToType(typeof(uint), null));
        //    ExceptionAssert.Throws<InvalidCastException>(() => ((IConvertible)UnixTimestamp.MaxValue).ToType(typeof(ulong), null));

        //    Assert.AreEqual(((IConvertible)UnixTimestamp.MaxValue).GetTypeCode(), TypeCode.Object);

        //    Assert.IsInstanceOfType((UnixTimestamp)rawTimestamp, typeof(UnixTimestamp));
        //    Assert.IsInstanceOfType((long)UnixTimestamp.MaxValue, typeof(long));
        //}

        [TestMethod]
        public void MinMax()
        {
            Assert.AreEqual(0, UnixTimestamp.MinValue.DateTime.Ticks);
            Assert.AreEqual(new DateTime(9999, 12, 31, 23, 59, 59).Ticks, UnixTimestamp.MaxValue.DateTime.Ticks);
        }

        [TestMethod]
        public void Days()
        {
            Assert.AreEqual(12677, new UnixTimestamp(1095292800L).Days);
            Assert.AreEqual(-4472, new UnixTimestamp(-386380800L).Days);
        }

        [TestMethod]
        public void AddDays()
        {
            UnixTimestamp timestamp = rawTimestamp;

            Assert.AreEqual(date.AddDays(2).Ticks, timestamp.AddDays(2).DateTime.Ticks);
            Assert.AreEqual(date.AddDays(-2).Ticks, timestamp.AddDays(-2).DateTime.Ticks);
        }

        [TestMethod]
        public void AddHours()
        {
            UnixTimestamp timestamp = rawTimestamp;

            Assert.AreEqual(date.AddHours(2).Ticks, timestamp.AddHours(2).DateTime.Ticks);
            Assert.AreEqual(date.AddHours(-2).Ticks, timestamp.AddHours(-2).DateTime.Ticks);
        }

        [TestMethod]
        public void AddMinutes()
        {
            UnixTimestamp timestamp = rawTimestamp;

            Assert.AreEqual(date.AddMinutes(2).Ticks, timestamp.AddMinutes(2).DateTime.Ticks);
            Assert.AreEqual(date.AddMinutes(-2).Ticks, timestamp.AddMinutes(-2).DateTime.Ticks);
        }

        [TestMethod]
        public void AddMonths()
        {
            UnixTimestamp timestamp = rawTimestamp;

            Assert.AreEqual(date.AddMonths(2).Ticks, timestamp.AddMonths(2).DateTime.Ticks);
            Assert.AreEqual(date.AddMonths(-2).Ticks, timestamp.AddMonths(-2).DateTime.Ticks);

            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => timestamp.AddMonths(130000));
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => timestamp.AddMonths(-130000));
        }

        [TestMethod]
        public void AddSeconds()
        {
            UnixTimestamp timestamp = rawTimestamp;

            Assert.AreEqual(date.AddSeconds(2).Ticks, timestamp.AddSeconds(2).DateTime.Ticks);
            Assert.AreEqual(date.AddSeconds(-2).Ticks, timestamp.AddSeconds(-2).DateTime.Ticks);
        }

        [TestMethod]
        public void AddYears()
        {
            UnixTimestamp timestamp = rawTimestamp;

            Assert.AreEqual(date.AddYears(2).Ticks, timestamp.AddYears(2).DateTime.Ticks);
            Assert.AreEqual(date.AddYears(-2).Ticks, timestamp.AddYears(-2).DateTime.Ticks);

            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => timestamp.AddYears(10000));
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => timestamp.AddYears(-10000));

            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => timestamp.AddYears(12000));
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => timestamp.AddYears(-12000));
        }

        [TestMethod]
        public void Add()
        {
            UnixTimestamp timestamp = rawTimestamp;

            Assert.AreEqual(date.Add(TimeSpan.FromMinutes(2)).Ticks, timestamp.Add(TimeSpan.FromMinutes(2)).DateTime.Ticks);
            Assert.AreEqual(date.Add(TimeSpan.FromMinutes(-2)).Ticks, timestamp.Add(TimeSpan.FromMinutes(-2)).DateTime.Ticks);
        }

        [TestMethod]
        public void Subtract()
        {
            UnixTimestamp timestamp = rawTimestamp;

            Assert.AreEqual(date.Subtract(TimeSpan.FromMinutes(2)).Ticks, timestamp.Subtract(TimeSpan.FromMinutes(2)).DateTime.Ticks);
            Assert.AreEqual(date.Subtract(TimeSpan.FromMinutes(-2)).Ticks, timestamp.Subtract(TimeSpan.FromMinutes(-2)).DateTime.Ticks);

            Assert.AreEqual(date.Subtract(date.AddDays(2)).Ticks, timestamp.Subtract((UnixTimestamp)1395349200L).Ticks);
            Assert.AreEqual(date.Subtract(date.AddDays(-2)).Ticks, timestamp.Subtract((UnixTimestamp)1395003600L).Ticks);
        }

        [TestMethod]
        public void ToStringTests()
        {
            UnixTimestamp timestamp = rawTimestamp;

            Assert.AreEqual("1395176400", timestamp.ToString());
            Assert.AreEqual("1395176400", timestamp.ToString("g", null));
            Assert.AreEqual("1395176400", timestamp.ToString("g", System.Globalization.CultureInfo.CurrentCulture));
        }

        [TestMethod]
        public void Comparison()
        {
            UnixTimestamp timestamp = rawTimestamp;
            UnixTimestamp timestamp2 = rawTimestamp;
            UnixTimestamp timestamp3 = new UnixTimestamp(rawTimestamp).AddMonths(1);
            UnixTimestamp timestamp4 = new UnixTimestamp(rawTimestamp).AddMonths(-1);

            Assert.AreEqual(1, timestamp.CompareTo(null));
            ExceptionAssert.Throws<ArgumentException>(() => timestamp.CompareTo("test"));
            Assert.AreEqual(0, timestamp.CompareTo((object)timestamp2));
            Assert.AreEqual(0, timestamp.CompareTo(timestamp2));
            ConditionAssert.Greater(0, timestamp.CompareTo(timestamp3));
            ConditionAssert.Less(0, timestamp.CompareTo(timestamp4));
        }

        [TestMethod]
        public void Equality()
        {
            UnixTimestamp timestamp = rawTimestamp;
            UnixTimestamp timestamp2 = rawTimestamp;
            UnixTimestamp timestamp3 = new UnixTimestamp(rawTimestamp).AddMonths(1);
            UnixTimestamp timestamp4 = new UnixTimestamp(rawTimestamp).AddMonths(-1);

            Assert.IsTrue(timestamp.Equals((object)timestamp2));
            Assert.IsFalse(timestamp.Equals((object)timestamp3));
            Assert.IsFalse(timestamp.Equals("test"));

            Assert.IsTrue(timestamp.Equals(timestamp2));
            Assert.IsFalse(timestamp.Equals(timestamp3));

            Assert.IsTrue(UnixTimestamp.Equals((object)timestamp, (object)timestamp2));
            Assert.IsFalse(UnixTimestamp.Equals((object)timestamp, (object)timestamp3));

            Assert.IsTrue(UnixTimestamp.Equals(timestamp, timestamp2));
            Assert.IsFalse(UnixTimestamp.Equals(timestamp, timestamp3));

            Assert.AreEqual(rawTimestamp.GetHashCode(), timestamp.GetHashCode());
        }

        [TestMethod]
        public void Now()
        {
            Assert.AreEqual(DateTime.Now.Date.Ticks, UnixTimestamp.Now.DateTime.Date.Ticks);

        }
    }
}
