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
    public static partial class AssertExtensions
    {
        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first byte to compare. This is the byte the tests expects.</param>
        /// <param name="actual">The second byte to compare. This is the byte produced by the code under test.</param>
        public static void IsGreater(this Assert assert, byte expected, byte actual)
        {
            assert.IsGreater(expected, actual, Strings.IsGreaterFailMsg);
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first byte to compare. This is the byte the tests expects.</param>
        /// <param name="actual">The second byte to compare. This is the byte produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        public static void IsGreater(this Assert assert, byte expected, byte actual, string message)
        {
            assert.IsGreater(expected, actual, message, expected.ToString(CultureInfo.CurrentCulture.NumberFormat), actual.ToString(CultureInfo.CurrentCulture.NumberFormat));
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first byte to compare. This is the byte the tests expects.</param>
        /// <param name="actual">The second byte to compare. This is the byte produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsGreater(this Assert assert, byte expected, byte actual, string message, params object[] parameters)
        {
            if (expected <= actual)
            {
                Helpers.HandleFail("Assert.IsGreater", message, parameters);
            }
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first sbyte to compare. This is the sbyte the tests expects.</param>
        /// <param name="actual">The second sbyte to compare. This is the sbyte produced by the code under test.</param>
        public static void IsGreater(this Assert assert, sbyte expected, sbyte actual)
        {
            assert.IsGreater(expected, actual, Strings.IsGreaterFailMsg);
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first sbyte to compare. This is the sbyte the tests expects.</param>
        /// <param name="actual">The second sbyte to compare. This is the sbyte produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        public static void IsGreater(this Assert assert, sbyte expected, sbyte actual, string message)
        {
            assert.IsGreater(expected, actual, message, expected.ToString(CultureInfo.CurrentCulture.NumberFormat), actual.ToString(CultureInfo.CurrentCulture.NumberFormat));
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first sbyte to compare. This is the sbyte the tests expects.</param>
        /// <param name="actual">The second sbyte to compare. This is the sbyte produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsGreater(this Assert assert, sbyte expected, sbyte actual, string message, params object[] parameters)
        {
            if (expected <= actual)
            {
                Helpers.HandleFail("Assert.IsGreater", message, parameters);
            }
        }


        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first char to compare. This is the char the tests expects.</param>
        /// <param name="actual">The second char to compare. This is the char produced by the code under test.</param>
        public static void IsGreater(this Assert assert, char expected, char actual)
        {
            assert.IsGreater(expected, actual, Strings.IsGreaterFailMsg);
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first char to compare. This is the char the tests expects.</param>
        /// <param name="actual">The second char to compare. This is the char produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        public static void IsGreater(this Assert assert, char expected, char actual, string message)
        {
            assert.IsGreater(expected, actual, message, expected.ToString(CultureInfo.CurrentCulture.NumberFormat), actual.ToString(CultureInfo.CurrentCulture.NumberFormat));
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first char to compare. This is the char the tests expects.</param>
        /// <param name="actual">The second char to compare. This is the char produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsGreater(this Assert assert, char expected, char actual, string message, params object[] parameters)
        {
            if (expected <= actual)
            {
                Helpers.HandleFail("Assert.IsGreater", message, parameters);
            }
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first decimal to compare. This is the decimal the tests expects.</param>
        /// <param name="actual">The second decimal to compare. This is the decimal produced by the code under test.</param>
        public static void IsGreater(this Assert assert, decimal expected, decimal actual)
        {
            assert.IsGreater(expected, actual, Strings.IsGreaterFailMsg);
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first decimal to compare. This is the decimal the tests expects.</param>
        /// <param name="actual">The second decimal to compare. This is the decimal produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        public static void IsGreater(this Assert assert, decimal expected, decimal actual, string message)
        {
            assert.IsGreater(expected, actual, message, expected.ToString(CultureInfo.CurrentCulture.NumberFormat), actual.ToString(CultureInfo.CurrentCulture.NumberFormat));
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first decimal to compare. This is the decimal the tests expects.</param>
        /// <param name="actual">The second decimal to compare. This is the decimal produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsGreater(this Assert assert, decimal expected, decimal actual, string message, params object[] parameters)
        {
            if (expected <= actual)
            {
                Helpers.HandleFail("Assert.IsGreater", message, parameters);
            }
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first double to compare. This is the double the tests expects.</param>
        /// <param name="actual">The second double to compare. This is the double produced by the code under test.</param>
        public static void IsGreater(this Assert assert, double expected, double actual)
        {
            assert.IsGreater(expected, actual, Strings.IsGreaterFailMsg);
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first double to compare. This is the double the tests expects.</param>
        /// <param name="actual">The second double to compare. This is the double produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        public static void IsGreater(this Assert assert, double expected, double actual, string message)
        {
            assert.IsGreater(expected, actual, message, expected.ToString(CultureInfo.CurrentCulture.NumberFormat), actual.ToString(CultureInfo.CurrentCulture.NumberFormat));
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first double to compare. This is the double the tests expects.</param>
        /// <param name="actual">The second double to compare. This is the double produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsGreater(this Assert assert, double expected, double actual, string message, params object[] parameters)
        {
            if (Double.IsNaN(expected) || Double.IsNaN(actual) || expected <= actual)
            {
                Helpers.HandleFail("Assert.IsGreater", message, parameters);
            }
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first float to compare. This is the float the tests expects.</param>
        /// <param name="actual">The second float to compare. This is the float produced by the code under test.</param>
        public static void IsGreater(this Assert assert, float expected, float actual)
        {
            assert.IsGreater(expected, actual, Strings.IsGreaterFailMsg);
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first float to compare. This is the float the tests expects.</param>
        /// <param name="actual">The second float to compare. This is the float produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        public static void IsGreater(this Assert assert, float expected, float actual, string message)
        {
            assert.IsGreater(expected, actual, message, expected.ToString(CultureInfo.CurrentCulture.NumberFormat), actual.ToString(CultureInfo.CurrentCulture.NumberFormat));
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first float to compare. This is the float the tests expects.</param>
        /// <param name="actual">The second float to compare. This is the float produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsGreater(this Assert assert, float expected, float actual, string message, params object[] parameters)
        {
            if (Single.IsNaN(expected) || Single.IsNaN(actual) || expected <= actual)
            {
                Helpers.HandleFail("Assert.IsGreater", message, parameters);
            }
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first int to compare. This is the int the tests expects.</param>
        /// <param name="actual">The second int to compare. This is the int produced by the code under test.</param>
        public static void IsGreater(this Assert assert, int expected, int actual)
        {
            assert.IsGreater(expected, actual, Strings.IsGreaterFailMsg);
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first int to compare. This is the int the tests expects.</param>
        /// <param name="actual">The second int to compare. This is the int produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        public static void IsGreater(this Assert assert, int expected, int actual, string message)
        {
            assert.IsGreater(expected, actual, message, expected.ToString(CultureInfo.CurrentCulture.NumberFormat), actual.ToString(CultureInfo.CurrentCulture.NumberFormat));
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first int to compare. This is the int the tests expects.</param>
        /// <param name="actual">The second int to compare. This is the int produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsGreater(this Assert assert, int expected, int actual, string message, params object[] parameters)
        {
            if (expected <= actual)
            {
                Helpers.HandleFail("Assert.IsGreater", message, parameters);
            }
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first uint to compare. This is the uint the tests expects.</param>
        /// <param name="actual">The second uint to compare. This is the uint produced by the code under test.</param>
        public static void IsGreater(this Assert assert, uint expected, uint actual)
        {
            assert.IsGreater(expected, actual, Strings.IsGreaterFailMsg);
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first uint to compare. This is the uint the tests expects.</param>
        /// <param name="actual">The second uint to compare. This is the uint produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        public static void IsGreater(this Assert assert, uint expected, uint actual, string message)
        {
            assert.IsGreater(expected, actual, message, expected.ToString(CultureInfo.CurrentCulture.NumberFormat), actual.ToString(CultureInfo.CurrentCulture.NumberFormat));
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first uint to compare. This is the uint the tests expects.</param>
        /// <param name="actual">The second uint to compare. This is the uint produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsGreater(this Assert assert, uint expected, uint actual, string message, params object[] parameters)
        {
            if (expected <= actual)
            {
                Helpers.HandleFail("Assert.IsGreater", message, parameters);
            }
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first long to compare. This is the long the tests expects.</param>
        /// <param name="actual">The second long to compare. This is the long produced by the code under test.</param>
        public static void IsGreater(this Assert assert, long expected, long actual)
        {
            assert.IsGreater(expected, actual, Strings.IsGreaterFailMsg);
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first long to compare. This is the long the tests expects.</param>
        /// <param name="actual">The second long to compare. This is the long produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        public static void IsGreater(this Assert assert, long expected, long actual, string message)
        {
            assert.IsGreater(expected, actual, message, expected.ToString(CultureInfo.CurrentCulture.NumberFormat), actual.ToString(CultureInfo.CurrentCulture.NumberFormat));
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first long to compare. This is the long the tests expects.</param>
        /// <param name="actual">The second long to compare. This is the long produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsGreater(this Assert assert, long expected, long actual, string message, params object[] parameters)
        {
            if (expected <= actual)
            {
                Helpers.HandleFail("Assert.IsGreater", message, parameters);
            }
        }


        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first ulong to compare. This is the ulong the tests expects.</param>
        /// <param name="actual">The second ulong to compare. This is the ulong produced by the code under test.</param>
        public static void IsGreater(this Assert assert, ulong expected, ulong actual)
        {
            assert.IsGreater(expected, actual, Strings.IsGreaterFailMsg);
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first ulong to compare. This is the ulong the tests expects.</param>
        /// <param name="actual">The second ulong to compare. This is the ulong produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        public static void IsGreater(this Assert assert, ulong expected, ulong actual, string message)
        {
            assert.IsGreater(expected, actual, message, expected.ToString(CultureInfo.CurrentCulture.NumberFormat), actual.ToString(CultureInfo.CurrentCulture.NumberFormat));
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first ulong to compare. This is the ulong the tests expects.</param>
        /// <param name="actual">The second ulong to compare. This is the ulong produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsGreater(this Assert assert, ulong expected, ulong actual, string message, params object[] parameters)
        {
            if (expected <= actual)
            {
                Helpers.HandleFail("Assert.IsGreater", message, parameters);
            }
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first short to compare. This is the short the tests expects.</param>
        /// <param name="actual">The second short to compare. This is the short produced by the code under test.</param>
        public static void IsGreater(this Assert assert, short expected, short actual)
        {
            assert.IsGreater(expected, actual, Strings.IsGreaterFailMsg);
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first short to compare. This is the short the tests expects.</param>
        /// <param name="actual">The second short to compare. This is the short produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        public static void IsGreater(this Assert assert, short expected, short actual, string message)
        {
            assert.IsGreater(expected, actual, message, expected.ToString(CultureInfo.CurrentCulture.NumberFormat), actual.ToString(CultureInfo.CurrentCulture.NumberFormat));
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first short to compare. This is the short the tests expects.</param>
        /// <param name="actual">The second short to compare. This is the short produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsGreater(this Assert assert, short expected, short actual, string message, params object[] parameters)
        {
            if (expected <= actual)
            {
                Helpers.HandleFail("Assert.IsGreater", message, parameters);
            }
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first ushort to compare. This is the ushort the tests expects.</param>
        /// <param name="actual">The second ushort to compare. This is the ushort produced by the code under test.</param>
        public static void IsGreater(this Assert assert, ushort expected, ushort actual)
        {
            assert.IsGreater(expected, actual, Strings.IsGreaterFailMsg);
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first ushort to compare. This is the ushort the tests expects.</param>
        /// <param name="actual">The second ushort to compare. This is the ushort produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        public static void IsGreater(this Assert assert, ushort expected, ushort actual, string message)
        {
            assert.IsGreater(expected, actual, message, expected.ToString(CultureInfo.CurrentCulture.NumberFormat), actual.ToString(CultureInfo.CurrentCulture.NumberFormat));
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first ushort to compare. This is the ushort the tests expects.</param>
        /// <param name="actual">The second ushort to compare. This is the ushort produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsGreater(this Assert assert, ushort expected, ushort actual, string message, params object[] parameters)
        {
            if (expected <= actual)
            {
                Helpers.HandleFail("Assert.IsGreater", message, parameters);
            }
        }


        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first <typeparamref name="T"/> to compare. This is the <typeparamref name="T"/> the tests expects.</param>
        /// <param name="actual">The second <typeparamref name="T"/> to compare. This is the <typeparamref name="T"/> produced by the code under test.</param>
        public static void IsGreater<T>(this Assert assert, T expected, T actual) where T : IComparable
        {
            assert.IsGreater(expected, actual, Strings.IsGreaterFailMsg);
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first <typeparamref name="T"/> to compare. This is the <typeparamref name="T"/> the tests expects.</param>
        /// <param name="actual">The second <typeparamref name="T"/> to compare. This is the <typeparamref name="T"/> produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        public static void IsGreater<T>(this Assert assert, T expected, T actual, string message) where T : IComparable
        {
            assert.IsGreater(expected, actual, message, expected, actual);
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="expected">The first <typeparamref name="T"/> to compare. This is the <typeparamref name="T"/> the tests expects.</param>
        /// <param name="actual">The second <typeparamref name="T"/> to compare. This is the <typeparamref name="T"/> produced by the code under test.</param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="actual"/>
        /// is less than <paramref name="expected"/>. The message is shown in test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsGreater<T>(this Assert assert, T expected, T actual, string message, params object[] parameters) where T : IComparable
        {
            if (expected.CompareTo(actual) <= 0)
            {
                Helpers.HandleFail("Assert.IsGreater", message, parameters);
            }
        }
    }
}
