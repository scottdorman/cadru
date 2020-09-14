
using System;
using System.Globalization;

using Cadru.UnitTest.Framework.Resources;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.UnitTest.Framework
{
    internal static class Helpers
    {
        /// <summary>
        /// Helper function that creates and throws an AssertionFailedException
        /// </summary>
        /// <param name="assertionName">
        /// name of the assertion throwing an exception
        /// </param>
        /// <param name="message">
        /// message describing conditions for assertion failure
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        internal static void HandleFail(string assertionName, string? message, params object[]? parameters)
        {
            var finalMessage = String.Empty;
            if (!String.IsNullOrEmpty(message))
            {
                if (parameters == null)
                {
                    finalMessage = ReplaceNulls(message);
                }
                else
                {
                    finalMessage = String.Format(CultureInfo.CurrentCulture, ReplaceNulls(message), parameters);
                }
            }

            throw new AssertFailedException(String.Format(CultureInfo.CurrentCulture, Strings.AssertionFailed, assertionName, finalMessage));
        }

        /// <summary>
        /// Safely converts an object to a string, handling null values and null characters.
        /// Null values are converted to "(null)". Null characters are converted to "\\0".
        /// </summary>
        /// <param name="input">
        /// The object to convert to a string.
        /// </param>
        /// <returns>
        /// The converted string.
        /// </returns>
        internal static string? ReplaceNulls(object? input)
        {
            // Use the localized "(null)" string for null values.
            if (input == null)
            {
                return Strings.Common_NullInMessages;
            }
            else
            {
                // Convert it to a string.
                var inputString = input.ToString();

                // Make sure the class didn't override ToString and return null.
                if (inputString == null)
                {
                    return Strings.Common_NullInMessages;
                }

                return ReplaceNullChars(inputString);
            }
        }

        /// <summary>
        /// Replaces null characters ('\0') with "\\0".
        /// </summary>
        /// <param name="input">
        /// The string to search.
        /// </param>
        /// <returns>
        /// The converted string with null characters replaced by "\\0".
        /// </returns>
        /// <remarks>
        /// This is only public and still present to preserve compatibility with the V1 framework.
        /// </remarks>
        public static string? ReplaceNullChars(string? input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return input;
            }

            return input.Replace("\0", "\\0");
        }

    }
}
