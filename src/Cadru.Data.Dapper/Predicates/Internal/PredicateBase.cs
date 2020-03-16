//------------------------------------------------------------------------------
// <copyright file="PredicateBase.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2017 Scott Dorman.
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

namespace Cadru.Data.Dapper.Predicates.Internal
{
    using System;
    using System.Linq;
    using System.Text;

    using Cadru.Extensions;

    using global::Dapper;

    internal abstract partial class PredicateBase : IPredicateBase
    {
        public bool Not { get; set; }

        public string PropertyName { get; set; }

        public abstract string GetSql(DynamicParameters parameters);

        internal static string GetColumnName<T>(string propertyName, bool alias) where T : class
        {
            Contracts.Requires.NotNullOrWhiteSpace(propertyName, nameof(propertyName));

            var columnName = String.Empty;
            if (Database.Mappings.TryGetValue(typeof(T), out var tableMap))
            {
                var propertyMap = tableMap.Properties.SingleOrDefault(p => p.PropertyName == propertyName);
                if (propertyMap == null)
                {
                    throw new NullReferenceException($"{propertyName} was not found for {typeof(T)}");
                }

                string columnAlias = null;
                if (propertyMap.ColumnName != propertyMap.PropertyName && alias)
                {
                    columnAlias = propertyMap.PropertyName;
                }

                columnName = GetColumnName(tableMap.CommandAdapter, GetTableName(tableMap, null), propertyMap.ColumnName, columnAlias);
            }

            return columnName;
        }

        private static string GetColumnName(CommandAdapter commandAdapter, string prefix, string columnName, string alias)
        {
            Contracts.Requires.NotNullOrWhiteSpace(columnName, nameof(columnName));

            var result = new StringBuilder();
            result.AppendIf(!String.IsNullOrWhiteSpace(prefix), $"{prefix}{commandAdapter.SchemaSeparator}");
            result.Append(commandAdapter.QuoteIdentifier(columnName));

            if (!String.IsNullOrWhiteSpace(alias))
            {
                result.Append($"{CommandAdapter.As}{commandAdapter.QuoteIdentifier(alias)}");
            }

            return result.ToString();
        }

        private static string GetTableName(IObjectMap objectMap, string alias)
        {
            Contracts.Requires.NotNull(objectMap, nameof(objectMap));

            var result = new StringBuilder();
            result.Append(objectMap.FullyQualifiedObjectName);
            if (!String.IsNullOrWhiteSpace(alias))
            {
                result.Append($"{CommandAdapter.As}{objectMap.CommandAdapter.QuoteIdentifier(alias)}");
            }

            return result.ToString();
        }
    }
}
