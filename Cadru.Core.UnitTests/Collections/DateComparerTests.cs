using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Cadru.Collections;
using System.Net;
using System.Diagnostics.CodeAnalysis;
using Cadru.UnitTest.Framework;

namespace Cadru.UnitTests.Collections
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
            DateComparer comparer = DateComparer.Default as DateComparer;
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
            DateComparer comparer = DateComparer.Default as DateComparer;
            string x = "10/31/2006";
            string y = "11/1/2006";

            Assert.IsTrue(comparer.Compare(x, y) < 0);
            Assert.IsTrue(comparer.Compare(y, x) > 0);
            Assert.IsTrue(comparer.Compare(x, x) == 0);
        }

        [TestMethod]
        public void Compare2()
        {
            DateComparer comparer = DateComparer.Default as DateComparer;

            Assert.IsTrue(comparer.Compare(DateTime.Today, DateTime.Today.AddDays(1)) < 0);
            Assert.IsTrue(comparer.Compare(DateTime.Today.AddDays(1), DateTime.Today) > 0);
            Assert.IsTrue(comparer.Compare(DateTime.Today, DateTime.Today) == 0);
        }

        [TestMethod]
        public void Compare3()
        {
            DateComparer comparer = DateComparer.Default as DateComparer;

            ExceptionAssert.Throws<FormatException>(() => comparer.Compare("10/31/2006", "abc123"));
            ExceptionAssert.Throws<FormatException>(() => comparer.Compare("abc123", "10/31/2006"));
            ExceptionAssert.Throws<FormatException>(() => comparer.Compare("10/31/2006", String.Empty));
            ExceptionAssert.Throws<FormatException>(() => comparer.Compare(String.Empty, "10/31/2006"));
            ExceptionAssert.Throws<ArgumentNullException>(() => comparer.Compare("10/31/2006", null));
            ExceptionAssert.Throws<ArgumentNullException>(() => comparer.Compare(null, "10/31/2006"));
        }

        [TestMethod]
        public void Compare4()
        {
            DateComparer comparer = DateComparer.DefaultInvariant as DateComparer;
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
            DateComparer comparer = DateComparer.DefaultInvariant as DateComparer;
            string x = "10/31/2006";
            string y = "11/1/2006";

            Assert.IsTrue(comparer.Compare(x, y) < 0);
            Assert.IsTrue(comparer.Compare(y, x) > 0);
            Assert.IsTrue(comparer.Compare(x, x) == 0);
        }

        [TestMethod]
        public void Compare6()
        {
            DateComparer comparer = DateComparer.DefaultInvariant as DateComparer;

            Assert.IsTrue(comparer.Compare(DateTime.Today, DateTime.Today.AddDays(1)) < 0);
            Assert.IsTrue(comparer.Compare(DateTime.Today.AddDays(1), DateTime.Today) > 0);
            Assert.IsTrue(comparer.Compare(DateTime.Today, DateTime.Today) == 0);
        }

        [TestMethod]
        public void Compare7()
        {
            DateComparer comparer = DateComparer.DefaultInvariant as DateComparer;

            ExceptionAssert.Throws<FormatException>(() => comparer.Compare("10/31/2006", "abc123"));
            ExceptionAssert.Throws<FormatException>(() => comparer.Compare("abc123", "10/31/2006"));
            ExceptionAssert.Throws<FormatException>(() => comparer.Compare("10/31/2006", String.Empty));
            ExceptionAssert.Throws<FormatException>(() => comparer.Compare(String.Empty, "10/31/2006"));
            ExceptionAssert.Throws<ArgumentNullException>(() => comparer.Compare("10/31/2006", null)).WithParameter("y");
            ExceptionAssert.Throws<ArgumentNullException>(() => comparer.Compare(null, "10/31/2006")).WithParameter("x");

            ExceptionAssert.Throws<ArgumentNullException>(() => new DateComparer(null));
        }

        [TestMethod]
        public void Compare9()
        {
            DateComparer comparer = new DateComparer();
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
            DateComparer comparer = DateComparer.Default as DateComparer;
            DateTime x = DateTime.Today;
            DateTime y = DateTime.Today.AddDays(1);

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.IsTrue(comparer.Equals(x, x));
        }

        [TestMethod]
        public void Equals1()
        {
            DateComparer comparer = DateComparer.Default as DateComparer;
            string x = "10/31/2006";
            string y = "11/1/2006";

            Assert.IsFalse(((IEqualityComparer)comparer).Equals(x, y));
            Assert.IsTrue(((IEqualityComparer)comparer).Equals(x, x));
        }

        [TestMethod]
        public void Equals2()
        {
            DateComparer comparer = DateComparer.Default as DateComparer;
            string x = "10/31/2006";
            string y = "11/1/2006";

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.IsTrue(comparer.Equals(x, x));
        }

        [TestMethod]
        public void Equals3()
        {
            DateComparer comparer = DateComparer.Default as DateComparer;
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
            DateComparer comparer = DateComparer.DefaultInvariant as DateComparer;
            DateTime x = DateTime.Today;
            DateTime y = DateTime.Today.AddDays(1);

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.IsTrue(comparer.Equals(x, x));
        }

        [TestMethod]
        public void Equals5()
        {
            DateComparer comparer = DateComparer.DefaultInvariant as DateComparer;
            string x = "10/31/2006";
            string y = "11/1/2006";

            Assert.IsFalse(((IEqualityComparer)comparer).Equals(x, y));
            Assert.IsTrue(((IEqualityComparer)comparer).Equals(x, x));
        }

        [TestMethod]
        public void Equals6()
        {
            DateComparer comparer = DateComparer.DefaultInvariant as DateComparer;
            string x = "10/31/2006";
            string y = "11/1/2006";

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.IsTrue(comparer.Equals(x, x));
        }

        [TestMethod]
        public void Equals7()
        {
            DateComparer comparer = DateComparer.DefaultInvariant as DateComparer;
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
            DateComparer comparer = DateComparer.Default as DateComparer;
            string x = "10/31/2006";

            Assert.AreEqual(DateTime.Parse(x).GetHashCode(), comparer.GetHashCode(x));

            object y = "10/31/2006";
            Assert.AreEqual(DateTime.Parse(y.ToString()).GetHashCode(), comparer.GetHashCode(y));

            DateTime z = DateTime.Today;
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
            DateComparer comparer = DateComparer.Default as DateComparer;

            ExceptionAssert.Throws<ArgumentNullException>(() => ((DateComparer)DateComparer.Default).GetHashCode(((string)null)));
            Assert.AreEqual(String.Empty.GetHashCode(), comparer.GetHashCode(String.Empty));

            string c = "abc";
            Assert.AreEqual(c.GetHashCode(), comparer.GetHashCode(c));
        }

        [TestMethod]
        public void GetHashCode4()
        {
            DateComparer comparer = DateComparer.DefaultInvariant as DateComparer;
            string x = "10/31/2006";

            Assert.AreEqual(DateTime.Parse(x).GetHashCode(), comparer.GetHashCode(x));

            object y = "10/31/2006";
            Assert.AreEqual(DateTime.Parse(y.ToString()).GetHashCode(), comparer.GetHashCode(y));

            DateTime z = DateTime.Today;
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
            DateComparer comparer = DateComparer.DefaultInvariant as DateComparer;
            ExceptionAssert.Throws<ArgumentNullException>(() => comparer.GetHashCode(((object)null)));
            ExceptionAssert.Throws<ArgumentNullException>(() => comparer.GetHashCode(((string)null)));
            Assert.AreEqual(String.Empty.GetHashCode(), comparer.GetHashCode(String.Empty));

            string c = "abc";
            Assert.AreEqual(c.GetHashCode(), comparer.GetHashCode(c)); 
        }
    }
}
