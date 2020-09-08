//------------------------------------------------------------------------------
// <copyright file="SqlCommandAdapter.cs"
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