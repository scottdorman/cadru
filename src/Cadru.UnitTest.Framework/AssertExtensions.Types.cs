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
        /// Asserts that an object is of the given <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="T">The expected <see cref="Type"/>.</typeparam>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to be tested.</param>
        public static void IsType<T>(this Assert assert, object value)
        {
            assert.IsType<T>(value, Strings.Assertion_WrongType);
        }

        /// <summary>
        /// Asserts that an object is of the given <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="T">The expected <see cref="Type"/>.</typeparam>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        public static void IsType<T>(this Assert assert, object value, string message)
        {
            assert.IsType<T>(value, message, typeof(T), value.GetType());
        }

        /// <summary>
        /// Asserts that an object is of the given <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="T">The expected <see cref="Type"/>.</typeparam>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsType<T>(this Assert assert, object value, string message, params object?[] parameters)
        {
            assert.IsType(value, typeof(T), message, parameters);
        }

        internal static void IsType(this Assert assert, object value, Type expectedType, string message, params object?[] parameters)
        {
            if (value?.GetType() != expectedType)
            {
                Helpers.HandleFail("Assert.IsType", message, parameters);
            }
        }

        /// <summary>
        /// Asserts that an object may be assigned a value of a given <see cref="Type"/>.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to be tested.</param>
        public static void IsAssignableFrom<T>(this Assert assert, object value)
        {
            assert.IsAssignableFrom<T>(value, Strings.Assertion_ExpectedToBeAssignableFrom);
        }

        /// <summary>
        /// Asserts that an object may be assigned a value of a given <see cref="Type"/>.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        public static void IsAssignableFrom<T>(this Assert assert, object value, string message)
        {
            assert.IsAssignableFrom<T>(value, message, value.GetType(), typeof(T));
        }

        /// <summary>
        /// Asserts that an object may be assigned a value of a given <see cref="Type"/>.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsAssignableFrom<T>(this Assert assert, object value, string message, params object?[] parameters)
        {
            assert.IsAssignableFrom(value, typeof(T), message, parameters);
        }

        internal static void IsAssignableFrom(this Assert assert, object value, Type expectedType, string message, params object?[] parameters)
        {
            if (value == null || !value.GetType().IsAssignableFrom(expectedType))
            {
                Helpers.HandleFail("Assert.IsAssignableFrom", message, parameters);
            }
        }


        /// <summary>
        /// Asserts that an object may not be assigned a value of a given <see cref="Type"/>.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to be tested.</param>
        public static void IsNotAssignableFrom<T>(this Assert assert, object value)
        {
            assert.IsNotAssignableFrom<T>(value, Strings.Assertion_ExpectedToBeAssignableFrom);
        }

        /// <summary>
        /// Asserts that an object may not be assigned a value of a given <see cref="Type"/>.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        public static void IsNotAssignableFrom<T>(this Assert assert, object value, string message)
        {
            assert.IsNotAssignableFrom<T>(value, message, value.GetType(), typeof(T));
        }

        /// <summary>
        /// Asserts that an object may not be assigned a value of a given <see cref="Type"/>.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsNotAssignableFrom<T>(this Assert assert, object value, string message, params object?[] parameters)
        {
            assert.IsNotAssignableFrom(value, typeof(T), message, parameters);
        }

        internal static void IsNotAssignableFrom(this Assert assert, object value, Type expectedType, string message, params object?[] parameters)
        {
            if (value == null || value.GetType().IsAssignableFrom(expectedType))
            {
                Helpers.HandleFail("Assert.IsNotAssignableFrom", message, parameters);
            }
        }
    }
}
