//------------------------------------------------------------------------------
// <copyright file="MissingFieldCsvException.cs"
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
    /// Represents the exception that is thrown when a there is a missing field in a record of the CSV file.
    /// </summary>
    /// <remarks>
    /// MissingFieldException would have been a better name, but there is already a <see cref="T:System.MissingFieldException" />.
    /// </remarks>
    public class MissingFieldCsvException : MalformedCsvException
    {
        /// <summary>
        /// Initializes a new instance of the MissingFieldCsvException class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MissingFieldCsvException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MissingFieldCsvException class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public MissingFieldCsvException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MissingFieldCsvException class.
        /// </summary>
        /// <param name="rawData">The raw data when the error occured.</param>
        /// <param name="currentPosition">The current position in the raw data.</param>
        /// <param name="currentRecordIndex">The current record index.</param>
        /// <param name="currentFieldIndex">The current field index.</param>
        public MissingFieldCsvException(string rawData, int currentPosition, long currentRecordIndex, int currentFieldIndex)
            : base(rawData, currentPosition, currentRecordIndex, currentFieldIndex)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MissingFieldCsvException class.
        /// </summary>
        /// <param name="rawData">The raw data when the error occured.</param>
        /// <param name="currentPosition">The current position in the raw data.</param>
        /// <param name="currentRecordIndex">The current record index.</param>
        /// <param name="currentFieldIndex">The current field index.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public MissingFieldCsvException(string rawData, int currentPosition, long currentRecordIndex, int currentFieldIndex, Exception innerException)
            : base(rawData, currentPosition, currentRecordIndex, currentFieldIndex, innerException)
        {
        }
    }
}