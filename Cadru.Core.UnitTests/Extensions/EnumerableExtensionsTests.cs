using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cadru;
using Cadru.Text;
using System.Diagnostics.CodeAnalysis;
using Cadru.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Cadru.UnitTest.Framework.UnitTests.Extensions
{
    [TestClass, ExcludeFromCodeCoverage]
    public class EnumerableExtensionsTests
    {
        [TestMethod]
        public void IsEmpty()
        {
            Assert.IsTrue(Enumerable.Empty<string>().IsEmpty());
            Assert.IsFalse(new[] { "test", "test2" }.IsEmpty());
            Assert.IsTrue(new ArrayList().IsEmpty());
            Assert.IsTrue(new List<int>().IsEmpty());

            Assert.IsTrue(String.Empty.IsEmpty());
            Assert.IsFalse("test".IsEmpty());
        }

        [TestMethod]
        public void IsNull()
        {
            Assert.IsFalse(Enumerable.Empty<string>().IsNull());
            Assert.IsFalse(new[] { "test", "test2" }.IsNull());
            Assert.IsTrue(((IEnumerable)null).IsNull());

            Assert.IsFalse(new ArrayList().IsNull());
            Assert.IsFalse(new List<int>().IsNull());
        }

        [TestMethod]
        public void IsNullOrEmpty()
        {
            Assert.IsTrue(Enumerable.Empty<string>().IsNullOrEmpty());
            Assert.IsFalse(new[] { "test", "test2" }.IsNullOrEmpty());
            Assert.IsTrue(((IEnumerable)null).IsNullOrEmpty());
            Assert.IsTrue(Enumerable.Empty<string>().IsNullOrEmpty());

            Assert.IsTrue(new ArrayList().IsNullOrEmpty());
            Assert.IsTrue(new List<int>().IsNullOrEmpty());
        }

        [TestMethod]
        public void Join()
        {
            string[] test = { "this", "is", "a", "test" };
            Assert.AreEqual("this,is,a,test", test.Join());
            Assert.AreEqual("this, is, a, test", test.Join(", "));
            Assert.AreEqual("this, is, a, test", test.Join(", ", 0, 4));
            Assert.AreEqual("this,is,a,test", test.Join(0, 4));

            var test1 = new List<string>()
            {
                "this", "is", "a", "test" 
            };

            Assert.AreEqual("this,is,a,test", test1.Join());
            Assert.AreEqual("this, is, a, test", test1.Join(", "));
            Assert.AreEqual("this, is, a, test", test1.Join(", ", 0, 4));
            Assert.AreEqual("this,is,a,test", test1.Join(0, 4));

            var test2 = test1.Select(s => s);
            Assert.AreEqual("this,is,a,test", test2.Join());
            Assert.AreEqual("this, is, a, test", test2.Join(", "));

            var test3 = new List<int>() 
            {
                1, 2, 3 
            }.Select(i => i);

            Assert.AreEqual("1,2,3", test3.Join());
            Assert.AreEqual("1, 2, 3", test3.Join(", "));
        }
    }
}
