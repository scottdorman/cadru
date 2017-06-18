using Cadru.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
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

        [TestMethod]
        public void HasInterface()
        {
            Assert.IsFalse(typeof(string).HasInterface<IDisposable>());
            Assert.IsTrue(typeof(StringComparer).HasInterface<IComparer>());
            Assert.IsTrue(typeof(StringComparer).HasInterface<IEqualityComparer<string>>());
        }

        [TestMethod]
        public void IsNumeric()
        {
            Assert.IsFalse(typeof(string).IsNumeric());
            Assert.IsTrue(typeof(int).IsNumeric());
            Assert.IsTrue(typeof(int?).IsNumeric());
        }

        [TestMethod]
        public void IsDate()
        {
            Assert.IsFalse(typeof(string).IsDate());
            Assert.IsTrue(typeof(DateTime).IsDate());
            Assert.IsTrue(typeof(DateTime?).IsDate());
        }


    }
}
