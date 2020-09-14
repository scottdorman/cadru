using System;

using Cadru.UnitTest.Framework.Resources;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.UnitTest.Framework
{
    /// <summary>
    /// A collection of helpers to test various conditions within unit tests.
    /// If the condition being tested is not met, an exception is thrown.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public static partial class AssertExtensions
    {
        /// <summary>
        /// Asserts that two strings are equal, without regard to case.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        public static void AreEqualIgnoringCase(this Assert assert, string expected, string actual)
        {
            assert.AreEqualIgnoringCase(expected, actual, Strings.Assertion_GenericFailure);
        }

        /// <summary>
        /// Asserts that two strings are equal, without regard to case.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        public static void AreEqualIgnoringCase(this Assert assert, string expected, string actual, string message)
        {
            assert.AreEqualIgnoringCase(expected, actual, Strings.Assertion_GenericFailure, expected, actual);
        }

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
        public static void AreEqualIgnoringCase(this Assert assert, string expected, string actual, string message, params object[] parameters)
        {
            if (String.Compare(expected, actual, StringComparison.CurrentCultureIgnoreCase) != 0)
            {
                Helpers.HandleFail("Assert.AreEqualIgnoringCase", message, parameters);
            }
        }

        /// <summary>
        /// Asserts that two strings are not equal, without regard to case.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        public static void AreNotEqualIgnoringCase(this Assert assert, string expected, string actual)
        {
            assert.AreNotEqualIgnoringCase(expected, actual, Strings.Assertion_GenericFailure);
        }

        /// <summary>
        /// Asserts that two strings are not equal, without regard to case.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        public static void AreNotEqualIgnoringCase(this Assert assert, string expected, string actual, string message)
        {
            assert.AreNotEqualIgnoringCase(expected, actual, Strings.Assertion_GenericFailure, expected, actual);
        }

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
        public static void AreNotEqualIgnoringCase(this Assert assert, string expected, string actual, string message, params object[] parameters)
        {
            if (String.Compare(expected, actual, StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                Helpers.HandleFail("Assert.AreEqualIgnoringCase", message, parameters);
            }
        }

    }
}
