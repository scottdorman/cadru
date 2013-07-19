using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.UnitTest.Framework.UnitTests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExceptionAssertTests
    {
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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void Throws()
        {
            ExceptionAssert.Throws<ArgumentException>(() => { throw new ArgumentException(); });
            ExceptionAssert.Throws<ArgumentException>("Test message", () => { throw new ArgumentException("Test message"); });
        }

        [TestMethod(), ExpectedException(typeof(AssertFailedException))]
        public void Throws1()
        {
            ExceptionAssert.Throws<ArgumentException>("Test", () => { throw new ArgumentException("Test message"); });
        }
    }
}
