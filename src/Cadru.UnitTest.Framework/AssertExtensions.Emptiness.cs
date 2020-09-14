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
        /// Asserts that a string is empty.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to be tested.</param>
        public static void IsEmpty(this Assert assert, string? value)
        {
            assert.IsEmpty(value, Strings.IsEmptyStringFailMsg);
        }

        /// <summary>
        /// Asserts that a string is empty.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        public static void IsEmpty(this Assert assert, string? value, string message)
        {
            assert.IsEmpty(value, message, value?.Length!);
        }

        /// <summary>
        /// Asserts that a string is empty.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsEmpty(this Assert assert, string? value, string message, params object[] parameters)
        {
            if (value == null)
            {
                Helpers.HandleFail("Assert.IsEmpty", Strings.IsEmptyStringFailOnNullMsg);
            }

            if (value!.Length != 0)
            {
                Helpers.HandleFail("Assert.IsEmpty", message, parameters);
            }
        }

        /// <summary>
        /// Asserts that a string is not empty.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to be tested.</param>
        public static void IsNotEmpty(this Assert assert, string? value)
        {
            assert.IsNotEmpty(value, Strings.IsNotEmptyStringFailMsg);
        }

        /// <summary>
        /// Asserts that a string is not empty.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        public static void IsNotEmpty(this Assert assert, string? value, string message)
        {
            assert.IsNotEmpty(value, message, Array.Empty<object>());
        }

        /// <summary>
        /// Asserts that a string is not empty.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsNotEmpty(this Assert assert, string? value, string message, params object[] parameters)
        {
            if (value == null)
            {
                Helpers.HandleFail("Assert.IsNotEmpty", Strings.IsNotEmptyStringFailOnNullMsg);
            }

            if (value?.Length == 0)
            {
                Helpers.HandleFail("Assert.IsNotEmpty", message, parameters);
            }
        }
    }
}
