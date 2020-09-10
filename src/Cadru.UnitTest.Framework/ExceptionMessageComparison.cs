//------------------------------------------------------------------------------
// <copyright file="ExceptionMessageComparison.cs"
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

namespace Cadru.UnitTest.Framework
{
    /// <summary>
    /// Options used by the
    /// <see cref="Cadru.UnitTest.Framework.ExceptionAssert.WithMessage{T}(T, String, ExceptionMessageComparison)"/>
    /// method to determine how to compare the exception message.
    /// </summary>
    public enum ExceptionMessageComparison
    {
        /// <summary>
        /// The exception message should exactly match.
        /// </summary>
        Exact,

        /// <summary>
        /// The exception message should contain the given string.
        /// </summary>
        Contains,

        /// <summary>
        /// The exception message should start with the given string.
        /// </summary>
        StartsWith,

        /// <summary>
        /// The exception message should end with the given string.
        /// </summary>
        EndsWith
    }
}