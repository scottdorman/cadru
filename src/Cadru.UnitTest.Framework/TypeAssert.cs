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
using System.Reflection;

using Cadru.Contracts;
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
        public static void IsAssignableFrom(object value, Type expectedType)
        {
            IsAssignableFrom(value, expectedType, Strings.Assertion_ExpectedToBeAssignableFrom, value, expectedType);
        }

        /// <summary>
        /// Asserts that an object may be assigned a value of a given <see cref="Type"/>.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="expectedType">The expected <see cref="Type"/>.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        public static void IsAssignableFrom(object value, Type expectedType, string message)
        {
            IsAssignableFrom(value, expectedType, message, null);
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
        public static void IsAssignableFrom(object value, Type expectedType, string message, params object[] parameters)
        {
            if (!value.GetType().GetTypeInfo().IsAssignableFrom(expectedType.GetTypeInfo()))
            {
                Assert.Fail(message, parameters);
            }
        }

        /// <summary>
        /// Asserts that an object may not be assigned a value of a given <see cref="Type"/>.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="expectedType">The expected <see cref="Type"/>.</param>
        public static void IsNotAssignableFrom(object value, Type expectedType)
        {
            IsNotAssignableFrom(value, expectedType, Strings.Assertion_ExpectedToBeAssignableFrom, value, expectedType);
        }

        /// <summary>
        /// Asserts that an object may not be assigned a value of a given <see cref="Type"/>.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="expectedType">The expected <see cref="Type"/>.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        public static void IsNotAssignableFrom(object value, Type expectedType, string message)
        {
            IsNotAssignableFrom(value, expectedType, message, null);
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
        public static void IsNotAssignableFrom(object value, Type expectedType, string message, params object[] parameters)
        {
            Requires.NotNull(value, "value");
            if (value.GetType().GetTypeInfo().IsAssignableFrom(expectedType.GetTypeInfo()))
            {
                Assert.Fail(message, parameters);
            }
        }

        /// <summary>
        /// Asserts that an object is of the given <see cref="Type"/>.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="expectedType">The expected <see cref="Type"/>.</param>
        public static void IsType(object value, Type expectedType)
        {
            Requires.NotNull(value, "value");
            IsType(value, expectedType, Strings.Assertion_WrongType, expectedType, value.GetType());
        }

        /// <summary>
        /// Asserts that an object is of the given <see cref="Type"/>.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="expectedType">The expected <see cref="Type"/>.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        public static void IsType(object value, Type expectedType, string message)
        {
            IsType(value, expectedType, message, null);
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
        public static void IsType(object value, Type expectedType, string message, params object[] parameters)
        {
            Requires.NotNull(value, "value");
            var actualType = value.GetType();
            if (actualType != expectedType)
            {
                if (String.IsNullOrWhiteSpace(message))
                {
                    Assert.Fail(Strings.Assertion_WrongType, expectedType, actualType);
                }
                else
                {
                    Assert.Fail(message, parameters);
                }
            }
        }
    }
}