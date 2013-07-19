using System;
using System.Collections.Generic;

namespace StringTools
{

    #region [Summary and remarks]
    /// <summary>
    /// Implements a StringTools.StringTokenizer class for splitting a string
    /// into substrings using a set of delimiters.
    /// </summary>
    /// <remarks>
    /// C# version of the java.util.StringTokenizer class.
    /// Basicly it's a wrapper class around the <c>String.Split</c> method.<pare/>
    /// It implements all of it's Java equivalent methods apart from those only needed by the Enumeration interface.
    /// All implemented Java-compilant methods have their C# equivalents in properties. They however differ in names
    /// since Java uses the (Hungarian-like) notation <c>runMe()</c> while C# uses Camel-cased <c>RunMe()</c> and thus
    /// Java's <c>nextToken()</c> method is just an alias of the <c>NextToken</c> property.
    /// </remarks>
    #endregion
    public class StringTokenizer : IEnumerable<string>
    {
        /// <summary>
        /// String conatining the default set of delimiters which are <c>" \t\n\r\f"</c>:
        /// the space character, the tab character, the newline character, the carriage-return character, and the form-feed character.
        /// </summary>
        public const string DefaultDelimiters = " \t\n\r\f";

        private readonly string delims = DefaultDelimiters;
        private string[] tokens=null;
        private int index = 0;
        private string empty = String.Empty;

        #region [Constructors]
        /// <summary>
        /// Constructs a string tokenizer for the specified string using the <see cref="F:DefaultDelimiters">default delimiters</see>.
        /// </summary>
        /// <param name="str">The string to be tokenized.</param>
        /// <exception cref="System.NullReferenceException">Thrown when the passed string is <c>null</c></exception>
        public StringTokenizer(string str)
        {
            Tokenize(str, false, false);
        }

        /// <summary>
        /// Constructs a string tokenizer for the specified string using the given delimiters.
        /// </summary>
        /// <param name="str">The string to be tokenized.</param>
        /// <param name="delims">The delimiters used to tokenize the string (each <see cref="!:char"/> will be used as a delimiter).</param>
        /// <exception cref="System.NullReferenceException">Thrown when the passed string is <c>null</c></exception>
        public StringTokenizer(string str, string delims)
        {
            if(delims!=null) this.delims = delims;
            Tokenize(str, false, false);
        }

        /// <summary>
        /// Constructs a string tokenizer for the specified string using the given delimiters.
        /// </summary>
        /// <param name="str">The string to be tokenized.</param>
        /// <param name="delims">The delimiters used to tokenize the string.</param>
        public StringTokenizer(string str, params char[] delims)
        {
            if (delims != null) this.delims = new string(delims);
            Tokenize(str, false, false);
        }

        /// <summary>
        /// Constructs a string tokenizer for the specified string using the given delimiters and optionally returning them as tokens.
        /// </summary>
        /// <param name="str">The string to be tokenized.</param>
        /// <param name="delims">The delimiters used to tokenize the string (each <see cref="!:char"/> will be used as a delimiter).</param>
        /// <param name="returnDelims">If set to <c>true</c> the encountered delimiters will also be returned as tokens.</param>
        /// <exception cref="System.NullReferenceException">Thrown when the passed string is <c>null</c></exception>
        public StringTokenizer(string str, string delims, bool returnDelims)
        {
            if (delims != null) this.delims = delims;
            Tokenize(str, returnDelims, false);
        }

        /// <summary>
        /// Constructs a string tokenizer for the specified string using the given delimiters,
        /// optionally returning them as tokens. Also empty tokens may be returned using the <see cref="!:String.Empty"/> string.
        /// </summary>
        /// <param name="str">The string to be tokenized.</param>
        /// <param name="delims">The delimiters used to tokenize the string (each <see cref="!:char"/> will be used as a delimiter).</param>
        /// <param name="returnDelims">If set to <c>true</c> the encountered delimiters will also be returned as tokens.</param>
        /// <param name="returnEmpty">If set to <c>true</c> empty tokens will also be returned.</param>
        /// <exception cref="System.NullReferenceException">Thrown when the passed string is <c>null</c></exception>
        public StringTokenizer(string str, string delims, bool returnDelims, bool returnEmpty)
        {
            if (delims != null) this.delims = delims;
            Tokenize(str, returnDelims, returnEmpty);
        }

        /// <summary>
        /// Constructs a string tokenizer for the specified string using the given delimiters,
        /// optionally returning them as tokens. Also empty tokens may be returned using the <paramref name="empty"/> string.
        /// </summary>
        /// <param name="str">The string to be tokenized.</param>
        /// <param name="delims">The delimiters used to tokenize the string (each <see cref="!:char"/> will be used as a delimiter).</param>
        /// <param name="returnDelims">If set to <c>true</c> the encountered delimiters will also be returned as tokens.</param>
        /// <param name="returnEmpty">If set to <c>true</c> empty tokens will also be returned.</param>
        /// <param name="empty">The string to be returned as an empty token.</param>
        /// <exception cref="System.NullReferenceException">Thrown when the passed string is <c>null</c></exception>
        public StringTokenizer(string str, string delims, bool returnDelims, bool returnEmpty, string empty)
        {
            if (delims != null) this.delims = delims;
            this.empty = empty;
            Tokenize(str, returnDelims, returnEmpty);
        }
#endregion

        #region [The big tokenization method]
        private void Tokenize(string str, bool returnDelims, bool returnEmpty)
        {
            if(returnDelims)
            {
                this.tokens = str.Split(this.delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                List<string> tmp = new List<string>(tokens.Length << 1);

                int delimIndex = str.IndexOfAny(this.delims.ToCharArray());
                int tokensIndex = 0;
                int prevDelimIdx = delimIndex-1;

                if (delimIndex == 0)
                    do
                    {
                        tmp.Add(new string(str[delimIndex], 1));
                        prevDelimIdx = delimIndex++;
                        delimIndex = str.IndexOfAny(this.delims.ToCharArray(), delimIndex);
                        if (returnEmpty && delimIndex == prevDelimIdx + 1)
                            tmp.Add(this.empty);
                    } while (delimIndex == prevDelimIdx + 1);

                while (delimIndex > -1)
                {
                    tmp.Add(this.tokens[tokensIndex++]);

                    do
                    {
                        tmp.Add(new string(str[delimIndex], 1));
                        prevDelimIdx = delimIndex++;
                        delimIndex = str.IndexOfAny(this.delims.ToCharArray(), delimIndex);
                        if (returnEmpty && delimIndex == prevDelimIdx + 1)
                            tmp.Add(this.empty);
                    } while (delimIndex == prevDelimIdx + 1);

                }
                if (tokensIndex < tokens.Length)
                    tmp.Add(this.tokens[tokensIndex++]);

                this.tokens = tmp.ToArray();
                tmp = null;
            }
            else if (returnEmpty)
            {
                this.tokens = str.Split(this.delims.ToCharArray(), StringSplitOptions.None);
                if (this.empty != String.Empty)
                    for(int i=0; i<this.tokens.Length; i++)
                        if (this.tokens[i] == String.Empty) this.tokens[i] = this.empty;
            }
            else
                this.tokens = str.Split(this.delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion

        #region [Properties covering Java methods]
        /// <summary>
        /// Tests if there are more tokens available from this tokenizer's string.
        /// If this method returns <c>true</c>, then a subsequent
        /// use of the <see cref="P:NextToken"/> property will successfully return a token.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if more tokens are available; otherwise <c>false</c>.
        /// </value>
        public bool HasMoreTokens
        {
            get { return this.index < this.tokens.Length; }
        }

        /// <summary>
        /// Gets the next token.
        /// </summary>
        /// <value>The next token.</value>
        /// <exception cref="System.IndexOutOfRangeException">Thrown when trying to get a token which doesn't exist.
        /// Usually caused by not checking if the <see cref="P:HasMoreTokens"/> property returns <c>true</c> before trying to get the next token.</exception>
        public string NextToken
        {
            get { return this.tokens[index++]; }
        }

        /// <summary>
        /// Counts the <see cref="!:remaining"/> tokens - the number of times the
        /// <see cref="P:NextToken"/> property can be used before it throws an exception.
        /// </summary>
        /// <value>The number of remaining tokens.</value>
        /// <seealso cref="P:Count"/>
        public int CountTokens
        {
            get { return this.tokens.Length - this.index; }
        }
#endregion

        #region [New methods/properties]
        /// <summary>
        /// Gets the total number of tokens extracted.
        /// </summary>
        /// <remarks>
        /// <see cref="!:Equivalent not available in Java!"/>
        /// This property returns the total number of extracted tokens,
        /// contrary to <see cref="P:CountTokens"/>.
        /// </remarks>
        /// <value>The number of tokens extracted.</value>
        /// <seealso cref="P:StringTokenizer.CountTokens"/>
        public int Count
        {
            get { return this.tokens.Length; }
        }

        /// <summary>
        /// Gets the token with the specified index from the tokenizer without moving the current position index.
        /// </summary>
        /// <remarks><see cref="!:Equivalent not available in Java!"/></remarks>
        /// <param name="index">The index of the token to get.</param>
        /// <value>The token with the given index</value>
        /// <exception cref="System.IndexOutOfRangeException">Thrown when trying to get a token which doesn't exist, that is when <see cref="!:index"/> is equal or greater then <see cref="!:Count"/> or <see cref="!:index"/> is negative.</exception>
        public string this[int index]
        {
            get { return this.tokens[index]; }
        }

        /// <summary>
        /// Resets the current position index so that the tokens can be extracted again.
        /// </summary>
        /// <remarks><see cref="!:Equivalent not available in Java!"/></remarks>
        public void Reset()
        {
            this.index = 0;
        }

        /// <summary>
        /// Gets the currently set string for empty tokens.
        /// </summary>
        /// <remarks>Default is <c>System.String.Empty</c></remarks>
        /// <value>The empty token string.</value>
        public string EmptyString
        {
            get { return this.empty; }
        }
        #endregion

        #region [Java-compilant methods]

        /// <summary>
        /// Tests if there are more tokens available from this tokenizer's string.
        /// If this method returns <c>true</c>, then a subsequent call to <see cref="M:nextToken"/> will successfully return a token.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if and only if there is at least one token in the string after the current position; otherwise <c>false</c>.
        /// </returns>
        /// <seealso cref="M:nextToken"/>
        public bool hasMoreTokens()
        {
            return HasMoreTokens;
        }

        /// <summary>
        /// Returns the next token from this string tokenizer.
        /// </summary>
        /// <returns>The next token from this string tokenizer.</returns>
        public string nextToken()
        {
            return NextToken;
        }

        /// <summary>
        /// Calculates the number of times that this tokenizer's <see cref="M:nextToken"/> method can be called before it generates an exception. The current position is not advanced.
        /// </summary>
        /// <returns>The number of tokens remaining in the string using the current delimiter set.</returns>
        public int countTokens()
        {
            return CountTokens;
        }
        #endregion

        #region [IEnumerable implementation]
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<string> GetEnumerator()
        {
            while (this.HasMoreTokens)
                yield return this.NextToken;
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }
        #endregion

    }
}
