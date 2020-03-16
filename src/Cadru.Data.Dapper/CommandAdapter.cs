using System;
using System.ComponentModel;
using System.Text;

namespace Cadru.Data.Dapper
{
    public class CommandAdapter
    {
        internal const string DeleteFrom = "DELETE FROM ";

        internal const string InsertInto = "INSERT INTO ";
        internal const string DefaultValues = " DEFAULT VALUES";
        internal const string Values = " VALUES ";

        internal const string Update = "UPDATE ";

        internal const string Set = " SET ";
        internal const string Where = " WHERE ";
        internal const string SpaceLeftParenthesis = " (";

        internal const string Comma = ", ";
        internal const string Equal = " = ";
        internal const string LeftParenthesis = "(";
        internal const string RightParenthesis = ")";
        internal const string NameSeparator = ".";

        internal const string Exists = " EXISTS ";
        internal const string NotExists = " NOT EXISTS ";
        internal const string IsNull = " IS NULL";
        internal const string IsNotNull = " IS NOT NULL";
        internal const string Like = " LIKE ";
        internal const string NotLike = " NOT LIKE ";
        internal const string Between = " BETWEEN ";
        internal const string NotBetween = " NOT BETWEEN ";
        internal const string In = " IN ";
        internal const string NotIn = " NOT IN ";
        internal const string EqualOne = " = 1";
        internal const string And = " AND ";
        internal const string Or = " OR ";
        internal const string Not = " NOT ";
        internal const string SelectOne = "SELECT 1 FROM ";
        internal const string SelectStar = "SELECT * FROM ";
        internal const string SelectTopOne = "SELECT TOP 1 * FROM ";
        internal const string As = " AS ";
        internal const string SetNoCountOn = "SET NOCOUNT ON ";

        private string _catalogSeparator = NameSeparator;
        private string _schemaSeparator = NameSeparator;
        private string _quotePrefix = String.Empty;
        private string _quoteSuffix = String.Empty;
        private string _parameterNamePattern = null;
        private string _parameterMarkerFormat = null;
        private int _parameterNameMaxLength = 0;

        [DefaultValueAttribute("")]
        public virtual string QuotePrefix
        {
            get { return _quotePrefix ?? String.Empty; }
            set
            {
                _quotePrefix = value;
            }
        }

        [DefaultValueAttribute("")]
        public virtual string QuoteSuffix
        {
            get
            {
                string quoteSuffix = _quoteSuffix;
                return ((null != quoteSuffix) ? quoteSuffix : String.Empty);
            }
            set
            {
                _quoteSuffix = value;
            }
        }


        [DefaultValueAttribute(CommandAdapter.NameSeparator)]
        public virtual string SchemaSeparator
        {
            get
            {
                string schemaSeparator = _schemaSeparator;
                return (((null != schemaSeparator) && (0 < schemaSeparator.Length)) ? schemaSeparator : NameSeparator);
            }
            set
            {
                _schemaSeparator = value;
            }
        }

        internal virtual void ConsistentQuoteDelimiters(string quotePrefix, string quoteSuffix)
        {
        }

        internal static string AppendQuotedString(StringBuilder buffer, string quotePrefix, string quoteSuffix, string unQuotedString)
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

            return buffer.ToString();
        }

        internal static string BuildQuotedString(string quotePrefix, string quoteSuffix, string unQuotedString)
        {
            var resultString = new StringBuilder(unQuotedString.Length + quoteSuffix.Length + quoteSuffix.Length);
            AppendQuotedString(resultString, quotePrefix, quoteSuffix, unQuotedString);
            return resultString.ToString();
        }

        // the return value is true if the string was quoted and false if it was not
        // this allows the caller to determine if it is an error or not for the quotedString to not be quoted
        internal static bool RemoveStringQuotes(string quotePrefix, string quoteSuffix, string quotedString, out string unquotedString)
        {
            var prefixLength = quotePrefix != null ? quotePrefix.Length : 0;
            var suffixLength = quoteSuffix != null ? quoteSuffix.Length : 0;

            if ((suffixLength + prefixLength) == 0)
            {
                unquotedString = quotedString;
                return true;
            }

            if (quotedString == null)
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
            if (prefixLength > 0)
            {
                if (!quotedString.StartsWith(quotePrefix, StringComparison.Ordinal))
                {
                    unquotedString = quotedString;
                    return false;
                }
            }

            // is the suffix present?
            if (suffixLength > 0)
            {
                if (!quotedString.EndsWith(quoteSuffix, StringComparison.Ordinal))
                {
                    unquotedString = quotedString;
                    return false;
                }
                unquotedString = quotedString.Substring(prefixLength, quotedStringLength - (prefixLength + suffixLength)).Replace(quoteSuffix + quoteSuffix, quoteSuffix);
            }
            else
            {
                unquotedString = quotedString.Substring(prefixLength, quotedStringLength - prefixLength);
            }
            return true;
        }

        public virtual string QuoteIdentifier(string unquotedIdentifier)
        {
            Contracts.Requires.NotNullOrWhiteSpace(unquotedIdentifier, nameof(unquotedIdentifier));
            var quoteSuffixLocal = QuoteSuffix;
            var quotePrefixLocal = QuotePrefix;
            ConsistentQuoteDelimiters(quotePrefixLocal, quoteSuffixLocal);
            return BuildQuotedString(quotePrefixLocal, quoteSuffixLocal, unquotedIdentifier);
        }

        public virtual string UnquoteIdentifier(string quotedIdentifier)
        {
            Contracts.Requires.NotNullOrWhiteSpace(quotedIdentifier, nameof(quotedIdentifier));
            string unquotedIdentifier;
            var quoteSuffixLocal = QuoteSuffix;
            var quotePrefixLocal = QuotePrefix;
            ConsistentQuoteDelimiters(quotePrefixLocal, quoteSuffixLocal);
            // ignoring the return value because an unquoted source string is OK here
            RemoveStringQuotes(quotePrefixLocal, quoteSuffixLocal, quotedIdentifier, out unquotedIdentifier);
            return unquotedIdentifier;
        }
    }
}
