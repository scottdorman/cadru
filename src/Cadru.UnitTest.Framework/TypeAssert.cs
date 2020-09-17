//------------------------------------------------------------------------------
// <copyright file="TypeAssert.cs"
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
    public static class TypeAssert
    {
        /// <summary>
        /// Asserts that an object may be assigned a value of a given <see cref="Type"/>.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="expectedType">The expected <see cref="Type"/>.</param>
        [Obsolete("Use Assert.That.IsAssignableFrom<T>(object).")]
        public static void IsAssignableFrom(object value, Type expectedType)
        {
            Assert.That.IsAssignableFrom(value, expectedType, Strings.Assertion_ExpectedToBeAssignableFrom, value, expectedType);
        }

        /// <summary>
        /// Asserts that an object may be assigned a value of a given <see cref="Type"/>.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="expectedType">The expected <see cref="Type"/>.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        [Obsolete("Use Assert.That.IsAssignableFrom<T>(object, string).")]
        public static void IsAssignableFrom(object value, Type expectedType, string message)
        {
            Assert.That.IsAssignableFrom(value, expectedType, message, value, expectedType);
        }

        /// <summary>
        /// Asserts that an object may be assigned a value of a given <see cref="Type"/>.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="expectedType">The expected <see cref="Type"/>.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        [Obsolete("Use Assert.That.IsAssignableFrom<T>(object, string, object[]).")]
        public static void IsAssignableFrom(object value, Type expectedType, string message, params object[] parameters)
        {
            Assert.That.IsAssignableFrom(value, expectedType, message, parameters);
        }

        /// <summary>
        /// Asserts that an object may not be assigned a value of a given <see cref="Type"/>.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="expectedType">The expected <see cref="Type"/>.</param>
        [Obsolete("Use Assert.That.IsNotAssignableFrom<T>(object).")]
        public static void IsNotAssignableFrom(object value, Type expectedType)
        {
            Assert.That.IsNotAssignableFrom(value, expectedType, Strings.Assertion_ExpectedToBeAssignableFrom, value, expectedType);
        }

        /// <summary>
        /// Asserts that an object may not be assigned a value of a given <see cref="Type"/>.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="expectedType">The expected <see cref="Type"/>.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        [Obsolete("Use Assert.That.IsNotAssignableFrom<T>(object, string).")]
        public static void IsNotAssignableFrom(object value, Type expectedType, string message)
        {
            Assert.That.IsNotAssignableFrom(value, expectedType, message, value, expectedType);
        }

        /// <summary>
        /// Asserts that an object may not be assigned a value of a given <see cref="Type"/>.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="expectedType">The expected <see cref="Type"/>.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        [Obsolete("Use Assert.That.IsNotAssignableFrom<T>(object, string, object[]).")]
        public static void IsNotAssignableFrom(object value, Type expectedType, string message, params object[] parameters)
        {
            Assert.That.IsNotAssignableFrom(value, expectedType, message, parameters);
        }

        /// <summary>
        /// Asserts that an object is of the given <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="T">The expected <see cref="Type"/>.</typeparam>
        /// <param name="value">The value to be tested.</param>
        [Obsolete("Use Assert.That.IsType<T>(object).")]
        public static void IsType<T>(object value)
        {
            Assert.That.IsType<T>(value);
        }

        /// <summary>
        /// Asserts that an object is of the given <see cref="Type"/>.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="expectedType">The expected <see cref="Type"/>.</param>
        [Obsolete("Use Assert.That.IsType<T>(object, string).")]
        public static void IsType(object value, Type expectedType)
        {
            Assert.That.IsType(value, expectedType, Strings.Assertion_WrongType, expectedType, value?.GetType()!);
        }

        /// <summary>
        /// Asserts that an object is of the given <see cref="Type"/>.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="expectedType">The expected <see cref="Type"/>.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        [Obsolete("Use Assert.That.IsType<T>(object, string).")]
        public static void IsType(object value, Type expectedType, string message)
        {
            Assert.That.IsType(value, expectedType, message, expectedType, value?.GetType()!);
        }

        /// <summary>
        /// Asserts that an object is of the given <see cref="Type"/>.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="expectedType">The expected <see cref="Type"/>.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        [Obsolete("Use Assert.That.IsType<T>(object, string, object[]).")]
        public static void IsType(object value, Type expectedType, string message, params object[] parameters)
        {
            Assert.That.IsType(value, expectedType, message, parameters);
        }
    }
}