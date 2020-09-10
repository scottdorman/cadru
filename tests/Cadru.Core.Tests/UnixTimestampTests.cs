//------------------------------------------------------------------------------
// <copyright file="UnixTimestampTests.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2020 Scott Dorman.
// </copyright>
//
// <license>
//    Licensed under the Microsoft Public License (Ms-PL) (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//    http://opensource.org/licenses/Ms-PL.html
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </license>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;

using Cadru.UnitTest.Framework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Core.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class UnixTimestampTests
    {
        private const long rawTimestamp = 1395176400L;
        private static readonly DateTime date = new DateTime(2014, 03, 18, 21, 0, 0, 0);

        [TestMethod]
        public void Add()
        {
            UnixTimestamp timestamp = rawTimestamp;

            Assert.AreEqual(date.Add(TimeSpan.FromMinutes(2)).Ticks, timestamp.Add(TimeSpan.FromMinutes(2)).DateTime.Ticks);
            Assert.AreEqual(date.Add(TimeSpan.FromMinutes(-2)).Ticks, timestamp.Add(TimeSpan.FromMinutes(-2)).DateTime.Ticks);
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
        public void Comparison()
        {
            UnixTimestamp timestamp = rawTimestamp;
            UnixTimestamp timestamp2 = rawTimestamp;
            var timestamp3 = new UnixTimestamp(rawTimestamp).AddMonths(1);
            var timestamp4 = new UnixTimestamp(rawTimestamp).AddMonths(-1);

            Assert.AreEqual(1, timestamp.CompareTo(null));
            ExceptionAssert.Throws<ArgumentException>(() => timestamp.CompareTo("test"));
            Assert.AreEqual(0, timestamp.CompareTo((object)timestamp2));
            Assert.AreEqual(0, timestamp.CompareTo(timestamp2));
            ConditionAssert.Greater(0, timestamp.CompareTo(timestamp3));
            ConditionAssert.Less(0, timestamp.CompareTo(timestamp4));
        }

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
        public void Days()
        {
            Assert.AreEqual(12677, new UnixTimestamp(1095292800L).Days);
            Assert.AreEqual(-4472, new UnixTimestamp(-386380800L).Days);
        }

        [TestMethod]
        public void Equality()
        {
            UnixTimestamp timestamp = rawTimestamp;
            UnixTimestamp timestamp2 = rawTimestamp;
            var timestamp3 = new UnixTimestamp(rawTimestamp).AddMonths(1);
            var timestamp4 = new UnixTimestamp(rawTimestamp).AddMonths(-1);

            Assert.IsTrue(timestamp.Equals((object)timestamp2));
            Assert.IsFalse(timestamp.Equals((object)timestamp3));
            Assert.IsFalse(timestamp.Equals((object)timestamp4));
            Assert.IsFalse(timestamp.Equals("test"));

            Assert.IsTrue(timestamp.Equals(timestamp2));
            Assert.IsFalse(timestamp.Equals(timestamp3));

            Assert.IsTrue(Equals((object)timestamp, (object)timestamp2));
            Assert.IsFalse(Equals((object)timestamp, (object)timestamp3));
            Assert.IsFalse(Equals((object)timestamp, (object)timestamp4));

            Assert.IsTrue(UnixTimestamp.Equals(timestamp, timestamp2));
            Assert.IsFalse(UnixTimestamp.Equals(timestamp, timestamp3));
            Assert.IsFalse(UnixTimestamp.Equals(timestamp, timestamp4));

            Assert.AreEqual(rawTimestamp.GetHashCode(), timestamp.GetHashCode());
            Assert.AreNotEqual(rawTimestamp.GetHashCode(), timestamp3.GetHashCode());
            Assert.AreNotEqual(rawTimestamp.GetHashCode(), timestamp4.GetHashCode());
        }

        [TestMethod]
        public void MinMax()
        {
            Assert.AreEqual(0, UnixTimestamp.MinValue.DateTime.Ticks);
            Assert.AreEqual(new DateTime(9999, 12, 31, 23, 59, 59).Ticks, UnixTimestamp.MaxValue.DateTime.Ticks);
        }

        [TestMethod]
        public void Now()
        {
            Assert.AreEqual(DateTime.Now.Date.Ticks, UnixTimestamp.Now.DateTime.Date.Ticks);
        }

        [TestMethod]
        public void Operators()
        {
            ConditionAssert.IsTrue(UnixTimestamp.MaxValue > UnixTimestamp.MinValue);
            ConditionAssert.IsFalse(UnixTimestamp.MinValue >= UnixTimestamp.MaxValue);
            ConditionAssert.IsFalse(UnixTimestamp.MaxValue <= UnixTimestamp.MinValue);
            ConditionAssert.IsTrue(UnixTimestamp.MinValue < UnixTimestamp.MaxValue);
            ConditionAssert.IsTrue(UnixTimestamp.MinValue != UnixTimestamp.MaxValue);
            ConditionAssert.IsTrue(((UnixTimestamp)1395176400L) == ((UnixTimestamp)1395176400L));
            ConditionAssert.IsFalse(((UnixTimestamp)1395176401L) == ((UnixTimestamp)1395176400L));
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
    }
}