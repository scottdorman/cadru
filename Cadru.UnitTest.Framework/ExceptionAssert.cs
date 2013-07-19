//------------------------------------------------------------------------------
// <copyright file="ExceptionAssert.cs" 
//  company="Scott Dorman" 
//  library="Cadru">
//    Copyright (C) 2001-2013 Scott Dorman.
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
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Contains assertion types that are not provided with the standard MSTest assertions.
    /// </summary>
    public static class ExceptionAssert
    {
        #region Throws<TException>(Action action)
        /// <summary>
        /// Checks to make sure that the input delegate throws a exception of type exceptionType.
        /// </summary>
        /// <typeparam name="TException">The type of exception expected.</typeparam>
        /// <param name="action">The action to execute to generate the exception.</param>
        public static void Throws<TException>(Action action) where TException : System.Exception
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(TException), "Expected exception was not thrown. ");
                return;
            }

            Assert.Fail("Expected exception of type " + typeof(TException) + " but no exception was thrown.");
        }
        #endregion

        #region Throws<TException>(string expectedMessage, Action action)
        /// <summary>
        /// Checks to make sure that the input delegate throws a exception of type exceptionType.
        /// </summary>
        /// <typeparam name="TException">The type of exception expected.</typeparam>
        /// <param name="expectedMessage">The expected message text.</param>
        /// <param name="action">The action to execute to generate the exception.</param>
        public static void Throws<TException>(string expectedMessage, Action action) where TException : System.Exception
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(TException), "Expected exception was not thrown. ");
                Assert.AreEqual(expectedMessage, ex.Message, "Expected exception with a message of '" + expectedMessage + "' but exception with message of '" + ex.Message + "' was thrown instead.");
                return;
            }

            Assert.Fail("Expected exception of type " + typeof(TException) + " but no exception was thrown.");
        }
        #endregion
    }
}
