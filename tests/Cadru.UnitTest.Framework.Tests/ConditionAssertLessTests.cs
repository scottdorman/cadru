using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.UnitTest.Framework.UnitTests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ConditionAssertLessTests
    {
        private readonly int i1 = 5;
        private readonly int i2 = 8;
        private readonly uint u1 = 12345678;
        private readonly uint u2 = 12345879;
        private readonly float f1 = 3.543F;
        private readonly float f2 = 8.543F;
        private readonly decimal de1 = 53.4M;
        private readonly decimal de2 = 83.4M;
        private readonly double d1 = 4.85948654;
        private readonly double d2 = 8.0;
        private readonly System.Enum e1 = System.Data.CommandType.StoredProcedure;
        private readonly System.Enum e2 = System.Data.CommandType.TableDirect;

        [TestMethod]
        public void Less()
        {
            ConditionAssert.Less(i1, i2);
            ConditionAssert.Less(i1, i2, "int");
            ConditionAssert.Less(i1, i2, "{0}", "int");
            ConditionAssert.Less(u1, u2, "uint");
            ConditionAssert.Less(u1, u2, "{0}", "uint");
            ConditionAssert.Less(d1, d2);
            ConditionAssert.Less(d1, d2, "double");
            ConditionAssert.Less(d1, d2, "{0}", "double");
            ConditionAssert.Less(de1, de2);
            ConditionAssert.Less(de1, de2, "decimal");
            ConditionAssert.Less(de1, de2, "{0}", "decimal");
            ConditionAssert.Less(f1, f2);
            ConditionAssert.Less(f1, f2, "float");
            ConditionAssert.Less(f1, f2, "{0}", "float");
        }

        [TestMethod, ExpectedException(typeof(AssertFailedException))]
        public void NotLessWhenEqual()
        {
            ConditionAssert.Less(i1, i1);
        }

        [TestMethod, ExpectedException(typeof(AssertFailedException))]
        public void NotLess()
        {
            ConditionAssert.Less(i2, i1);
        }

        [TestMethod, ExpectedException(typeof(AssertFailedException))]
        public void NotLessIComparable()
        {
            ConditionAssert.Less(e2, e1);
        }

        [TestMethod]
        public void FailureMessage()
        {
            string msg = null;

            try
            {
                ConditionAssert.Less(9, 4);
            }
            catch (AssertFailedException ex)
            {
                msg = ex.Message;
            }

            StringAssert.Contains("Assert.Fail failed. 4 is greater than or equal to 9.", msg);
        }
    }
}
