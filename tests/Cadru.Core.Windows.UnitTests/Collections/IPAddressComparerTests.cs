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
    ///This is a test class for CadruCollections.IPAddressComparer and is intended
    ///to contain all CadruCollections.IPAddressComparer Unit Tests
    ///</summary>
    [TestClass, ExcludeFromCodeCoverage]
    public class IPAddressComparerTests
    {
        /// <summary>
        ///A test for Compare (object, object)
        ///</summary>
        [TestMethod]
        public void Compare()
        {
            IPAddressComparer comparer = IPAddressComparer.Default as IPAddressComparer;
            object x = "127.0.0.0";
            object y = "128.0.0.0";

            Assert.IsTrue(comparer.Compare(x, y) < 0);
            Assert.IsTrue(comparer.Compare(y, x) > 0);
            Assert.IsTrue(comparer.Compare(x, x) == 0);

            x = "1567890";
            y = "1890329";

            Assert.IsTrue(comparer.Compare(x, y) < 0);
            Assert.IsTrue(comparer.Compare(y, x) > 0);
            Assert.IsTrue(comparer.Compare(x, x) == 0);

            x = String.Empty;
            y = String.Empty;

            Assert.IsTrue(comparer.Compare(x, "127.0.0.0") == -1);
            Assert.IsTrue(comparer.Compare("127.0.0.0", y) == 1);
            Assert.IsTrue(comparer.Compare(x, y) == 0);
        }

        /// <summary>
        ///A test for Compare (string, string)
        ///</summary>
        [TestMethod]
        public void Compare1()
        {
            IPAddressComparer comparer = IPAddressComparer.Default as IPAddressComparer;
            string x = "127.0.0.0";
            string y = "128.0.0.0";

            Assert.IsTrue(comparer.Compare(x, y) < 0);
            Assert.IsTrue(comparer.Compare(y, x) > 0);
            Assert.IsTrue(comparer.Compare(x, x) == 0);

            x = "1067890"; // 0.16.75.114
            y = "1890329"; // 0.28.216.25.0

            Assert.IsTrue(comparer.Compare(x, y) < 0);
            Assert.IsTrue(comparer.Compare(y, x) > 0);
            Assert.IsTrue(comparer.Compare(x, x) == 0);
        }

        [TestMethod]
        public void Compare2()
        {
            IPAddressComparer comparer = IPAddressComparer.Default as IPAddressComparer;

            Assert.IsTrue(comparer.Compare(IPAddress.Loopback, IPAddress.Parse("128.0.0.0")) < 0);
            Assert.IsTrue(comparer.Compare(IPAddress.Parse("128.0.0.0"), IPAddress.Loopback) > 0);
            Assert.IsTrue(comparer.Compare(IPAddress.Loopback, IPAddress.Loopback) == 0);

            IPAddress x = new IPAddress(1067890); // 114.75.16.0
            IPAddress y = new IPAddress(1890329); // 25.216.28.0

            Assert.IsTrue(comparer.Compare(x, y) > 0);
            Assert.IsTrue(comparer.Compare(y, x) < 0);
            Assert.IsTrue(comparer.Compare(x, x) == 0);
        }

        [TestMethod]
        public void Compare3()
        {
            IPAddressComparer comparer = IPAddressComparer.Default as IPAddressComparer;

            try
            {
                int result = comparer.Compare(IPAddress.Loopback, null);
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
                int result = comparer.Compare(null, IPAddress.Loopback);
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
            IPAddressComparer comparer = IPAddressComparer.Default as IPAddressComparer;
            //Assert.IsTrue(comparer.Compare(null, IPAddress.Parse("128.0.0.0")) == -1);
            //Assert.IsTrue(comparer.Compare(IPAddress.Parse("128.0.0.0"), null) == 1);

            object x = null;
            object y = null;
            Assert.IsTrue(comparer.Compare(x, y) == 0);
        }

        [TestMethod]
        public void Compare5()
        {
            IPAddressComparer comparer = IPAddressComparer.Default as IPAddressComparer;

            try
            {
                comparer.Compare("1067890", "abc123");
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
                comparer.Compare("abc123", "1067890");
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
                comparer.Compare("1067890", String.Empty);
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
                comparer.Compare(String.Empty, "1067890");
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
                comparer.Compare("1067890", null);
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
                comparer.Compare(null, "1067890");
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
        public void Compare6()
        {
            IPAddressComparer comparer = new IPAddressComparer();
            object x = "127.0.0.0";
            object y = "128.0.0.0";

            Assert.IsTrue(comparer.Compare(x, y) < 0);
            Assert.IsTrue(comparer.Compare(y, x) > 0);
            Assert.IsTrue(comparer.Compare(x, x) == 0);

            x = "1567890";
            y = "1890329";

            Assert.IsTrue(comparer.Compare(x, y) < 0);
            Assert.IsTrue(comparer.Compare(y, x) > 0);
            Assert.IsTrue(comparer.Compare(x, x) == 0);

            x = String.Empty;
            y = String.Empty;

            Assert.IsTrue(comparer.Compare(x, "127.0.0.0") == -1);
            Assert.IsTrue(comparer.Compare("127.0.0.0", y) == 1);
            Assert.IsTrue(comparer.Compare(x, y) == 0);
        }

        [TestMethod]
        public void Equals()
        {
            IPAddressComparer comparer = IPAddressComparer.Default as IPAddressComparer;
            IPAddress x = IPAddress.Loopback;
            IPAddress y = IPAddress.Parse("128.0.0.0");

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.IsTrue(comparer.Equals(x, x));
        }

        [TestMethod]
        public void Equals1()
        {
            IPAddressComparer comparer = IPAddressComparer.Default as IPAddressComparer;
            string x = "127.0.0.0";
            string y = "128.0.0.0";

            Assert.IsFalse(((IEqualityComparer)comparer).Equals(x, y));
            Assert.IsTrue(((IEqualityComparer)comparer).Equals(x, x));
        }

        [TestMethod]
        public void Equals2()
        {
            IPAddressComparer comparer = IPAddressComparer.Default as IPAddressComparer;
            string x = "127.0.0.0";
            string y = "128.0.0.0";

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.IsTrue(comparer.Equals(x, x));
        }
        
        [TestMethod]
        public void Equals3()
        {
            IPAddressComparer comparer = IPAddressComparer.Default as IPAddressComparer;
            string x = "abc";
            string y = "def";

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.IsTrue(comparer.Equals(x, x));

            x = null;
            y = null;
            Assert.IsTrue(comparer.Equals(x, y));
            Assert.IsFalse(comparer.Equals(x, "abc"));
            Assert.IsFalse(comparer.Equals("abc", y));

            Assert.IsFalse(comparer.Equals("abc", "127.0.0.0"));
            Assert.IsFalse(comparer.Equals("127.0.0.0", "abc"));

        }

        [TestMethod]
        public void Equals4()
        {
            IPAddressComparer comparer = IPAddressComparer.Default as IPAddressComparer;
            IPAddress x = null;
            IPAddress y = IPAddress.Parse("128.0.0.0");

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.IsFalse(comparer.Equals(y, x));
        }

        [TestMethod]
        public void GetHashCode1()
        {
            IPAddressComparer comparer = IPAddressComparer.Default as IPAddressComparer;
            string x = "127.0.0.0";
            Assert.AreEqual(IPAddress.Parse(x).GetHashCode(), comparer.GetHashCode(x));

            object y = "127.0.0.0";
            Assert.AreEqual(IPAddress.Parse(y.ToString()).GetHashCode(), comparer.GetHashCode(y));

            IPAddress z = IPAddress.Loopback;
            Assert.AreEqual(z.GetHashCode(), comparer.GetHashCode(z));

            string a = "abc";
            Assert.AreEqual(a.GetHashCode(), comparer.GetHashCode(a));
        }

        [TestMethod]
        public void GetHashCode2()
        {
            IPAddressComparer comparer = IPAddressComparer.Default as IPAddressComparer;
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

            try
            {
                IPAddress x = null;
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
            IPAddressComparer comparer = IPAddressComparer.Default as IPAddressComparer;
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
        }

        [TestMethod]
        public void GetHashCode4()
        {
            IPAddressComparer comparer = IPAddressComparer.Default as IPAddressComparer;

            object x = IPAddress.Loopback;
            Assert.AreEqual(IPAddress.Loopback.GetHashCode(), comparer.GetHashCode(x));

            object y = DateTime.Today;
            Assert.AreEqual(y.GetHashCode(), comparer.GetHashCode(y));

            object z = "127.0.0.0";
            Assert.AreEqual(IPAddress.Parse(z.ToString()).GetHashCode(), comparer.GetHashCode(z));

            object a = "xyz";
            Assert.AreEqual(a.GetHashCode(), comparer.GetHashCode(a));
        }
    }
}
