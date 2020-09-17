//------------------------------------------------------------------------------
// <copyright file="AssumesTests.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2020 Scott Dorman.
// </copyright>
//
// <license>
//    Licensed under the Microsoft Public License (Ms-PL) (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//    http://opensource.org/licenses/Ms-PL.html
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </license>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Cadru.UnitTest.Framework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Contracts.Tests
{
#if DEBUG

    public class TestingTraceListener : DefaultTraceListener
    {
        public override void Fail(string message, string detailMessage)
        {
            throw new AssumptionException(message + detailMessage);
        }
    }

    // These tests will always fail in a non-debug build since all of the
    // Assumes methods are marked with [Conditional("Debug")].
    [TestClass, ExcludeFromCodeCoverage]
    public class AssumesTests
    {
        [ClassInitialize()]
        public static void ClassInitialize(TestContext context)
        {
            Trace.Listeners.Clear();
            Trace.Listeners.Add(new TestingTraceListener());
        }

        [TestMethod]
        public void AssumptionException()
        {
            Assert.ThrowsException<AssumptionException>(() => { throw new AssumptionException(); }).WithMessage("Assumption failed.");
            Assert.ThrowsException<AssumptionException>(() => { throw new AssumptionException("Assumption failed.", new InvalidOperationException()); }).WithMessage("Assumption failed.").WithInnerException(typeof(InvalidOperationException));
        }

        [TestMethod]
        public void IsEnum()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Assumes.IsEnum(null, "foo"));
            Assert.ThrowsException<AssumptionException>(() => Assumes.IsEnum(2, "foo"));
            Assumes.IsEnum(DayOfWeek.Friday, "foo");

            Assert.ThrowsException<ArgumentNullException>(() => Assumes.IsEnum((object)null, "foo"));
            Assert.ThrowsException<AssumptionException>(() => Assumes.IsEnum((object)2, "foo"));
            Assumes.IsEnum((object)DayOfWeek.Friday, "foo");
        }

        [TestMethod]
        public void IsType()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Assumes.IsType(null, typeof(string), "foo"));
            Assert.ThrowsException<AssumptionException>(() => Assumes.IsType(2, typeof(string), "foo"));
            Assumes.IsType(String.Empty, typeof(string), "foo");

            Assert.ThrowsException<ArgumentNullException>(() => Assumes.IsType<string>(null, "foo"));
            Assert.ThrowsException<AssumptionException>(() => Assumes.IsType<string>(2, "foo"));
            Assumes.IsType<string>(String.Empty, "foo");
        }

        [TestMethod]
        public void Fail()
        {
            Assert.ThrowsException<AssumptionException>(() => { Assumes.Fail((string)null); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);
            Assert.ThrowsException<AssumptionException>(() => { Assumes.Fail(String.Empty); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);
            Assert.ThrowsException<AssumptionException>(() => { Assumes.Fail("test."); }).WithMessage("Assumption failed. test.", ExceptionMessageComparison.StartsWith);
        }

        [TestMethod]
        public void IsFalse()
        {
            Assert.ThrowsException<AssumptionException>(() => { Assumes.IsFalse(true); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);
            Assert.ThrowsException<AssumptionException>(() => { Assumes.IsFalse(true, "test."); }).WithMessage("Assumption failed. test.", ExceptionMessageComparison.StartsWith);

            Assumes.IsFalse(false);
            Assumes.IsFalse(false, "test");
        }

        [TestMethod]
        public void IsTrue()
        {
            Assert.ThrowsException<AssumptionException>(() => { Assumes.IsTrue(false); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);
            Assert.ThrowsException<AssumptionException>(() => { Assumes.IsTrue(false, "test."); }).WithMessage("Assumption failed. test.", ExceptionMessageComparison.StartsWith);

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

            Assert.ThrowsException<AssumptionException>(() => { Assumes.NotNull(s1); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);
            Assert.ThrowsException<AssumptionException>(() => { Assumes.NotNull(s1, s2); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);
            Assert.ThrowsException<AssumptionException>(() => { Assumes.NotNull(s1, s2, s3); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);
            Assert.ThrowsException<AssumptionException>(() => { Assumes.NotNull(s1, s2, s3, s4); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);

            Assumes.NotNull("");
            Assumes.NotNull("", "");
            Assumes.NotNull("", "", "");
            Assumes.NotNull("", "", "", "");
        }

        [TestMethod]
        public void NotNullOrEmptyString()
        {
            string s1 = null;

            Assert.ThrowsException<AssumptionException>(() => { Assumes.NotNullOrEmpty(s1); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);
            Assert.ThrowsException<AssumptionException>(() => { Assumes.NotNullOrEmpty(String.Empty); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);

            Assumes.NotNullOrEmpty("test");
        }

        [TestMethod]
        public void Null()
        {
            object o1 = null;

            Assert.ThrowsException<AssumptionException>(() => { Assumes.Null(new object()); }).WithMessage("Assumption failed.", ExceptionMessageComparison.StartsWith);
            Assumes.Null(o1);
        }
    }

#endif
}