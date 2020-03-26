using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.UnitTest.Framework.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ConditionAssertGreaterTests
    {
        private readonly int i1 = 5;
        private readonly int i2 = 4;
        private readonly uint u1 = 12345879;
        private readonly uint u2 = 12345678;
        private readonly float f1 = 3.543F;
        private readonly float f2 = 2.543F;
        private readonly decimal de1 = 53.4M;
        private readonly decimal de2 = 33.4M;
        private readonly double d1 = 4.85948654;
        private readonly double d2 = 1.0;

        [TestMethod]
        public void Greater()
        {
            ConditionAssert.Greater(this.i1, this.i2);
            ConditionAssert.Greater(this.u1, this.u2);
            ConditionAssert.Greater(this.d1, this.d2, "double");
            ConditionAssert.Greater(this.de1, this.de2, "{0}", "decimal");
            ConditionAssert.Greater(this.f1, this.f2, "float");
        }

        [TestMethod, ExpectedException(typeof(AssertFailedException))]
        public void NotGreaterWhenEqual()
        {
            ConditionAssert.Greater(this.i1, this.i1);
        }

        [TestMethod, ExpectedException(typeof(AssertFailedException))]
        public void NotGreater()
        {
            ConditionAssert.Greater(this.i2, this.i1);
        }

        [TestMethod, ExpectedException(typeof(AssertFailedException))]
        public void NotGreaterIComparable()
        {
            ConditionAssert.Greater(System.Net.DecompressionMethods.GZip, System.Net.DecompressionMethods.Deflate);
        }

        [TestMethod]
        public void FailureMessage()
        {
            string msg = null;

            try
            {
                ConditionAssert.Greater(7, 99);
            }
            catch (AssertFailedException ex)
            {
                msg = ex.Message;
            }

            StringAssert.Contains("Assert.Fail failed. 99 is less than or equal to 7.", msg);
        }
    }
}
