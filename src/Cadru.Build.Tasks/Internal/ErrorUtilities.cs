//------------------------------------------------------------------------------
// <copyright file="ErrorUtilities.cs"
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

using Cadru.Build.Tasks.Resources;

namespace Cadru.Build.Tasks.Internal
{
    internal static class ErrorUtilities
    {
        internal static void ThrowInternalError(string message, Exception innerException, params object[] args)
        {
            throw new Exception(String.Format(message, args), innerException);
        }

        internal static void VerifyThrow(bool condition, string unformattedMessage)
        {
            if (!condition)
            {
                // PERF NOTE: explicitly passing null for the arguments array
                // prevents memory allocation
                ThrowInternalError(unformattedMessage, null, null);
            }
        }

        internal static void VerifyThrowArgumentLength(string parameter, string parameterName)
        {
            VerifyThrowArgumentNull(parameter, parameterName);

            if (parameter.Length == 0)
            {
                throw new ArgumentException(String.Format(Strings.Shared_ParameterCannotHaveZeroLength, parameterName));
            }
        }

        internal static void VerifyThrowArgumentLengthIfNotNull(string parameter, string parameterName)
        {
            if (parameter != null && parameter.Length == 0)
            {
                throw new ArgumentException(String.Format(Strings.Shared_ParameterCannotHaveZeroLength, parameterName));
            }
        }

        internal static void VerifyThrowArgumentNull(object parameter, string parameterName)
        {
            if (parameter == null)
            {
                // Most ArgumentNullException overloads append its own rather
                // clunky multi-line message. So use the one overload that doesn't.
                throw new ArgumentNullException(String.Format(Strings.Shared_ParameterCannotBeNull, parameterName), (Exception)null);
            }
        }
    }
}