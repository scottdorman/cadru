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
        /// Asserts that a string is empty.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to be tested.</param>
        [Obsolete("Use StringAssert.That.IsEmpty", error: true)]
        public static void IsEmpty(this Assert assert, string? value) => StringAssert.That.IsEmpty(value);

        /// <summary>
        /// Asserts that a string is empty.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        [Obsolete("Use StringAssert.That.IsEmpty", error: true)]
        public static void IsEmpty(this Assert assert, string? value, string message) => StringAssert.That.IsEmpty(value, message);

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
        [Obsolete("Use StringAssert.That.IsEmpty", error: true)]
        public static void IsEmpty(this Assert assert, string? value, string message, params object?[] parameters) => StringAssert.That.IsEmpty(value, message, parameters);

        /// <summary>
        /// Asserts that a string is not empty.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to be tested.</param>
        [Obsolete("Use StringAssert.That.IsNotEmpty", error: true)]
        public static void IsNotEmpty(this Assert assert, string? value) => StringAssert.That.IsNotEmpty(value);

        /// <summary>
        /// Asserts that a string is not empty.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        [Obsolete("Use StringAssert.That.IsNotEmpty", error: true)]
        public static void IsNotEmpty(this Assert assert, string? value, string message) => StringAssert.That.IsNotEmpty(value, message);

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
        [Obsolete("Use StringAssert.That.IsNotEmpty", error: true)]
        public static void IsNotEmpty(this Assert assert, string? value, string message, params object?[] parameters) => StringAssert.That.IsNotEmpty(value, message, parameters);
    }
}
