//------------------------------------------------------------------------------
// <copyright file="IFilter.cs"
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

namespace Cadru.Scim.Filters
{
    /// <summary>
    /// Provides options to be used when formatting an <see cref="IFilter"/>.
    /// </summary>
    public sealed class FilterExpressionFormatOptions
    {
        /// <summary>
        /// To prepend the "?" query string separator,
        /// <see langword="true"/>; otherwise, <see langword="false"/>
        /// </summary>
        public bool IncludeQuerySeparator { get; set; } = true;

        /// <summary>
        /// To prepend the "filter=" query string parameter key,
        /// <see langword="true"/>; otherwise, <see langword="false"/>
        /// </summary>
        public bool IncludeFilterParameterName { get; set; } = true;
    }
}