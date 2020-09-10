//------------------------------------------------------------------------------
// <copyright file="CsvReader.RecordEnumerator.cs"
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
using System.Collections;
using System.Collections.Generic;

using Cadru.Data.Resources;

namespace Cadru.Data.Csv
{
    public partial class CsvReader
    {
        /// <summary>
        /// Supports a simple iteration over the records of a <see cref="T:CsvReader"/>.
        /// </summary>
        public struct RecordEnumerator : IEnumerator<string[]>
        {
            /// <summary>
            /// Contains the current record.
            /// </summary>
            private string[] _current;

            /// <summary>
            /// Contains the current record index.
            /// </summary>
            private long _currentRecordIndex;

            /// <summary>
            /// Contains the enumerated <see cref="T:CsvReader"/>.
            /// </summary>
            private CsvReader _reader;

            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="T:RecordEnumerator"/> class.
            /// </summary>
            /// <param name="reader">
            /// The <see cref="T:CsvReader"/> to iterate over.
            /// </param>
            /// <exception cref="T:ArgumentNullException">
            /// <paramref name="reader"/> is a <see langword="null"/>.
            /// </exception>
            public RecordEnumerator(CsvReader reader)
            {
                if (reader == null)
                {
                    throw new ArgumentNullException(nameof(reader));
                }

                this._reader = reader;
                this._current = null;

                this._currentRecordIndex = reader.CurrentRecordIndex;
            }

            /// <summary>
            /// Gets the current record.
            /// </summary>
            public string[] Current => this._current;

            /// <summary>
            /// Gets the current record.
            /// </summary>
            object IEnumerator.Current
            {
                get
                {
                    if (this._reader.CurrentRecordIndex != this._currentRecordIndex)
                    {
                        throw new InvalidOperationException(Strings.EnumerationVersionCheckFailed);
                    }

                    return this.Current;
                }
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing,
            /// releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                this._reader = null;
                this._current = null;
            }

            /// <summary>
            /// Advances the enumerator to the next record of the CSV.
            /// </summary>
            /// <returns>
            /// <see langword="true"/> if the enumerator was successfully
            /// advanced to the next record, <see langword="false"/> if the
            /// enumerator has passed the end of the CSV.
            /// </returns>
            public bool MoveNext()
            {
                if (this._reader.CurrentRecordIndex != this._currentRecordIndex)
                {
                    throw new InvalidOperationException(Strings.EnumerationVersionCheckFailed);
                }

                if (this._reader.ReadNextRecord())
                {
                    this._current = new string[this._reader._fieldCount];

                    this._reader.CopyCurrentRecordTo(this._current);
                    this._currentRecordIndex = this._reader.CurrentRecordIndex;

                    return true;
                }
                else
                {
                    this._current = null;
                    this._currentRecordIndex = this._reader.CurrentRecordIndex;

                    return false;
                }
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the
            /// first record in the CSV.
            /// </summary>
            public void Reset()
            {
                if (this._reader.CurrentRecordIndex != this._currentRecordIndex)
                {
                    throw new InvalidOperationException(Strings.EnumerationVersionCheckFailed);
                }

                this._reader.MoveTo(-1);

                this._current = null;
                this._currentRecordIndex = this._reader.CurrentRecordIndex;
            }
        }
    }
}