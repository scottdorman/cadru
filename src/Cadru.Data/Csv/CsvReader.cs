//------------------------------------------------------------------------------
// <copyright file="CsvReader.cs"
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
#if !NETSTANDARD1_3
using System.Data;
using System.Data.Common;
#endif
using System.Globalization;
using System.IO;

using Cadru.Data.Resources;

using Debug = System.Diagnostics.Debug;

namespace Cadru.Data.Csv
{
    using System.Text;
    using Cadru.Data.IO;

    /// <summary>
    /// Represents a reader that provides fast, non-cached, forward-only access to CSV data.
    /// </summary>
#if NETSTANDARD1_3
    public partial class CsvReader : IEnumerable<string[]>, IDisposable
#else
    public partial class CsvReader : IDataReader, IEnumerable<string[]>
#endif
    {
        /// <summary>
        /// Defines the default buffer size.
        /// </summary>
        public const int DefaultBufferSize = 0x1000;

        /// <summary>
        /// Defines the default delimiter character separating each field.
        /// </summary>
        public const char DefaultDelimiter = ',';

        /// <summary>
        /// Defines the default quote character wrapping every field.
        /// </summary>
        public const char DefaultQuote = '"';

        /// <summary>
        /// Defines the default escape character letting insert quotation characters inside a quoted field.
        /// </summary>
        public const char DefaultEscape = '"';

        /// <summary>
        /// Defines the default comment character indicating that a line is commented out.
        /// </summary>
        public const char DefaultComment = '#';

        /// <summary>
        /// Defines the default value for AddMark indicating should the CsvReader add null bytes removal mark ([removed x null bytes])
        /// </summary>
        private const bool DefaultAddMark = false;

        /// <summary>
        /// Defines the default value for Threshold indicating when the CsvReader should replace/remove consecutive null bytes
        /// </summary>
        private const int DefaultThreshold = 60;

        /// <summary>
        /// Contains the <see cref="T:TextReader"/> pointing to the CSV file.
        /// </summary>
        private TextReader _reader;

        /// <summary>
        /// Indicates if the class is initialized.
        /// </summary>
        private bool _initialized;

        /// <summary>
        /// Contains the dictionary of field indexes by header. The key is the field name and the value is its index.
        /// </summary>
        private Dictionary<string, int> _fieldHeaderIndexes;

        /// <summary>
        /// Contains the starting position of the next unread field.
        /// </summary>
        private int _nextFieldStart;

        /// <summary>
        /// Contains the index of the next unread field.
        /// </summary>
        private int _nextFieldIndex;

        /// <summary>
        /// Contains the array of the field values for the current record.
        /// A null value indicates that the field have not been parsed.
        /// </summary>
        private string[] _fields;

        /// <summary>
        /// Contains the maximum number of fields to retrieve for each record.
        /// </summary>
        private int _fieldCount;

        /// <summary>
        /// Contains the read buffer.
        /// </summary>
        private char[] _buffer;

        /// <summary>
        /// Contains the current read buffer length.
        /// </summary>
        private int _bufferLength;

        /// <summary>
        /// Indicates if the end of the reader has been reached.
        /// </summary>
        private bool _eof;

        /// <summary>
        /// Indicates if the last read operation reached an EOL character.
        /// </summary>
        private bool _eol;

        /// <summary>
        /// Indicates if the first record is in cache.
        /// This can happen when initializing a reader with no headers
        /// because one record must be read to get the field count automatically
        /// </summary>
        private bool _firstRecordInCache;

        /// <summary>
        /// Like CsvReader(TextReader reader, bool hasHeaders) but removes consecutive null bytes above a threshold from source stream.
        /// </summary>
        /// <param name="stream">A <see cref="T:Stream"/> pointing to the CSV file.</param>
        /// <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
        /// <param name="encoding"> specifies the encoding of the underlying stream.</param>
        /// <param name="addMark"><see langword="true"/> if want to add a mark ([removed x null bytes]) to indicate removal, remove silently if <see langword="false"/>.</param>
        /// <param name="threshold">only consecutive null bytes above this threshold will be removed or replaced by a mark.</param>
        /// <exception cref="T:ArgumentNullException"><paramref name="stream"/> is a <see langword="null"/>.</exception>
        /// <exception cref="T:ArgumentException">Cannot read from <paramref name="stream"/>.</exception>
        public CsvReader(Stream stream, bool hasHeaders, Encoding encoding, bool addMark = DefaultAddMark, int threshold = DefaultThreshold)
            : this(new NullRemovalStreamReader(stream, addMark, threshold, encoding), hasHeaders)
        {
        }

        /// <summary>
        /// Like CsvReader(TextReader reader, bool hasHeaders, int bufferSize) but removes consecutive null bytes above a threshold from source stream.
        /// </summary>
        /// <param name="stream">A <see cref="T:Stream"/> pointing to the CSV file.</param>
        /// <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
        /// <param name="encoding"> specifies the encoding of the underlying stream.</param>
        /// <param name="bufferSize">The buffer size in bytes.</param>
        /// <param name="addMark"><see langword="true"/> if want to add a mark ([removed x null bytes]) to indicate removal, remove silently if <see langword="false"/>.</param>
        /// <param name="threshold"> only consecutive null bytes above this threshold will be removed or replaced by a mark.</param>
        /// <exception cref="T:ArgumentNullException"><paramref name="stream"/> is a <see langword="null"/>.</exception>
        /// <exception cref="T:ArgumentException">Cannot read from <paramref name="stream"/>.</exception>
        public CsvReader(Stream stream, bool hasHeaders, Encoding encoding, int bufferSize, bool addMark = DefaultAddMark, int threshold = DefaultThreshold)
            : this(new NullRemovalStreamReader(stream, addMark, threshold, encoding, bufferSize), hasHeaders, bufferSize)
        {
        }

        /// <summary>
        /// Like CsvReader(TextReader reader, bool hasHeaders, char delimiter) but removes consecutive null bytes above a threshold from source stream.
        /// </summary>
        /// <param name="stream">A <see cref="T:Stream"/> pointing to the CSV file.</param>
        /// <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
        /// <param name="encoding"> specifies the encoding of the underlying stream.</param>
        /// <param name="delimiter">The delimiter character separating each field (default is ',').</param>
        /// <param name="addMark"><see langword="true"/> if want to add a mark ([removed x null bytes]) to indicate removal, remove silently if <see langword="false"/>.</param>
        /// <param name="threshold"> only consecutive null bytes above this threshold will be removed or replaced by a mark.</param>
        /// <exception cref="T:ArgumentNullException"><paramref name="stream"/> is a <see langword="null"/>.</exception>
        /// <exception cref="T:ArgumentException">Cannot read from <paramref name="stream"/>.</exception>
        public CsvReader(Stream stream, bool hasHeaders, Encoding encoding, char delimiter, bool addMark = DefaultAddMark, int threshold = DefaultThreshold)
            : this(new NullRemovalStreamReader(stream, addMark, threshold, encoding), hasHeaders, delimiter)
        {
        }

        /// <summary>
        /// Like CsvReader(TextReader reader, bool hasHeaders, char delimiter, int bufferSize) but removes consecutive null bytes above a threshold from source stream.
        /// </summary>
        /// <param name="stream">A <see cref="T:Stream"/> pointing to the CSV file.</param>
        /// <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
        /// <param name="encoding"> specifies the encoding of the underlying stream.</param>
        /// <param name="delimiter">The delimiter character separating each field (default is ',').</param>
        /// <param name="bufferSize">The buffer size in bytes.</param>
        /// <param name="addMark"><see langword="true"/> if want to add a mark ([removed x null bytes]) to indicate removal, remove silently if <see langword="false"/>.</param>
        /// <param name="threshold"> only consecutive null bytes above this threshold will be removed or replaced by a mark.</param>
        /// <exception cref="T:ArgumentNullException"><paramref name="stream"/> is a <see langword="null"/>.</exception>
        /// <exception cref="T:ArgumentException">Cannot read from <paramref name="stream"/>.</exception>
        public CsvReader(Stream stream, bool hasHeaders, Encoding encoding, char delimiter, int bufferSize, bool addMark = DefaultAddMark, int threshold = DefaultThreshold)
            : this(new NullRemovalStreamReader(stream, addMark, threshold, encoding, bufferSize), hasHeaders, delimiter, bufferSize)
        {
        }

        /// <summary>
        /// Like CsvReader(TextReader reader, bool hasHeaders, char delimiter, char quote, char escape, char comment, ValueTrimmingOptions trimmingOptions, string nullValue)
        /// but removes consecutive null bytes above a threshold from source stream.
        /// </summary>
        /// <param name="stream">A <see cref="T:Stream"/> pointing to the CSV file.</param>
        /// <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
        /// <param name="encoding"> specifies the encoding of the underlying stream.</param>
        /// <param name="delimiter">The delimiter character separating each field (default is ',').</param>
        /// <param name="quote">The quotation character wrapping every field (default is ''').</param>
        /// <param name="escape">
        /// The escape character letting insert quotation characters inside a quoted field (default is '\').
        /// If no escape character, set to '\0' to gain some performance.
        /// </param>
        /// <param name="comment">The comment character indicating that a line is commented out (default is '#').</param>
        /// <param name="trimmingOptions">Determines which values should be trimmed.</param>
        /// <param name="nullValue">The value which denotes a DbNull-value.</param>
        /// <param name="addMark"><see langword="true"/> if want to add a mark ([removed x null bytes]) to indicate removal, remove silently if <see langword="false"/>.</param>
        /// <param name="threshold"> only consecutive null bytes above this threshold will be removed or replaced by a mark.</param>
        /// <exception cref="T:ArgumentNullException"><paramref name="stream"/> is a <see langword="null"/>.</exception>
        /// <exception cref="T:ArgumentException">Cannot read from <paramref name="stream"/>.</exception>
        public CsvReader(Stream stream, bool hasHeaders, Encoding encoding, char delimiter, char quote, char escape, char comment, ValueTrimmingOptions trimmingOptions, string nullValue = null, bool addMark = DefaultAddMark, int threshold = DefaultThreshold)
            : this(new NullRemovalStreamReader(stream, addMark, threshold, encoding), hasHeaders, delimiter, quote, escape, comment, trimmingOptions, DefaultBufferSize, nullValue)
        {
        }

        /// <summary>
        ///     Like CsvReader(TextReader reader, bool hasHeaders, char delimiter, char quote, char escape, char comment, ValueTrimmingOptions trimmingOptions, int bufferSize, string nullValue)
        ///     but removes consecutive null bytes above a threshold from source stream.
        /// </summary>
        /// <param name="stream">A <see cref="T:Stream"/> pointing to the CSV file.</param>
        /// <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
        /// <param name="encoding"> specifies the encoding of the underlying stream.</param>
        /// <param name="delimiter">The delimiter character separating each field (default is ',').</param>
        /// <param name="quote">The quotation character wrapping every field (default is ''').</param>
        /// <param name="escape">
        /// The escape character letting insert quotation characters inside a quoted field (default is '\').
        /// If no escape character, set to '\0' to gain some performance.
        /// </param>
        /// <param name="comment">The comment character indicating that a line is commented out (default is '#').</param>
        /// <param name="trimmingOptions">Determines which values should be trimmed.</param>
        /// <param name="bufferSize">The buffer size in bytes.</param>
        /// <param name="nullValue">The value which denotes a DbNull-value.</param>
        /// <param name="addMark"><see langword="true"/> if want to add a mark ([removed x null bytes]) to indicate removal, remove silently if <see langword="false"/>.</param>
        /// <param name="threshold"> only consecutive null bytes above this threshold will be removed or replace by a mark.</param>
        /// <exception cref="T:ArgumentNullException"><paramref name="stream"/> is a <see langword="null"/>.</exception>
        /// <exception cref="T:ArgumentException">Cannot read from <paramref name="stream"/>.</exception>
        public CsvReader(Stream stream, bool hasHeaders, Encoding encoding, char delimiter, char quote, char escape, char comment, ValueTrimmingOptions trimmingOptions, int bufferSize, string nullValue = null, bool addMark = DefaultAddMark, int threshold = DefaultThreshold)
            : this(new NullRemovalStreamReader(stream, addMark, threshold, encoding, bufferSize), hasHeaders, delimiter, quote, escape, comment, trimmingOptions, bufferSize, nullValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CsvReader class.
        /// </summary>
        /// <param name="reader">A <see cref="T:TextReader"/> pointing to the CSV file.</param>
        /// <param name="hasHeaders"><see langword="true"/>If field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
        /// <exception cref="T:ArgumentNullException"><paramref name="reader"/> is a <see langword="null"/>.</exception>
        /// <exception cref="T:ArgumentException">Cannot read from <paramref name="reader"/>.</exception>
        public CsvReader(TextReader reader, bool hasHeaders) : this(reader, hasHeaders, DefaultDelimiter, DefaultQuote)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CsvReader class.
        /// </summary>
        /// <param name="reader">A <see cref="T:TextReader"/> pointing to the CSV file.</param>
        /// <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
        /// <param name="bufferSize">The buffer size in bytes.</param>
        /// <exception cref="T:ArgumentNullException"><paramref name="reader"/> is a <see langword="null"/>.</exception>
        /// <exception cref="T:ArgumentException">Cannot read from <paramref name="reader"/>.</exception>
        public CsvReader(TextReader reader, bool hasHeaders, int bufferSize) : this(reader, hasHeaders, DefaultDelimiter, DefaultQuote, bufferSize: bufferSize)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CsvReader class.
        /// </summary>
        /// <param name="reader">A <see cref="T:TextReader"/> pointing to the CSV file.</param>
        /// <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
        /// <param name="delimiter">The delimiter character separating each field (default is ',').</param>
        /// <exception cref="T:ArgumentNullException"><paramref name="reader"/> is a <see langword="null"/>.</exception>
        /// <exception cref="T:ArgumentException">Cannot read from <paramref name="reader"/>.</exception>
        public CsvReader(TextReader reader, bool hasHeaders, char delimiter) : this(reader, hasHeaders, delimiter, DefaultQuote)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CsvReader class.
        /// </summary>
        /// <param name="reader">A <see cref="T:TextReader"/> pointing to the CSV file.</param>
        /// <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
        /// <param name="delimiter">The delimiter character separating each field (default is ',').</param>
        /// <param name="bufferSize">The buffer size in bytes.</param>
        /// <exception cref="T:ArgumentNullException"><paramref name="reader"/> is a <see langword="null"/>.</exception>
        /// <exception cref="T:ArgumentException">Cannot read from <paramref name="reader"/>.</exception>
        public CsvReader(TextReader reader, bool hasHeaders, char delimiter, int bufferSize) : this(reader, hasHeaders, delimiter, DefaultQuote, bufferSize: bufferSize)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CsvReader class.
        /// </summary>
        /// <param name="reader">A <see cref="T:TextReader"/> pointing to the CSV file.</param>
        /// <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
        /// <param name="delimiter">The delimiter character separating each field (default is ',').</param>
        /// <param name="quote">The quotation character wrapping every field (default is ''').</param>
        /// <param name="escape">
        /// The escape character letting insert quotation characters inside a quoted field (default is '\').
        /// If no escape character, set to '\0' to gain some performance.
        /// </param>
        /// <param name="comment">The comment character indicating that a line is commented out (default is '#').</param>
        /// <param name="trimmingOptions">Determines which values should be trimmed.</param>
        /// <param name="bufferSize">The buffer size in bytes.</param>
        /// <param name="nullValue">The value which denotes a DbNull-value.</param>
        /// <exception cref="T:ArgumentNullException"><paramref name="reader"/> is a <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="bufferSize"/> must be 1 or more.</exception>
        public CsvReader(TextReader reader, bool hasHeaders = true, char delimiter = DefaultDelimiter, char quote = DefaultQuote, char escape = DefaultEscape, char comment = DefaultComment, ValueTrimmingOptions trimmingOptions = ValueTrimmingOptions.UnquotedOnly, int bufferSize = DefaultBufferSize, string nullValue = null)
        {
#if DEBUG
#if !NETSTANDARD1_3
            this._allocStack = new System.Diagnostics.StackTrace();
#endif
#endif

            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bufferSize), bufferSize, Strings.BufferSizeTooSmall);
            }

            this.BufferSize = bufferSize;

            var streamReader = reader as StreamReader;
            if (streamReader != null)
            {
                var stream = streamReader.BaseStream;

                if (stream.CanSeek)
                {
                    // Handle bad implementations returning 0 or less
                    if (stream.Length > 0)
                    {
                        this.BufferSize = (int)Math.Min(bufferSize, stream.Length);
                    }
                }
            }

            this._reader = reader;
            this.Delimiter = delimiter;
            this.Quote = quote;
            this.Escape = escape;
            this.Comment = comment;

            this.HasHeaders = hasHeaders;
            this.TrimmingOption = trimmingOptions;
            this.NullValue = nullValue;
            this.SupportsMultiline = true;
            this.SkipEmptyLines = true;

            this.Columns = new List<Column>();
            this.DefaultHeaderName = "Column";

            this.FileRecordIndex = -1;
            this.DefaultParseErrorAction = ParseErrorAction.RaiseEvent;
        }

        /// <summary>
        /// Occurs when there is an error while parsing the CSV stream.
        /// </summary>
        public event EventHandler<ParseErrorEventArgs> ParseError;

        /// <summary>
        /// Raises the <see cref="M:ParseError"/> event.
        /// </summary>
        /// <param name="e">The <see cref="ParseErrorEventArgs"/> that contains the event data.</param>
        protected virtual void OnParseError(ParseErrorEventArgs e)
        {
            var handler = ParseError;

            handler?.Invoke(this, e);
        }

        /// <summary>
        /// Occurs when HasHeaders is true and a duplicate Column Header Name is encountered.
        /// Setting the HeaderName property on this column will prevent the library from throwing a duplicate key exception
        /// </summary>
        public event EventHandler<DuplicateHeaderEventArgs> DuplicateHeaderEncountered;

        /// <summary>
        /// Gets the comment character indicating that a line is commented out.
        /// </summary>
        /// <value>The comment character indicating that a line is commented out.</value>
        public char Comment { get; }

        /// <summary>
        /// Gets the escape character letting insert quotation characters inside a quoted field.
        /// </summary>
        /// <value>The escape character letting insert quotation characters inside a quoted field.</value>
        public char Escape { get; }

        /// <summary>
        /// Gets the delimiter character separating each field.
        /// </summary>
        /// <value>The delimiter character separating each field.</value>
        public char Delimiter { get; }

        /// <summary>
        /// Gets the quotation character wrapping every field.
        /// </summary>
        /// <value>The quotation character wrapping every field.</value>
        public char Quote { get; }

        /// <summary>
        /// Indicates if field names are located on the first non commented line.
        /// </summary>
        /// <value><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</value>
        public bool HasHeaders { get; }

        /// <summary>
        /// Indicates if spaces at the start and end of a field are trimmed.
        /// </summary>
        /// <value><see langword="true"/> if spaces at the start and end of a field are trimmed, otherwise, <see langword="false"/>.</value>
        public ValueTrimmingOptions TrimmingOption { get; }

        /// <summary>
        /// Contains the value which denotes a DbNull-value.
        /// </summary>
        public string NullValue { get; }

        /// <summary>
        /// Gets the buffer size.
        /// </summary>
        public int BufferSize { get; }

        /// <summary>
        /// Gets or sets the default action to take when a parsing error has occured.
        /// </summary>
        /// <value>The default action to take when a parsing error has occured.</value>
        public ParseErrorAction DefaultParseErrorAction { get; set; }

        /// <summary>
        /// Gets or sets the action to take when a field is missing.
        /// </summary>
        /// <value>The action to take when a field is missing.</value>
        public MissingFieldAction MissingFieldAction { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the reader supports multiline fields.
        /// </summary>
        /// <value>A value indicating if the reader supports multiline field.</value>
        public bool SupportsMultiline { get; set; }

        /// <summary>
        /// Gets or sets a value giving a maxmimum length (in bytes) for any quoted field.
        /// </summary>
        /// <value>The maximum length (in bytes) of a CSV field.</value>
        public int? MaxQuotedFieldLength { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the reader will skip empty lines.
        /// </summary>
        /// <value>A value indicating if the reader will skip empty lines.</value>
        public bool SkipEmptyLines { get; set; }

        /// <summary>
        /// Gets or sets the default header name when it is an empty string or only whitespaces.
        /// The header index will be appended to the specified name.
        /// </summary>
        /// <value>The default header name when it is an empty string or only whitespaces.</value>
        public string DefaultHeaderName { get; set; }

        /// <summary>
        /// Gets or sets column information for the CSV.
        /// </summary>
        public IList<Column> Columns { get; set; }

        /// <summary>
        /// Gets or sets whether we should use the column default values if the field is not in the record.
        /// </summary>
        public bool UseColumnDefaults { get; set; }

        /// <summary>
        /// Gets the maximum number of fields to retrieve for each record.
        /// </summary>
        /// <value>The maximum number of fields to retrieve for each record.</value>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">The instance has been disposed of.</exception>
        public int FieldCount
        {
            get
            {
                this.EnsureInitialize();
                return this._fieldCount;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the current stream position is at the end of the stream.
        /// </summary>
        /// <value><see langword="true"/> if the current stream position is at the end of the stream; otherwise <see langword="false"/>.</value>
        public virtual bool EndOfStream { get; private set; }

        /// <summary>
        /// Gets the field headers.
        /// </summary>
        /// <returns>The field headers or an empty array if headers are not supported.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">The instance has been disposed of.</exception>
        public string[] GetFieldHeaders()
        {
            this.EnsureInitialize();
            Debug.Assert(this.Columns != null, "Columns must be non null.");

            var fieldHeaders = new string[this.Columns.Count];

            for (var i = 0; i < fieldHeaders.Length; i++)
            {
                fieldHeaders[i] = this.Columns[i].Name;
            }

            return fieldHeaders;
        }

        /// <summary>
        /// Gets the current record index in the CSV file.
        /// <para>
        /// A value of <see cref="M:Int32.MinValue"/> means that the reader has not been initialized yet.
        /// Otherwise, a negative value means that no record has been read yet.
        /// </para>
        /// </summary>
        /// <value>The current record index in the CSV file.</value>
        protected long FileRecordIndex { get; private set; }

        /// <summary>
        /// Gets the current record index in the CSV file.
        /// <para>
        /// A value of <see cref="M:Int32.MinValue"/> means that the reader has not been initialized yet.
        /// Otherwise, a negative value means that no record has been read yet.
        /// </para>
        /// </summary>
        /// <value>The current record index in the CSV file.</value>
        public virtual long CurrentRecordIndex => this.FileRecordIndex;

        /// <summary>
        /// Indicates if one or more field are missing for the current record.
        /// Resets after each successful record read.
        /// </summary>
        public bool MissingFieldFlag { get; private set; }

        /// <summary>
        /// Indicates if a parse error occured for the current record.
        /// Resets after each successful record read.
        /// </summary>
        public bool ParseErrorFlag { get; private set; }

        /// <summary>
        /// Gets the field with the specified name and record position. <see cref="M:hasHeaders"/> must be <see langword="true"/>.
        /// </summary>
        /// <value>
        /// The field with the specified name and record position.
        /// </value>
        /// <exception cref="T:ArgumentNullException"><paramref name="field"/> is <see langword="null"/> or an empty string.</exception>
        /// <exception cref="T:InvalidOperationException">The CSV does not have headers (<see cref="M:HasHeaders"/> property is <see langword="false"/>).</exception>
        /// <exception cref="T:ArgumentException"><paramref name="field"/> not found.</exception>
        /// <exception cref="T:ArgumentOutOfRangeException">Record index must be > 0.</exception>
        /// <exception cref="T:InvalidOperationException">Cannot move to a previous record in forward-only mode.</exception>
        /// <exception cref="T:EndOfStreamException">Cannot read record at <paramref name="record"/>.</exception>
        /// <exception cref="T:MalformedCsvException">The CSV appears to be corrupt at the current position.</exception>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">The instance has been disposed of.</exception>
        public string this[int record, string field]
        {
            get
            {
                if (!this.MoveTo(record))
                {
                    throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, Strings.CannotReadRecordAtIndex, record));
                }

                return this[field];
            }
        }

        /// <summary>
        /// Gets the field at the specified index and record position.
        /// </summary>
        /// <value>
        /// The field at the specified index and record position.
        /// A <see langword="null"/> is returned if the field cannot be found for the record.
        /// </value>
        /// <exception cref="T:ArgumentOutOfRangeException"><paramref name="field"/> must be included in [0, <see cref="M:FieldCount"/>[.</exception>
        /// <exception cref="T:ArgumentOutOfRangeException">Record index must be > 0.</exception>
        /// <exception cref="T:InvalidOperationException">Cannot move to a previous record in forward-only mode.</exception>
        /// <exception cref="T:EndOfStreamException">Cannot read record at <paramref name="record"/>.</exception>
        /// <exception cref="T:MalformedCsvException">The CSV appears to be corrupt at the current position.</exception>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">The instance has been disposed of.</exception>
        public string this[int record, int field]
        {
            get
            {
                if (!this.MoveTo(record))
                {
                    throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, Strings.CannotReadRecordAtIndex, record));
                }

                return this[field];
            }
        }

        /// <summary>
        /// Gets the field with the specified name. <see cref="M:hasHeaders"/> must be <see langword="true"/>.
        /// </summary>
        /// <value>
        /// The field with the specified name.
        /// </value>
        /// <exception cref="T:ArgumentNullException"><paramref name="field"/> is <see langword="null"/> or an empty string.</exception>
        /// <exception cref="T:InvalidOperationException">The CSV does not have headers (<see cref="M:HasHeaders"/> property is <see langword="false"/>).</exception>
        /// <exception cref="T:ArgumentException"><paramref name="field"/> not found.</exception>
        /// <exception cref="T:MalformedCsvException">The CSV appears to be corrupt at the current position.</exception>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">The instance has been disposed of.</exception>
        public string this[string field]
        {
            get
            {
                if (String.IsNullOrEmpty(field))
                {
                    throw new ArgumentNullException(nameof(field));
                }

                if (!this.HasHeaders)
                {
                    throw new InvalidOperationException(Strings.NoHeaders);
                }

                var index = this.GetFieldIndex(field);

                if (index < 0)
                {

/* Unmerged change from project 'Cadru.Data (netstandard1.3)'
Before:
                    throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, Strings.FieldHeaderNotFound, field), "field");
After:
                    throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, Strings.FieldHeaderNotFound, field), "field));
*/
                    throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, Strings.FieldHeaderNotFound, field), nameof(field));
                }

                return this[index];
            }
        }

        /// <summary>
        /// Gets the field at the specified index.
        /// </summary>
        /// <value>The field at the specified index.</value>
        /// <exception cref="T:ArgumentOutOfRangeException"><paramref name="field"/> must be included in [0, <see cref="M:FieldCount"/>[.</exception>
        /// <exception cref="T:InvalidOperationException">No record read yet. Call ReadLine() first.</exception>
        /// <exception cref="T:MalformedCsvException">The CSV appears to be corrupt at the current position.</exception>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">The instance has been disposed of.</exception>
        public virtual string this[int field] => this.ReadField(field, false, false);

        /// <summary>
        /// Ensures that the reader is initialized.
        /// </summary>
        private void EnsureInitialize()
        {
            if (!this._initialized)
            {
                this.ReadNextRecord(true, false);
            }

            Debug.Assert(this.Columns != null);
            Debug.Assert(this.Columns.Count > 0 || (this.Columns.Count == 0 && (this._fieldHeaderIndexes == null || this._fieldHeaderIndexes.Count == 0)));
        }

        /// <summary>
        /// Gets the field index for the provided header.
        /// </summary>
        /// <param name="header">The header to look for.</param>
        /// <returns>The field index for the provided header. -1 if not found.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">The instance has been disposed of.</exception>
        public int GetFieldIndex(string header)
        {
            this.EnsureInitialize();

            if (this._fieldHeaderIndexes != null && this._fieldHeaderIndexes.TryGetValue(header, out var index))
            {
                return index;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Checks if a header exists in the current fieldHedaerIndexes
        /// </summary>
        /// <param name="header">The header to look for.</param>
        /// <returns>A flag indicating if the header exists</returns>
        public bool HasHeader(string header)
        {
            this.EnsureInitialize();

            if (String.IsNullOrEmpty(header))
            {
                throw new ArgumentNullException(nameof(header));
            }

            if (this._fieldHeaderIndexes != null)
            {
                return this._fieldHeaderIndexes.ContainsKey(header);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Copies the field array of the current record to a one-dimensional array, starting at the beginning of the target array.
        /// </summary>
        /// <param name="array"> The one-dimensional <see cref="T:Array"/> that is the destination of the fields of the current record.</param>
        /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="T:ArgumentNullException"><paramref name="array"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:ArgumentOutOfRangeException"><paramref name="index"/> is les than zero or is equal to or greater than the length <paramref name="array"/>.</exception>
        /// <exception cref="InvalidOperationException">No current record.</exception>
        /// <exception cref="ArgumentException">The number of fields in the record is greater than the available space from <paramref name="index"/> to the end of <paramref name="array"/>.</exception>
        public void CopyCurrentRecordTo(string[] array, int index = 0)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (index < 0 || index >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, String.Empty);
            }

            if (this.FileRecordIndex < 0 || !this._initialized)
            {
                throw new InvalidOperationException(Strings.NoCurrentRecord);
            }

            if (array.Length - index < this._fieldCount)
            {
                throw new ArgumentException(Strings.NotEnoughSpaceInArray, nameof(array));
            }

            for (var i = 0; i < this._fieldCount; i++)
            {
                array[index + i] = this.ParseErrorFlag ? null : this[i];
            }
        }

        /// <summary>
        /// Gets the current raw CSV data.
        /// </summary>
        /// <remarks>Used for exception handling purpose.</remarks>
        /// <returns>The current raw CSV data.</returns>
        public string GetCurrentRawData()
        {
            if (this._buffer != null && this._bufferLength > 0)
            {
                return new string(this._buffer, 0, this._bufferLength);
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Indicates whether the specified Unicode character is categorized as white space.
        /// </summary>
        /// <param name="c">A Unicode character.</param>
        /// <returns><see langword="true"/> if <paramref name="c"/> is white space; otherwise, <see langword="false"/>.</returns>
        private bool IsWhiteSpace(char c)
        {
            // Handle cases where the delimiter is a whitespace (e.g. tab)
            if (c == this.Delimiter)
            {
                return false;
            }
            else
            {
                // See char.IsLatin1(char c) in Reflector
                if (c <= '\x00ff')
                {
                    return (c == ' ' || c == '\t');
                }
                else
                {
                    return (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.SpaceSeparator);
                }
            }
        }

        /// <summary>
        /// Moves to the specified record index.
        /// </summary>
        /// <param name="record">The record index.</param>
        /// <returns><c>true</c> if the operation was successful; otherwise, <c>false</c>.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">The instance has been disposed of.</exception>
        public virtual bool MoveTo(long record)
        {
            if (record < this.FileRecordIndex)
            {
                return false;
            }

            // Get number of record to read
            var offset = record - this.FileRecordIndex;

            while (offset > 0)
            {
                if (!this.ReadNextRecord())
                {
                    return false;
                }

                offset--;
            }

            return true;
        }

        /// <summary>
        /// Parses a new line delimiter.
        /// </summary>
        /// <param name="pos">The starting position of the parsing. Will contain the resulting end position.</param>
        /// <returns><see langword="true"/> if a new line delimiter was found; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">The instance has been disposed of.</exception>
        private bool ParseNewLine(ref int pos)
        {
            Debug.Assert(pos <= this._bufferLength);

            // Check if already at the end of the buffer
            if (pos == this._bufferLength)
            {
                pos = 0;

                if (!this.ReadBuffer())
                {
                    return false;
                }
            }

            var c = this._buffer[pos];

            // Treat \r as new line only if it's not the delimiter
            if (c == '\r' && this.Delimiter != '\r')
            {
                pos++;

                // Skip following \n (if there is one)

                if (pos < this._bufferLength)
                {
                    if (this._buffer[pos] == '\n')
                    {
                        pos++;
                    }
                }
                else
                {
                    if (this.ReadBuffer())
                    {
                        if (this._buffer[0] == '\n')
                        {
                            pos = 1;
                        }
                        else
                        {
                            pos = 0;
                        }
                    }
                }

                if (pos >= this._bufferLength)
                {
                    this.ReadBuffer();
                    pos = 0;
                }

                return true;
            }
            else if (c == '\n')
            {
                pos++;

                if (pos >= this._bufferLength)
                {
                    this.ReadBuffer();
                    pos = 0;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the character at the specified position is a new line delimiter.
        /// </summary>
        /// <param name="pos">The position of the character to verify.</param>
        /// <returns><see langword="true"/> if the character at the specified position is a new line delimiter; otherwise, <see langword="false"/>.</returns>
        private bool IsNewLine(int pos)
        {
            Debug.Assert(pos < this._bufferLength);

            var c = this._buffer[pos];

            if (c == '\n')
            {
                return true;
            }
            else if (c == '\r' && this.Delimiter != '\r')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Fills the buffer with data from the reader.
        /// </summary>
        /// <returns><see langword="true"/> if data was successfully read; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">The instance has been disposed of.</exception>
        private bool ReadBuffer()
        {
            if (this._eof)
            {
                return false;
            }

            this.CheckDisposed();

            this._bufferLength = this._reader.Read(this._buffer, 0, this.BufferSize);

            if (this._bufferLength > 0)
            {
                return true;
            }
            else
            {
                this._eof = true;
                this._buffer = null;

                return false;
            }
        }

        /// <summary>
        /// Reads the field at the specified index.
        /// Any unread fields with an inferior index will also be read as part of the required parsing.
        /// </summary>
        /// <param name="field">The field index.</param>
        /// <param name="initializing">Indicates if the reader is currently initializing.</param>
        /// <param name="discardValue">Indicates if the value(s) are discarded.</param>
        /// <returns>
        /// The field at the specified index.
        /// A <see langword="null"/> indicates that an error occured or that the last field has been reached during initialization.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="field"/> is out of range.</exception>
        /// <exception cref="InvalidOperationException">There is no current record.</exception>
        /// <exception cref="MissingFieldCsvException">The CSV data appears to be missing a field.</exception>
        /// <exception cref="MalformedCsvException">The CSV data appears to be malformed.</exception>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">The instance has been disposed of.</exception>
        private string ReadField(int field, bool initializing, bool discardValue)
        {
            if (!initializing)
            {
                var maxField = this.UseColumnDefaults ? this.Columns.Count : this._fieldCount;
                if (field < 0 || field >= maxField)
                {
                    throw new ArgumentOutOfRangeException(nameof(field), field, String.Format(CultureInfo.InvariantCulture, Strings.FieldIndexOutOfRange, field));
                }

                if (this.FileRecordIndex < 0)
                {
                    throw new InvalidOperationException(Strings.NoCurrentRecord);
                }

                if (this.Columns.Count > field && !String.IsNullOrEmpty(this.Columns[field].OverrideValue))
                {
                    // Use the override value for this column.
                    return this.Columns[field].OverrideValue;
                }

                if (field >= this._fieldCount)
                {
                    // Use the column default as UseColumnDefaults is true at this point
                    return this.Columns[field].DefaultValue;
                }

                // Directly return field if cached
                if (this._fields[field] != null)
                {
                    return this._fields[field];
                }

                if (this.MissingFieldFlag)
                {
                    return this.HandleMissingField(null, field, ref this._nextFieldStart);
                }
            }

            this.CheckDisposed();

            var index = this._nextFieldIndex;

            while (index < field + 1)
            {
                // Handle case where stated start of field is past buffer
                // This can occur because _nextFieldStart is simply 1 + last char position of previous field
                if (this._nextFieldStart == this._bufferLength)
                {
                    this._nextFieldStart = 0;

                    // Possible EOF will be handled later (see Handle_EOF1)
                    this.ReadBuffer();
                }

                StringBuilder value = null;

                if (this.MissingFieldFlag)
                {
                    var result = this.HandleMissingField(value?.ToString(), index, ref this._nextFieldStart);
                    if (value == null && result == String.Empty && this.MissingFieldAction == MissingFieldAction.ReplaceByEmpty)
                    {
                        value = new StringBuilder();
                    }
                }
                else if (this._nextFieldStart == this._bufferLength)
                {
                    // Handle_EOF1: Handle EOF here

                    // If current field is the requested field, then the value of the field is "" as in "f1,f2,f3,(\s*)"
                    // otherwise, the CSV is malformed

                    if (index == field)
                    {
                        if (!discardValue)
                        {
                            value = new StringBuilder();
                            this._fields[index] = String.Empty;
                        }

                        this.MissingFieldFlag = true;
                    }
                    else
                    {
                        var result = this.HandleMissingField(value?.ToString(), index, ref this._nextFieldStart);
                        if (value == null && result == String.Empty && this.MissingFieldAction == MissingFieldAction.ReplaceByEmpty)
                        {
                            value = new StringBuilder();
                        }
                    }
                }
                else
                {
                    // Trim spaces at start
                    if ((this.TrimmingOption & ValueTrimmingOptions.UnquotedOnly) != 0)
                    {
                        this.SkipWhiteSpaces(ref this._nextFieldStart);
                    }

                    if (this._eof)
                    {
                        value = new StringBuilder();
                        this._fields[field] = String.Empty;

                        if (field < this._fieldCount)
                        {
                            this.MissingFieldFlag = true;
                        }
                    }
                    else if (this._buffer[this._nextFieldStart] != this.Quote)
                    {
                        // Non-quoted field

                        var start = this._nextFieldStart;
                        var pos = this._nextFieldStart;

                        for (; ; )
                        {
                            while (pos < this._bufferLength)
                            {
                                var c = this._buffer[pos];

                                if (c == this.Delimiter)
                                {
                                    this._nextFieldStart = pos + 1;

                                    break;
                                }
                                else if (c == '\r' || c == '\n')
                                {
                                    this._nextFieldStart = pos;
                                    this._eol = true;

                                    break;
                                }
                                else
                                {
                                    pos++;
                                }
                            }

                            if (pos < this._bufferLength)
                            {
                                break;
                            }
                            else
                            {
                                if (!discardValue)
                                {
                                    value = value ?? new StringBuilder();
                                    value.Append(this._buffer, start, pos - start);
                                }

                                start = 0;
                                pos = 0;
                                this._nextFieldStart = 0;

                                if (!this.ReadBuffer())
                                {
                                    break;
                                }
                            }
                        }

                        if (!discardValue)
                        {
                            if ((this.TrimmingOption & ValueTrimmingOptions.UnquotedOnly) == 0)
                            {
                                if (!this._eof && pos > start)
                                {
                                    value = value ?? new StringBuilder();
                                    value.Append(this._buffer, start, pos - start);
                                }
                            }
                            else
                            {
                                if (!this._eof && pos > start)
                                {
                                    // Do the trimming
                                    pos--;
                                    while (pos > -1 && this.IsWhiteSpace(this._buffer[pos]))
                                    {
                                        pos--;
                                    }

                                    pos++;

                                    if (pos > 0)
                                    {
                                        value = value ?? new StringBuilder();
                                        value.Append(this._buffer, start, pos - start);
                                    }
                                }
                                else
                                {
                                    pos = -1;
                                }

                                // If pos <= 0, that means the trimming went past buffer start,
                                // and the concatenated value needs to be trimmed too.
                                if (pos <= 0)
                                {
                                    pos = value?.Length - 1 ?? -1;

                                    // Do the trimming
                                    while (pos > -1 && this.IsWhiteSpace(value[pos]))
                                    {
                                        pos--;
                                    }

                                    pos++;

                                    if (pos > 0 && pos != value.Length)
                                    {
                                        value.Length = pos;
                                    }
                                }
                            }

                            value = value ?? new StringBuilder();
                        }

                        if (this._eol || this._eof)
                        {
                            this._eol = this.ParseNewLine(ref this._nextFieldStart);

                            // Reaching a new line is ok as long as the parser is initializing or it is the last field
                            if (!initializing && index != this._fieldCount - 1)
                            {
                                if (value != null && value.Length == 0)
                                {
                                    value = null;
                                }

                                var result = this.HandleMissingField(value?.ToString(), index, ref this._nextFieldStart);
                                if (value == null && result == String.Empty && this.MissingFieldAction == MissingFieldAction.ReplaceByEmpty)
                                {
                                    value = new StringBuilder();
                                }
                            }
                        }

                        if (!discardValue)
                        {
                            this._fields[index] = value?.ToString();
                        }
                    }
                    else
                    {
                        // Quoted field

                        // Skip quote
                        var start = this._nextFieldStart + 1;
                        var pos = start;

                        var quoted = true;
                        var escaped = false;
                        var fieldLength = 0;

                        if ((this.TrimmingOption & ValueTrimmingOptions.QuotedOnly) != 0)
                        {
                            this.SkipWhiteSpaces(ref start);
                            pos = start;
                        }

                        for (; ; )
                        {
                            while (pos < this._bufferLength)
                            {
                                var c = this._buffer[pos];

                                if (escaped)
                                {
                                    escaped = false;
                                    start = pos;
                                }
                                // IF current char is escape AND (escape and quote are different OR next char is a quote)
                                else if (c == this.Escape && (this.Escape != this.Quote || (pos + 1 < this._bufferLength && this._buffer[pos + 1] == this.Quote) || (pos + 1 == this._bufferLength && this._reader.Peek() == this.Quote)))
                                {
                                    if (!discardValue)
                                    {
                                        value = value ?? new StringBuilder();
                                        value.Append(this._buffer, start, pos - start);
                                    }

                                    escaped = true;
                                }
                                else if (c == this.Quote)
                                {
                                    quoted = false;
                                    break;
                                }

                                fieldLength++;

                                if (this.MaxQuotedFieldLength.HasValue && fieldLength > this.MaxQuotedFieldLength.Value)
                                {
                                    this.HandleParseError(new MalformedCsvException(this.GetCurrentRawData(), this._nextFieldStart, Math.Max(0, this.FileRecordIndex), index), ref this._nextFieldStart);
                                    return null;
                                }

                                pos++;
                            }

                            if (!quoted)
                            {
                                break;
                            }
                            else
                            {
                                if (!discardValue && !escaped)
                                {
                                    value = value ?? new StringBuilder();
                                    value.Append(this._buffer, start, pos - start);
                                }

                                start = 0;
                                pos = 0;
                                this._nextFieldStart = 0;

                                if (!this.ReadBuffer())
                                {
                                    this.HandleParseError(new MalformedCsvException(this.GetCurrentRawData(), this._nextFieldStart, Math.Max(0, this.FileRecordIndex), index), ref this._nextFieldStart);
                                    return null;
                                }
                            }
                        }

                        if (!this._eof)
                        {
                            // Append remaining parsed buffer content
                            if (!discardValue && pos > start)
                            {
                                value = value ?? new StringBuilder();
                                value.Append(this._buffer, start, pos - start);
                            }

                            if (!discardValue && value != null && (this.TrimmingOption & ValueTrimmingOptions.QuotedOnly) != 0)
                            {
                                var newLength = value.Length;
                                while (newLength > 0 && this.IsWhiteSpace(value[newLength - 1]))
                                {
                                    newLength--;
                                }

                                if (newLength < value.Length)
                                {
                                    value.Length = newLength;
                                }
                            }

                            // Skip quote
                            this._nextFieldStart = pos + 1;

                            // Skip whitespaces between the quote and the delimiter/eol
                            this.SkipWhiteSpaces(ref this._nextFieldStart);

                            // Skip delimiter
                            bool delimiterSkipped;
                            if (this._nextFieldStart < this._bufferLength && this._buffer[this._nextFieldStart] == this.Delimiter)
                            {
                                this._nextFieldStart++;
                                delimiterSkipped = true;
                            }
                            else if (this._nextFieldStart < this._bufferLength && (this._buffer[this._nextFieldStart] == '\r' || this._buffer[this._nextFieldStart] == '\n'))
                            {
                                this._nextFieldStart++;
                                this._eol = true;
                                delimiterSkipped = true;
                            }
                            else
                            {
                                delimiterSkipped = false;
                            }

                            // Skip new line delimiter if initializing or last field
                            // (if the next field is missing, it will be caught when parsed)
                            if (!this._eof && !delimiterSkipped && (initializing || index == this._fieldCount - 1))
                            {
                                this._eol = this.ParseNewLine(ref this._nextFieldStart);
                            }

                            // If no delimiter is present after the quoted field and it is not the last field, then it is a parsing error
                            if (!delimiterSkipped && !this._eof && !(this._eol || this.IsNewLine(this._nextFieldStart)))
                            {
                                this.HandleParseError(new MalformedCsvException(this.GetCurrentRawData(), this._nextFieldStart, Math.Max(0, this.FileRecordIndex), index), ref this._nextFieldStart);
                            }
                        }

                        // If we are at the end, then verify we have all the fields
                        if (this._eol || this._eof)
                        {
                            if (!initializing && index < this._fieldCount - 1)
                            {
                                var result = this.HandleMissingField(value?.ToString(), index, ref this._nextFieldStart);
                                if (value == null && result == String.Empty && this.MissingFieldAction == MissingFieldAction.ReplaceByEmpty)
                                {
                                    value = new StringBuilder();
                                }
                            }
                        }

                        if (!discardValue)
                        {
                            value = value ?? new StringBuilder();
                            this._fields[index] = value.ToString();
                        }
                    }
                }

                this._nextFieldIndex = Math.Max(index + 1, this._nextFieldIndex);

                if (index == field)
                {
                    // If initializing, return null to signify the last field has been reached

                    if (initializing)
                    {
                        if (this._eol || this._eof)
                        {
                            return null;
                        }
                        else
                        {
                            return value == null ? String.Empty : value.ToString();
                        }
                    }
                    else
                    {
                        return value?.ToString();
                    }
                }

                index++;
            }

            // Getting here is bad ...
            this.HandleParseError(new MalformedCsvException(this.GetCurrentRawData(), this._nextFieldStart, Math.Max(0, this.FileRecordIndex), index), ref this._nextFieldStart);
            return null;
        }

        /// <summary>
        /// Reads the next record.
        /// </summary>
        /// <returns><see langword="true"/> if a record has been successfully reads; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">The instance has been disposed of.</exception>
        public bool ReadNextRecord()
        {
            return this.ReadNextRecord(false, false);
        }

        /// <summary>
        /// Reads the next record.
        /// </summary>
        /// <param name="onlyReadHeaders">
        /// Indicates if the reader will proceed to the next record after having read headers.
        /// <see langword="true"/> if it stops after having read headers; otherwise, <see langword="false"/>.
        /// </param>
        /// <param name="skipToNextLine">
        /// Indicates if the reader will skip directly to the next line without parsing the current one.
        /// To be used when an error occurs.
        /// </param>
        /// <returns><see langword="true"/> if a record has been successfully reads; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">The instance has been disposed of.</exception>
        protected virtual bool ReadNextRecord(bool onlyReadHeaders, bool skipToNextLine)
        {
            if (this._eof)
            {
                if (this._firstRecordInCache)
                {
                    this._firstRecordInCache = false;
                    this.FileRecordIndex++;

                    return true;
                }
                else
                {
                    return false;
                }
            }

            this.CheckDisposed();

            if (!this._initialized)
            {
                this._buffer = new char[this.BufferSize];

                if (!this.ReadBuffer())
                {
                    return false;
                }

                if (!this.SkipEmptyAndCommentedLines(ref this._nextFieldStart))
                {
                    return false;
                }

                // Keep growing _fields array until the last field has been found
                // and then resize it to its final correct size

                this._fieldCount = 0;
                this._fields = new string[16];

                while (this.ReadField(this._fieldCount, true, false) != null)
                {
                    if (this.ParseErrorFlag)
                    {
                        this._fieldCount = 0;
                        Array.Clear(this._fields, 0, this._fields.Length);
                        this.ParseErrorFlag = false;
                        this._nextFieldIndex = 0;
                    }
                    else
                    {
                        this._fieldCount++;

                        if (this._fieldCount == this._fields.Length)
                        {
                            Array.Resize(ref this._fields, (this._fieldCount + 1) * 2);
                        }
                    }
                }

                // _fieldCount contains the last field index, but it must contains the field count,
                // so increment by 1
                this._fieldCount++;

                if (this._fields.Length != this._fieldCount)
                {
                    Array.Resize(ref this._fields, this._fieldCount);
                }

                this._fieldHeaderIndexes = new Dictionary<string, int>(this._fieldCount, StringComparer.CurrentCultureIgnoreCase);

                this._initialized = true;

                // If headers are present, call ReadNextRecord again
                if (this.HasHeaders)
                {
                    // Don't count first record as it was the headers
                    this.FileRecordIndex = -1;

                    this._firstRecordInCache = false;

                    for (var i = 0; i < this._fields.Length; i++)
                    {
                        var headerName = this._fields[i];
                        if (String.IsNullOrEmpty(headerName) || headerName.Trim().Length == 0)
                        {
                            headerName = this.DefaultHeaderName + i;
                        }

                        // Create it if we haven't already set it explicitly
                        var col = this.Columns.Count < i + 1 ? null : this.Columns[i];
                        if (col == null)
                        {
                            col = new Column
                            {
                                Name = headerName,
                                // Default to string if not assigned.
                                Type = typeof(string)
                            };

                            int existingIndex;
                            if (this._fieldHeaderIndexes.TryGetValue(headerName, out existingIndex))
                            {
                                if (DuplicateHeaderEncountered == null)
                                {
                                    throw new DuplicateHeaderException(headerName, i);
                                }

                                var args = new DuplicateHeaderEventArgs(headerName, i, existingIndex);
                                DuplicateHeaderEncountered(this, args);
                                col.Name = args.HeaderName;
                            }

                            this._fieldHeaderIndexes.Add(col.Name, i);
                            // Should be correct as we are going in ascending order.
                            this.Columns.Add(col);
                        }
                    }

                    // Proceed to first record
                    if (!onlyReadHeaders)
                    {
                        // Calling again ReadNextRecord() seems to be simpler,
                        // but in fact would probably cause many subtle bugs because a derived class does not expect a recursive behavior
                        // so simply do what is needed here and no more.

                        if (!this.SkipEmptyAndCommentedLines(ref this._nextFieldStart))
                        {
                            return false;
                        }

                        Array.Clear(this._fields, 0, this._fields.Length);
                        this._nextFieldIndex = 0;
                        this._eol = false;

                        this.FileRecordIndex++;
                        return true;
                    }
                }
                else
                {
                    // If we have explicity columne, now build up the reverse dictionary
                    for (var i = 0; i < this.Columns.Count; i++)
                    {
                        this._fieldHeaderIndexes.Add(this.Columns[i].Name, i);
                    }

                    if (onlyReadHeaders)
                    {
                        this._firstRecordInCache = true;
                        this.FileRecordIndex = -1;
                    }
                    else
                    {
                        this._firstRecordInCache = false;
                        this.FileRecordIndex = 0;
                    }
                }
            }
            else
            {
                if (skipToNextLine)
                {
                    this.SkipToNewLine(ref this._nextFieldStart);
                }
                else if (this.FileRecordIndex > -1 && !this.MissingFieldFlag)
                {
                    // If not already at end of record, move there
                    if (!this._eol && !this._eof)
                    {
                        this.HandleExtraFieldsInCurrentRecord(this._nextFieldStart);
                    }
                }

                if (!this._firstRecordInCache && !this.SkipEmptyAndCommentedLines(ref this._nextFieldStart))
                {
                    return false;
                }

                if (this.HasHeaders || !this._firstRecordInCache)
                {
                    this._eol = false;
                }

                // Check to see if the first record is in cache.
                // This can happen when initializing a reader with no headers
                // because one record must be read to get the field count automatically
                if (this._firstRecordInCache)
                {
                    this._firstRecordInCache = false;
                }
                else
                {
                    Array.Clear(this._fields, 0, this._fields.Length);
                    this._nextFieldIndex = 0;
                }

                this.MissingFieldFlag = false;
                this.ParseErrorFlag = false;
                this.FileRecordIndex++;
            }

            return true;
        }

        private void HandleExtraFieldsInCurrentRecord(int currentFieldIndex)
        {
            if (this.DefaultParseErrorAction == ParseErrorAction.AdvanceToNextLine)
            {
                this.SkipToNextRecord();
            }
            else
            {
                var exception = new MalformedCsvException(this.GetCurrentRawData(), this._nextFieldStart, Math.Max(0, this.FileRecordIndex), currentFieldIndex);

                if (this.DefaultParseErrorAction == ParseErrorAction.RaiseEvent)
                {
                    var e = new ParseErrorEventArgs(exception, ParseErrorAction.ThrowException);
                    this.OnParseError(e);
                    this.SkipToNextRecord();
                }
                else if (this.DefaultParseErrorAction == ParseErrorAction.ThrowException)
                {
                    throw exception;
                }
            }
        }

        private void SkipToNextRecord()
        {
            if (!this.SupportsMultiline)
            {
                this.SkipToNewLine(ref this._nextFieldStart);
            }
            else
            {
                while (this.ReadField(this._nextFieldIndex, true, true) != null)
                {
                }
            }
        }

        /// <summary>
        /// Skips empty and commented lines.
        /// If the end of the buffer is reached, its content be discarded and filled again from the reader.
        /// </summary>
        /// <param name="pos">
        /// The position in the buffer where to start parsing.
        /// Will contains the resulting position after the operation.
        /// </param>
        /// <returns><see langword="true"/> if the end of the reader has not been reached; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///    The instance has been disposed of.
        /// </exception>
        private bool SkipEmptyAndCommentedLines(ref int pos)
        {
            if (pos < this._bufferLength)
            {
                this.DoSkipEmptyAndCommentedLines(ref pos);
            }

            while (pos >= this._bufferLength && !this._eof)
            {
                if (this.ReadBuffer())
                {
                    pos = 0;
                    this.DoSkipEmptyAndCommentedLines(ref pos);
                }
                else
                {
                    return false;
                }
            }

            return !this._eof;
        }

        /// <summary>
        /// <para>Worker method.</para>
        /// <para>Skips empty and commented lines.</para>
        /// </summary>
        /// <param name="pos">
        /// The position in the buffer where to start parsing.
        /// Will contains the resulting position after the operation.
        /// </param>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">The instance has been disposed of.</exception>
        private void DoSkipEmptyAndCommentedLines(ref int pos)
        {
            while (pos < this._bufferLength)
            {
                if (this._buffer[pos] == this.Comment)
                {
                    pos++;
                    this.SkipToNewLine(ref pos);
                }
                else if (this.SkipEmptyLines && this.ParseNewLine(ref pos))
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Skips whitespace characters.
        /// </summary>
        /// <param name="pos">The starting position of the parsing. Will contain the resulting end position.</param>
        /// <returns><see langword="true"/> if the end of the reader has not been reached; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">The instance has been disposed of.</exception>
        private bool SkipWhiteSpaces(ref int pos)
        {
            for (; ; )
            {
                while (pos < this._bufferLength && this.IsWhiteSpace(this._buffer[pos]))
                {
                    pos++;
                }

                if (pos < this._bufferLength)
                {
                    break;
                }
                else
                {
                    pos = 0;

                    if (!this.ReadBuffer())
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Skips ahead to the next NewLine character.
        /// If the end of the buffer is reached, its content be discarded and filled again from the reader.
        /// </summary>
        /// <param name="pos">
        /// The position in the buffer where to start parsing.
        /// Will contains the resulting position after the operation.
        /// </param>
        /// <returns><see langword="true"/> if the end of the reader has not been reached; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">The instance has been disposed of.</exception>
        private bool SkipToNewLine(ref int pos)
        {
            // ((pos = 0) == 0) is a little trick to reset position inline
            while ((pos < this._bufferLength || (this.ReadBuffer() && ((pos = 0) == 0))) && !this.ParseNewLine(ref pos))
            {
                pos++;
            }

            return !this._eof;
        }

        /// <summary>
        /// Handles a parsing error.
        /// </summary>
        /// <param name="error">The parsing error that occured.</param>
        /// <param name="pos">The current position in the buffer.</param>
        /// <exception cref="ArgumentNullException"><paramref name="error"/> is <see langword="null"/>.</exception>
        private void HandleParseError(MalformedCsvException error, ref int pos)
        {
            if (error == null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            this.ParseErrorFlag = true;

            switch (this.DefaultParseErrorAction)
            {
                case ParseErrorAction.ThrowException:
                    throw error;

                case ParseErrorAction.RaiseEvent:
                    var e = new ParseErrorEventArgs(error, ParseErrorAction.ThrowException);
                    this.OnParseError(e);

                    switch (e.Action)
                    {
                        case ParseErrorAction.ThrowException:
                            throw e.Error;

                        case ParseErrorAction.RaiseEvent:
                            throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, Strings.ParseErrorActionInvalidInsideParseErrorEvent, e.Action), e.Error);

                        case ParseErrorAction.AdvanceToNextLine:
                            // already at EOL when fields are missing, so don't skip to next line in that case
                            if (!this.MissingFieldFlag && pos >= 0)
                            {
                                this.SkipToNewLine(ref pos);
                            }
                            break;

                        default:
                            throw new NotSupportedException(String.Format(CultureInfo.InvariantCulture, Strings.ParseErrorActionNotSupported, e.Action), e.Error);
                    }
                    break;

                case ParseErrorAction.AdvanceToNextLine:
                    // already at EOL when fields are missing, so don't skip to next line in that case
                    if (!this.MissingFieldFlag && pos >= 0)
                    {
                        this.SkipToNewLine(ref pos);
                    }

                    break;

                default:
                    throw new NotSupportedException(String.Format(CultureInfo.InvariantCulture, Strings.ParseErrorActionNotSupported, this.DefaultParseErrorAction), error);
            }
        }

        /// <summary>
        /// Handles a missing field error.
        /// </summary>
        /// <param name="value">The partially parsed value, if available.</param>
        /// <param name="fieldIndex">The missing field index.</param>
        /// <param name="currentPosition">The current position in the raw data.</param>
        /// <returns>
        /// The resulting value according to <see cref="M:MissingFieldAction"/>.
        /// If the action is set to <see cref="T:MissingFieldAction.TreatAsParseError"/>,
        /// then the parse error will be handled according to <see cref="DefaultParseErrorAction"/>.
        /// </returns>
        private string HandleMissingField(string value, int fieldIndex, ref int currentPosition)
        {
            if (fieldIndex < 0 || fieldIndex >= this._fieldCount)
            {
                throw new ArgumentOutOfRangeException(nameof(fieldIndex), fieldIndex, String.Format(CultureInfo.InvariantCulture, Strings.FieldIndexOutOfRange, fieldIndex));
            }

            this.MissingFieldFlag = true;

            for (var i = fieldIndex + 1; i < this._fieldCount; i++)
            {
                this._fields[i] = null;
            }

            if (value != null)
            {
                return value;
            }
            else
            {
                switch (this.MissingFieldAction)
                {
                    case MissingFieldAction.ParseError:
                        this.HandleParseError(new MissingFieldCsvException(this.GetCurrentRawData(), currentPosition, Math.Max(0, this.FileRecordIndex), fieldIndex), ref currentPosition);
                        return value;

                    case MissingFieldAction.ReplaceByEmpty:
                        return String.Empty;

                    case MissingFieldAction.ReplaceByNull:
                        return null;

                    default:
                        throw new NotSupportedException(String.Format(CultureInfo.InvariantCulture, Strings.MissingFieldActionNotSupported, this.MissingFieldAction));
                }
            }
        }

        /// <summary>
        /// Validates the state of the data reader.
        /// </summary>
        /// <param name="validations">The validations to accomplish.</param>
        /// <exception cref="InvalidOperationException">No current record.</exception>
        /// <exception cref="InvalidOperationException">This operation is invalid when the reader is closed.</exception>
        private void ValidateDataReader(DataReaderValidations validations)
        {
            if ((validations & DataReaderValidations.IsInitialized) != 0 && !this._initialized)
            {
                throw new InvalidOperationException(Strings.NoCurrentRecord);
            }

            if ((validations & DataReaderValidations.IsNotClosed) != 0 && this._isDisposed)
            {
                throw new InvalidOperationException(Strings.ReaderClosed);
            }
        }

        /// <summary>
        /// Copy the value of the specified field to an array.
        /// </summary>
        /// <param name="field">The index of the field.</param>
        /// <param name="fieldOffset">The offset in the field value.</param>
        /// <param name="destinationArray">The destination array where the field value will be copied.</param>
        /// <param name="destinationOffset">The destination array offset.</param>
        /// <param name="length">The number of characters to copy from the field value.</param>
        /// <returns></returns>
        private long CopyFieldToArray(int field, long fieldOffset, Array destinationArray, int destinationOffset, int length)
        {
            this.EnsureInitialize();

            if (field < 0 || field >= this._fieldCount)
            {
                throw new ArgumentOutOfRangeException(nameof(field), field, String.Format(CultureInfo.InvariantCulture, Strings.FieldIndexOutOfRange, field));
            }

            if (fieldOffset < 0 || fieldOffset >= Int32.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(fieldOffset));
            }

            // Array.Copy(...) will do the remaining argument checks

            if (length == 0)
            {
                return 0;
            }

            var value = this[field];

            if (value == null)
            {
                value = String.Empty;
            }

            Debug.Assert(fieldOffset < Int32.MaxValue);

            Debug.Assert(destinationArray.GetType() == typeof(char[]) || destinationArray.GetType() == typeof(byte[]));

            if (destinationArray.GetType() == typeof(char[]))
            {
                Array.Copy(value.ToCharArray((int)fieldOffset, length), 0, destinationArray, destinationOffset, length);
            }
            else
            {
                var chars = value.ToCharArray((int)fieldOffset, length);
                var source = new byte[chars.Length];

                for (var i = 0; i < chars.Length; i++)
                {
                    source[i] = Convert.ToByte(chars[i]);
                }

                Array.Copy(source, 0, destinationArray, destinationOffset, length);
            }

            return length;
        }

#if !NETSTANDARD1_3
        int System.Data.IDataReader.RecordsAffected =>
                // For SELECT statements, -1 must be returned.
                -1;

        bool System.Data.IDataReader.IsClosed => this._eof;

        bool System.Data.IDataReader.NextResult()
        {
            this.ValidateDataReader(DataReaderValidations.IsNotClosed);

            return false;
        }

        void System.Data.IDataReader.Close()
        {
            this.Dispose();
        }

        bool System.Data.IDataReader.Read()
        {
            this.ValidateDataReader(DataReaderValidations.IsNotClosed);

            return this.ReadNextRecord();
        }

        int System.Data.IDataReader.Depth
        {
            get
            {
                this.ValidateDataReader(DataReaderValidations.IsNotClosed);

                return 0;
            }
        }

        System.Data.DataTable System.Data.IDataReader.GetSchemaTable()
        {
            this.EnsureInitialize();
            this.ValidateDataReader(DataReaderValidations.IsNotClosed);

            var schema = new DataTable("SchemaTable")
            {
                Locale = CultureInfo.InvariantCulture,
                MinimumCapacity = _fieldCount
            };

            schema.Columns.Add(SchemaTableColumn.AllowDBNull, typeof(bool)).ReadOnly = true;
            schema.Columns.Add(SchemaTableColumn.BaseColumnName, typeof(string)).ReadOnly = true;
            schema.Columns.Add(SchemaTableColumn.BaseSchemaName, typeof(string)).ReadOnly = true;
            schema.Columns.Add(SchemaTableColumn.BaseTableName, typeof(string)).ReadOnly = true;
            schema.Columns.Add(SchemaTableColumn.ColumnName, typeof(string)).ReadOnly = true;
            schema.Columns.Add(SchemaTableColumn.ColumnOrdinal, typeof(int)).ReadOnly = true;
            schema.Columns.Add(SchemaTableColumn.ColumnSize, typeof(int)).ReadOnly = true;
            schema.Columns.Add(SchemaTableColumn.DataType, typeof(object)).ReadOnly = true;
            schema.Columns.Add(SchemaTableColumn.IsAliased, typeof(bool)).ReadOnly = true;
            schema.Columns.Add(SchemaTableColumn.IsExpression, typeof(bool)).ReadOnly = true;
            schema.Columns.Add(SchemaTableColumn.IsKey, typeof(bool)).ReadOnly = true;
            schema.Columns.Add(SchemaTableColumn.IsLong, typeof(bool)).ReadOnly = true;
            schema.Columns.Add(SchemaTableColumn.IsUnique, typeof(bool)).ReadOnly = true;
            schema.Columns.Add(SchemaTableColumn.NumericPrecision, typeof(short)).ReadOnly = true;
            schema.Columns.Add(SchemaTableColumn.NumericScale, typeof(short)).ReadOnly = true;
            schema.Columns.Add(SchemaTableColumn.ProviderType, typeof(int)).ReadOnly = true;

            schema.Columns.Add(SchemaTableOptionalColumn.BaseCatalogName, typeof(string)).ReadOnly = true;
            schema.Columns.Add(SchemaTableOptionalColumn.BaseServerName, typeof(string)).ReadOnly = true;
            schema.Columns.Add(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool)).ReadOnly = true;
            schema.Columns.Add(SchemaTableOptionalColumn.IsHidden, typeof(bool)).ReadOnly = true;
            schema.Columns.Add(SchemaTableOptionalColumn.IsReadOnly, typeof(bool)).ReadOnly = true;
            schema.Columns.Add(SchemaTableOptionalColumn.IsRowVersion, typeof(bool)).ReadOnly = true;

            // null marks columns that will change for each row
            object[] schemaRow =
            {
                true,                    // 00- AllowDBNull
                null,                    // 01- BaseColumnName
                String.Empty,            // 02- BaseSchemaName
                String.Empty,            // 03- BaseTableName
                null,                    // 04- ColumnName
                null,                    // 05- ColumnOrdinal
                Int32.MaxValue,            // 06- ColumnSize
                typeof(string),          // 07- DataType
                false,                   // 08- IsAliased
                false,                   // 09- IsExpression
                false,                   // 10- IsKey
                false,                   // 11- IsLong
                false,                   // 12- IsUnique
                DBNull.Value,            // 13- NumericPrecision
                DBNull.Value,            // 14- NumericScale
                (int) DbType.String,     // 15- ProviderType

                String.Empty,            // 16- BaseCatalogName
                String.Empty,            // 17- BaseServerName
                false,                   // 18- IsAutoIncrement
                false,                   // 19- IsHidden
                true,                    // 20- IsReadOnly
                false                    // 21- IsRowVersion
            };

            IList<Column> columns;
            if (this.Columns.Count > 0)
            {
                columns = this.Columns;
            }
            else
            {
                columns = new List<Column>();
                for (var i = 0; i < this._fieldCount; i++)
                {
                    columns.Add(new Column
                    {
                        Name = this.DefaultHeaderName + i,
                        Type = typeof(string)
                    });
                }
            }

            for (var i = 0; i < columns.Count; i++)
            {
                schemaRow[1] = columns[i].Name; // Base column name
                schemaRow[4] = columns[i].Name; // Column name
                schemaRow[5] = i;               // Column ordinal
                schemaRow[7] = columns[i].Type; // Data type

                schema.Rows.Add(schemaRow);
            }

            return schema;
        }

        int IDataRecord.GetInt32(int i)
        {
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);

            var value = this[i];

            return Int32.Parse(value ?? String.Empty, CultureInfo.CurrentCulture);
        }

        object IDataRecord.this[string name]
        {
            get
            {
                this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
                return this[name];
            }
        }

        object IDataRecord.this[int i]
        {
            get
            {
                this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
                return this.FieldValue(i);
            }
        }

        object IDataRecord.GetValue(int i)
        {
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);

            return ((IDataRecord)this).IsDBNull(i) ? DBNull.Value : this.FieldValue(i);
        }

        bool IDataRecord.IsDBNull(int i)
        {
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return this.NullValue == null ? String.IsNullOrEmpty(this[i]) : String.Equals(this[i], this.NullValue, StringComparison.OrdinalIgnoreCase);
        }

        long IDataRecord.GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);

            return this.CopyFieldToArray(i, fieldOffset, buffer, bufferoffset, length);
        }

        byte IDataRecord.GetByte(int i)
        {
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return Byte.Parse(this[i], CultureInfo.CurrentCulture);
        }

        Type IDataRecord.GetFieldType(int i)
        {
            this.EnsureInitialize();
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);

            if (i < 0 || i >= this._fieldCount)
            {
                throw new ArgumentOutOfRangeException(nameof(i), i, String.Format(CultureInfo.InvariantCulture, Strings.FieldIndexOutOfRange, i));
            }

            if (this.Columns == null || i < 0 || i >= this.Columns.Count)
            {
                return typeof(string);
            }
            var column = this.Columns[i];
            return column.Type;
        }

        decimal IDataRecord.GetDecimal(int i)
        {
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return Decimal.Parse(this[i], CultureInfo.CurrentCulture);
        }

        int IDataRecord.GetValues(object[] values)
        {
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);

            var record = (IDataRecord)this;

            for (var i = 0; i < this._fieldCount; i++)
            {
                values[i] = record.GetValue(i);
            }

            return this._fieldCount;
        }

        string IDataRecord.GetName(int i)
        {
            this.EnsureInitialize();
            this.ValidateDataReader(DataReaderValidations.IsNotClosed);

            if (i < 0 || i >= this.FieldCount)
            {
                throw new ArgumentOutOfRangeException(nameof(i), i, String.Format(CultureInfo.InvariantCulture, Strings.FieldIndexOutOfRange, i));
            }

            if (i >= this.Columns.Count)
            {
                return null;
            }

            return this.Columns[i].Name;
        }

        long IDataRecord.GetInt64(int i)
        {
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return Int64.Parse(this[i], CultureInfo.CurrentCulture);
        }

        double IDataRecord.GetDouble(int i)
        {
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return Double.Parse(this[i], CultureInfo.CurrentCulture);
        }

        bool IDataRecord.GetBoolean(int i)
        {
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);

            var value = this[i];

            if (Int32.TryParse(value, out var result))
            {
                return (result != 0);
            }

            return Boolean.Parse(value);
        }

        Guid IDataRecord.GetGuid(int i)
        {
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return new Guid(this[i]);
        }

        DateTime IDataRecord.GetDateTime(int i)
        {
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return DateTime.Parse(this[i], CultureInfo.CurrentCulture);
        }

        int IDataRecord.GetOrdinal(string name)
        {
            this.EnsureInitialize();
            this.ValidateDataReader(DataReaderValidations.IsNotClosed);

            if (!this._fieldHeaderIndexes.TryGetValue(name, out var index))
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, Strings.FieldHeaderNotFound, name), nameof(name));
            }

            return index;
        }

        string IDataRecord.GetDataTypeName(int i)
        {
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return typeof(string).FullName;
        }

        float IDataRecord.GetFloat(int i)
        {
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return Single.Parse(this[i], CultureInfo.CurrentCulture);
        }

        IDataReader IDataRecord.GetData(int i)
        {
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);

            return i == 0 ? this : null;
        }

        long IDataRecord.GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);

            return this.CopyFieldToArray(i, fieldoffset, buffer, bufferoffset, length);
        }

        string IDataRecord.GetString(int i)
        {
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return this[i];
        }

        char IDataRecord.GetChar(int i)
        {
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return Char.Parse(this[i]);
        }

        short IDataRecord.GetInt16(int i)
        {
            this.ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return Int16.Parse(this[i], CultureInfo.CurrentCulture);
        }
#endif
        object FieldValue(int i)
        {
            var value = this[i];
            if (this.Columns == null || i < 0 || i >= this.Columns.Count)
            {
                return value;
            }
            var column = this.Columns[i];
            return column.Convert(value);
        }

        /// <summary>
        /// Returns an <see cref="T:RecordEnumerator"/>  that can iterate through CSV records.
        /// </summary>
        /// <returns>An <see cref="T:RecordEnumerator"/>  that can iterate through CSV records.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///    The instance has been disposed of.
        /// </exception>
        public CsvReader.RecordEnumerator GetEnumerator()
        {
            return new CsvReader.RecordEnumerator(this);
        }

        /// <summary>
        /// Returns an <see cref="T:System.Collections.Generics.IEnumerator"/>  that can iterate through CSV records.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.Generics.IEnumerator"/>  that can iterate through CSV records.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///    The instance has been disposed of.
        /// </exception>
        IEnumerator<string[]> IEnumerable<string[]>.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns an <see cref="T:System.Collections.IEnumerator"/>  that can iterate through CSV records.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator"/>  that can iterate through CSV records.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///    The instance has been disposed of.
        /// </exception>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

#if DEBUG
#if !NETSTANDARD1_3
        /// <summary>
        /// Contains the stack when the object was allocated.
        /// </summary>
        private readonly System.Diagnostics.StackTrace _allocStack;
#endif
#endif

        /// <summary>
        /// Contains the disposed status flag.
        /// </summary>
        private bool _isDisposed = false;

        /// <summary>
        /// Contains the locking object for multi-threading purpose.
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// Occurs when the instance is disposed of.
        /// </summary>
        public event EventHandler Disposed;

        /// <summary>
        /// Gets a value indicating whether the instance has been disposed of.
        /// </summary>
        /// <value>
        ///     <see langword="true"/> if the instance has been disposed of; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsDisposed => this._isDisposed;

        /// <summary>
        /// Raises the <see cref="M:Disposed"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected virtual void OnDisposed(EventArgs e)
        {
            Disposed?.Invoke(this, e);
        }

        /// <summary>
        /// Checks if the instance has been disposed of, and if it has, throws an <see cref="T:System.ComponentModel.ObjectDisposedException"/>; otherwise, does nothing.
        /// </summary>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///     The instance has been disposed of.
        /// </exception>
        /// <remarks>
        ///     Derived classes should call this method at the start of all methods and properties that should not be accessed after a call to <see cref="M:Dispose()"/>.
        /// </remarks>
        protected void CheckDisposed()
        {
            if (this._isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
        }

        /// <summary>
        /// Releases all resources used by the instance.
        /// </summary>
        /// <remarks>
        /// Calls <see cref="M:Dispose(Boolean)"/> with the disposing parameter set to <see langword="true"/> to free unmanaged and managed resources.
        /// </remarks>
        public void Dispose()
        {
            if (!this._isDisposed)
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Releases the unmanaged resources used by this instance and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Refer to http://www.bluebytesoftware.com/blog/PermaLink,guid,88e62cdf-5919-4ac7-bc33-20c06ae539ae.aspx
            // Refer to http://www.gotdotnet.com/team/libraries/whitepapers/resourcemanagement/resourcemanagement.aspx

            // No exception should ever be thrown except in critical scenarios.
            // Unhandled exceptions during finalization will tear down the process.
            if (!this._isDisposed)
            {
                try
                {
                    // Dispose-time code should call Dispose() on all owned objects that implement the IDisposable interface.
                    // "owned" means objects whose lifetime is solely controlled by the container.
                    // In cases where ownership is not as straightforward, techniques such as HandleCollector can be used.
                    // Large managed object fields should be nulled out.

                    // Dispose-time code should also set references of all owned objects to null, after disposing them. This will allow the referenced objects to be garbage collected even if not all references to the "parent" are released. It may be a significant memory consumption win if the referenced objects are large, such as big arrays, collections, etc.
                    if (disposing)
                    {
                        // Acquire a lock on the object while disposing.

                        if (this._reader != null)
                        {
                            lock (this._lock)
                            {
                                if (this._reader != null)
                                {
                                    this._reader.Dispose();

                                    this._reader = null;
                                    this._buffer = null;
                                    this._eof = true;
                                }
                            }
                        }
                    }
                }
                finally
                {
                    // Ensure that the flag is set
                    this._isDisposed = true;

                    // Catch any issues about firing an event on an already disposed object.
                    try
                    {
                        this.OnDisposed(EventArgs.Empty);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the instance is reclaimed by garbage collection.
        /// </summary>
        ~CsvReader()
        {
#if DEBUG
#if !NETSTANDARD1_3
            Debug.WriteLine("FinalizableObject was not disposed" + this._allocStack.ToString());
#endif
#endif
            this.Dispose(false);
        }
    }
}
