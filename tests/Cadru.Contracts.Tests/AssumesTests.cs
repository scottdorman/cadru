using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Cadru.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.UnitTest.Framework.UnitTests.Contracts
{
#if DEBUG
    // These tests will always fail in a non-debug build since all of the
    // Assumes methods are marked with [Conditional("Debug")].
    [TestClass, ExcludeFromCodeCoverage]
    public class AssumesTests
    {
        private static Type debugAssertException;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext context)
        {
            Trace.Listeners.Clear();
            debugAssertException = typeof(System.Diagnostics.Debug).GetNestedType("DebugAssertException", BindingFlags.NonPublic);

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
            ExceptionAssert.Throws(debugAssertException, () => Assumes.IsEnum(2, "foo"));
            Assumes.IsEnum(DayOfWeek.Friday, "foo");

            ExceptionAssert.Throws<ArgumentNullException>(() => Assumes.IsEnum((object)null, "foo"));
            ExceptionAssert.Throws(debugAssertException, () => Assumes.IsEnum((object)2, "foo"));
            Assumes.IsEnum((object)DayOfWeek.Friday, "foo");
        }

        [TestMethod]
        public void IsType()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() => Assumes.IsType(null, typeof(string), "foo"));
            ExceptionAssert.Throws(debugAssertException, () => Assumes.IsType(2, typeof(string), "foo"));
            Assumes.IsType(String.Empty, typeof(string), "foo");

            ExceptionAssert.Throws<ArgumentNullException>(() => Assumes.IsType<string>(null, "foo"));
            ExceptionAssert.Throws(debugAssertException, () => Assumes.IsType<string>(2, "foo"));
            Assumes.IsType<string>(String.Empty, "foo");
        }

        [TestMethod]
        public void Fail()
        {
            ExceptionAssert.Throws(debugAssertException, () => { Assumes.Fail(String.Empty); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);
            ExceptionAssert.Throws(debugAssertException, () => { Assumes.Fail("test."); }).WithMessage("Assumption failed. test.", ExceptionMessageComparison.StartsWith);
        }

        [TestMethod]
        public void IsFalse()
        {
            ExceptionAssert.Throws(debugAssertException, () => { Assumes.IsFalse(true); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);
            ExceptionAssert.Throws(debugAssertException, () => { Assumes.IsFalse(true, "test."); }).WithMessage("Assumption failed. test.", ExceptionMessageComparison.StartsWith);

            Assumes.IsFalse(false);
            Assumes.IsFalse(false, "test");
        }

        [TestMethod]
        public void IsTrue()
        {
            ExceptionAssert.Throws(debugAssertException, () => { Assumes.IsTrue(false); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);
            ExceptionAssert.Throws(debugAssertException, () => { Assumes.IsTrue(false, "test."); }).WithMessage("Assumption failed. test.", ExceptionMessageComparison.StartsWith);

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

            ExceptionAssert.Throws(debugAssertException, () => { Assumes.NotNull(s1); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);
            ExceptionAssert.Throws(debugAssertException, () => { Assumes.NotNull(s1, s2); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);
            ExceptionAssert.Throws(debugAssertException, () => { Assumes.NotNull(s1, s2, s3); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);
            ExceptionAssert.Throws(debugAssertException, () => { Assumes.NotNull(s1, s2, s3, s4); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);

            Assumes.NotNull("");
            Assumes.NotNull("", "");
            Assumes.NotNull("", "", "");
            Assumes.NotNull("", "", "", "");
        }

        [TestMethod]
        public void NotNullOrEmptyString()
        {
            string s1 = null;

            ExceptionAssert.Throws(debugAssertException, () => { Assumes.NotNullOrEmpty(s1); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);
            ExceptionAssert.Throws(debugAssertException, () => { Assumes.NotNullOrEmpty(String.Empty); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);

            Assumes.NotNullOrEmpty("test");
        }

        [TestMethod]
        public void Null()
        {
            object o1 = null;

            ExceptionAssert.Throws(debugAssertException, () => { Assumes.Null(new object()); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);
            Assumes.Null(o1);
        }
    }
#endif
}
