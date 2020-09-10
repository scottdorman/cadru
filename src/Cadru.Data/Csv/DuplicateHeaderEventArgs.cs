//------------------------------------------------------------------------------
// <copyright file="DuplicateHeaderEventArgs.cs"
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
    /// Provides data for the <see cref="M:CsvReader.OnDuplicateHeader"/> event.
    /// </summary>
    public class DuplicateHeaderEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the DuplicateHeaderEventArgs class.
        /// </summary>
        /// <param name="headerName">The name of the duplicate header.</param>
        /// <param name="index">The index of the duplicate header being added.</param>
        /// <param name="existingDuplicateIndex">
        /// The index of the duplicate header that is already in the Column collection.
        /// </param>
        public DuplicateHeaderEventArgs(string headerName, int index, int existingDuplicateIndex)
        {
            this.HeaderName = headerName;
            this.Index = index;
            this.ExistingDuplicateIndex = existingDuplicateIndex;
        }

        /// <summary>
        /// Index of the duplicate header that has already been added to the
        /// Column collection
        /// </summary>
        /// <value>The column index</value>
        public int ExistingDuplicateIndex { get; }

        /// <summary>
        /// Name of the header that is a duplicate.
        /// </summary>
        /// <value>The header name.</value>
        public string HeaderName { get; set; }

        /// <summary>
        /// Index of the duplicate header being added
        /// </summary>
        /// <value>The column index</value>
        public int Index { get; }
    }
}