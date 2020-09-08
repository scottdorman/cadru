//------------------------------------------------------------------------------
// <copyright file="DapperCommandBuilder.cs"
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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

using Cadru.Data.Dapper.Predicates;

using Dapper;

namespace Cadru.Data.Dapper
{
    /// <summary>
    /// Represents a way to create <see cref="CommandDefinition"/> instances.
    /// </summary>
    public class DapperCommandBuilder : IDapperCommandBuilder
    {
        private static readonly ConcurrentDictionary<Type, List<string>> paramNameCache = new ConcurrentDictionary<Type, List<string>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DapperCommandBuilder"/> class.
        /// </summary>
        /// <param name="databaseObject">The <see cref="IDatabaseObject"/> for which commands will be created.</param>
        public DapperCommandBuilder(IDatabaseObject databaseObject)
        {
            this.DatabaseObject = databaseObject;
        }

        /// <summary>
        /// The <see cref="IDatabaseObject"/> for which commands will be created.
        /// </summary>
        protected IDatabaseObject DatabaseObject { get; }

        /// <inheritdoc/>
        public virtual CommandDefinition GetDeleteCommand(IPredicate? predicate = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.Append(CommandAdapter.DeleteFrom);
            builder.Append(this.DatabaseObject.ObjectMap.FullyQualifiedObjectName);
            this.AppendWhere(builder, predicate, parameters);

            return new CommandDefinition(
                commandText: builder.ToString(),
                commandType: CommandType.Text,
                parameters: parameters,
                transaction: transaction,
                commandTimeout: commandTimeout,
                flags: flags,
                cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public virtual CommandDefinition GetInsertCommand(object data, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            var (parameterNames, parameters) = this.DeriveParameters(data);

            var builder = new StringBuilder();
            builder.Append(CommandAdapter.Update);
            builder.Append(this.DatabaseObject.ObjectMap.FullyQualifiedObjectName);
            builder.Append(CommandAdapter.Set);
            builder.AppendLine(String.Join(", ", parameterNames.Select(p => $"{p}{CommandAdapter.Equal}{this.DatabaseObject.Context.CommandAdapter.GetParameterName(p)}")));

            return new CommandDefinition(
                commandText: builder.ToString(),
                commandType: CommandType.Text,
                parameters: null,
                transaction: transaction,
                commandTimeout: commandTimeout,
                flags: flags,
                cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public virtual CommandDefinition GetSelectCommand(IPredicate? predicate = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.Append(CommandAdapter.SelectStar);
            builder.Append(this.DatabaseObject.ObjectMap.FullyQualifiedObjectName);
            this.AppendWhere(builder, predicate, parameters);

            return new CommandDefinition(
                commandText: builder.ToString(),
                commandType: CommandType.Text,
                parameters: parameters,
                transaction: transaction,
                commandTimeout: commandTimeout,
                flags: flags,
                cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public virtual CommandDefinition GetSelectTopCommand(int count = 1, IPredicate? predicate = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.Append(CommandAdapter.SelectTopOne);
            builder.Append(this.DatabaseObject.ObjectMap.FullyQualifiedObjectName);
            this.AppendWhere(builder, predicate, parameters);

            return new CommandDefinition(
                commandText: builder.ToString(),
                commandType: CommandType.Text,
                parameters: parameters,
                transaction: transaction,
                commandTimeout: commandTimeout,
                flags: flags,
                cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public virtual CommandDefinition GetUpdateCommand(object data, IPredicate? predicate = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            var (parameterNames, parameters) = this.DeriveParameters(data);

            var builder = new StringBuilder();
            builder.Append(CommandAdapter.Update);
            builder.Append(this.DatabaseObject.ObjectMap.FullyQualifiedObjectName);
            builder.Append(CommandAdapter.Set);
            builder.AppendLine(String.Join(", ", parameterNames.Select(p => $"{p}{CommandAdapter.Equal}{this.DatabaseObject.Context.CommandAdapter.GetParameterName(p)}")));
            this.AppendWhere(builder, predicate, parameters);

            return new CommandDefinition(
                commandText: builder.ToString(),
                commandType: CommandType.Text,
                parameters: null,
                transaction: transaction,
                commandTimeout: commandTimeout,
                flags: flags,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Appends the SQL representation of the specified <see cref="IPredicate"/> to the WHERE clause.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> for the SQL statement being created.</param>
        /// <param name="predicate">An optional <see cref="IPredicate"/> to append.</param>
        /// <param name="parameters">A bag of parameters.</param>
        protected void AppendWhere(StringBuilder builder, IPredicate? predicate, DynamicParameters parameters)
        {
            if (predicate != null)
            {
                builder.Append(CommandAdapter.Where);
                builder.Append(predicate.GetSql(parameters, this.DatabaseObject.ObjectMap));
            }
        }

        /// <summary>
        /// Derives the parameter names and parameters for INSERT and UPDATE
        /// statements.
        /// </summary>
        /// <param name="template">The anonymous type of <see
        /// cref="DynamicParameters"/> bag representing the template which
        /// contains the parameter information.</param>
        /// <returns>The <see cref="ValueTuple"/> which represents the parameter
        /// names and parameter bag derived from <paramref
        /// name="template"/>.</returns>
        protected virtual (IList<string> ParameterNames, DynamicParameters Parameters) DeriveParameters(object template)
        {
            var templateType = template.GetType();
            if (!paramNameCache.TryGetValue(templateType, out var paramNames))
            {
                paramNames = new List<string>();
                foreach (var prop in templateType.GetRuntimeProperties())
                {
                    var mappedProperty = this.DatabaseObject.ObjectMap.Properties.SingleOrDefault(p => p.PropertyName == prop.Name);
                    if (mappedProperty != null && mappedProperty.IsUpdatable)
                    {
                        paramNames.Add(mappedProperty.ColumnName);
                    }
                }

                paramNameCache[templateType] = paramNames;
            }

            return (paramNames, new DynamicParameters(template));
        }
    }
}