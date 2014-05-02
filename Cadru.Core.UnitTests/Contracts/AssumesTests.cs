using Cadru.Contracts;
using Cadru.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Cadru.UnitTest.Framework.UnitTests.Contracts
{
    [TestClass, ExcludeFromCodeCoverage]
    public class AssumesTests
    {
        [ClassInitialize()]
        public static void ClassInitialize(TestContext context)
        {
            Trace.Listeners.Clear();
        }

        [TestMethod]
        public void Fail()
        {
            ExceptionAssert.Throws<AssumptionException>(() => { Assumes.Fail(String.Empty); }).WithMessage("Assumption failed.");
            ExceptionAssert.Throws<AssumptionException>(() => { Assumes.Fail("test."); }).WithMessage("Assumption failed. test.");
        }

        [TestMethod]
        public void IsFalse()
        {
            ExceptionAssert.Throws<AssumptionException>(() => { Assumes.IsFalse(true); }).WithMessage("Assumption failed.");
            ExceptionAssert.Throws<AssumptionException>(() => { Assumes.IsFalse(true, "test."); }).WithMessage("Assumption failed. test.");

            Assumes.IsFalse(false);
        }

        [TestMethod]
        public void IsTrue()
        {
            ExceptionAssert.Throws<AssumptionException>(() => { Assumes.IsTrue(false); }).WithMessage("Assumption failed.");
            ExceptionAssert.Throws<AssumptionException>(() => { Assumes.IsTrue(false, "test."); }).WithMessage("Assumption failed. test.");

            Assumes.IsTrue(true);
        }

        [TestMethod]
        public void NotNull()
        {
            string s1 = null;
            string s2 = null;
            string s3 = null;
            string s4 = null;

            ExceptionAssert.Throws<AssumptionException>(() => { Assumes.NotNull(s1); }).WithMessage("Assumption failed.");
            ExceptionAssert.Throws<AssumptionException>(() => { Assumes.NotNull(s1, s2); }).WithMessage("Assumption failed.");
            ExceptionAssert.Throws<AssumptionException>(() => { Assumes.NotNull(s1, s2, s3); }).WithMessage("Assumption failed.");
            ExceptionAssert.Throws<AssumptionException>(() => { Assumes.NotNull(s1, s2, s3, s4); }).WithMessage("Assumption failed.");

            Assumes.NotNull("");
            Assumes.NotNull("", "");
            Assumes.NotNull("", "", "");
            Assumes.NotNull("", "", "", "");
        }

        [TestMethod]
        public void NotNullOrEmptyString()
        {
            string s1 = null;

            ExceptionAssert.Throws<AssumptionException>(() => { Assumes.NotNullOrEmpty(s1); }).WithMessage("Assumption failed.");
            ExceptionAssert.Throws<AssumptionException>(() => { Assumes.NotNullOrEmpty(String.Empty); }).WithMessage("Assumption failed.");

            Assumes.NotNullOrEmpty("test");
        }

        [TestMethod]
        public void Null()
        {
            object o1 = null;

            ExceptionAssert.Throws<AssumptionException>(() => { Assumes.Null(new object()); }).WithMessage("Assumption failed.");
            Assumes.Null(o1);
        }
    }
}
