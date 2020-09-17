using System;

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
        /// Verifies that the value is <see cref="Double.NaN"/>.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to test.</param>
        public static void IsNaN(this Assert assert, double value)
        {
            assert.IsNaN(value, Strings.Assertion_GenericFailure);
        }

        /// <summary>
        /// Verifies that the value is <see cref="Double.NaN"/>.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        public static void IsNaN(this Assert assert, double value, string message)
        {
            assert.IsNaN(value, message, Double.NaN, value);
        }

        /// <summary>
        /// Verifies that the value is <see cref="Double.NaN"/>.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsNaN(this Assert assert, double value, string message, params object[] parameters)
        {
            if (!Double.IsNaN(value))
            {
                Helpers.HandleFail("Assert.IsNaN", message, parameters);
            }
        }

        /// <summary>
        /// Verifies that the value is <see cref="Single.NaN"/>.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to test.</param>
        public static void IsNaN(this Assert assert, float value)
        {
            assert.IsNaN(value, Strings.Assertion_GenericFailure);
        }

        /// <summary>
        /// Verifies that the value is <see cref="Single.NaN"/>.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        public static void IsNaN(this Assert assert, float value, string message)
        {
            assert.IsNaN(value, message, Single.NaN, value);
        }

        /// <summary>
        /// Verifies that the value is <see cref="Single.NaN"/>.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsNaN(this Assert assert, float value, string message, params object[] parameters)
        {
            if (!Single.IsNaN(value))
            {
                Helpers.HandleFail("Assert.IsNaN", message, parameters);
            }
        }
    }
}
