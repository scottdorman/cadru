using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Cadru.Data.Dapper
{
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

        public virtual string CatalogSeparator => this.NameSeparator;
        public virtual string NameSeparator => ".";
        public virtual int ParameterNameMaxLength => 128;
        public virtual string ParameterNamePattern => "{parameterMarker}{parameterName}";
        public virtual string ParameterPrefix => "@";
        public virtual string QuotePrefix => "'";
        public virtual string QuoteSuffix => this.QuotePrefix;
        public virtual string SchemaSeparator => this.NameSeparator;
        public virtual string IdentifierPrefix => String.Empty;
        public virtual string IdentifierSuffix => String.Empty;

        /// <summary>
        ///
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        /// <remarks>
        /// <list type="number">
        /// <item>
        /// <description>
        /// The first character must be one of the following:
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// A letter as defined by the Unicode Standard 3.2. The Unicode
        /// definition of letters includes Latin characters from a through z,
        /// from A through Z, and also letter characters from other languages.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// <para>The underscore (_), at sign (@), or number sign (#).</para>
        /// <para>
        /// Certain symbols at the beginning of an identifier have special
        /// meaning in SQL Server. A regular identifier that starts with the at
        /// sign always denotes a local variable or parameter and cannot be used
        /// as the name of any other type of object. An identifier that starts
        /// with a number sign denotes a temporary table or procedure. An
        /// identifier that starts with double number signs (##) denotes a
        /// global temporary object. Although the number sign or double number
        /// sign characters can be used to begin the names of other types of
        /// objects, we do not recommend this practice.
        /// </para>
        /// <para>
        /// Some Transact-SQL functions have names that start with double at
        /// signs (@@). To avoid confusion with these functions, you should not
        /// use names that start with @@.
        /// </para>
        /// </description>
        /// </item>
        /// </list>
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Subsequent characters can include the following:
        /// <list type="bullet">
        /// <item>
        /// <description>Letters as defined in the Unicode Standard
        /// 3.2.</description>
        /// </item>
        /// <item>
        /// <description>Decimal numbers from either Basic Latin or other
        /// national scripts.</description>
        /// </item>
        /// <item>
        /// <description>The at sign, dollar sign ($), number sign, or
        /// underscore.</description>
        /// </item>
        /// </list>
        /// </description>
        /// </item>
        /// <item>
        /// <description>Embedded spaces or special characters are not
        /// allowed.</description>
        /// </item>
        /// <item>
        /// <description>Supplementary characters are not allowed.</description>
        /// </item>
        /// </list>
        /// </remarks>
        public virtual bool IsValidIdentifier(string identifier)
        {
            return Regex.IsMatch(identifier, @"^[\p{L}_@#]{1}[\p{L}\p{Nd}$@#_]+$");
        }

        public virtual string GetParameterName(string parameterName)
        {
            if (!parameterName.StartsWith(this.ParameterPrefix, StringComparison.InvariantCultureIgnoreCase))
            {
                parameterName = $"{this.ParameterPrefix}{parameterName}";
            }

            return parameterName;
        }

        public virtual string QouteStringLiteral(string value)
        {
            Contracts.Requires.NotNullOrWhiteSpace(value, nameof(value));
            this.ConsistentQuoteDelimiters(this.QuotePrefix, this.QuoteSuffix);
            return this.BuildQuotedString(this.QuotePrefix, this.QuoteSuffix, value);
        }

        /// <inheritdoc/>
        public virtual string QuoteIdentifier(string identifier)
        {
            Contracts.Requires.NotNullOrWhiteSpace(identifier, nameof(identifier));
            Contracts.Requires.IsTrue(this.IsValidIdentifier(identifier));

            this.ConsistentQuoteDelimiters(this.IdentifierPrefix, this.IdentifierSuffix);
            return this.BuildQuotedString(this.IdentifierPrefix, this.IdentifierSuffix, identifier);
        }

        /// <inheritdoc/>
        public virtual string UnquoteIdentifier(string identifier)
        {
            Contracts.Requires.NotNullOrWhiteSpace(identifier, nameof(identifier));

            this.ConsistentQuoteDelimiters(this.IdentifierPrefix, this.IdentifierSuffix);

            if (this.RemoveStringQuotes(identifier, this.IdentifierPrefix, this.IdentifierSuffix, out var unquotedIdentifier)
                && !this.IsValidIdentifier(unquotedIdentifier))
            {
                throw new InvalidOperationException();
            }

            return unquotedIdentifier;
        }

        private static void AppendQuotedString(StringBuilder buffer, string quotePrefix, string quoteSuffix, string unQuotedString)
        {

            if (!String.IsNullOrEmpty(quotePrefix))
            {
                buffer.Append(quotePrefix);
            }

            // Assuming that the suffix is escaped by doubling it. i.e. foo"bar becomes "foo""bar".
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

        internal string BuildQuotedString(string prefix, string suffix, string value)
        {
            var resultString = new StringBuilder(value.Length + prefix.Length + suffix.Length);
            AppendQuotedString(resultString, prefix, suffix, value);
            return resultString.ToString();
        }

        // the return value is true if the string was quoted and false if it was not
        // this allows the caller to determine if it is an error or not for the quotedString to not be quoted
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
                unquotedString = quotedString[prefixLength..quotedStringLength];
            }
            return true;
        }

        internal virtual void ConsistentQuoteDelimiters(string quotePrefix, string quoteSuffix)
        {
        }
    }
}
