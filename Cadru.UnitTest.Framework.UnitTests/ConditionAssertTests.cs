using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.UnitTest.Framework.UnitTests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ConditionAssertTests
    {
        [TestMethod]
        public void IsNaN()
        {
            ConditionAssert.IsNaN(double.NaN);
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNaNFails()
        {
            ConditionAssert.IsNaN(10.0);
        }
    }
}
