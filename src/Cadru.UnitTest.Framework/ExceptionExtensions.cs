//------------------------------------------------------------------------------
// <copyright file="ExceptionExtensions.cs"
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
using System.Diagnostics.CodeAnalysis;

using Cadru.UnitTest.Framework.Resources;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.UnitTest.Framework
{
    /// <summary>
    /// Contains extensions for working with Exceptions in MSTest.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Verifies that <see cref="Exception.InnerException"/>
        /// is of the specified type.
        /// </summary>
        /// <typeparam name="T">The source exception type.</typeparam>
        /// <param name="exception">
        /// The exception whose <see cref="Exception.InnerException"/> will be tested.
        /// </param>
        /// <param name="innerException">The expected type of the inner exception.</param>
        /// <returns>The original <see cref="Exception"/>.</returns>
        public static T WithInnerException<T>(this T exception, Type innerException) where T : Exception
        {
            Assert.IsNotNull(exception.InnerException);
            Assert.That.IsType(exception.InnerException!, innerException, Strings.WrongExceptionThrown, innerException.Name, typeof(T).Name, exception.Message, exception.StackTrace);
            return exception;
        }

        /// <summary>
        /// Verifies that <see cref="Exception.InnerException"/>
        /// is of the specified type.
        /// </summary>
        /// <typeparam name="T">The inner exception type.</typeparam>
        /// <param name="exception">
        /// The exception whose <see cref="Exception.InnerException"/> will be tested.
        /// </param>
        /// <returns>The original <see cref="Exception"/>.</returns>
        public static Exception WithInnerException<T>(this Exception exception)
            where T : Exception
        {
            Assert.IsNotNull(exception.InnerException);
            Assert.That.IsType<T>(exception.InnerException!, Strings.WrongExceptionThrown, exception.InnerException!.GetType().Name, typeof(T).Name, exception.Message, exception.StackTrace);
            return exception;
        }

        /// <summary>
        /// Verifies that the exception <see cref="Exception.Message"/> contains
        /// the given text.
        /// </summary>
        /// <typeparam name="T">The exception type.</typeparam>
        /// <param name="exception">
        /// The exception whose <see cref="Exception.Message"/> will be searched.
        /// </param>
        /// <param name="message">The text to search for in the exception <see cref="Exception.Message"/>.</param>
        /// <param name="comparison">
        /// One of the <see cref="ExceptionMessageComparison"/> values.
        /// </param>
        /// <returns>The original <see cref="Exception"/>.</returns>
        public static T WithMessage<T>(this T exception, string message, ExceptionMessageComparison comparison) where T : Exception
        {
            Assert.IsNotNull(exception);

            switch (comparison)
            {
                case ExceptionMessageComparison.Exact:
                    Assert.AreEqual(exception.Message, message);
                    break;

                case ExceptionMessageComparison.Contains:
                    Assert.IsTrue(exception.Message.Contains(message));
                    break;

                case ExceptionMessageComparison.StartsWith:
                    Assert.IsTrue(exception.Message.StartsWith(message));
                    break;

                case ExceptionMessageComparison.EndsWith:
                    Assert.IsTrue(exception.Message.EndsWith(message));
                    break;
            }

            return exception;
        }

        /// <summary>
        /// Verifies that the exception <see cref="Exception.Message"/> is equal
        /// to the given text.
        /// </summary>
        /// <typeparam name="T">The exception type.</typeparam>
        /// <param name="exception">
        /// The exception whose <see cref="Exception.Message"/> will be compared.
        /// </param>
        /// <param name="message">The text to compare.</param>
        /// <returns>The original <see cref="Exception"/>.</returns>
        public static T WithMessage<T>(this T exception, string message) where T : Exception
        {
            return exception.WithMessage(message, ExceptionMessageComparison.Exact);
        }

        /// <summary>
        /// Verifies that the exception
        /// <see cref="ArgumentException.ParamName"/> property is equal to the
        /// given text.
        /// </summary>
        /// <typeparam name="T">The exception type.</typeparam>
        /// <param name="exception">
        /// The exception whose <see cref="ArgumentException.ParamName"/>
        /// property will be compared.
        /// </param>
        /// <param name="parameterName">The text to compare.</param>
        /// <returns>The original <see cref="Exception"/>.</returns>
        public static T WithParameter<T>(this T exception, string parameterName) where T : ArgumentException
        {
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.ParamName, parameterName);
            return exception;
        }
    }
}