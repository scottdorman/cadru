//------------------------------------------------------------------------------
// <copyright file="ExceptionAssert.cs" 
//  company="Scott Dorman" 
//  library="Cadru">
//    Copyright (C) 2001-2014 Scott Dorman.
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

namespace Cadru.UnitTest.Framework
{
    using System;
    using Cadru.UnitTest.Framework.Properties;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Contains assertion types that are not provided with the standard MSTest assertions.
    /// </summary>
    public static class ExceptionAssert
    {
        #region fields
        #endregion

        #region constructors
        #endregion

        #region events
        #endregion

        #region properties
        #endregion

        #region methods

        #region Throws

        #region Throws(Action code, string message, params object[] parameters)
        /// <summary>
        /// Verifies that <paramref name="code"/> throws an exception when called.
        /// </summary>
        /// <param name="code">The code to test.</param>
        /// <param name="message">The message that will be displayed on failure.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <returns>The <see cref="Exception"/> thrown by <paramref name="code"/>.</returns>
        public static Exception Throws(Action code, string message, params object[] parameters)
        {
            return Throws(typeof(Exception), code, message, parameters);
        }
        #endregion

        #region Throws(Action code, string message)
        /// <summary>
        /// Verifies that <paramref name="code"/> throws an exception when called.
        /// </summary>
        /// <param name="code">The code to test.</param>
        /// <param name="message">The message that will be displayed on failure.</param>
        /// <returns>The <see cref="Exception"/> thrown by <paramref name="code"/>.</returns>
        public static Exception Throws(Action code, string message)
        {
            return Throws(typeof(Exception), code, message);
        }
        #endregion

        #region Throws(Action code)
        /// <summary>
        /// Verifies that <paramref name="code"/> throws an exception when called.
        /// </summary>
        /// <param name="code">The code to test.</param>
        /// <returns>The <see cref="Exception"/> thrown by <paramref name="code"/>.</returns>
        public static Exception Throws(Action code)
        {
            return Throws(typeof(Exception), code);
        }
        #endregion

        #region Throws(Type expectedExceptionType, Action code, string message, params object[] parameters)
        /// <summary>
        /// Verifies that <paramref name="code"/> throws a particular exception when called.
        /// </summary>
        /// <param name="expectedExceptionType">The exception Type expected.</param>
        /// <param name="code">The code to test.</param>
        /// <param name="message">The message that will be displayed on failure.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <returns>The <see cref="Exception"/> thrown by <paramref name="code"/>.</returns>
        public static Exception Throws(Type expectedExceptionType, Action code, string message, params object[] parameters)
        {
            Exception caughtException = null;

            try
            {
                code();
            }
            catch (Exception ex)
            {
                caughtException = ex;
                TypeAssert.IsType(caughtException, expectedExceptionType, message, parameters);
            }

            if (caughtException == null)
            {
                Assert.Fail(Resources.Assertion_ExceptionNotThrown, expectedExceptionType);
            }

            return caughtException;
        }
        #endregion

        #region Throws(Type expectedExceptionType, Action code, string message)
        /// <summary>
        /// Verifies that <paramref name="code"/> throws a particular exception when called.
        /// </summary>
        /// <param name="expectedExceptionType">The exception Type expected.</param>
        /// <param name="code">The code to test.</param>
        /// <param name="message">The message that will be displayed on failure.</param>
        /// <returns>The <see cref="Exception"/> thrown by <paramref name="code"/>.</returns>
        public static Exception Throws(Type expectedExceptionType, Action code, string message)
        {
            return Throws(expectedExceptionType, code, message, null);
        }
        #endregion

        #region Throws(Type expectedExceptionType, Action code)
        /// <summary>
        /// Verifies that <paramref name="code"/> throws a particular exception when called.
        /// </summary>
        /// <param name="expectedExceptionType">The exception Type expected.</param>
        /// <param name="code">The code to test.</param>
        /// <returns>The <see cref="Exception"/> thrown by <paramref name="code"/>.</returns>
        public static Exception Throws(Type expectedExceptionType, Action code)
        {
            return Throws(expectedExceptionType, code, String.Empty, null);
        }
        #endregion

        #region Throws<T>(Action code, string message, params object[] parameters)
        /// <summary>
        /// Verifies that <paramref name="code"/> throws a particular exception when called.
        /// </summary>
        /// <typeparam name="T">Type of the expected exception.</typeparam>
        /// <param name="code">The code to test.</param>
        /// <param name="message">The message that will be displayed on failure.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <returns>The <see cref="Exception"/> thrown by <paramref name="code"/>.</returns>
        public static T Throws<T>(Action code, string message, params object[] parameters) where T : Exception
        {
            return (T)Throws(typeof(T), code, message, parameters);
        }
        #endregion

        #region Throws<T>(Action code, string message)
        /// <summary>
        /// Verifies that <paramref name="code"/> throws a particular exception when called.
        /// </summary>
        /// <typeparam name="T">Type of the expected exception.</typeparam>
        /// <param name="code">The code to test.</param>
        /// <param name="message">The message that will be displayed on failure.</param>
        /// <returns>The <see cref="Exception"/> thrown by <paramref name="code"/>.</returns>
        public static T Throws<T>(Action code, string message) where T : Exception
        {
            return Throws<T>(code, message, null);
        }
        #endregion

        #region Throws<T>(Action code)
        /// <summary>
        /// Verifies that <paramref name="code"/> throws a particular exception when called.
        /// </summary>
        /// <typeparam name="T">Type of the expected exception.</typeparam>
        /// <param name="code">The code to test.</param>
        /// <returns>The <see cref="Exception"/> thrown by <paramref name="code"/>.</returns>
        public static T Throws<T>(Action code) where T : Exception
        {
            return Throws<T>(code, String.Empty, null);
        }
        #endregion

        #endregion

        #region Catch

        #region Catch(Action code, string message, params object[] parameters)
        /// <summary>
        /// Verifies that <paramref name="code"/> throws an exception when called
        /// and returns it.
        /// </summary>
        /// <param name="code">The code to test.</param>
        /// <param name="message">The message that will be displayed on failure.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <returns>The <see cref="Exception"/> thrown by <paramref name="code"/>.</returns>
        public static Exception Catch(Action code, string message, params object[] parameters)
        {
            return Catch(typeof(Exception), code, message, parameters);
        }
        #endregion

        #region Catch(Action code, string message)
        /// <summary>
        /// Verifies that <paramref name="code"/> throws an exception when called
        /// and returns it.
        /// </summary>
        /// <param name="code">The code to test.</param>
        /// <param name="message">The message that will be displayed on failure.</param>
        /// <returns>The <see cref="Exception"/> thrown by <paramref name="code"/>.</returns>
        public static Exception Catch(Action code, string message)
        {
            return Catch(typeof(Exception), code, message);
        }
        #endregion

        #region Catch(Action code)
        /// <summary>
        /// Verifies that <paramref name="code"/> throws an exception when called
        /// and returns it.
        /// </summary>
        /// <param name="code">The code to test.</param>
        /// <returns>The <see cref="Exception"/> thrown by <paramref name="code"/>.</returns>
        public static Exception Catch(Action code)
        {
            return Catch(typeof(Exception), code);
        }
        #endregion

        #region Catch(Type expectedExceptionType, Action code, string message, params object[] parameters)
        /// <summary>
        /// Verifies that <paramref name="code"/> throws an exception of a certain Type
        /// or one derived from it when called and returns it.
        /// </summary>
        /// <param name="expectedExceptionType">The expected exception type.</param>
        /// <param name="code">The code to test.</param>
        /// <param name="message">The message that will be displayed on failure.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <returns>The <see cref="Exception"/> thrown by <paramref name="code"/>.</returns>
        public static Exception Catch(Type expectedExceptionType, Action code, string message, params object[] parameters)
        {
            Exception caughtException = null;

            try
            {
                code();
            }
            catch (Exception ex)
            {
                caughtException = ex;
                Assert.IsInstanceOfType(caughtException, expectedExceptionType, message, parameters);
            }

            if (caughtException == null)
            {
                Assert.Fail(Resources.Assertion_ExceptionNotThrown, expectedExceptionType);
            }

            return caughtException;
        }
        #endregion

        #region Catch(Type expectedExceptionType, Action code, string message)
        /// <summary>
        /// Verifies that <paramref name="code"/> throws an exception of a certain Type
        /// or one derived from it when called and returns it.
        /// </summary>
        /// <param name="expectedExceptionType">The expected exception type.</param>
        /// <param name="code">The code to test.</param>
        /// <param name="message">The message that will be displayed on failure.</param>
        /// <returns>The <see cref="Exception"/> thrown by <paramref name="code"/>.</returns>
        public static Exception Catch(Type expectedExceptionType, Action code, string message)
        {
            return Catch(expectedExceptionType, code, message, null);
        }
        #endregion

        #region Catch(Type expectedExceptionType, Action code)
        /// <summary>
        /// Verifies that <paramref name="code"/> throws an exception of a certain Type
        /// or one derived from it when called and returns it.
        /// </summary>
        /// <param name="expectedExceptionType">The expected exception type.</param>
        /// <param name="code">The code to test.</param>
        /// <returns>The <see cref="Exception"/> thrown by <paramref name="code"/>.</returns>
        public static Exception Catch(Type expectedExceptionType, Action code)
        {
            return Catch(expectedExceptionType, code, String.Empty, null);
        }
        #endregion

        #region Catch<T>(Action code, string message, params object[] parameters)
        /// <summary>
        /// Verifies that <paramref name="code"/> throws an exception of a certain Type
        /// or one derived from it when called and returns it.
        /// </summary>
        /// <typeparam name="T">The expected exception type.</typeparam>
        /// <param name="code">The code to test.</param>
        /// <param name="message">The message that will be displayed on failure.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <returns>The <see cref="Exception"/> thrown by <paramref name="code"/>.</returns>
        public static T Catch<T>(Action code, string message, params object[] parameters) where T : System.Exception
        {
            return (T)Catch(typeof(T), code, message, parameters);
        }
        #endregion

        #region Catch<T>(Action code, string message)
        /// <summary>
        /// Verifies that <paramref name="code"/> throws an exception of a certain Type
        /// or one derived from it when called and returns it.
        /// </summary>
        /// <typeparam name="T">The expected exception type.</typeparam>
        /// <param name="code">The code to test.</param>
        /// <param name="message">The message that will be displayed on failure.</param>
        /// <returns>The <see cref="Exception"/> thrown by <paramref name="code"/>.</returns>
        public static T Catch<T>(Action code, string message) where T : System.Exception
        {
            return (T)Catch(typeof(T), code, message);
        }
        #endregion

        #region Catch<T>(Action code)
        /// <summary>
        /// Verifies that <paramref name="code"/> throws an exception of a certain Type
        /// or one derived from it when called and returns it.
        /// </summary>
        /// <typeparam name="T">The expected exception type.</typeparam>
        /// <param name="code">The code to test.</param>
        /// <returns>The <see cref="Exception"/> thrown by <paramref name="code"/>.</returns>
        public static T Catch<T>(Action code) where T : System.Exception
        {
            return (T)Catch(typeof(T), code);
        }
        #endregion

        #endregion

        #region DoesNotThrow

        #region DoesNotThrow(Action code, string message, params object[] parameters)
        /// <summary>
        /// Verifies that <paramref name="code"/> does not throw an exception.
        /// </summary>
        /// <param name="code">The code to test.</param>
        /// <param name="message">The message that will be displayed on failure.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public static void DoesNotThrow(Action code, string message, params object[] parameters)
        {
            Exception caughtException = null;

            try
            {
                code();
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }

            Assert.IsNull(caughtException, message, parameters);
        }
        #endregion

        #region DoesNotThrow(Action code, string message)
        /// <summary>
        /// Verifies that <paramref name="code"/> does not throw an exception.
        /// </summary>
        /// <param name="code">The code to test.</param>
        /// <param name="message">The message that will be displayed on failure.</param>
        public static void DoesNotThrow(Action code, string message)
        {
            DoesNotThrow(code, message, null);
        }
        #endregion

        #region DoesNotThrow(Action code)
        /// <summary>
        /// Verifies that <paramref name="code"/> does not throw an exception.
        /// </summary>
        /// <param name="code">The code to test.</param>
        public static void DoesNotThrow(Action code)
        {
            DoesNotThrow(code, String.Empty, null);
        }
        #endregion

        #endregion

        #region WithMessage

        #region WithMessage<T>(this T exception, string message, ExceptionMessageComparison comparison)
        /// <summary>
        /// Verifies that the exception <see cref="Exception.Message"/>
        /// contains the given text.
        /// </summary>
        /// <typeparam name="T">The exception type.</typeparam>
        /// <param name="exception">The exception whose <see cref="Exception.Message"/>
        /// will be searched.</param>
        /// <param name="message">The text to search for in the exception
        /// <see cref="Exception.Message"/>.</param>
        /// <param name="comparison">One of the <see cref="ExceptionMessageComparison"/> values.</param>
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
        #endregion

        #region WithMessage<T>(this T exception, string message)
        /// <summary>
        /// Verifies that the exception <see cref="Exception.Message"/>
        /// is equal to the given text.
        /// </summary>
        /// <typeparam name="T">The exception type.</typeparam>
        /// <param name="exception">The exception whose <see cref="Exception.Message"/>
        /// will be compared.</param>
        /// <param name="message">The text to compare.</param>
        /// <returns>The original <see cref="Exception"/>.</returns>
        public static T WithMessage<T>(this T exception, string message) where T : Exception
        {
            return exception.WithMessage(message, ExceptionMessageComparison.Exact);
        }
        #endregion

        #endregion

        #region WithParameter
        /// <summary>
        /// Verifies that the exception <see cref="ArgumentException.ParamName"/> 
        /// property is equal to the given text.
        /// </summary>
        /// <typeparam name="T">The exception type.</typeparam>
        /// <param name="exception">The exception whose <see cref="ArgumentException.ParamName"/>
        /// property will be compared.</param>
        /// <param name="parameterName">The text to compare.</param>
        /// <returns>The original <see cref="Exception"/>.</returns>
        public static T WithParameter<T>(this T exception, string parameterName) where T : ArgumentException
        {
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.ParamName, parameterName);
            return exception;
        }
        #endregion

        #endregion
    }
}
