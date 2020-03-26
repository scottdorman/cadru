using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.UnitTest.Framework.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ConditionAssertTests
    {
        [TestMethod]
        public void IsNaN()
        {
            ConditionAssert.IsNaN(System.Double.NaN);
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNaNFails()
        {
            ConditionAssert.IsNaN(10.0);
        }
    }
}
