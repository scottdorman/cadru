using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cadru.Extensions;
using Cadru.UnitTest.Framework;
using System.Diagnostics.CodeAnalysis;
using System.Collections;
using System.Collections.Generic;

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

        [TestMethod]
        public void HasInterface()
        {
            Assert.IsFalse(typeof(string).HasInterface<IDisposable>());
            Assert.IsTrue(typeof(StringComparer).HasInterface<IComparer>());
            Assert.IsTrue(typeof(StringComparer).HasInterface<IEqualityComparer<string>>());
        }
    }
}
