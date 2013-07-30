using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cadru.Extensions;

namespace Cadru.UnitTest.Framework.UnitTests.Extensions
{
    [TestClass]
    public class BooleanExtensions
    {
        [TestMethod]
        public void ToBinary()
        {
            Assert.IsTrue(true.ToBit() == 1);
            Assert.IsFalse(false.ToBit() == 1);
            Assert.IsFalse(true.ToBit() == 0);
            Assert.IsTrue(false.ToBit() == 0);
        }

        [TestMethod]
        public void ToChar()
        {
            Assert.IsTrue(true.ToChar() == 'T');
            Assert.IsFalse(false.ToChar() == 'T');
            Assert.IsFalse(true.ToChar() == 'F');
            Assert.IsTrue(false.ToChar() == 'F');
        }
    }
}
