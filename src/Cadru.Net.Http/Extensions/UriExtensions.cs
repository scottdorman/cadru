//------------------------------------------------------------------------------
// <copyright file="UriExtensions.cs"
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

namespace Cadru.Net.Http.Extensions
{
    /// <summary>
    /// Extensions for working with <see cref="Uri"/> instances.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Gets the path segment making up the specified URI at the specified index.
        /// </summary>
        /// <param name="uri">The URI instance.</param>
        /// <param name="index">The index of the path segment.</param>
        /// <returns>The path segment at the specified index or <see cref="String.Empty"/> if the segment is not found.</returns>
        public static string GetUriSegment(this Uri uri, int index)
        {
            var segment = String.Empty;
            var segments = uri.Segments;
            if (index <= segments.Length || index < 0)
            {
                segment = segments[index];
            }

            return segment;
        }
    }
}