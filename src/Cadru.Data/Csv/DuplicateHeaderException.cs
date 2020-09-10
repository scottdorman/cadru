//------------------------------------------------------------------------------
// <copyright file="DuplicateHeaderException.cs"
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
    /// Represents the exception that is thrown when a duplicate column header name encounter.
    /// </summary>
    public class DuplicateHeaderException : Exception
    {
        /// <summary>
        /// Contains the message that describes the error.
        /// </summary>
        private readonly string _headerName;

        /// <summary>
        /// Contains the column index where the duplicate was found.
        /// </summary>
        private readonly int _columnIndex;

        /// <summary>
        /// Initializes a new instance of the DuplicateHeaderException class.
        /// </summary>
        public DuplicateHeaderException(string headerName, int columnIndex) : base($"Duplicate header {headerName} encountered at column index {columnIndex}")
        {
            this._headerName = headerName;
            this._columnIndex = columnIndex;
        }

        /// <summary>
        /// Gets the HeaderName of the column with the duplicate.
        /// </summary>
        /// <value>The name of the column header.</value>
        public string HeaderName => this._headerName;

        /// <summary>
        /// Gets the column index where the duplicate was found.
        /// </summary>
        /// <value>The index of the column.</value>
        public int ColumnIndex => this._columnIndex;
    }
}