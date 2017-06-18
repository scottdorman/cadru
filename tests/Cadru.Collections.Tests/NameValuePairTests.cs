using Cadru.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.UnitTest.Framework.UnitTests.Collections
{
    [TestClass, ExcludeFromCodeCoverage]
    public class NameValuePairTests
    {
        [TestMethod]
        public void Constructor()
        {
            var nvp = new NameValuePair<string>("test");
            Assert.IsNotNull(nvp.Key);
            Assert.AreEqual("test", nvp.Key);
            Assert.IsNotNull(nvp.Value);
            CustomAssert.IsEmpty((ICollection)nvp.Value);
        }

        [TestMethod]
        public void Add()
        {
            var nvp = new NameValuePair<string>("test");
            nvp.Value.Add("one");
            nvp.Value.Add("two");
            CustomAssert.IsNotEmpty((ICollection)nvp.Value);
            Assert.IsTrue(nvp.Value.Count == 2);
            Assert.AreEqual("one", nvp.Value[0]);
            Assert.AreEqual("two", nvp.Value[1]);
        }

        [TestMethod]
        public void String()
        {
            var nvp = new NameValuePair<string>("test");
            nvp.Value.Add("one");
            nvp.Value.Add("two");

            Assert.AreEqual("[test: one, two]", nvp.ToString());
        }

        [TestMethod]
        public void Equals()
        {
            var nvp = new NameValuePair<string>("test");
            nvp.Value.Add("one");
            nvp.Value.Add("two");

            var nvp2 = new NameValuePair<string>("test");
            nvp2.Value.Add("one");
            nvp2.Value.Add("two");

            var nvp3 = new NameValuePair<string>("test3");
            nvp3.Value.Add("one");
            nvp3.Value.Add("two");

            Assert.IsTrue(nvp == nvp2);
            Assert.IsFalse(nvp != nvp2);
            Assert.IsTrue(nvp.Equals(nvp2));
            Assert.IsTrue(nvp.Equals((object)nvp2));

            Assert.IsFalse(nvp == null);
            Assert.IsTrue(nvp != null);
            Assert.IsFalse(nvp.Equals(null));

            Assert.IsFalse(nvp.Equals("test"));
            Assert.IsFalse(nvp == nvp3);
            Assert.IsTrue(nvp != nvp3);
            Assert.IsFalse(nvp.Equals(nvp3));
            Assert.IsFalse(nvp.Equals((object)nvp3));
        }

        [TestMethod]
        public void GetHashCodeTests()
        {
            var nvp = new NameValuePair<string>("test");
            nvp.Value.Add("one");
            nvp.Value.Add("two");

            Assert.AreEqual("test".GetHashCode(), nvp.GetHashCode());
        }
    }
}
