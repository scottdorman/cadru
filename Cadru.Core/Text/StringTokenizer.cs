// ---------------------------------------------------------------------------
// Campari Software
//
// StringTokenizer.cs
//
//
// ---------------------------------------------------------------------------
// Copyright (C) 2006 Campari Software
// All rights reserved.
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
// FITNESS FOR A PARTICULAR PURPOSE.
// ---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Campari.Software.Text
{
    #region class StringTokenizer
    /// <summary>
    /// Implements a StringTools.StringTokenizer class for splitting a string
    /// into substrings using a set of delimiters.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification="Renaming this class to end in 'Collection' would change the implied meaning.")]
    public class StringTokenizer : IEnumerable, IEnumerable<string>
    {
        #region events

        #endregion

        #region class-wide fields
        private static char[] defaultSeparator = new char[] { ' ', '\t', '\n', '\r', '\f' };
        private readonly int optionsMask = (int)~(StringTokenizeOptions.RemoveEmptyEntries | StringTokenizeOptions.None);

        private int index;
        private string[] tokens;

        #endregion

        #region private and internal properties and methods

        #region properties

        #endregion

        #region methods

        #region constructor
        private StringTokenizer()
        {
        }
        #endregion

        #region IEnumerable.GetEnumerator
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region InternalTokenize

        #region InternalTokenize(string source, string[] separator, bool omitEmptyEntries)
        private void InternalTokenize(string source, string[] separator, bool omitEmptyEntries)
        {
            if (omitEmptyEntries)
            {
                this.tokens = source.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                this.tokens = source.Split(separator, StringSplitOptions.None);
            }
        }
        #endregion

        #region InternalTokenize(string source, char[] separator, bool omitEmptyEntries)
        private void InternalTokenize(string source, char[] separator, bool omitEmptyEntries)
        {
            if (omitEmptyEntries)
            {
                this.tokens = source.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                this.tokens = source.Split(separator, StringSplitOptions.None);
            }
        }
        #endregion

        #endregion

        #endregion

        #endregion

        #region public properties and methods

        #region properties

        #region Count
        /// <summary>
        /// Gets the total number of tokens extracted.
        /// </summary>
        /// <value>The total number of tokens extracted.</value>
        /// <seealso cref="RemainingTokens"/>
        public int Count
        {
            get
            {
                return this.tokens.Length;
            }
        }
        #endregion

        #region HasMoreTokens
        /// <summary>
        /// Gets a <see cref="Boolean"/> value indicating if 
        /// there are more tokens available for processing.
        /// </summary>
        /// <value><c>true</c> if more tokens are available; otherwise <c>false</c>.
        /// </value>
        /// <remarks><see cref="HasMoreTokens"/> should be called before a call to
        /// <see cref="NextToken"/> to prevent an <see cref="IndexOutOfRangeException"/>.</remarks>
        public bool HasMoreTokens
        {
            get
            {
                return this.index < this.tokens.Length;
            }
        }
        #endregion

        #region RemainingTokens
        /// <summary>
        /// Gets the number of tokens remaining to be processed.
        /// </summary>
        /// <value>The number of remaining tokens.</value>
        /// <seealso cref="Count"/>
        public int RemainingTokens
        {
            get
            {
                return this.tokens.Length - this.index;
            }
        }
        #endregion

        #endregion

        #region methods

        #region constructors

        #region StringTokenizer(string source)
        /// <summary>
        /// Constructs a string tokenizer for the specified string 
        /// using the default separators.
        /// </summary>
        /// <param name="source">The string to be tokenized.</param>
        /// <remarks><para>The default separators are the space character, 
        /// the tab character, the newline character, the carriage-return
        /// character, and the form-feed character.</para>
        /// <para>Any empty tokens contained in <paramref name="source"/> will be returned.</para>
        /// </remarks>
        public StringTokenizer(string source)
            : this(source, StringTokenizer.defaultSeparator, StringTokenizeOptions.None)
        {
        }
        #endregion

        #region StringTokenizer(string source, params char[] separator)
        /// <summary>
        /// Constructs a string tokenizer for the specified string 
        /// using the provided separator array.
        /// </summary>
        /// <param name="source">The string to be tokenized.</param>
        /// <param name="separator">The array of characters used to separate the tokens.</param>
        /// <remarks>Any empty tokens contained in <paramref name="source"/> will be returned.</remarks>
        public StringTokenizer(string source, params char[] separator)
            : this(source, separator, StringTokenizeOptions.None)
        {
        }
        #endregion

        #region StringTokenizer(string source, char[] separator, StringTokenizeOptions options)
        /// <summary>
        /// Constructs a string tokenizer for the specified string 
        /// using the <see cref="F:DefaultSeparator">default delimiters</see>.
        /// </summary>
        /// <param name="source">The string to be tokenized.</param>
        /// <param name="separator">The array of characters used to separate the tokens.</param>
        /// <param name="options">One of the <see cref="StringTokenizeOptions"/> values.</param>
        public StringTokenizer(string source, char[] separator, StringTokenizeOptions options) : this()
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source.Length == 0)
            {
                throw new ArgumentException(Properties.Resources.Argument_StringZeroLength, "source");
            }

            if (((int)options & optionsMask) != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.CurrentUICulture, Properties.Resources.Argument_EnumIllegalVal, (int)options), "options");
            }

            bool omitEmptyEntries = ((options & StringTokenizeOptions.RemoveEmptyEntries) != 0);
            InternalTokenize(source, separator, omitEmptyEntries);
        }
        #endregion

        #region StringTokenizer(string source, params string[] separator)
        /// <summary>
        /// Constructs a string tokenizer for the specified string 
        /// using the <see cref="F:DefaultSeparator">default delimiters</see>.
        /// </summary>
        /// <param name="source">The string to be tokenized.</param>
        /// <param name="separator">The array of strings used to separate the tokens.</param>
        /// <remarks>Any empty tokens contained in <paramref name="source"/> will be returned.</remarks>
        public StringTokenizer(string source, params string[] separator)
            : this(source, separator, StringTokenizeOptions.None)
        {
        }
        #endregion

        #region StringTokenizer(string source, string[] separator, StringTokenizeOptions options)
        /// <summary>
        /// Constructs a string tokenizer for the specified string 
        /// using the <see cref="F:DefaultSeparator">default delimiters</see>.
        /// </summary>
        /// <param name="source">The string to be tokenized.</param>
        /// <param name="separator">The array of strings used to separate the tokens.</param>
        /// <param name="options">One of the <see cref="StringTokenizeOptions"/> values.</param>
        public StringTokenizer(string source, string[] separator, StringTokenizeOptions options)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source.Length == 0)
            {
                throw new ArgumentException(Properties.Resources.Argument_StringZeroLength, "source");
            }

            if (((int)options & optionsMask) != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.CurrentUICulture, Properties.Resources.Argument_EnumIllegalVal, (int)options), "options");
            }

            bool omitEmptyEntries = ((options & StringTokenizeOptions.RemoveEmptyEntries) != 0);
            InternalTokenize(source, separator, omitEmptyEntries);
        }
        #endregion

        #endregion

        #region Advance
        /// <summary>
        /// Advances the index by <paramref name="count"/> tokens.
        /// </summary>
        /// <param name="count">The number of tokens to advance.</param>
        /// <returns><c>true</c> if the index was advanced; otherwise <c>false</c></returns>
        /// <remarks>If advancing the index would pass the number of remaining tokens, the index will not be
        /// advanced and the return value will be <c>false</c>.</remarks>
        public bool Advance(int count)
        {
            if (index + count < this.RemainingTokens)
            {
                index += count;
                return true;
            }
            return (false);
        }
        #endregion

        #region GetEnumerator
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<string> GetEnumerator()
        {
            while (this.HasMoreTokens)
            {
                yield return this.NextToken();
            }
        }

        #endregion

        #region Indexer
        /// <summary>
        /// Gets the token with the specified index without moving the current position.
        /// </summary>
        /// <param name="index">The index of the token to get.</param>
        /// <value>The token with the given index</value>
        /// <exception cref="System.IndexOutOfRangeException">
        /// <para><see cref="index"/> is equal or greater than <see cref="Count"/>.</para>
        /// <para>-or-</para>
        /// <para><see cref="index"/> is negative.</para>
        /// </exception>
        public string this[int index]
        {
            get { 
                return this.tokens[index]; 
            }
        }
        #endregion

        #region NextToken
        /// <summary>
        /// Returns the next token from this string tokenizer.
        /// </summary>
        /// <returns>The next token from this string tokenizer.</returns>
        /// <exception cref="System.IndexOutOfRangeException">
        /// <para><see cref="index"/> is equal or greater than <see cref="Count"/>.</para>
        /// <para>-or-</para>
        /// <para><see cref="index"/> is negative.</para>
        /// </exception>
        public string NextToken()
        {
            return this.tokens[index++];
        }
        #endregion

        #region Reset
        /// <summary>
        /// Resets the current position index so that the tokens can be extracted again.
        /// </summary>
        public void Reset()
        {
            this.index = 0;
        }
        #endregion

        #endregion

        #endregion
    }
    #endregion
}
