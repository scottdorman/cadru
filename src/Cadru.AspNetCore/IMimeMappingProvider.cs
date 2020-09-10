//------------------------------------------------------------------------------
// <copyright file="IMimeMappingProvider.cs"
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

using System.Collections.Generic;

using Microsoft.AspNetCore.StaticFiles;

namespace Cadru.AspNetCore
{
    /// <inheritdoc/>
    public interface IMimeMappingProvider : IContentTypeProvider
    {
        /// <summary>
        /// The cross reference table of file extensions and content-types.
        /// </summary>
        IDictionary<string, string> Mappings { get; }

        /// <summary>
        /// Given a file path, determine the MIME type.
        /// </summary>
        /// <param name="subpath">A file path</param>
        /// <returns>The resulting MIME type</returns>
        /// <remarks>If the MIME type can't be determined, <see
        /// cref="MimeTypes.OctetStream" /> will be returned.</remarks>
        string GetContentType(string subpath);
    }
}