using System;

using Cadru.Data.Dapper.Resources;

namespace Cadru.Data.Dapper
{
    public class SqlCommandAdapter : CommandAdapter
    {
        public SqlCommandAdapter() : base()
        {
        }

        public override string IdentifierPrefix => "[";
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
