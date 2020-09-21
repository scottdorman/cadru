//------------------------------------------------------------------------------
// <copyright file="CommandAdapter.cs"
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
using System.Text;
using System.Text.RegularExpressions;

using Cadru.Data.Dapper.Resources;

using Validation;

namespace Cadru.Data.Dapper
{
    /// <summary>
    /// Represents a way to create provider specific SQL statements.
    /// </summary>
    public class CommandAdapter : ICommandAdapter
    {
        internal const string And = " AND ";
        internal const string As = " AS ";
        internal const string Between = " BETWEEN ";
        internal const string Comma = ", ";
        internal const string DefaultValues = " DEFAULT VALUES";
        internal const string DeleteFrom = "DELETE FROM ";
        internal const string Equal = " = ";
        internal const string EqualOne = " = 1";
        internal const string Exists = " EXISTS ";
        internal const string In = " IN ";
        internal const string InsertInto = "INSERT INTO ";
        internal const string IsNotNull = " IS NOT NULL";
        internal const string IsNull = " IS NULL";
        internal const string LeftParenthesis = "(";
        internal const string Like = " LIKE ";
        internal const string Not = " NOT ";
        internal const string NotBetween = " NOT BETWEEN ";
        internal const string NotExists = " NOT EXISTS ";
        internal const string NotIn = " NOT IN ";
        internal const string NotLike = " NOT LIKE ";
        internal const string Or = " OR ";
        internal const string RightParenthesis = ")";
        internal const string SelectOne = "SELECT 1 FROM ";
        internal const string SelectStar = "SELECT * FROM ";
        internal const string SelectTopOne = "SELECT TOP 1 * FROM ";
        internal const string Set = " SET ";
        internal const string SetNoCountOn = "SET NOCOUNT ON ";
        internal const string SpaceLeftParenthesis = " (";
        internal const string Update = "UPDATE ";
        internal const string Values = " VALUES ";
        internal const string Where = " WHERE ";

        /// <inheritdoc/>
        public virtual string CatalogSeparator => this.NameSeparator;

        /// <inheritdoc/>
        public virtual string IdentifierPrefix => String.Empty;

        /// <inheritdoc/>
        public virtual string IdentifierSuffix => String.Empty;

        /// <inheritdoc/>
        public virtual string NameSeparator => ".";

        /// <inheritdoc/>
        public virtual int ParameterNameMaxLength => 128;

        /// <inheritdoc/>
        public virtual string ParameterNamePattern => "{parameterMarker}{parameterName}";

        /// <inheritdoc/>
        public virtual string ParameterPrefix => "@";

        /// <inheritdoc/>
        public virtual string QuotePrefix => "'";

        /// <inheritdoc/>
        public virtual string QuoteSuffix => this.QuotePrefix;

        /// <inheritdoc/>
        public virtual string SchemaSeparator => this.NameSeparator;

        /// <inheritdoc/>
        public virtual string GetParameterName(string parameterName)
        {
            if (!parameterName.StartsWith(this.ParameterPrefix, StringComparison.InvariantCultureIgnoreCase))
            {
                parameterName = $"{this.ParameterPrefix}{parameterName}";
            }

            return parameterName;
        }

        /// <inheritdoc/>
        public virtual bool IsValidIdentifier(string identifier)
        {
            return Regex.IsMatch(identifier, @"^[\p{L}_@#]{1}[\p{L}\p{Nd}$@#_]+$");
        }

        /// <inheritdoc/>
        public virtual string QuoteIdentifier(string identifier)
        {
            Requires.NotNullOrWhiteSpace(identifier, nameof(identifier));
            Requires.That(this.IsValidIdentifier(identifier), nameof(identifier), Strings.InvalidIdentifier);

            this.ConsistentQuoteDelimiters(this.IdentifierPrefix, this.IdentifierSuffix);
            return this.BuildQuotedString(this.IdentifierPrefix, this.IdentifierSuffix, identifier);
        }

        /// <inheritdoc/>
        public virtual string QuoteStringLiteral(string value)
        {
            Requires.NotNullOrWhiteSpace(value, nameof(value));
            this.ConsistentQuoteDelimiters(this.QuotePrefix, this.QuoteSuffix);
            return this.BuildQuotedString(this.QuotePrefix, this.QuoteSuffix, value);
        }

        /// <inheritdoc/>
        public virtual string UnquoteIdentifier(string identifier)
        {
            Requires.NotNullOrWhiteSpace(identifier, nameof(identifier));

            this.ConsistentQuoteDelimiters(this.IdentifierPrefix, this.IdentifierSuffix);

            if (this.RemoveStringQuotes(identifier, this.IdentifierPrefix, this.IdentifierSuffix, out var unquotedIdentifier)
                && !this.IsValidIdentifier(unquotedIdentifier))
            {
                throw new InvalidOperationException();
            }

            return unquotedIdentifier;
        }

        internal string BuildQuotedString(string prefix, string suffix, string value)
        {
            var resultString = new StringBuilder(value.Length + prefix.Length + suffix.Length);
            AppendQuotedString(resultString, prefix, suffix, value);
            return resultString.ToString();
        }

        internal virtual void ConsistentQuoteDelimiters(string quotePrefix, string quoteSuffix)
        {
        }

        private static void AppendQuotedString(StringBuilder buffer, string quotePrefix, string quoteSuffix, string unQuotedString)
        {
            if (!String.IsNullOrEmpty(quotePrefix))
            {
                buffer.Append(quotePrefix);
            }

            // Assuming that the suffix is escaped by doubling it. i.e. foo"bar
            // becomes "foo""bar".
            if (!String.IsNullOrEmpty(quoteSuffix))
            {
                var start = buffer.Length;
                buffer.Append(unQuotedString);
                buffer.Replace(quoteSuffix, quoteSuffix + quoteSuffix, start, unQuotedString.Length);
                buffer.Append(quoteSuffix);
            }
            else
            {
                buffer.Append(unQuotedString);
            }
        }

        // the return value is true if the string was quoted and false if it was
        // not this allows the caller to determine if it is an error or not for
        // the quotedString to not be quoted
        private bool RemoveStringQuotes(string quotedString, string prefix, string suffix, out string unquotedString)
        {
            var prefixLength = prefix != null ? prefix.Length : 0;
            var suffixLength = suffix != null ? suffix.Length : 0;

            if ((suffixLength + prefixLength) == 0)
            {
                unquotedString = quotedString;
                return false;
            }

            var quotedStringLength = quotedString.Length;

            // is the source string too short to be quoted
            if (quotedStringLength < prefixLength + suffixLength)
            {
                unquotedString = quotedString;
                return false;
            }

            // is the prefix present?
            if (prefixLength > 0 && !quotedString.StartsWith(prefix, StringComparison.Ordinal))
            {
                unquotedString = quotedString;
                return false;
            }

            // is the suffix present?
            if (suffixLength > 0)
            {
                if (!quotedString.EndsWith(suffix, StringComparison.Ordinal))
                {
                    unquotedString = quotedString;
                    return false;
                }

                unquotedString = quotedString.Substring(prefixLength, quotedStringLength - (prefixLength + suffixLength)).Replace(suffix + suffix, suffix);
            }
            else
            {
#if NETSTANDARD2_0
                unquotedString = quotedString.AsSpan().Slice(prefixLength, quotedStringLength).ToString();
#else
                unquotedString = quotedString[prefixLength..quotedStringLength];
#endif
            }
            return true;
        }
    }
}