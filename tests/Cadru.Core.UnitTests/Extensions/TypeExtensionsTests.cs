using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cadru.Extensions;
using Cadru.UnitTest.Framework;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.UnitTest.Framework.UnitTests.Extensions
{
    [TestClass, ExcludeFromCodeCoverage]
    public class TypeExtensionsTests
    {
        [TestMethod]
        public void IsNullable()
        {
            Assert.IsTrue(typeof(bool?).IsNullable());
            Assert.IsFalse(typeof(bool).IsNullable());
        }
    }
}
