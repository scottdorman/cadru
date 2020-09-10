//------------------------------------------------------------------------------
// <copyright file="ConditionAssert.cs"
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

using Cadru.UnitTest.Framework.Resources;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.UnitTest.Framework
{
    /// <summary>
    /// Contains assertion types that are not provided with the standard MSTest assertions.
    /// </summary>
    public static class ConditionAssert
    {
        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be greater.</param>
        /// <param name="arg2">The second value, expected to be less.</param>
        public static void Greater<T>(T arg1, T arg2) where T : IComparable
        {
            Greater(arg1, arg2, Strings.Assertion_IsComparisonOrEqualTo, arg2, "less", arg1);
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be greater.</param>
        /// <param name="arg2">The second value, expected to be less.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        public static void Greater<T>(T arg1, T arg2, string message) where T : IComparable
        {
            Greater(arg1, arg2, message, null);
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be greater.</param>
        /// <param name="arg2">The second value, expected to be less.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void Greater<T>(T arg1, T arg2, string message, params object[] parameters) where T : IComparable
        {
            if (((IComparable)arg1).CompareTo(arg2) <= 0)
            {
                Assert.Fail(message, parameters);
            }
        }

        /// <summary>
        /// Verifies that the first value is greater than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be greater.</param>
        /// <param name="arg2">The second value, expected to be less.</param>
        public static void GreaterOrEqual<T>(T arg1, T arg2) where T : IComparable
        {
            GreaterOrEqual(arg1, arg2, Strings.Assertion_IsComparison, arg2, "less", arg1);
        }

        /// <summary>
        /// Verifies that the first value is greater than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be greater.</param>
        /// <param name="arg2">The second value, expected to be less.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        public static void GreaterOrEqual<T>(T arg1, T arg2, string message) where T : IComparable
        {
            GreaterOrEqual(arg1, arg2, message, null);
        }

        /// <summary>
        /// Verifies that the first value is greater than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be greater.</param>
        /// <param name="arg2">The second value, expected to be less.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void GreaterOrEqual<T>(T arg1, T arg2, string message, params object[] parameters) where T : IComparable
        {
            if (((IComparable)arg1).CompareTo(arg2) < 0)
            {
                Assert.Fail(message, parameters);
            }
        }

        /// <summary>
        /// Verifies that the specified condition is <see langword="false"/>.
        /// The assertion fails if the condition is <see langword="true"/>.
        /// </summary>
        /// <param name="condition">The condition to verify is <see langword="false"/>.</param>
        /// <exception cref="Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">
        /// <paramref name="condition"/> evaluates to <see langword="true"/>.
        /// </exception>
        public static void IsFalse(bool? condition)
        {
            IsFalse(condition, Strings.Assertion_GenericFailure, true, condition);
        }

        /// <summary>
        /// Verifies that the specified condition is <see langword="false"/>.
        /// The assertion fails if the condition is <see langword="true"/>.
        /// Displays a message if the assertion fails.
        /// </summary>
        /// <param name="condition">The condition to verify is <see langword="false"/>.</param>
        /// <param name="message">
        /// A message to display if the assertion fails. This message can be
        /// seen in the unit test results.
        /// </param>
        /// <exception cref="Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">
        /// <paramref name="condition"/> evaluates to <see langword="true"/>.
        /// </exception>
        public static void IsFalse(bool? condition, string message)
        {
            IsFalse(condition, message, null);
        }

        /// <summary>
        /// Verifies that the specified condition is <see langword="false"/>.
        /// The assertion fails if the condition is <see langword="true"/>.
        /// Displays a message if the assertion fails, and applies the specified
        /// formatting to it.
        /// </summary>
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
        public static void IsFalse(bool? condition, string message, params object[] parameters)
        {
            if (!condition.HasValue || condition.Value)
            {
                Assert.Fail(message, parameters);
            }
        }

        /// <summary>
        /// Verifies that the value is <see cref="Double.NaN"/>.
        /// </summary>
        /// <param name="value">The value to test.</param>
        public static void IsNaN(double value)
        {
            IsNaN(value, Strings.Assertion_GenericFailure, Double.NaN, value);
        }

        /// <summary>
        /// Verifies that the value is <see cref="Double.NaN"/>.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        public static void IsNaN(double value, string message)
        {
            IsNaN(value, message, null);
        }

        /// <summary>
        /// Verifies that the value is <see cref="Double.NaN"/>.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsNaN(double value, string message, params object[] parameters)
        {
            if (!Double.IsNaN(value))
            {
                Assert.Fail(message, parameters);
            }
        }

        /// <summary>
        /// Verifies that the specified condition is <see langword="true"/>. The
        /// assertion fails if the condition is <see langword="false"/>.
        /// </summary>
        /// <param name="condition">The condition to verify is <see langword="true"/>.</param>
        /// <exception cref="Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">
        /// <paramref name="condition"/> evaluates to <see langword="false"/>.
        /// </exception>
        public static void IsTrue(bool? condition)
        {
            IsTrue(condition, Strings.Assertion_GenericFailure, true, condition);
        }

        /// <summary>
        /// Verifies that the specified condition is <see langword="true"/>. The
        /// assertion fails if the condition is <see langword="false"/>.
        /// Displays a message if the assertion fails.
        /// </summary>
        /// <param name="condition">The condition to verify is <see langword="true"/>.</param>
        /// <param name="message">
        /// A message to display if the assertion fails. This message can be
        /// seen in the unit test results.
        /// </param>
        /// <exception cref="Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">
        /// <paramref name="condition"/> evaluates to <see langword="false"/>.
        /// </exception>
        public static void IsTrue(bool? condition, string message)
        {
            IsTrue(condition, message, null);
        }

        /// <summary>
        /// Verifies that the specified condition is <see langword="true"/>. The
        /// assertion fails if the condition is <see langword="false"/>.
        /// Displays a message if the assertion fails, and applies the specified
        /// formatting to it.
        /// </summary>
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
        public static void IsTrue(bool? condition, string message, params object[] parameters)
        {
            if (!condition.HasValue || !condition.Value)
            {
                Assert.Fail(message, parameters);
            }
        }

        /// <summary>
        /// Verifies that the first value is less than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        public static void Less<T>(T arg1, T arg2) where T : IComparable
        {
            Less(arg1, arg2, Strings.Assertion_IsComparisonOrEqualTo, arg2, "greater", arg1);
        }

        /// <summary>
        /// Verifies that the first value is less than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        public static void Less<T>(T arg1, T arg2, string message) where T : IComparable
        {
            Less(arg1, arg2, message, null);
        }

        /// <summary>
        /// Verifies that the first value is less than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void Less<T>(T arg1, T arg2, string message, params object[] parameters) where T : IComparable
        {
            if (((IComparable)arg1).CompareTo(arg2) >= 0)
            {
                Assert.Fail(message, parameters);
            }
        }

        /// <summary>
        /// Verifies that the first value is less than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        public static void LessOrEqual<T>(T arg1, T arg2) where T : IComparable
        {
            LessOrEqual(arg1, arg2, Strings.Assertion_IsComparison, arg2, "greater", arg1);
        }

        /// <summary>
        /// Verifies that the first value is less than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        public static void LessOrEqual<T>(T arg1, T arg2, string message) where T : IComparable
        {
            LessOrEqual(arg1, arg2, message, null);
        }

        /// <summary>
        /// Verifies that the first value is less than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void LessOrEqual<T>(T arg1, T arg2, string message, params object[] parameters) where T : IComparable
        {
            if (((IComparable)arg1).CompareTo(arg2) > 0)
            {
                Assert.Fail(message, parameters);
            }
        }
    }
}