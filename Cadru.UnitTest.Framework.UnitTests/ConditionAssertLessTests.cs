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

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional TestMethod() attributes
        // 
        //You can use the following additional attributes as you write your TestMethod()s:
        //
        //Use ClassInitialize to run code before running the first TestMethod() in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestMethod()Context TestMethod()Context)
        //{
        //}
        //
        //Use ClassCleanup to run code after all TestMethod()s in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestMethod()Initialize to run code before running each TestMethod()
        //[TestMethod()Initialize()]
        //public void MyTestMethod()Initialize()
        //{
        //}
        //
        //Use TestMethod()Cleanup to run code after each TestMethod() has run
        //[TestMethod()Cleanup()]
        //public void MyTestMethod()Cleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void Less()
        {
            // Testing all forms after seeing some bugs. CFP
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

        [TestMethod(), ExpectedException(typeof(AssertFailedException))]
        public void NotLessWhenEqual()
        {
            ConditionAssert.Less(i1, i1);
        }

        [TestMethod(), ExpectedException(typeof(AssertFailedException))]
        public void NotLess()
        {
            ConditionAssert.Less(i2, i1);
        }

        [TestMethod(), ExpectedException(typeof(AssertFailedException))]
        public void NotLessIComparable()
        {
            ConditionAssert.Less(e2, e1);
        }

        [TestMethod()]
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
