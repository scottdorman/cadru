//------------------------------------------------------------------------------
// <copyright file="NullExtensions.cs"
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

using Cadru.Internal;

namespace Cadru.Extensions
{
    /// <summary>
    /// Provides basic routines for determining if an instance is <see langword="null"/>.
    /// </summary>
    public static class NullExtensions
    {
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="source"/> is not <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The type of the instance to test.</typeparam>
        /// <param name="source">The source instance.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="source"/> is not
        /// <see langword="null"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsNotNull<T>([ValidatedNotNull] this T source)
        {
            return source != null;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="source"/> is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The type of the instance to test.</typeparam>
        /// <param name="source">The source instance.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="source"/> is
        /// <see langword="null"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsNull<T>(this T source)
        {
            return source == null;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="source"/> is not <see langword="null"/> or <see cref="Guid.Empty"/>.
        /// </summary>
        /// <param name="source">The source instance.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="source"/> is not
        /// <see langword="null"/> or <see cref="Guid.Empty"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsNullOrEmpty([ValidatedNotNull] this Guid? source)
        {
            return source == null || source.Value == Guid.Empty;
        }
    }
}