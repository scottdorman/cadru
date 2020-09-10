//------------------------------------------------------------------------------
// <copyright file="ValueTrimmingOptions.cs"
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

namespace Cadru.Data.Csv
{
    /// <summary>
    /// Determines which values should be trimmed.
    /// </summary>
    [Flags]
    public enum ValueTrimmingOptions
    {
        /// <summary>
        /// Don't do any trimming
        /// </summary>
        None = 0,
        
        /// <summary>
        /// Only trim unquoted values
        /// </summary>
        UnquotedOnly = 1,

        /// <summary>
        /// Only trim quoted values
        /// </summary>
        QuotedOnly = 2,

        /// <summary>
        /// trim all values
        /// </summary>
        All = UnquotedOnly | QuotedOnly
    }
}