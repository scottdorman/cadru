//------------------------------------------------------------------------------
// <copyright file="Extensions.cs"
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
using System.Diagnostics;
using System.Text;

using Cadru.Contracts;

namespace Cadru.Diagnostics
{
    /// <summary>
    /// Provides extension methods to simplify diagnostics.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Flattens all exception messages into a single string.
        /// </summary>
        /// <param name="e">The exception to flatten.</param>
        /// <returns>A string containing all of the exception
        /// messages.</returns>
        /// <remarks>This method recursively follows all nested <see
        /// cref="Exception.InnerException"/> properties.</remarks>
        public static string Flatten(this Exception e)
        {
            Requires.NotNull(e, nameof(e));
            return e.Flatten(new StringBuilder());
        }

        /// <summary>
        /// Formats the elapsed time of a <see cref="Stopwatch"/>
        /// as hh':'mm':'ss'.'ff.
        /// </summary>
        /// <param name="stopwatch"></param>
        /// <returns>The formatted elapsed time as a string.</returns>
        public static string ToElapsedTime(this Stopwatch stopwatch)
        {
            return stopwatch.Elapsed.ToString("hh':'mm':'ss'.'ff");
        }

        private static string Flatten(this Exception e, StringBuilder messages)
        {
            messages.AppendLine(e.Message);
            if (e.InnerException != null)
            {
                messages.AppendLine(Flatten(e.InnerException, messages));
            }

            return messages.ToString();
        }
    }
}