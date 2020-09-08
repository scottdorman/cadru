using System;

using Cadru.Data.Dapper.Resources;

namespace Cadru.Data.Dapper
{
    /// <summary>
    /// Represents a way to create Microsoft SQL Server specific SQL statements.
    /// </summary>
    public class SqlCommandAdapter : CommandAdapter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCommandAdapter"/> class.
        /// </summary>
        public SqlCommandAdapter() : base()
        {
        }

        /// <inheritdoc/>
        public override string IdentifierPrefix => "[";

        /// <inheritdoc/>
        public override string IdentifierSuffix => "]";

        internal override void ConsistentQuoteDelimiters(string quotePrefix, string quoteSuffix)
        {
            if ((("\"" == quotePrefix) && ("\"" != quoteSuffix)) ||
                (("[" == quotePrefix) && ("]" != quoteSuffix)) ||
                (("'" == quotePrefix) && ("'" != quoteSuffix)))
            {
                throw new ArgumentException(Strings.InvalidPrefixSuffix);
            }
        }
    }
}
