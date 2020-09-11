//------------------------------------------------------------------------------
// <copyright file="DateComparerTests.cs"
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
using System.Collections;
using System.Diagnostics.CodeAnalysis;

using Cadru.UnitTest.Framework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Collections.Tests
{
    /// <summary>
    ///This is a test class for CadruCollections.DateComparer and is intended
    ///to contain all CadruCollections.DateComparer Unit Tests
    ///</summary>
    [TestClass, ExcludeFromCodeCoverage]
    public class DateComparerTests
    {
        /// <summary>
        ///A test for Compare (object, object)
        ///</summary>
        [TestMethod]
        public void Compare()
        {
            var comparer = DateComparer.Default as DateComparer;
            object x = "10/31/2006";
            object y = "11/1/2006";

            Assert.IsTrue(comparer.Compare(x, y) < 0);
            Assert.IsTrue(comparer.Compare(y, x) > 0);
            Assert.IsTrue(comparer.Compare(x, x) == 0);

            x = String.Empty;
            y = String.Empty;

            Assert.IsTrue(comparer.Compare(x, "10/31/2006") == -1);
            Assert.IsTrue(comparer.Compare("10/31/2006", y) == 1);
            Assert.IsTrue(comparer.Compare(x, y) == 0);
        }

        /// <summary>
        ///A test for Compare (string, string)
        ///</summary>
        [TestMethod]
        public void Compare1()
        {
            var comparer = DateComparer.Default as DateComparer;
            var x = "10/31/2006";
            var y = "11/1/2006";

            Assert.IsTrue(comparer.Compare(x, y) < 0);
            Assert.IsTrue(comparer.Compare(y, x) > 0);
            Assert.IsTrue(comparer.Compare(x, x) == 0);
        }

        [TestMethod]
        public void Compare2()
        {
            var comparer = DateComparer.Default as DateComparer;

            Assert.IsTrue(comparer.Compare(DateTime.Today, DateTime.Today.AddDays(1)) < 0);
            Assert.IsTrue(comparer.Compare(DateTime.Today.AddDays(1), DateTime.Today) > 0);
            Assert.IsTrue(comparer.Compare(DateTime.Today, DateTime.Today) == 0);
        }

        [TestMethod]
        public void Compare3()
        {
            var comparer = DateComparer.Default as DateComparer;

            ExceptionAssert.Throws<FormatException>(() => comparer.Compare("10/31/2006", "abc123"));
            ExceptionAssert.Throws<FormatException>(() => comparer.Compare("abc123", "10/31/2006"));
            ExceptionAssert.Throws<FormatException>(() => comparer.Compare("10/31/2006", String.Empty));
            ExceptionAssert.Throws<FormatException>(() => comparer.Compare(String.Empty, "10/31/2006"));
            ExceptionAssert.Throws<FormatException>(() => comparer.Compare("10/31/2006", null));
            ExceptionAssert.Throws<FormatException>(() => comparer.Compare(null, "10/31/2006"));
        }

        [TestMethod]
        public void Compare4()
        {
            var comparer = DateComparer.DefaultInvariant as DateComparer;
            object x = "10/31/2006";
            object y = "11/1/2006";

            Assert.IsTrue(comparer.Compare(x, y) < 0);
            Assert.IsTrue(comparer.Compare(y, x) > 0);
            Assert.IsTrue(comparer.Compare(x, x) == 0);

            x = String.Empty;
            y = String.Empty;

            Assert.IsTrue(comparer.Compare(x, "10/31/2006") == -1);
            Assert.IsTrue(comparer.Compare("10/31/2006", y) == 1);
            Assert.IsTrue(comparer.Compare(x, y) == 0);
        }

        /// <summary>
        ///A test for Compare (string, string)
        ///</summary>
        [TestMethod]
        public void Compare5()
        {
            var comparer = DateComparer.DefaultInvariant as DateComparer;
            var x = "10/31/2006";
            var y = "11/1/2006";

            Assert.IsTrue(comparer.Compare(x, y) < 0);
            Assert.IsTrue(comparer.Compare(y, x) > 0);
            Assert.IsTrue(comparer.Compare(x, x) == 0);
        }

        [TestMethod]
        public void Compare6()
        {
            var comparer = DateComparer.DefaultInvariant as DateComparer;

            Assert.IsTrue(comparer.Compare(DateTime.Today, DateTime.Today.AddDays(1)) < 0);
            Assert.IsTrue(comparer.Compare(DateTime.Today.AddDays(1), DateTime.Today) > 0);
            Assert.IsTrue(comparer.Compare(DateTime.Today, DateTime.Today) == 0);
        }

        [TestMethod]
        public void Compare7()
        {
            var comparer = DateComparer.DefaultInvariant as DateComparer;

            ExceptionAssert.Throws<FormatException>(() => comparer.Compare("10/31/2006", "abc123"));
            ExceptionAssert.Throws<FormatException>(() => comparer.Compare("abc123", "10/31/2006"));
            ExceptionAssert.Throws<FormatException>(() => comparer.Compare("10/31/2006", String.Empty));
            ExceptionAssert.Throws<FormatException>(() => comparer.Compare(String.Empty, "10/31/2006"));
            ExceptionAssert.Throws<FormatException>(() => comparer.Compare("10/31/2006", null));
            ExceptionAssert.Throws<FormatException>(() => comparer.Compare(null, "10/31/2006"));

            ExceptionAssert.Throws<ArgumentNullException>(() => new DateComparer(null));
        }

        [TestMethod]
        public void Compare9()
        {
            var comparer = new DateComparer();
            object x = "10/31/2006";
            object y = "11/1/2006";

            Assert.IsTrue(comparer.Compare(x, y) < 0);
            Assert.IsTrue(comparer.Compare(y, x) > 0);
            Assert.IsTrue(comparer.Compare(x, x) == 0);

            x = String.Empty;
            y = String.Empty;

            Assert.IsTrue(comparer.Compare(x, "10/31/2006") == -1);
            Assert.IsTrue(comparer.Compare("10/31/2006", y) == 1);
            Assert.IsTrue(comparer.Compare(x, y) == 0);
        }

        [TestMethod]
        public void Equals()
        {
            var comparer = DateComparer.Default as DateComparer;
            var x = DateTime.Today;
            var y = DateTime.Today.AddDays(1);

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.IsTrue(comparer.Equals(x, x));
        }

        [TestMethod]
        public void Equals1()
        {
            var comparer = DateComparer.Default as DateComparer;
            var x = "10/31/2006";
            var y = "11/1/2006";

            Assert.IsFalse(((IEqualityComparer)comparer).Equals(x, y));
            Assert.IsTrue(((IEqualityComparer)comparer).Equals(x, x));
        }

        [TestMethod]
        public void Equals2()
        {
            var comparer = DateComparer.Default as DateComparer;
            var x = "10/31/2006";
            var y = "11/1/2006";

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.IsTrue(comparer.Equals(x, x));
        }

        [TestMethod]
        public void Equals3()
        {
            var comparer = DateComparer.Default as DateComparer;
            string x = null;
            string y = null;

            Assert.IsFalse(comparer.Equals(x, "10/31/2006"));
            Assert.IsFalse(comparer.Equals("10/31/2006", y));
            Assert.IsTrue(comparer.Equals(x, x));

            Assert.IsTrue(comparer.Equals("abc", "abc"));
        }

        [TestMethod]
        public void Equals4()
        {
            var comparer = DateComparer.DefaultInvariant as DateComparer;
            var x = DateTime.Today;
            var y = DateTime.Today.AddDays(1);

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.IsTrue(comparer.Equals(x, x));
        }

        [TestMethod]
        public void Equals5()
        {
            var comparer = DateComparer.DefaultInvariant as DateComparer;
            var x = "10/31/2006";
            var y = "11/1/2006";

            Assert.IsFalse(((IEqualityComparer)comparer).Equals(x, y));
            Assert.IsTrue(((IEqualityComparer)comparer).Equals(x, x));
        }

        [TestMethod]
        public void Equals6()
        {
            var comparer = DateComparer.DefaultInvariant as DateComparer;
            var x = "10/31/2006";
            var y = "11/1/2006";

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.IsTrue(comparer.Equals(x, x));
        }

        [TestMethod]
        public void Equals7()
        {
            var comparer = DateComparer.DefaultInvariant as DateComparer;
            string x = null;
            string y = null;

            Assert.IsFalse(comparer.Equals(x, "10/31/2006"));
            Assert.IsFalse(comparer.Equals("10/31/2006", y));
            Assert.IsTrue(comparer.Equals(x, x));

            Assert.IsTrue(comparer.Equals("abc", "abc"));
        }

        [TestMethod]
        public void GetHashCode1()
        {
            var comparer = DateComparer.Default as DateComparer;
            var x = "10/31/2006";

            Assert.AreEqual(DateTime.Parse(x).GetHashCode(), comparer.GetHashCode(x));

            object y = "10/31/2006";
            Assert.AreEqual(DateTime.Parse(y.ToString()).GetHashCode(), comparer.GetHashCode(y));

            var z = DateTime.Today;
            Assert.AreEqual(z.GetHashCode(), comparer.GetHashCode(z));

            object a = DateTime.Today;
            Assert.AreEqual(a.GetHashCode(), comparer.GetHashCode(a));

            object b = 3;
            Assert.AreEqual(b.GetHashCode(), comparer.GetHashCode(b));

            object c = "abc";
            Assert.AreEqual(c.GetHashCode(), comparer.GetHashCode(c));

            ExceptionAssert.Throws<ArgumentNullException>(() => ((DateComparer)DateComparer.Default).GetHashCode(((object)null)));
        }

        [TestMethod]
        public void GetHashCode3()
        {
            var comparer = DateComparer.Default as DateComparer;

            ExceptionAssert.Throws<ArgumentNullException>(() => ((DateComparer)DateComparer.Default).GetHashCode(((string)null)));
            Assert.AreEqual(String.Empty.GetHashCode(), comparer.GetHashCode(String.Empty));

            var c = "abc";
            Assert.AreEqual(c.GetHashCode(), comparer.GetHashCode(c));
        }

        [TestMethod]
        public void GetHashCode4()
        {
            var comparer = DateComparer.DefaultInvariant as DateComparer;
            var x = "10/31/2006";

            Assert.AreEqual(DateTime.Parse(x).GetHashCode(), comparer.GetHashCode(x));

            object y = "10/31/2006";
            Assert.AreEqual(DateTime.Parse(y.ToString()).GetHashCode(), comparer.GetHashCode(y));

            var z = DateTime.Today;
            Assert.AreEqual(z.GetHashCode(), comparer.GetHashCode(z));

            object a = DateTime.Today;
            Assert.AreEqual(a.GetHashCode(), comparer.GetHashCode(a));

            object b = 3;
            Assert.AreEqual(b.GetHashCode(), comparer.GetHashCode(b));

            object c = "abc";
            Assert.AreEqual(c.GetHashCode(), comparer.GetHashCode(c));
        }

        [TestMethod]
        public void GetHashCode5()
        {
            var comparer = DateComparer.DefaultInvariant as DateComparer;
            ExceptionAssert.Throws<ArgumentNullException>(() => comparer.GetHashCode(((object)null)));
            ExceptionAssert.Throws<ArgumentNullException>(() => comparer.GetHashCode(((string)null)));
            Assert.AreEqual(String.Empty.GetHashCode(), comparer.GetHashCode(String.Empty));

            var c = "abc";
            Assert.AreEqual(c.GetHashCode(), comparer.GetHashCode(c));
        }
    }
}