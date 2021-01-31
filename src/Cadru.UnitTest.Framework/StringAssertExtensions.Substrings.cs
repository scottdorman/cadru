using System;
using System.Globalization;

using Cadru.UnitTest.Framework.Resources;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.UnitTest.Framework
{
    /// <summary>
    /// A collection of helpers to test various conditions within unit tests.
    /// If the condition being tested is not met, an exception is thrown.
    /// </summary>
    public static partial class StringAssertExtensions
    {
        /// <summary>
        /// Tests whether the specified string does not contain the specified substring
        /// and throws an exception if the substring does occur within the
        /// test string.
        /// </summary>
        /// <param name="assert">The <see cref="StringAssert"/> instance to extend.</param>
        /// <param name="value">
        /// The string that is expected to contain <paramref name="substring"/>.
        /// </param>
        /// <param name="substring">
        /// The string expected to occur within <paramref name="value"/>.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="substring"/> is not found in
        /// <paramref name="value"/>.
        /// </exception>
        public static void DoesNotContain(this StringAssert assert, string value, string substring)
        {
            assert.DoesNotContain(value, substring, String.Empty, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests whether the specified string does not contain the specified substring
        /// and throws an exception if the substring does occur within the
        /// test string.
        /// </summary>
        /// <param name="assert">The <see cref="StringAssert"/> instance to extend.</param>
        /// <param name="value">
        /// The string that is expected to contain <paramref name="substring"/>.
        /// </param>
        /// <param name="substring">
        /// The string expected to occur within <paramref name="value"/>.
        /// </param>
        /// <param name="comparisonType">
        /// The comparison method to compare strings <paramref name="comparisonType"/>.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="substring"/> is not found in
        /// <paramref name="value"/>.
        /// </exception>
        public static void DoesNotContain(this StringAssert assert, string value, string substring, StringComparison comparisonType)
        {
            assert.DoesNotContain(value, substring, String.Empty, comparisonType);
        }

        /// <summary>
        /// Tests whether the specified string does not contain the specified substring
        /// and throws an exception if the substring does occur within the
        /// test string.
        /// </summary>
        /// <param name="assert">The <see cref="StringAssert"/> instance to extend.</param>
        /// <param name="value">
        /// The string that is expected to contain <paramref name="substring"/>.
        /// </param>
        /// <param name="substring">
        /// The string expected to occur within <paramref name="value"/>.
        /// </param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="substring"/>
        /// is not in <paramref name="value"/>. The message is shown in
        /// test results.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="substring"/> is not found in
        /// <paramref name="value"/>.
        /// </exception>
        public static void DoesNotContain(this StringAssert assert, string value, string substring, string message)
        {
            assert.DoesNotContain(value, substring, message, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests whether the specified string does not contain the specified substring
        /// and throws an exception if the substring does occur within the
        /// test string.
        /// </summary>
        /// <param name="assert">The <see cref="StringAssert"/> instance to extend.</param>
        /// <param name="value">
        /// The string that is expected to contain <paramref name="substring"/>.
        /// </param>
        /// <param name="substring">
        /// The string expected to occur within <paramref name="value"/>.
        /// </param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="substring"/>
        /// is not in <paramref name="value"/>. The message is shown in
        /// test results.
        /// </param>
        /// <param name="comparisonType">
        /// The comparison method to compare strings <paramref name="comparisonType"/>.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="substring"/> is not found in
        /// <paramref name="value"/>.
        /// </exception>
        public static void DoesNotContain(this StringAssert assert, string value, string substring, string message, StringComparison comparisonType)
        {
            assert.DoesNotContain(value, substring, message, comparisonType, Empty);
        }

        /// <summary>
        /// Tests whether the specified string does not contain the specified substring
        /// and throws an exception if the substring does occur within the
        /// test string.
        /// </summary>
        /// <param name="assert">The <see cref="StringAssert"/> instance to extend.</param>
        /// <param name="value">
        /// The string that is expected to contain <paramref name="substring"/>.
        /// </param>
        /// <param name="substring">
        /// The string expected to occur within <paramref name="value"/>.
        /// </param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="substring"/>
        /// is not in <paramref name="value"/>. The message is shown in
        /// test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="substring"/> is not found in
        /// <paramref name="value"/>.
        /// </exception>
        public static void DoesNotContain(this StringAssert assert, string value, string substring, string message, params object?[] parameters)
        {
            assert.DoesNotContain(value, substring, message, StringComparison.Ordinal, parameters);
        }

        /// <summary>
        /// Tests whether the specified string does not contain the specified substring
        /// and throws an exception if the substring does occur within the
        /// test string.
        /// </summary>
        /// <param name="assert">The <see cref="StringAssert"/> instance to extend.</param>
        /// <param name="value">
        /// The string that is expected to contain <paramref name="substring"/>.
        /// </param>
        /// <param name="substring">
        /// The string expected to occur within <paramref name="value"/>.
        /// </param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="substring"/>
        /// is not in <paramref name="value"/>. The message is shown in
        /// test results.
        /// </param>
        /// <param name="comparisonType">
        /// The comparison method to compare strings <paramref name="comparisonType"/>.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="substring"/> is not found in
        /// <paramref name="value"/>.
        /// </exception>
        public static void DoesNotContain(this StringAssert assert, string value, string substring, string message, StringComparison comparisonType, params object?[] parameters)
        {
            Helpers.CheckParameterNotNull(value, "StringAssert.DoesNotContain", "value", String.Empty);
            Helpers.CheckParameterNotNull(substring, "StringAssert.DoesNotContain", "substring", String.Empty);
            if (value.IndexOf(substring, comparisonType) > 0)
            {
                var finalMessage = String.Format(CultureInfo.CurrentCulture, Strings.DoesNotContainFail, value, substring, message);
                Helpers.HandleFail("StringAssert.DoesNotContain", finalMessage, parameters);
            }
        }

        /// <summary>
        /// Tests whether the specified string begins with the specified substring
        /// and throws an exception if the test string does not start with the
        /// substring.
        /// </summary>
        /// <param name="assert">The <see cref="StringAssert"/> instance to extend.</param>
        /// <param name="value">
        /// The string that is expected to begin with <paramref name="substring"/>.
        /// </param>
        /// <param name="substring">
        /// The string expected to be a prefix of <paramref name="value"/>.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="value"/> does not begin with
        /// <paramref name="substring"/>.
        /// </exception>
        public static void DoesNotStartWith(this StringAssert assert, string value, string substring)
        {
            assert.DoesNotStartWith(value, substring, String.Empty, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests whether the specified string begins with the specified substring
        /// and throws an exception if the test string does not start with the
        /// substring.
        /// </summary>
        /// <param name="assert">The <see cref="StringAssert"/> instance to extend.</param>
        /// <param name="value">
        /// The string that is expected to begin with <paramref name="substring"/>.
        /// </param>
        /// <param name="substring">
        /// The string expected to be a prefix of <paramref name="value"/>.
        /// </param>
        /// <param name="comparisonType">
        /// The comparison method to compare strings <paramref name="comparisonType"/>.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="value"/> does not begin with
        /// <paramref name="substring"/>.
        /// </exception>
        public static void DoesNotStartWith(this StringAssert assert, string value, string substring, StringComparison comparisonType)
        {
            assert.DoesNotStartWith(value, substring, String.Empty, comparisonType, Empty);
        }

        /// <summary>
        /// Tests whether the specified string begins with the specified substring
        /// and throws an exception if the test string does not start with the
        /// substring.
        /// </summary>
        /// <param name="assert">The <see cref="StringAssert"/> instance to extend.</param>
        /// <param name="value">
        /// The string that is expected to begin with <paramref name="substring"/>.
        /// </param>
        /// <param name="substring">
        /// The string expected to be a prefix of <paramref name="value"/>.
        /// </param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="value"/>
        /// does not begin with <paramref name="substring"/>. The message is
        /// shown in test results.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="value"/> does not begin with
        /// <paramref name="substring"/>.
        /// </exception>
        public static void DoesNotStartWith(this StringAssert assert, string value, string substring, string message)
        {
            assert.DoesNotStartWith(value, substring, message, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests whether the specified string begins with the specified substring
        /// and throws an exception if the test string does not start with the
        /// substring.
        /// </summary>
        /// <param name="assert">The <see cref="StringAssert"/> instance to extend.</param>
        /// <param name="value">
        /// The string that is expected to begin with <paramref name="substring"/>.
        /// </param>
        /// <param name="substring">
        /// The string expected to be a prefix of <paramref name="value"/>.
        /// </param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="value"/>
        /// does not begin with <paramref name="substring"/>. The message is
        /// shown in test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="value"/> does not begin with
        /// <paramref name="substring"/>.
        /// </exception>
        public static void DoesNotStartWith(this StringAssert assert, string value, string substring, string message, params object?[] parameters)
        {
            assert.DoesNotStartWith(value, substring, message, StringComparison.Ordinal, parameters);
        }

        /// <summary>
        /// Tests whether the specified string begins with the specified substring
        /// and throws an exception if the test string does not start with the
        /// substring.
        /// </summary>
        /// <param name="assert">The <see cref="StringAssert"/> instance to extend.</param>
        /// <param name="value">
        /// The string that is expected to begin with <paramref name="substring"/>.
        /// </param>
        /// <param name="substring">
        /// The string expected to be a prefix of <paramref name="value"/>.
        /// </param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="value"/>
        /// does not begin with <paramref name="substring"/>. The message is
        /// shown in test results.
        /// </param>
        /// <param name="comparisonType">
        /// The comparison method to compare strings <paramref name="comparisonType"/>.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="value"/> does not begin with
        /// <paramref name="substring"/>.
        /// </exception>
        public static void DoesNotStartWith(this StringAssert assert, string value, string substring, string message, StringComparison comparisonType)
        {
            assert.DoesNotStartWith(value, substring, message, comparisonType, Empty);
        }

        /// <summary>
        /// Tests whether the specified string begins with the specified substring
        /// and throws an exception if the test string does not start with the
        /// substring.
        /// </summary>
        /// <param name="assert">The <see cref="StringAssert"/> instance to extend.</param>
        /// <param name="value">
        /// The string that is expected to begin with <paramref name="substring"/>.
        /// </param>
        /// <param name="substring">
        /// The string expected to be a prefix of <paramref name="value"/>.
        /// </param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="value"/>
        /// does not begin with <paramref name="substring"/>. The message is
        /// shown in test results.
        /// </param>
        /// <param name="comparisonType">
        /// The comparison method to compare strings <paramref name="comparisonType"/>.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="value"/> does not begin with
        /// <paramref name="substring"/>.
        /// </exception>
        public static void DoesNotStartWith(this StringAssert assert, string value, string substring, string message, StringComparison comparisonType, params object?[] parameters)
        {
            Helpers.CheckParameterNotNull(value, "StringAssert.DoesNotStartWith", "value", String.Empty);
            Helpers.CheckParameterNotNull(substring, "StringAssert.DoesNotStartWith", "substring", String.Empty);
            if (value.StartsWith(substring, comparisonType))
            {
                var finalMessage = String.Format(CultureInfo.CurrentCulture, Strings.DoesNotStartWithFail, value, substring, message);
                Helpers.HandleFail("StringAssert.DoesNotStartWith", finalMessage, parameters);
            }
        }

        /// <summary>
        /// Tests whether the specified string ends with the specified substring
        /// and throws an exception if the test string does not end with the
        /// substring.
        /// </summary>
        /// <param name="assert">The <see cref="StringAssert"/> instance to extend.</param>
        /// <param name="value">
        /// The string that is expected to end with <paramref name="substring"/>.
        /// </param>
        /// <param name="substring">
        /// The string expected to be a suffix of <paramref name="value"/>.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="value"/> does not end with
        /// <paramref name="substring"/>.
        /// </exception>
        public static void DoesNotEndWith(this StringAssert assert, string value, string substring)
        {
            assert.DoesNotEndWith(value, substring, String.Empty, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests whether the specified string ends with the specified substring
        /// and throws an exception if the test string does not end with the
        /// substring.
        /// </summary>
        /// <param name="assert">The <see cref="StringAssert"/> instance to extend.</param>
        /// <param name="value">
        /// The string that is expected to end with <paramref name="substring"/>.
        /// </param>
        /// <param name="substring">
        /// The string expected to be a suffix of <paramref name="value"/>.
        /// </param>
        /// <param name="comparisonType">
        /// The comparison method to compare strings <paramref name="comparisonType"/>.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="value"/> does not end with
        /// <paramref name="substring"/>.
        /// </exception>
        public static void DoesNotEndWith(this StringAssert assert, string value, string substring, StringComparison comparisonType)
        {
            assert.DoesNotEndWith(value, substring, String.Empty, comparisonType, Empty);
        }

        /// <summary>
        /// Tests whether the specified string ends with the specified substring
        /// and throws an exception if the test string does not end with the
        /// substring.
        /// </summary>
        /// <param name="assert">The <see cref="StringAssert"/> instance to extend.</param>
        /// <param name="value">
        /// The string that is expected to end with <paramref name="substring"/>.
        /// </param>
        /// <param name="substring">
        /// The string expected to be a suffix of <paramref name="value"/>.
        /// </param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="value"/>
        /// does not end with <paramref name="substring"/>. The message is
        /// shown in test results.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="value"/> does not end with
        /// <paramref name="substring"/>.
        /// </exception>
        public static void DoesNotEndWith(this StringAssert assert, string value, string substring, string message)
        {
            assert.DoesNotEndWith(value, substring, message, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests whether the specified string ends with the specified substring
        /// and throws an exception if the test string does not end with the
        /// substring.
        /// </summary>
        /// <param name="assert">The <see cref="StringAssert"/> instance to extend.</param>
        /// <param name="value">
        /// The string that is expected to end with <paramref name="substring"/>.
        /// </param>
        /// <param name="substring">
        /// The string expected to be a suffix of <paramref name="value"/>.
        /// </param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="value"/>
        /// does not end with <paramref name="substring"/>. The message is
        /// shown in test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="value"/> does not end with
        /// <paramref name="substring"/>.
        /// </exception>
        public static void DoesNotEndWith(this StringAssert assert, string value, string substring, string message, params object?[] parameters)
        {
            assert.DoesNotEndWith(value, substring, message, StringComparison.Ordinal, parameters);
        }

        /// <summary>
        /// Tests whether the specified string ends with the specified substring
        /// and throws an exception if the test string does not end with the
        /// substring.
        /// </summary>
        /// <param name="assert">The <see cref="StringAssert"/> instance to extend.</param>
        /// <param name="value">
        /// The string that is expected to end with <paramref name="substring"/>.
        /// </param>
        /// <param name="substring">
        /// The string expected to be a suffix of <paramref name="value"/>.
        /// </param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="value"/>
        /// does not end with <paramref name="substring"/>. The message is
        /// shown in test results.
        /// </param>
        /// <param name="comparisonType">
        /// The comparison method to compare strings <paramref name="comparisonType"/>.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="value"/> does not end with
        /// <paramref name="substring"/>.
        /// </exception>
        public static void DoesNotEndWith(this StringAssert assert, string value, string substring, string message, StringComparison comparisonType)
        {
            assert.DoesNotEndWith(value, substring, message, comparisonType, Empty);
        }

        /// <summary>
        /// Tests whether the specified string ends with the specified substring
        /// and throws an exception if the test string does not end with the
        /// substring.
        /// </summary>
        /// <param name="assert">The <see cref="StringAssert"/> instance to extend.</param>
        /// <param name="value">
        /// The string that is expected to end with <paramref name="substring"/>.
        /// </param>
        /// <param name="substring">
        /// The string expected to be a suffix of <paramref name="value"/>.
        /// </param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="value"/>
        /// does not end with <paramref name="substring"/>. The message is
        /// shown in test results.
        /// </param>
        /// <param name="comparisonType">
        /// The comparison method to compare strings <paramref name="comparisonType"/>.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="value"/> does not end with
        /// <paramref name="substring"/>.
        /// </exception>
        public static void DoesNotEndWith(this StringAssert assert, string value, string substring, string message, StringComparison comparisonType, params object?[] parameters)
        {
            Helpers.CheckParameterNotNull(value, "StringAssert.DoesNotEndWith", "value", String.Empty);
            Helpers.CheckParameterNotNull(substring, "StringAssert.DoesNotEndWith", "substring", String.Empty);
            if (value.EndsWith(substring, comparisonType))
            {
                var finalMessage = String.Format(CultureInfo.CurrentCulture, Strings.DoesNotEndWithFail, value, substring, message);
                Helpers.HandleFail("StringAssert.DoesNotEndWith", finalMessage, parameters);
            }
        }
    }
}
