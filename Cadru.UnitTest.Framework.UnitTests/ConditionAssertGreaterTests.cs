using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.UnitTest.Framework.UnitTests
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
        private readonly System.Enum e1 = System.Data.CommandType.TableDirect;
        private readonly System.Enum e2 = System.Data.CommandType.StoredProcedure;

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
        public void Greater()
        {
            ConditionAssert.Greater(i1, i2);
            ConditionAssert.Greater(u1, u2);
            ConditionAssert.Greater(d1, d2, "double");
            ConditionAssert.Greater(de1, de2, "{0}", "decimal");
            ConditionAssert.Greater(f1, f2, "float");
        }

        [TestMethod(), ExpectedException(typeof(AssertFailedException))]
        public void NotGreaterWhenEqual()
        {
            ConditionAssert.Greater(i1, i1);
        }

        [TestMethod(), ExpectedException(typeof(AssertFailedException))]
        public void NotGreater()
        {
            ConditionAssert.Greater(i2, i1);
        }

        [TestMethod(), ExpectedException(typeof(AssertFailedException))]
        public void NotGreaterIComparable()
        {
            ConditionAssert.Greater(e2, e1);
        }

        [TestMethod()]
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
