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
        [Obsolete("Use Assert.That.IsGreater<T>(T, T).")]
        public static void Greater<T>(T arg1, T arg2) where T : IComparable
        {
            Assert.That.IsGreater(arg1, arg2);
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
        [Obsolete("Use Assert.That.IsGreater<T>(T, T, string).")]
        public static void Greater<T>(T arg1, T arg2, string message) where T : IComparable
        {
            Assert.That.IsGreater(arg1, arg2, message);
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
        [Obsolete("Use Assert.That.IsGreater<T>(T, T, string, object[].")]
        public static void Greater<T>(T arg1, T arg2, string message, params object[] parameters) where T : IComparable
        {
            Assert.That.IsGreater(arg1, arg2, message, parameters);

        }

        /// <summary>
        /// Verifies that the first value is greater than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be greater.</param>
        /// <param name="arg2">The second value, expected to be less.</param>
        [Obsolete("Use Assert.That.GreaterOrEqual<T>(T, T)")]
        public static void IsGreaterOrEqual<T>(T arg1, T arg2) where T : IComparable
        {
            Assert.That.IsGreaterOrEqual(arg1, arg2);
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
        [Obsolete("Use Assert.That.IsGreaterOrEqual<T>(T, T, string)")]
        public static void GreaterOrEqual<T>(T arg1, T arg2, string message) where T : IComparable
        {
            Assert.That.IsGreaterOrEqual(arg1, arg2, message);
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
        [Obsolete("Use Assert.That.IsGreaterOrEqual<T>(T, T, string, object[])")]
        public static void GreaterOrEqual<T>(T arg1, T arg2, string message, params object[] parameters) where T : IComparable
        {
            Assert.That.IsGreaterOrEqual(arg1, arg2, message, parameters);
        }

        /// <summary>
        /// Verifies that the specified condition is <see langword="false"/>.
        /// The assertion fails if the condition is <see langword="true"/>.
        /// </summary>
        /// <param name="condition">The condition to verify is <see langword="false"/>.</param>
        /// <exception cref="Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">
        /// <paramref name="condition"/> evaluates to <see langword="true"/>.
        /// </exception>
        [Obsolete("Use Assert.That.IsFalse(bool?)")]
        public static void IsFalse(bool? condition)
        {
            Assert.That.IsFalse(condition);
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
        [Obsolete("Use Assert.That.IsFalse(bool?, string)")]
        public static void IsFalse(bool? condition, string message)
        {
            Assert.That.IsFalse(condition, message);
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
        [Obsolete("Use Assert.That.IsFalse(bool?, string, object[])")]
        public static void IsFalse(bool? condition, string message, params object[] parameters)
        {
            Assert.That.IsFalse(condition, message, parameters);
        }

        /// <summary>
        /// Verifies that the value is <see cref="Double.NaN"/>.
        /// </summary>
        /// <param name="value">The value to test.</param>
        [Obsolete("Use Assert.That.IsNan(double)")]
        public static void IsNaN(double value)
        {
            Assert.That.IsNaN(value);
        }

        /// <summary>
        /// Verifies that the value is <see cref="Double.NaN"/>.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        [Obsolete("Use Assert.That.IsNan(double, string)")]
        public static void IsNaN(double value, string message)
        {
            Assert.That.IsNaN(value, message);
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
        [Obsolete("Use Assert.That.IsNan(double, string, object[])")]
        public static void IsNaN(double value, string message, params object[] parameters)
        {
            Assert.That.IsNaN(value, message, parameters);
        }

        /// <summary>
        /// Verifies that the specified condition is <see langword="true"/>. The
        /// assertion fails if the condition is <see langword="false"/>.
        /// </summary>
        /// <param name="condition">The condition to verify is <see langword="true"/>.</param>
        /// <exception cref="Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">
        /// <paramref name="condition"/> evaluates to <see langword="false"/>.
        /// </exception>
        [Obsolete("Use Assert.That.IsTrue(bool?)")]
        public static void IsTrue(bool? condition)
        {
            Assert.That.IsTrue(condition);
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
        [Obsolete("Use Assert.That.IsTrue(bool?, string)")]
        public static void IsTrue(bool? condition, string message)
        {
            Assert.That.IsTrue(condition, message);
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
        [Obsolete("Use Assert.That.IsTrue(bool?, string, object[])")]
        public static void IsTrue(bool? condition, string message, params object[] parameters)
        {
            Assert.That.IsTrue(condition, message, parameters);
        }

        /// <summary>
        /// Verifies that the first value is less than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        [Obsolete("Use Assert.That.IsLess<T>(T, T).")]
        public static void Less<T>(T arg1, T arg2) where T : IComparable
        {
            Assert.That.IsLess(arg1, arg2);
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
        [Obsolete("Use Assert.That.IsLess<T>(T, T, string).")]
        public static void Less<T>(T arg1, T arg2, string message) where T : IComparable
        {
            Assert.That.IsLess(arg1, arg2, message);
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
        [Obsolete("Use Assert.That.IsLess<T>(T, T, string, object[]).")]
        public static void Less<T>(T arg1, T arg2, string message, params object[] parameters) where T : IComparable
        {
            Assert.That.IsLess(arg1, arg2, message, parameters);
        }

        /// <summary>
        /// Verifies that the first value is less than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        [Obsolete("Use Assert.That.IsLessOrEqual<T>(T, T).")]
        public static void LessOrEqual<T>(T arg1, T arg2) where T : IComparable
        {
            Assert.That.IsLessOrEqual(arg1, arg2);
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
        [Obsolete("Use Assert.That.IsLessOrEqual<T>(T, T, string).")]
        public static void LessOrEqual<T>(T arg1, T arg2, string message) where T : IComparable
        {
            Assert.That.IsLessOrEqual(arg1, arg2, message);
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
        [Obsolete("Use Assert.That.IsLessOrEqual<T>(T, T, string, object[]).")]
        public static void LessOrEqual<T>(T arg1, T arg2, string message, params object[] parameters) where T : IComparable
        {
            Assert.That.IsLessOrEqual(arg1, arg2, message, parameters);
        }
    }
}