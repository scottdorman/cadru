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
        /// Tests whether the code specified by delegate
        /// <paramref name="action"/> does not throw an exception.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="action">
        /// Delegate to code to be tested and which is expected to throw exception.
        /// </param>
        public static void DoesNotThrowException(this Assert assert, Action action)
        {
            assert.DoesNotThrowException(action, Strings.DoesNotThrowExceptionFailMsg, Array.Empty<object>());
        }

        /// <summary>
        /// Tests whether the code specified by delegate
        /// <paramref name="action"/> does not throw an exception.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="action">
        /// Delegate to code to be tested and which is expected to throw exception.
        /// </param>
        /// <param name="message">
        /// The message to include in the exception when
        /// <paramref name="action"/> does throw an exception.
        /// </param>
        public static void DoesNotThrowException(this Assert assert, Action action, string message)
        {
            assert.DoesNotThrowException(action, message, Array.Empty<object>());

        }


        /// <summary>
        /// Tests whether the code specified by delegate
        /// <paramref name="action"/> does not throw an exception.
        /// </summary>
        /// <param name="assert">The <see cref="Assert"/> instance to extend.</param>
        /// <param name="action">
        /// Delegate to code to be tested and which is expected to throw exception.
        /// </param>
        /// <param name="message">
        /// The message to include in the exception when
        /// <paramref name="action"/> does throw an exception.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public static void DoesNotThrowException(this Assert assert, Action action, string message, params object[] parameters)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            try
            {
                action();
            }
            catch (Exception ex)
            {
                Helpers.HandleFail("Assert.DoesNotThrowException", message, parameters, ex.GetType().Name,
                    ex.Message,
                    ex.StackTrace);
            }
        }
    }
}
