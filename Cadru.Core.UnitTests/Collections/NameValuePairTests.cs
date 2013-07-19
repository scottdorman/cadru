using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Cadru.Collections;
using Cadru.UnitTest.Framework;
using System.Collections;

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
    }
}
