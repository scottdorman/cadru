//------------------------------------------------------------------------------
// <copyright file="MalformedCsvException.cs"
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
using System.Globalization;

using Cadru.Data.Resources;

namespace Cadru.Data.Csv
{
    /// <summary>
    /// Represents the exception that is thrown when a CSV file is malformed.
    /// </summary>
    public class MalformedCsvException : Exception
    {
        /// <summary>
        /// Contains the message that describes the error.
        /// </summary>
        private readonly string _message;

        /// <summary>
        /// Contains the raw data when the error occured.
        /// </summary>
        private readonly string _rawData;

        /// <summary>
        /// Contains the current field index.
        /// </summary>
        private readonly int _currentFieldIndex;

        /// <summary>
        /// Contains the current record index.
        /// </summary>
        private readonly long _currentRecordIndex;

        /// <summary>
        /// Contains the current position in the raw data.
        /// </summary>
        private readonly int _currentPosition;

        /// <summary>
        /// Initializes a new instance of the MalformedCsvException class.
        /// </summary>
        public MalformedCsvException() : this(null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MalformedCsvException class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MalformedCsvException(string message) : this(message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MalformedCsvException class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public MalformedCsvException(string message, Exception innerException) : base(String.Empty, innerException)
        {
            this._message = (message == null ? String.Empty : message);

            this._rawData = String.Empty;
            this._currentPosition = -1;
            this._currentRecordIndex = -1;
            this._currentFieldIndex = -1;
        }

        /// <summary>
        /// Initializes a new instance of the MalformedCsvException class.
        /// </summary>
        /// <param name="rawData">The raw data when the error occured.</param>
        /// <param name="currentPosition">The current position in the raw data.</param>
        /// <param name="currentRecordIndex">The current record index.</param>
        /// <param name="currentFieldIndex">The current field index.</param>
        public MalformedCsvException(string rawData, int currentPosition, long currentRecordIndex, int currentFieldIndex)
            : this(rawData, currentPosition, currentRecordIndex, currentFieldIndex, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MalformedCsvException class.
        /// </summary>
        /// <param name="rawData">The raw data when the error occured.</param>
        /// <param name="currentPosition">The current position in the raw data.</param>
        /// <param name="currentRecordIndex">The current record index.</param>
        /// <param name="currentFieldIndex">The current field index.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public MalformedCsvException(string rawData, int currentPosition, long currentRecordIndex, int currentFieldIndex, Exception innerException) : base(String.Empty, innerException)
        {
            this._rawData = (rawData == null ? String.Empty : rawData);
            this._currentPosition = currentPosition;
            this._currentRecordIndex = currentRecordIndex;
            this._currentFieldIndex = currentFieldIndex;

            this._message = String.Format(CultureInfo.InvariantCulture, Strings.MalformedCsvException, this._currentRecordIndex, this._currentFieldIndex, this._currentPosition, this._rawData);
        }

        /// <summary>
        /// Gets the raw data when the error occured.
        /// </summary>
        /// <value>The raw data when the error occured.</value>
        public string RawData => this._rawData;

        /// <summary>
        /// Gets the current position in the raw data.
        /// </summary>
        /// <value>The current position in the raw data.</value>
        public int CurrentPosition => this._currentPosition;

        /// <summary>
        /// Gets the current record index.
        /// </summary>
        /// <value>The current record index.</value>
        public long CurrentRecordIndex => this._currentRecordIndex;

        /// <summary>
        /// Gets the current field index.
        /// </summary>
        /// <value>The current record index.</value>
        public int CurrentFieldIndex => this._currentFieldIndex;

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        /// <value>A message that describes the current exception.</value>
        public override string Message => this._message;
    }
}