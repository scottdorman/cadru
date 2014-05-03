using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cadru.Extensions;
using Cadru.UnitTest.Framework;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.UnitTest.Framework.UnitTests.Extensions
{
    [TestClass, ExcludeFromCodeCoverage]
    public class NullExtensionsTests
    {
        [TestMethod]
        public void IsNull()
        {
            string test = null;
            Assert.IsTrue(((string)null).IsNull());
            Assert.IsTrue(test.IsNull());
            Assert.IsFalse("test".IsNull());
        }

        [TestMethod]
        public void IsNotNull()
        {
            string test = null;
            Assert.IsFalse(((string)null).IsNotNull());
            Assert.IsFalse(NullExtensions.IsNotNull(test));
            Assert.IsTrue(NullExtensions.IsNotNull("test"));
        }
    }
}
