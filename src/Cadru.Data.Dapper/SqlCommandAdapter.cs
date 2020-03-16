using System;
using System.Diagnostics;
using System.Text;

using Cadru.Data.Dapper.Resources;

namespace Cadru.Data.Dapper
{
    public class SqlCommandAdapter : CommandAdapter
    {
        public SqlCommandAdapter() : base()
        {
            base.QuotePrefix = "[";
            base.QuoteSuffix = "]";
        }

        internal override void ConsistentQuoteDelimiters(string quotePrefix, string quoteSuffix)
        {
            if ((("\"" == quotePrefix) && ("\"" != quoteSuffix)) ||
                (("[" == quotePrefix) && ("]" != quoteSuffix)))
            {
                throw new ArgumentException(Strings.InvalidPrefixSuffix);
            }
        }

        /// <summary>
        /// Escapes the identifier with square brackets. The input has to be in unescaped form, like the parts received from MultipartIdentifier.ParseMultipartIdentifier.
        /// </summary>
        /// <param name="name">name of the identifier, in unescaped form</param>
        /// <returns>escapes the name with [], also escapes the last close bracket with double-bracket</returns>
        public static string EscapeIdentifier(string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name), "null or empty identifiers are not allowed");
            return "[" + name.Replace("]", "]]") + "]";
        }

        /// <summary>
        /// Same as above EscapeIdentifier, except that output is written into StringBuilder
        /// </summary>
        public static void EscapeIdentifier(StringBuilder builder, string name)
        {
            Debug.Assert(builder != null, "builder cannot be null");
            Debug.Assert(!string.IsNullOrEmpty(name), "null or empty identifiers are not allowed");

            builder.Append("[");
            builder.Append(name.Replace("]", "]]"));
            builder.Append("]");
        }

        /// <summary>
        ///  Escape a string to be used inside TSQL literal, such as N'somename' or 'somename'
        /// </summary>
        public static string EscapeStringAsLiteral(string input)
        {
            Debug.Assert(input != null, "input string cannot be null");
            return input.Replace("'", "''");
        }

        /// <summary>
        /// Escape a string as a TSQL literal, wrapping it around with single quotes.
        /// Use this method to escape input strings to prevent SQL injection
        /// and to get correct behavior for embedded quotes.
        /// </summary>
        /// <param name="input">unescaped string</param>
        /// <returns>escaped and quoted literal string</returns>
        public static string MakeStringLiteral(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "''";
            }
            else
            {
                return "'" + EscapeStringAsLiteral(input) + "'";
            }
        }
    }
}
