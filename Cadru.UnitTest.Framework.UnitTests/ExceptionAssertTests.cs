using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.UnitTest.Framework.UnitTests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExceptionAssertTests
    {
        [TestMethod()]
        public void Throws()
        {
            ExceptionAssert.Throws<ArgumentException>(() => { throw new ArgumentException(); });
            ExceptionAssert.Throws<ArgumentException>(() => { throw new ArgumentException("Test message"); }).WithMessage("Test message");
        }

        [TestMethod(), ExpectedException(typeof(AssertFailedException))]
        public void Throws1()
        {
            ExceptionAssert.Throws<ArgumentException>(() => { throw new ArgumentException("Test message"); }).WithMessage("Test");
        }
    }
}
