using System;
using System.Diagnostics.CodeAnalysis;

using Cadru.UnitTest.Framework.Resources;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.UnitTest.Framework
{
    /// <summary>
    /// A collection of helpers to test various conditions within unit tests.
    /// If the condition being tested is not met, an exception is thrown.
    /// </summary>
    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public static partial class AssertExtensions
    {
        /// <summary>
        /// Asserts that two strings are equal, without regard to case.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        [Obsolete("Use StringAssert.That.AreEqualIgnoringCase", error: true)]
        public static void AreEqualIgnoringCase(this Assert assert, string expected, string actual) => StringAssert.That.AreEqualIgnoringCase(expected, actual);

        /// <summary>
        /// Asserts that two strings are equal, without regard to case.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        [Obsolete("Use StringAssert.That.AreEqualIgnoringCase", error: true)]
        public static void AreEqualIgnoringCase(this Assert assert, string expected, string actual, string message) => StringAssert.That.AreEqualIgnoringCase(expected, actual, message);

        /// <summary>
        /// Asserts that two strings are equal, without regard to case.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        [Obsolete("Use StringAssert.That.AreEqualIgnoringCase", error: true)]
        public static void AreEqualIgnoringCase(this Assert assert, string expected, string actual, string message, params object[] parameters) => StringAssert.That.AreEqualIgnoringCase(expected, actual, message, parameters);

        /// <summary>
        /// Asserts that two strings are not equal, without regard to case.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        [Obsolete("Use StringAssert.That.AreNotEqualIgnoringCase", error: true)]
        public static void AreNotEqualIgnoringCase(this Assert assert, string expected, string actual) => StringAssert.That.AreNotEqualIgnoringCase(expected, actual);

        /// <summary>
        /// Asserts that two strings are not equal, without regard to case.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        [Obsolete("Use StringAssert.That.AreNotEqualIgnoringCase", error: true)]
        public static void AreNotEqualIgnoringCase(this Assert assert, string expected, string actual, string message) => StringAssert.That.AreNotEqualIgnoringCase(expected, actual, message);

        /// <summary>
        /// Asserts that two strings are not equal, without regard to case.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        [Obsolete("Use StringAssert.That.AreNotEqualIgnoringCase", error: true)]
        public static void AreNotEqualIgnoringCase(this Assert assert, string expected, string actual, string message, params object[] parameters) => StringAssert.That.AreNotEqualIgnoringCase(expected, actual, message, parameters);
    }
}
