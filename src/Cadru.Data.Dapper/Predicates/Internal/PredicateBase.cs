//------------------------------------------------------------------------------
// <copyright file="PredicateBase.cs"
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
using System.Linq;
using System.Text;

using Cadru.Extensions;

using Dapper;

namespace Cadru.Data.Dapper.Predicates.Internal
{
    internal abstract partial class PredicateBase : IPredicateBase
    {
        public bool Not { get; set; }

        public string? PropertyName { get; set; }

        public abstract string GetSql(DynamicParameters parameters, IObjectMap objectMap);

        internal static bool IsValidPropertyName(string? propertyName)
        {
            return !String.IsNullOrWhiteSpace(propertyName);
        }

        internal static string GetColumnName<T>(IObjectMap objectMap, string propertyName, bool alias) where T : class
        {
            string? columnName = null;
            if (objectMap != null && IsValidPropertyName(propertyName))
            {
                var propertyMap = objectMap.Properties.SingleOrDefault(p => p.PropertyName == propertyName);
                if (propertyMap != null)
                {
                    string? columnAlias = null;
                    if (propertyMap.ColumnName != propertyMap.PropertyName && alias)
                    {
                        columnAlias = propertyMap.PropertyName;
                    }

                    columnName = GetColumnName(objectMap.CommandAdapter, GetTableName(objectMap, null), propertyMap.ColumnName, columnAlias);
                }
            }

            return columnName ?? throw new InvalidOperationException($"Property '{propertyName}' was not found for {typeof(T)}.");
        }

        private static string GetColumnName(ICommandAdapter commandAdapter, string? prefix, string columnName, string? alias)
        {
            Contracts.Requires.NotNullOrWhiteSpace(columnName, nameof(columnName));

            var result = new StringBuilder();
            result.AppendIf(!String.IsNullOrWhiteSpace(prefix), $"{prefix}{commandAdapter.SchemaSeparator}");
            result.Append(commandAdapter.QuoteIdentifier(columnName));

            if (!String.IsNullOrWhiteSpace(alias))
            {
                result.Append($"{CommandAdapter.As}{commandAdapter.QuoteIdentifier(alias!)}");
            }

            return result.ToString();
        }

        private static string GetTableName(IObjectMap objectMap, string? alias)
        {
            Contracts.Requires.NotNull(objectMap, nameof(objectMap));

            var result = new StringBuilder();
            result.Append(objectMap.FullyQualifiedObjectName);
            if (!String.IsNullOrWhiteSpace(alias))
            {
                result.Append($"{CommandAdapter.As}{objectMap.CommandAdapter.QuoteIdentifier(alias!)}");
            }

            return result.ToString();
        }
    }
}