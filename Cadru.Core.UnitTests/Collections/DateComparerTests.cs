using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Cadru.Collections;
using System.Net;
using System.Diagnostics.CodeAnalysis;

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

            try
            {
                comparer.Compare("10/31/2006", "abc123");
            }
            catch (FormatException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                comparer.Compare("abc123", "10/31/2006");
            }
            catch (FormatException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                comparer.Compare("10/31/2006", String.Empty);
            }
            catch (FormatException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                comparer.Compare(String.Empty, "10/31/2006");
            }
            catch (FormatException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                comparer.Compare("10/31/2006", null);
            }
            catch (ArgumentNullException e)
            {
                if (e.ParamName == "y")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                comparer.Compare(null, "10/31/2006");
            }
            catch (ArgumentNullException e)
            {
                if (e.ParamName == "x")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
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

            try
            {
                comparer.Compare("10/31/2006", "abc123");
            }
            catch (FormatException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                comparer.Compare("abc123", "10/31/2006");
            }
            catch (FormatException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                comparer.Compare("10/31/2006", String.Empty);
            }
            catch (FormatException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                comparer.Compare(String.Empty, "10/31/2006");
            }
            catch (FormatException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                comparer.Compare("10/31/2006", null);
            }
            catch (ArgumentNullException e)
            {
                if (e.ParamName == "y")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                comparer.Compare(null, "10/31/2006");
            }
            catch (ArgumentNullException e)
            {
                if (e.ParamName == "x")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Compare8()
        {
            try
            {
                DateComparer comparer = new DateComparer(null);
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
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
        }

        [TestMethod]
        public void GetHashCode2()
        {
            DateComparer comparer = DateComparer.Default as DateComparer;
            try
            {
                object x = null;
                int hash = comparer.GetHashCode(x);
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetHashCode3()
        {
            DateComparer comparer = DateComparer.Default as DateComparer;
            try
            {
                string x = null;
                int hash = comparer.GetHashCode(x);
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

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
            try
            {
                object x = null;
                int hash = comparer.GetHashCode(x);
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetHashCode6()
        {
            DateComparer comparer = DateComparer.DefaultInvariant as DateComparer;
            try
            {
                string x = null;
                int hash = comparer.GetHashCode(x);
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.AreEqual(String.Empty.GetHashCode(), comparer.GetHashCode(String.Empty));

            string c = "abc";
            Assert.AreEqual(c.GetHashCode(), comparer.GetHashCode(c));
        }
    }
}
