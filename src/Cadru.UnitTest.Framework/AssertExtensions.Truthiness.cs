
using Cadru.UnitTest.Framework.Resources;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.UnitTest.Framework
{
    /// <summary>
    /// A collection of helpers to test various conditions within unit tests.
    /// If the condition being tested is not met, an exception is thrown.
    /// </summary>
    public static partial class AssertExtensions
    {
        /// <summary>
        /// Verifies that the specified condition is <see langword="false"/>.
        /// The assertion fails if the condition is <see langword="true"/>.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="condition">The condition to verify is <see langword="false"/>.</param>
        /// <exception cref="Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">
        /// <paramref name="condition"/> evaluates to <see langword="true"/>.
        /// </exception>
        public static void IsFalse(this Assert assert, bool? condition)
        {
            assert.IsFalse(condition, Strings.Assertion_GenericFailure);
        }

        /// <summary>
        /// Verifies that the specified condition is <see langword="false"/>.
        /// The assertion fails if the condition is <see langword="true"/>.
        /// Displays a message if the assertion fails.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="condition">The condition to verify is <see langword="false"/>.</param>
        /// <param name="message">
        /// A message to display if the assertion fails. This message can be
        /// seen in the unit test results.
        /// </param>
        /// <exception cref="Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">
        /// <paramref name="condition"/> evaluates to <see langword="true"/>.
        /// </exception>
        public static void IsFalse(this Assert assert, bool? condition, string message)
        {
            assert.IsFalse(condition, message, true, condition!);
        }

        /// <summary>
        /// Verifies that the specified condition is <see langword="false"/>.
        /// The assertion fails if the condition is <see langword="true"/>.
        /// Displays a message if the assertion fails, and applies the specified
        /// formatting to it.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="condition">The condition to verify is <see langword="false"/>.</param>
        /// <param name="message">
        /// A message to display if the assertion fails. This message can be
        /// seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        /// <exception cref="Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">
        /// <paramref name="condition"/> evaluates to <see langword="true"/>.
        /// </exception>
        public static void IsFalse(this Assert assert, bool? condition, string message, params object[] parameters)
        {
            if (!condition.HasValue || condition.Value)
            {
                Helpers.HandleFail("Assert.IsFalse", message, parameters);
            }
        }

        /// <summary>
        /// Verifies that the specified condition is <see langword="true"/>. The
        /// assertion fails if the condition is <see langword="false"/>.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="condition">The condition to verify is <see langword="true"/>.</param>
        /// <exception cref="Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">
        /// <paramref name="condition"/> evaluates to <see langword="false"/>.
        /// </exception>
        public static void IsTrue(this Assert assert, bool? condition)
        {
            assert.IsTrue(condition, Strings.Assertion_GenericFailure);
        }

        /// <summary>
        /// Verifies that the specified condition is <see langword="true"/>. The
        /// assertion fails if the condition is <see langword="false"/>.
        /// Displays a message if the assertion fails.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="condition">The condition to verify is <see langword="true"/>.</param>
        /// <param name="message">
        /// A message to display if the assertion fails. This message can be
        /// seen in the unit test results.
        /// </param>
        /// <exception cref="Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">
        /// <paramref name="condition"/> evaluates to <see langword="false"/>.
        /// </exception>
        public static void IsTrue(this Assert assert, bool? condition, string message)
        {
            assert.IsTrue(condition, message, true, condition!);
        }

        /// <summary>
        /// Verifies that the specified condition is <see langword="true"/>. The
        /// assertion fails if the condition is <see langword="false"/>.
        /// Displays a message if the assertion fails, and applies the specified
        /// formatting to it.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="condition">The condition to verify is <see langword="true"/>.</param>
        /// <param name="message">
        /// A message to display if the assertion fails. This message can be
        /// seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        /// <exception cref="Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">
        /// <paramref name="condition"/> evaluates to <see langword="false"/>.
        /// </exception>
        public static void IsTrue(this Assert assert, bool? condition, string message, params object[] parameters)
        {
            if (!condition.HasValue || !condition.Value)
            {
                Helpers.HandleFail("Assert.IsTrue", message, parameters);
            }
        }

    }
}
