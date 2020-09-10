//------------------------------------------------------------------------------
// <copyright file="MimeMappingProvider.cs"
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
using System.Collections.Generic;

using Microsoft.AspNetCore.StaticFiles;

namespace Cadru.AspNetCore
{
    /// <summary>
    /// Used to look up MIME types given a file path
    /// </summary>
    public class MimeMappingProvider : IMimeMappingProvider
    {
        private readonly FileExtensionContentTypeProvider _contentTypeProvider;

        /// <summary>
        /// Creates a new provider with a set of default mappings.
        /// </summary>
        public MimeMappingProvider()
        {
            this._contentTypeProvider = new FileExtensionContentTypeProvider();
        }

        /// <summary>
        /// Creates a lookup engine using the provided mapping.
        /// </summary>
        /// <param name="mappings">
        /// The cross reference table of file extensions and content-types.
        /// </param>
        /// <remarks>
        /// It is recommended that the <see cref="IDictionary{T,K}"/> instance
        /// use <see cref="StringComparer.OrdinalIgnoreCase"/>. This
        /// <see cref="IDictionary{T,K}"/> instance replaces the default mappings.
        /// </remarks>
        public MimeMappingProvider(IDictionary<string, string> mappings)
        {
            this._contentTypeProvider = new FileExtensionContentTypeProvider(mappings);
        }

        /// <summary>
        /// Creates a new provider with a set of default mappings plus the
        /// additional mappings provided.
        /// </summary>
        /// <param name="mappings">
        /// The mappings to add to the cross reference table.
        /// </param>
        public MimeMappingProvider(params KeyValuePair<string, string>[] mappings)
        {
            this._contentTypeProvider = new FileExtensionContentTypeProvider();
            foreach (var mapping in mappings)
            {
                this._contentTypeProvider.Mappings.Add(mapping);
            }
        }

        /// <inheritdoc/>
        public IDictionary<string, string> Mappings => this._contentTypeProvider.Mappings;

        /// <inheritdoc/>
        public string GetContentType(string subpath)
        {
            if (!this.TryGetContentType(subpath, out var contentType))
            {
                contentType = MimeTypes.OctetStream;
            }

            return contentType;
        }

        /// <inheritdoc/>
        public bool TryGetContentType(string subpath, out string contentType)
        {
            return this._contentTypeProvider.TryGetContentType(subpath, out contentType);
        }
    }
}