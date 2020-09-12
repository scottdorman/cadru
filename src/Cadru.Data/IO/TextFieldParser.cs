//------------------------------------------------------------------------------
// <copyright file="TextFieldParser.cs"
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
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Cadru.Contracts;
using Cadru.Data.Resources;
using Cadru.Extensions;

namespace Cadru.Data.IO
{
    /// <summary>
    /// Enables parsing very large delimited or fixed width field files
    /// </summary>
    public partial class TextFieldParser : IDisposable
    {
        private const string BeginningQuoteRegularExpressionPattern = @"\G[{0}]*""";
        private const int DefaultBufferLength = 4096;
        private const RegexOptions DefaultRegexOptions = RegexOptions.CultureInvariant;
        private const int DefaultStringBuilderIncrease = 10;
        private const string EndingQuoteRegularExpressionPattern = "\"[{0}]*";
        private const int MaxBufferSize = 10000000;
        private const int MaxLineSize = 10000000;

        private readonly bool leaveOpen = false;

        // Codes for whitespace as used by String.Trim excluding line end chars
        // as those are handled separately
        private readonly int[] whitespaceCodes = new int[] { 0x9, 0xB, 0xC, 0x20, 0x85, 0xA0, 0x1680, 0x2000, 0x2001, 0x2002, 0x2003, 0x2004, 0x2005, 0x2006, 0x2007, 0x2008, 0x2009, 0x200A, 0x200B, 0x2028, 0x2029, 0x3000, 0xFEFF };
        private readonly Regex whiteSpaceRegEx = new Regex(@"\s", DefaultRegexOptions);

        private Regex? beginQuotesRegex;

        // Buffer used to hold data read from the file. It holds data that must
        // be read ahead of the cursor (for PeekChars and EndOfData)
        private char[] buffer = new char[DefaultBufferLength];

        private int charsRead = 0;

        // An array holding the strings that indicate a line is a comment
        private string[] commentTokens = Array.Empty<string>();

        // Regular expression used to find delimiters
        private Regex? delimiterRegex;

        // An array of the delimiters used for the fields in the file
        private string[] delimiters = Array.Empty<string>();

        // Regex used with BuildField
        private Regex? delimiterWithEndCharsRegex;

        // Indicates reader has been disposed
        private bool disposed;

        // Flags whether or not there is data left to read. Assume there is at creation
        private bool endOfData = false;

        private FieldType fieldType = FieldType.Delimited;

        private int[] fieldWidths = Array.Empty<int>();
        private int lineLength;
        private long lineNumber = 1L;

        // Indicates that the user has changed properties so that we need to
        // validate before a read
        private bool needPropertyCheck = true;

        private int peekPosition = 0;
        private int position = 0;
        private TextReader? reader;

        // A string of the chars that count as spaces (used for csv format). The
        // norm is spaces and tabs.
        private string? spaceChars;

        /// <summary>
        /// Creates a new TextFieldParser to parse the passed in file
        /// </summary>
        /// <param name="path">The path of the file to be parsed</param>
        /// <remarks></remarks>
        public TextFieldParser(string path)
        {
            this.InitializeFromPath(path, Encoding.UTF8, true);
        }

        /// <summary>
        /// Creates a new TextFieldParser to parse the passed in file
        /// </summary>
        /// <param name="path">The path of the file to be parsed</param>
        /// <param name="defaultEncoding">
        /// The decoding to default to if encoding isn't determined from file
        /// </param>
        /// <remarks></remarks>
        public TextFieldParser(string path, Encoding defaultEncoding)
        {
            this.InitializeFromPath(path, defaultEncoding, true);
        }

        /// <summary>
        /// Creates a new TextFieldParser to parse the passed in file
        /// </summary>
        /// <param name="path">The path of the file to be parsed</param>
        /// <param name="defaultEncoding">
        /// The decoding to default to if encoding isn't determined from file
        /// </param>
        /// <param name="detectEncoding">
        /// Indicates whether or not to try to detect the encoding from the BOM
        /// </param>
        /// <remarks></remarks>
        public TextFieldParser(string path, Encoding defaultEncoding, bool detectEncoding)
        {
            this.InitializeFromPath(path, defaultEncoding, detectEncoding);
        }

        /// <summary>
        /// Creates a new TextFieldParser to parse a file represented by the
        /// passed in stream
        /// </summary>
        /// <param name="stream"></param>
        /// <remarks></remarks>
        public TextFieldParser(Stream stream)
        {
            this.InitializeFromStream(stream, Encoding.UTF8, true);
        }

        /// <summary>
        /// Creates a new TextFieldParser to parse a file represented by the
        /// passed in stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="defaultEncoding">
        /// The decoding to default to if encoding isn't determined from file
        /// </param>
        /// <remarks></remarks>
        public TextFieldParser(Stream stream, Encoding defaultEncoding)
        {
            this.InitializeFromStream(stream, defaultEncoding, true);
        }

        /// <summary>
        /// Creates a new TextFieldParser to parse a file represented by the
        /// passed in stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="defaultEncoding">
        /// The decoding to default to if encoding isn't determined from file
        /// </param>
        /// <param name="detectEncoding">
        /// Indicates whether or not to try to detect the encoding from the BOM
        /// </param>
        /// <remarks></remarks>
        public TextFieldParser(Stream stream, Encoding defaultEncoding, bool detectEncoding)
        {
            this.InitializeFromStream(stream, defaultEncoding, detectEncoding);
        }

        /// <summary>
        /// Creates a new TextFieldParser to parse a file represented by the
        /// passed in stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="defaultEncoding">
        /// The decoding to default to if encoding isn't determined from file
        /// </param>
        /// <param name="detectEncoding">
        /// Indicates whether or not to try to detect the encoding from the BOM
        /// </param>
        /// <param name="leaveOpen">
        /// Indicates whether or not to leave the passed in stream open
        /// </param>
        /// <remarks></remarks>
        public TextFieldParser(Stream stream, Encoding defaultEncoding, bool detectEncoding, bool leaveOpen)
        {
            this.leaveOpen = leaveOpen;
            this.InitializeFromStream(stream, defaultEncoding, detectEncoding);
        }

        /// <summary>
        /// Creates a new TextFieldParser to parse a stream or file represented
        /// by the passed in TextReader
        /// </summary>
        /// <param name="reader">The TextReader that does the reading</param>
        /// <remarks></remarks>
        public TextFieldParser(TextReader reader)
        {
            Requires.NotNull(reader, nameof(reader));

            this.reader = reader;
            this.ReadToBuffer();
        }

        /// <summary>
        /// Clean up following dispose pattern
        /// </summary>
        /// <remarks></remarks>
        ~TextFieldParser()
        {
            // Do not change this code. Put cleanup code in Dispose(ByVal
            // disposing As Boolean) above.
            this.Dispose(false);
        }

        /// <summary>
        /// Function to call when we're at the end of the buffer. We either re
        /// fill the buffer or change the size of the buffer
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private delegate int ChangeBufferFunction();

        /// <summary>
        /// Gets the strings that indicate a line is a comment
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public IEnumerable<string> CommentTokens => this.commentTokens;

        /// <summary>
        /// Gets the delimiters used in a file
        /// </summary>
        public IEnumerable<string> Delimiters => this.delimiters;

        /// <summary>
        /// Gets a value indicating whether or not there is any data (non
        /// ignorable lines) left to read in the file
        /// </summary>
        /// <value>
        /// <see langword="true"/> if there is more data to read; otherwise <see langword="false"/>
        /// </value>
        /// <remarks>This ignores comments and blank lines.</remarks>
        public bool EndOfData
        {
            get
            {
                if (this.endOfData)
                {
                    return this.endOfData;
                }

                // Make sure we're not at end of file
                if (this.reader is null || this.buffer is null)
                {
                    this.endOfData = true;
                    return true;
                }

                // See if we can get a data line
                if (this.PeekNextDataLine(out _))
                {
                    return false;
                }

                this.endOfData = true;
                return true;
            }
        }

        /// <summary>
        /// Returns the last malformed line if there is one.
        /// </summary>
        /// <value>The last malformed line</value>
        public string ErrorLine { get; private set; } = "";

        /// <summary>
        /// Returns the line number of last malformed line if there is one.
        /// </summary>
        /// <value>The last malformed line number</value>
        public long ErrorLineNumber { get; private set; } = -1;

        /// <summary>
        /// Gets or sets the widths of the fields for reading a fixed width file
        /// </summary>
        /// <value>An array of the widths</value>
        public IReadOnlyList<int> FieldWidths => this.fieldWidths;

        /// <summary>
        /// Indicates whether or not to handle quotes in a csv friendly way
        /// </summary>
        /// <value>True if we escape quotes otherwise false</value>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool HasFieldsEnclosedInQuotes { get; set; } = true;

        /// <summary>
        /// The line to the right of the cursor.
        /// </summary>
        /// <value>The number of the line</value>
        /// <remarks>
        /// LineNumber returns the location in the file and has nothing to do
        /// with rows or fields
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public long LineNumber
        {
            get
            {
                // See if we're at the end of file
                if (this.lineNumber != -1 && this.position == this.charsRead && this.reader?.Peek() == -1)
                {
                    this.CloseReader();
                }

                return this.lineNumber;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the type of file being read, either
        /// fixed width or delimited
        /// </summary>
        public FieldType TextFieldType
        {
            get => this.fieldType;
            set
            {
                this.fieldType = value;
                this.needPropertyCheck = true;
            }
        }

        /// <summary>
        /// Indicates whether or not leading and trailing white space should be
        /// removed when returning a field
        /// </summary>
        /// <value><see langword="true"/> if white space should be removed; otherwise <see langword="true"/></value>
        public bool TrimWhiteSpace { get; set; } = true;

        /// <summary>
        /// Gets the appropriate expression for finding ending quote of a field
        /// </summary>
        /// <value>The expression</value>
        /// <remarks></remarks>
        private string EndQuotePattern => String.Format(CultureInfo.InvariantCulture, EndingQuoteRegularExpressionPattern, this.GetWhitespacePattern());

        /// <summary>
        /// Closes the StreamReader
        /// </summary>
        /// <remarks></remarks>
        public void Close()
        {
            this.CloseReader();
        }

        /// <summary>
        /// Closes the StreamReader
        /// </summary>
        /// <remarks></remarks>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Enables looking at the passed in number of characters of the next
        /// data line without reading the line
        /// </summary>
        /// <param name="numberOfChars"></param>
        /// <returns>
        /// A string consisting of the first <paramref name="numberOfChars"/>
        /// characters of the next line
        /// </returns>
        /// <remarks>
        /// If <paramref name="numberOfChars"/> is greater than the next line,
        /// only the next line is returned
        /// </remarks>
        public string? PeekChars(int numberOfChars)
        {
            Requires.IsTrue(numberOfChars > 0, nameof(numberOfChars), String.Format(Strings.TextFieldParser_NumberOfCharsMustBePositive, "numberOfChars"));

            if (this.reader is null || this.buffer is null || this.endOfData)
            {
                return null;
            }

            // Get the next line without reading it
            if (this.PeekNextDataLine(out var line))
            {
                line = line.TrimEnd('\r', '\n');

                // If the number of chars is larger than the line, return the
                // whole line. Otherwise return the numberOfChars characters
                // from the beginning of the line
                return (line.Length < numberOfChars) ? line : new StringInfo(line).SubstringByTextElements(0, numberOfChars);
            }

            this.endOfData = true;
            return null;
        }

        /// <summary>
        /// Reads a non ignorable line and parses it into fields
        /// </summary>
        /// <returns>The line parsed into fields</returns>
        /// <remarks>
        /// This is a data aware method. Comments and blank lines are ignored.
        /// </remarks>
        public string[]? ReadFields()
        {
            if (this.reader is null || this.buffer is null)
            {
                return null;
            }

            this.ValidateReadyToRead();
            return this.fieldType switch
            {
                FieldType.FixedWidth => this.ParseFixedWidthLine(),
                FieldType.Delimited => this.ParseDelimitedLine(),
                _ => null
            };
        }

        /// <summary>
        /// Reads and returns the next line from the file
        /// </summary>
        /// <returns>
        /// The line read or <see langword="null"/> if at the end of the file
        /// </returns>
        /// <remarks>
        /// This is data unaware method. It simply reads the next line in the file.
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string? ReadLine()
        {
            if (this.reader is null || this.buffer is null)
            {
                return null;
            }

            string? line;

            // Set the method to be used when we reach the end of the buffer
            var BufferFunction = new ChangeBufferFunction(this.ReadToBuffer);
            line = this.ReadNextLine(ref this.position, BufferFunction);
            if (line is null)
            {
                this.FinishReading();
                return null;
            }
            else
            {
                this.lineNumber += 1L;
                return line.TrimEnd('\r', '\n');
            }
        }

        /// <summary>
        /// Reads the file starting at the current position and moving to the
        /// end of the file
        /// </summary>
        /// <returns>
        /// The contents of the file from the current position to the end of the file
        /// </returns>
        /// <remarks>
        /// This is not a data aware method. Everything in the file from the
        /// current position to the end is read
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string? ReadToEnd()
        {
            if (this.reader is null || this.buffer is null)
            {
                return null;
            }

            var builder = new StringBuilder(this.buffer.Length);
            builder.Append(this.buffer, this.position, this.charsRead - this.position);
            builder.Append(this.reader.ReadToEnd());
            this.FinishReading();
            return builder.ToString();
        }

        /// <summary>
        /// Set the comment tokens
        /// </summary>
        /// <param name="tokens">A list of the comment tokens</param>
        public void SetCommentTokens(params string[] tokens)
        {
            Requires.ValidElements(tokens, t => !this.whiteSpaceRegEx.IsMatch(t), nameof(tokens), String.Format(Strings.TextFieldParser_DelimitersNothing, "tokens"));

            this.commentTokens = tokens;
            this.needPropertyCheck = true;
        }

        /// <summary>
        /// Set the delimiters
        /// </summary>
        /// <param name="delimiters">A list of the delimiters</param>
        public void SetDelimiters(params string[] delimiters)
        {
            Requires.ValidElements(delimiters, d => !String.IsNullOrWhiteSpace(d), nameof(delimiters), String.Format(Strings.TextFieldParser_DelimitersNothing, "delimiters"));
            Requires.ValidElements(delimiters, d => d.IndexOfAny(new char[] { '\r', '\n' }) <= 0, nameof(delimiters), String.Format(Strings.TextFieldParser_DelimitersNothing, "delimiters"));

            this.delimiters = delimiters;
            this.needPropertyCheck = true;
            // Force rebuilding of regex
            this.beginQuotesRegex = null;
        }

        /// <summary>
        /// Set the field widths
        /// </summary>
        /// <param name="fieldWidths">A list of field widths</param>
        public void SetFieldWidths(params int[] fieldWidths)
        {
            Requires.ValidElements(fieldWidths[0..^1], fw => fw >= 0, nameof(fieldWidths), String.Format(Strings.TextFieldParser_FieldWidthsMustPositive, "fieldWidths"));

            this.fieldWidths = fieldWidths;
            this.needPropertyCheck = true;
        }

        /// <summary>
        /// Standard implementation of IDisposable.Dispose for non sealed
        /// classes. Classes derived from TextFieldParser should override this
        /// method. After doing their own cleanup, they should call this method (MyBase.Dispose(disposing))
        /// </summary>
        /// <param name="disposing">
        /// Indicates we are called by Dispose and not GC
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!this.disposed)
                {
                    this.Close();
                }

                this.disposed = true;
            }
        }

        /// <summary>
        /// Checks to see if the passed in character is in any of the delimiters
        /// </summary>
        /// <param name="testCharacter">The character to look for</param>
        /// <returns>
        /// <see langword="true"/> if the character is found in a delimiter; otherwise <see langword="false"/>
        /// </returns>
        private bool CharacterIsInDelimiter(char testCharacter)
        {
            Debug.Assert(this.delimiters != null, "No delimiters set!");
            return this.delimiters.Any(d => d.IndexOf(testCharacter) > -1);
        }

        /// <summary>
        /// Closes the StreamReader
        /// </summary>
        private void CloseReader()
        {
            this.FinishReading();
            if (this.reader != null)
            {
                if (!this.leaveOpen)
                {
                    this.reader.Close();
                }

                this.reader = null;
            }
        }

        /// <summary>
        /// Cleans up managed resources except the StreamReader and indicates
        /// reading is finished
        /// </summary>
        private void FinishReading()
        {
            this.lineNumber = -1;
            this.endOfData = true;
            this.buffer = new char[DefaultBufferLength];
            this.delimiterRegex = null;
            this.beginQuotesRegex = null;
        }

        /// <summary>
        /// Gets the appropriate regex for finding a field beginning with quotes
        /// </summary>
        private Regex GetBeginQuotesRegex()
        {
            if (this.beginQuotesRegex is null)
            {
                var pattern = String.Format(CultureInfo.InvariantCulture, BeginningQuoteRegularExpressionPattern, this.GetWhitespacePattern());
                this.beginQuotesRegex = new Regex(pattern, DefaultRegexOptions);
            }

            return this.beginQuotesRegex;
        }

        /// <summary>
        /// Gets the index of the first end of line character
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        /// <remarks>
        /// When there are no end of line characters, the index is the length
        /// (one past the end)
        /// </remarks>
        private int GetEndOfLineIndex(string line)
        {
            Debug.Assert(line is object, "We are parsing a Nothing");
            var length = line.Length;
            Debug.Assert(length > 0, "A blank line shouldn't be parsed");

            if (length == 1)
            {
                Debug.Assert(line[0] != '\r' && line[0] != '\n', "A blank line shouldn't be parsed");
                return length;
            }

            // Check the next to last and last char for end line characters
            if (line[length - 2] == '\r' || line[length - 2] == '\n')
            {
                return length - 2;
            }
            else if (line[length - 1] == '\r' || line[length - 1] == '\n')
            {
                return length - 1;
            }
            else
            {
                return length;
            }
        }

        /// <summary>
        /// Returns the field at the passed in index
        /// </summary>
        /// <param name="line">The string containing the fields</param>
        /// <param name="index">The start of the field</param>
        /// <param name="fieldLength">The length of the field</param>
        /// <returns>The field</returns>
        private string GetFixedWidthField(StringInfo line, int index, int fieldLength)
        {
            string field;
            if (fieldLength > 0)
            {
                field = line.SubstringByTextElements(index, fieldLength);
            }
            else if (index >= line.LengthInTextElements)
            {
                // Make sure the index isn't past the string
                field = String.Empty;
            }
            else
            {
                field = line.SubstringByTextElements(index).TrimEnd('\r', '\n');
            }

            return this.TrimWhiteSpace ? field.Trim() : field;
        }

        /// <summary>
        /// Gets the character set of white-spaces to be used in a regex pattern
        /// </summary>
        private string GetWhitespacePattern()
        {
            var builder = new StringBuilder(this.whitespaceCodes.Length);
            this.whitespaceCodes.ForEach(code =>
            {
                if (!this.CharacterIsInDelimiter((char)code))
                {
                    // Gives us something like \u00A0
                    builder.Append(@"\u" + code.ToString("X4", CultureInfo.InvariantCulture));
                }
            });

            return builder.ToString();
        }

        /// <summary>
        /// Indicates whether or not the passed in line should be ignored
        /// </summary>
        /// <param name="line">The line to be tested</param>
        /// <returns><see langword="true"/> if the line should be ignored; otherwise <see langword="false"/></returns>
        /// <remarks>Lines to ignore are blank lines and comments</remarks>
        private bool IgnoreLine(string? line)
        {
            if (line != null)
            {
                // Ignore empty or whitespace lines
                var trimmedLine = line.Trim();
                if (trimmedLine.Length == 0)
                {
                    return true;
                }

                // Ignore comments
                foreach (var token in this.commentTokens)
                {
                    if (String.IsNullOrEmpty(token))
                    {
                        continue;
                    }

                    // Test original line in case whitespace char is a comment token
                    if (trimmedLine.StartsWith(token, StringComparison.Ordinal) || line.StartsWith(token, StringComparison.Ordinal))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Increases the size of the buffer. Used when we are at the end of the
        /// buffer, we need to read more data from the file, and we can't
        /// discard what we've already read.
        /// </summary>
        /// <returns>The number of characters read to fill the new buffer</returns>
        /// <remarks>This is needed for PeekChars and EndOfData</remarks>
        private int IncreaseBufferSize()
        {
            Debug.Assert(this.buffer is object, "There's no buffer");
            Debug.Assert(this.reader is object, "There's no StreamReader");

            // Set cursor
            this.peekPosition = this.charsRead;

            // Create a larger buffer and copy our data into it
            var bufferSize = this.buffer.Length + DefaultBufferLength;

            // Make sure the buffer hasn't grown too large
            if (bufferSize > MaxBufferSize)
            {
                throw new InvalidOperationException(Strings.TextFieldParser_BufferExceededMaxSize);
            }

            var tempArray = new char[bufferSize];
            Array.Copy(this.buffer, tempArray, this.buffer.Length);

            var r = this.reader.Read(tempArray, this.buffer.Length, DefaultBufferLength);
            this.buffer = tempArray;
            this.charsRead += r;
            Debug.Assert(this.charsRead <= bufferSize, "We've read more chars than we have space for");
            return r;
        }

        /// <summary>
        /// Creates a StreamReader for the passed in Path
        /// </summary>
        /// <param name="path">The passed in path</param>
        /// <param name="defaultEncoding">
        /// The encoding to default to if encoding can't be detected
        /// </param>
        /// <param name="detectEncoding">
        /// Indicates whether or not to detect encoding from the BOM
        /// </param>
        /// <remarks>
        /// We validate the arguments here for the three Public constructors
        /// that take a Path
        /// </remarks>
        private void InitializeFromPath(string path, Encoding defaultEncoding, bool detectEncoding)
        {
            Requires.NotNullOrWhiteSpace(path, nameof(path));
            Requires.NotNull(defaultEncoding, nameof(defaultEncoding));

            var fullPath = this.ValidatePath(path);
            var fileStreamTemp = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            this.InitializeFromStream(fileStreamTemp, defaultEncoding, detectEncoding);
        }

        /// <summary>
        /// Creates a StreamReader for a passed in stream
        /// </summary>
        /// <param name="stream">The passed in stream</param>
        /// <param name="defaultEncoding">
        /// The encoding to default to if encoding can't be detected
        /// </param>
        /// <param name="detectEncoding">
        /// Indicates whether or not to detect encoding from the BOM
        /// </param>
        /// <remarks>
        /// We validate the arguments here for the three Public constructors
        /// that take a Stream
        /// </remarks>
        private void InitializeFromStream(Stream stream, Encoding defaultEncoding, bool detectEncoding)
        {
            Requires.NotNull(stream, nameof(stream));
            Requires.IsTrue(stream.CanRead, nameof(stream), String.Format(Strings.TextFieldParser_StreamNotReadable, "stream"));
            Requires.NotNull(defaultEncoding, nameof(defaultEncoding));

            this.reader = new StreamReader(stream, defaultEncoding, detectEncoding);
            this.ReadToBuffer();
        }

        /// <summary>
        /// Gets the next data line and parses it with the delimiters
        /// </summary>
        /// <returns>An array of the fields in the line</returns>
        /// <remarks></remarks>
        private string[]? ParseDelimitedLine()
        {
            if (this.ReadNextDataLine(out var line))
            {
                // The line number is that of the line just read
                var currentLineNumber = this.lineNumber - 1L;
                var index = 0;
                var fields = new List<string>();
                string field;
                var lineEndIndex = this.GetEndOfLineIndex(line);

                while (index <= lineEndIndex)
                {
                    var qouteDelimited = false;
                    Match? matchResult = null;

                    // Is the field delimited in quotes? We only care about this
                    // if EscapedQuotes is True
                    if (this.HasFieldsEnclosedInQuotes)
                    {
                        matchResult = this.GetBeginQuotesRegex().Match(line, index);
                        qouteDelimited = matchResult.Success;
                    }

                    if (qouteDelimited)
                    {
                        if (matchResult!.Success)
                        {
                            // Move the Index beyond quote
                            index = matchResult.Index + matchResult.Length;
                            // Look for the closing "
                            var endHelper = new QuoteDelimitedFieldBuilder(this.delimiterWithEndCharsRegex!, this.spaceChars!);
                            endHelper.BuildField(line, index);
                            if (endHelper.MalformedLine)
                            {
                                this.ThrowMalformedLineException(line.TrimEnd('\r', '\n'), currentLineNumber);
                            }

                            if (endHelper.FieldFinished)
                            {
                                field = endHelper.Field;
                                index = endHelper.Index + endHelper.DelimiterLength;
                            }
                            else
                            {
                                // We may have an embedded line end character,
                                // so grab next line
                                var lineBuilder = new StringBuilder(line);

                                do
                                {
                                    var endOfLine = line.Length;
                                    if (this.ReadNextDataLine(out var newLine))
                                    {
                                        if (line.Length + newLine.Length > MaxLineSize)
                                        {
                                            this.ThrowMalformedLineException(line.TrimEnd('\r', '\n'), currentLineNumber);
                                        }

                                        lineBuilder.Append(newLine);
                                        lineEndIndex = this.GetEndOfLineIndex(line);
                                        endHelper.BuildField(line, endOfLine);
                                        if (endHelper.MalformedLine)
                                        {
                                            this.ThrowMalformedLineException(line.TrimEnd('\r', '\n'), currentLineNumber);
                                        }
                                    }
                                    else
                                    {
                                        // If we didn't get a new line, we're at
                                        // the end of the file so our original
                                        // line is malformed
                                        this.ThrowMalformedLineException(line.TrimEnd('\r', '\n'), currentLineNumber);
                                    }
                                } while (!endHelper.FieldFinished);

                                line = lineBuilder.ToString();
                                field = endHelper.Field;
                                index = endHelper.Index + endHelper.DelimiterLength;
                            }

                            fields.Add(this.TrimWhiteSpace ? field.Trim() : field);
                        }
                    }
                    else
                    {
                        // Find the next delimiter
                        var delimiterMatch = this.delimiterRegex!.Match(line, index);
                        if (delimiterMatch.Success)
                        {
                            field = line[index..delimiterMatch.Index];
                            fields.Add(this.TrimWhiteSpace ? field.Trim() : field);
                            index = delimiterMatch.Index + delimiterMatch.Length;
                        }
                        else
                        {
                            // We're at the end of the line so the field
                            // consists of all that's left of the line minus the
                            // end of line chars
                            field = line.Substring(index).TrimEnd('\r', '\n');
                            fields.Add(this.TrimWhiteSpace ? field.Trim() : field);
                            break;
                        }
                    }
                }

                return fields.ToArray();
            }

            return null;
        }

        /// <summary>
        /// Gets the next data line and parses into fixed width fields
        /// </summary>
        /// <returns>An array of the fields in the line</returns>
        /// <remarks></remarks>
        private string[]? ParseFixedWidthLine()
        {
            Debug.Assert(this.fieldWidths is object, "No field widths");

            if (this.ReadNextDataLine(out var line))
            {
                var lineInfo = new StringInfo(line.TrimEnd('\r', '\n'));
                this.ValidateFixedWidthLine(lineInfo, this.lineNumber - 1L);
                var index = 0;
                var fields = new string[this.fieldWidths.Length];
                for (var i = 0; i <= this.fieldWidths.Length - 1; i++)
                {
                    fields[i] = this.GetFixedWidthField(lineInfo, index, this.fieldWidths[i]);
                    index += this.fieldWidths[i];
                }

                return fields;
            }

            return null;
        }

        /// <summary>
        /// Returns the next data line but doesn't move the cursor
        /// </summary>
        /// <returns>
        /// The next data line, or <see langword="null"/> if there's no more data
        /// </returns>
        private bool PeekNextDataLine([NotNullWhen(true)] out string? line)
        {
            line = null;

            // Set function to use when we reach the end of the buffer
            var bufferFunction = new ChangeBufferFunction(this.IncreaseBufferSize);

            // Slide the data to the left so that we make maximum use of the buffer
            this.SlideCursorToStartOfBuffer();
            this.peekPosition = 0;
            do
            {
                line = this.ReadNextLine(ref this.peekPosition, bufferFunction);
            } while (this.IgnoreLine(line));

            return line != null;
        }

        /// <summary>
        /// Returns the next line of data or nothing if there's no more data to
        /// be read
        /// </summary>
        /// <returns>The next line of data</returns>
        /// <remarks>Moves the cursor past the line read</remarks>
        private bool ReadNextDataLine([NotNullWhen(true)] out string? line)
        {
            // Set function to use when we reach the end of the buffer
            var bufferFunction = new ChangeBufferFunction(this.ReadToBuffer);
            do
            {
                line = this.ReadNextLine(ref this.position, bufferFunction);
                this.lineNumber += 1L;
            }
            while (this.IgnoreLine(line));

            if (line is null)
            {
                this.CloseReader();
            }

            return line != null;
        }

        /// <summary>
        /// Gets the next line from the file and moves the passed in cursor past
        /// the line
        /// </summary>
        /// <param name="cursor">Indicates the current position in the buffer</param>
        /// <param name="changeBuffer">
        /// Function to call when we've reached the end of the buffer
        /// </param>
        /// <returns>The next line in the file</returns>
        /// <remarks>Returns Nothing if we are at the end of the file</remarks>
        private string? ReadNextLine(ref int cursor, ChangeBufferFunction changeBuffer)
        {
            Debug.Assert(this.buffer is object, "There's no buffer");
            Debug.Assert(cursor >= 0 && cursor <= this.charsRead, "The cursor is out of range");

            // Check to see if the cursor is at the end of the chars in the
            // buffer. If it is, re fill the buffer
            if (cursor == this.charsRead && changeBuffer() == 0)
            {
                // We're at the end of the file
                return null;
            }

            StringBuilder? builder = null;
            do
            {
                // Walk through buffer looking for the end of a line. End of
                // line can be vbLf (\n), vbCr (\r) or vbCrLf (\r\n)
                for (var i = cursor; i <= this.charsRead - 1; i++)
                {
                    var character = this.buffer[i];
                    if (character == '\r' || character == '\n')
                    {
                        // We've found the end of a line so add everything we've
                        // read so far to the builder. We include the end of
                        // line char because we need to know what it is in case
                        // it's embedded in a field.
                        if (builder == null)
                        {
                            builder = new StringBuilder(i + 1);
                        }

                        builder.Append(this.buffer, cursor, i - cursor + 1);
                        cursor = i + 1;

                        // See if vbLf should be added as well
                        if (character == '\r')
                        {
                            if (cursor < this.charsRead)
                            {
                                if (this.buffer[cursor] == '\n')
                                {
                                    cursor += 1;
                                    builder.Append('\n');
                                }
                            }
                            else if (changeBuffer() > 0 && this.buffer[cursor] == '\n')
                            {
                                cursor += 1;
                                builder.Append('\n');
                            }
                        }

                        return builder.ToString();
                    }
                }

                // We've searched the whole buffer and haven't found an end of
                // line. Save what we have, and read more to the buffer.
                var size = this.charsRead - cursor;
                if (builder == null)
                {
                    builder = new StringBuilder(size + DefaultStringBuilderIncrease);
                }

                builder.Append(this.buffer, cursor, size);
            } while (changeBuffer() > 0);

            return builder.ToString();
        }

        /// <summary>
        /// Reads characters from the file into the buffer
        /// </summary>
        /// <returns>
        /// The number of Chars read. If no Chars are read, we're at the end of
        /// the file
        /// </returns>
        /// <remarks></remarks>
        private int ReadToBuffer()
        {
            Debug.Assert(this.buffer is object, "There's no buffer");
            Debug.Assert(this.reader is object, "There's no StreamReader");

            // Set cursor to beginning of buffer
            this.position = 0;
            var bufferLength = this.buffer.Length;
            Debug.Assert(bufferLength >= DefaultBufferLength, "Buffer shrunk to below default");

            // If the buffer has grown, shrink it back to the default size
            if (bufferLength > DefaultBufferLength)
            {
                bufferLength = DefaultBufferLength;
                this.buffer = new char[bufferLength];
            }

            this.charsRead = this.reader.Read(this.buffer, 0, bufferLength);
            return this.charsRead;
        }

        /// <summary>
        /// Moves the cursor and all the data to the right of the cursor to the
        /// front of the buffer. It then fills the remainder of the buffer from
        /// the file
        /// </summary>
        /// <returns>
        /// The number of Chars read in filling the remainder of the buffer
        /// </returns>
        /// <remarks>
        /// This should be called when we want to make maximum use of the space
        /// in the buffer. Characters to the left of the cursor have already
        /// been read and can be discarded.
        /// </remarks>
        private void SlideCursorToStartOfBuffer()
        {
            Debug.Assert(this.buffer is object, "There's no buffer");
            Debug.Assert(this.reader is object, "There's no StreamReader");
            Debug.Assert(this.position >= 0 && this.position <= this.buffer.Length, "The cursor is out of range");

            // No need to slide if we're already at the beginning
            if (this.position > 0)
            {
                var bufferLength = this.buffer.Length;
                var tempArray = new char[bufferLength];

                Array.Copy(this.buffer, this.position, tempArray, 0, bufferLength - this.position);

                // Fill the rest of the buffer
                var internalCharsRead = this.reader.Read(tempArray, bufferLength - this.position, this.position);
                this.charsRead = this.charsRead - this.position + internalCharsRead;
                this.position = 0;
                this.buffer = tempArray;
            }
        }

        private void ThrowMalformedLineException(string line, long lineNumber)
        {
            this.ErrorLine = line;
            this.ErrorLineNumber = lineNumber;
            throw new MalformedLineException(String.Format(Strings.TextFieldParser_MalFormedFixedWidthLine, lineNumber.ToString(CultureInfo.InvariantCulture)), lineNumber);
        }

        /// <summary>
        /// Validates the delimiters and creates the Regex objects for finding
        /// delimiters or quotes followed by delimiters
        /// </summary>
        /// <remarks></remarks>
        private void ValidateAndEscapeDelimiters()
        {
            Requires.NotNullOrEmpty(this.delimiters, nameof(this.delimiters));

            var Length = this.delimiters.Length;
            var Builder = new StringBuilder();
            var QuoteBuilder = new StringBuilder();

            // Add ending quote pattern. It will be followed by delimiters
            // resulting in a string like: "[ ]*(d1|d2|d3)
            QuoteBuilder.Append(this.EndQuotePattern + "(");
            for (int i = 0, loopTo = Length - 1; i <= loopTo; i++)
            {
                if (this.delimiters[i] is object)
                {
                    // Make sure delimiter is legal
                    if (this.HasFieldsEnclosedInQuotes && this.delimiters[i].IndexOf('"') > -1)
                    {
                        throw new InvalidOperationException(Strings.TextFieldParser_IllegalDelimiter);
                    }

                    var EscapedDelimiter = Regex.Escape(this.delimiters[i]);
                    Builder.Append(EscapedDelimiter + "|");
                    QuoteBuilder.Append(EscapedDelimiter + "|");
                }
                else
                {
                    Debug.Fail("Delimiter element is empty. This should have been caught on input");
                }
            }

            var whitespaceCharacterbuilder = new StringBuilder();
            this.whitespaceCodes.Select(c => (char)c).ForEach(code =>
            {
                if (!this.CharacterIsInDelimiter(code))
                {
                    whitespaceCharacterbuilder.Append(code);
                }
            });

            this.spaceChars = whitespaceCharacterbuilder.ToString();

            // Get rid of trailing | and set regex
            this.delimiterRegex = new Regex(Builder.ToString(0, Builder.Length - 1), DefaultRegexOptions);
            Builder.Append('\r' + "|" + '\n');
            this.delimiterWithEndCharsRegex = new Regex(Builder.ToString(), DefaultRegexOptions);

            // Add end of line (either Cr, Ln, or nothing) and set regex
            QuoteBuilder.Append('\r' + "|" + '\n' + ")|\"$");
        }

        /// <summary>
        /// Determines whether or not the field widths are valid, and sets the
        /// size of a line
        /// </summary>
        /// <remarks></remarks>
        private void ValidateFieldWidths()
        {
            Requires.IsTrue(this.fieldWidths != null && this.FieldWidths.Any(), nameof(this.fieldWidths));

            var length = 0;

            this.fieldWidths![0..^1].ForEach(fw =>
            {
                Debug.Assert(fw > 0, "Bad field width, this should have been caught on input");
                length += fw;
            });

            var lastFieldWidth = this.fieldWidths[^1];
            if (lastFieldWidth > 0)
            {
                length += lastFieldWidth;
            }

            this.lineLength = length;
        }

        /// <summary>
        /// Indicates whether or not a line is valid
        /// </summary>
        /// <param name="line">The line to be tested</param>
        /// <param name="lineNumber">The line number, used for exception</param>
        /// <remarks></remarks>
        private void ValidateFixedWidthLine(StringInfo line, long lineNumber)
        {
            Debug.Assert(line is object, "No Line sent");

            // The only malformed line for fixed length fields is one that's too short
            if (line.LengthInTextElements < this.lineLength)
            {
                this.ThrowMalformedLineException(line.String, lineNumber);
            }
        }

        /// <summary>
        /// Gets full name and path from passed in path.
        /// </summary>
        /// <param name="path">The path to be validated</param>
        /// <returns>The full name and path</returns>
        /// <remarks>Throws if the file doesn't exist or if the path is malformed</remarks>
        private string ValidatePath(string path)
        {
            // Validate and get full path
            var fullPath = FileSystem.NormalizeFilePath(path, nameof(path));

            // Make sure the file exists
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException(String.Format(Strings.IO_FileNotFound_Path, fullPath));
            }

            return fullPath;
        }

        /// <summary>
        /// Checks property settings to ensure we're able to read fields.
        /// </summary>
        /// <remarks>
        /// Throws if we're not able to read fields with current property settings
        /// </remarks>
        private void ValidateReadyToRead()
        {
            if (this.needPropertyCheck)
            {
                switch (this.fieldType)
                {
                    case FieldType.Delimited:
                        this.ValidateAndEscapeDelimiters();
                        break;

                    case FieldType.FixedWidth:
                        this.ValidateFieldWidths();
                        break;

                    default:
                        Debug.Fail("Unknown TextFieldType");
                        break;
                }

                // Check Comment Tokens
                foreach (var Token in this.commentTokens)
                {
                    if (!String.IsNullOrEmpty(Token) && this.HasFieldsEnclosedInQuotes && this.fieldType == FieldType.Delimited && String.Compare(Token.Trim(), "\"", StringComparison.Ordinal) == 0)
                    {
                        throw new InvalidOperationException(Strings.TextFieldParser_InvalidComment);
                    }
                }

                this.needPropertyCheck = false;
            }
        }
    }
}