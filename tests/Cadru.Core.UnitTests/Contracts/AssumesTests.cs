using Cadru.Contracts;
using Cadru.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Cadru.UnitTest.Framework.UnitTests.Contracts
{

    // These tests will always fail in a non-debug build since all of the
    // Assumes methods are marked with [Conditional("Debug")].
#if DEBUG
    [TestClass, ExcludeFromCodeCoverage]
    public class AssumesTests
    {
        [ClassInitialize()]
        public static void ClassInitialize(TestContext context)
        {
            Trace.Listeners.Clear();
        }

        [TestMethod]
        public void AssumptionException()
        {
            ExceptionAssert.Throws<AssumptionException>(() => { throw new AssumptionException(); }).WithMessage("Assumption failed.");
            ExceptionAssert.Throws<AssumptionException>(() => { throw new AssumptionException("Assumption failed.", new InvalidOperationException()); }).WithMessage("Assumption failed.").WithInnerException(typeof(InvalidOperationException));
        }

        [TestMethod]
        public void IsEnum()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() => Assumes.IsEnum(null, "foo"));
            ExceptionAssert.Throws<AssumptionException>(() => Assumes.IsEnum(2, "foo"));
            Assumes.IsEnum(DayOfWeek.Friday, "foo");

            ExceptionAssert.Throws<ArgumentNullException>(() => Assumes.IsEnum((object)null, "foo"));
            ExceptionAssert.Throws<AssumptionException>(() => Assumes.IsEnum((object)2, "foo"));
            Assumes.IsEnum((object)DayOfWeek.Friday, "foo");
        }

        [TestMethod]
        public void IsType()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() => Assumes.IsType(null, typeof(string), "foo"));
            ExceptionAssert.Throws<AssumptionException>(() => Assumes.IsType(2, typeof(string), "foo"));
            Assumes.IsType(String.Empty, typeof(string), "foo");

            ExceptionAssert.Throws<ArgumentNullException>(() => Assumes.IsType<string>(null, "foo"));
            ExceptionAssert.Throws<AssumptionException>(() => Assumes.IsType<string>(2, "foo"));
            Assumes.IsType<string>(String.Empty, "foo");
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
            Assumes.IsFalse(false, "test");
        }

        [TestMethod]
        public void IsTrue()
        {
            ExceptionAssert.Throws<AssumptionException>(() => { Assumes.IsTrue(false); }).WithMessage("Assumption failed.");
            ExceptionAssert.Throws<AssumptionException>(() => { Assumes.IsTrue(false, "test."); }).WithMessage("Assumption failed. test.");

            Assumes.IsTrue(true);
            Assumes.IsTrue(true, "test");
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
#endif
}
