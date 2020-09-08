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
    public class DapperCommandBuilder : IDapperCommandBuilder
    {
        private static readonly ConcurrentDictionary<Type, List<string>> paramNameCache = new ConcurrentDictionary<Type, List<string>>();

        public DapperCommandBuilder(IDatabaseObject databaseObject)
        {
            this.DatabaseObject = databaseObject;
        }

        protected IDatabaseObject DatabaseObject { get; }

        public virtual CommandDefinition GetDeleteCommand(IPredicate? predicate = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.Append(CommandAdapter.DeleteFrom);
            builder.Append(this.DatabaseObject.FullyQualifiedObjectName);
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

        public virtual CommandDefinition GetInsertCommand(object data, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            var (parameterNames, parameters) = this.DeriveParameters(data);

            var builder = new StringBuilder();
            builder.Append(CommandAdapter.Update);
            builder.Append(this.DatabaseObject.FullyQualifiedObjectName);
            builder.Append(CommandAdapter.Set);
            builder.AppendLine(String.Join(", ", parameterNames.Select(p => $"{p}{CommandAdapter.Equal}{this.DatabaseObject.CommandAdapter.GetParameterName(p)}")));

            return new CommandDefinition(
                commandText: builder.ToString(),
                commandType: CommandType.Text,
                parameters: null,
                transaction: transaction,
                commandTimeout: commandTimeout,
                flags: flags,
                cancellationToken: cancellationToken);
        }

        public virtual CommandDefinition GetSelectCommand(IPredicate? predicate = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.Append(CommandAdapter.SelectStar);
            builder.Append(this.DatabaseObject.FullyQualifiedObjectName);
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

        public virtual CommandDefinition GetSelectTopCommand(int count = 1, IPredicate? predicate = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.Append(CommandAdapter.SelectTopOne);
            builder.Append(this.DatabaseObject.FullyQualifiedObjectName);
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

        public virtual CommandDefinition GetUpdateCommand(object data, IPredicate? predicate = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            var (parameterNames, parameters) = this.DeriveParameters(data);

            var builder = new StringBuilder();
            builder.Append(CommandAdapter.Update);
            builder.Append(this.DatabaseObject.FullyQualifiedObjectName);
            builder.Append(CommandAdapter.Set);
            builder.AppendLine(String.Join(", ", parameterNames.Select(p => $"{p}{CommandAdapter.Equal}{this.DatabaseObject.CommandAdapter.GetParameterName(p)}")));
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

        protected void AppendWhere(StringBuilder builder, IPredicate? predicate, DynamicParameters parameters)
        {
            if (predicate != null)
            {
                builder.Append(CommandAdapter.Where);
                builder.Append(predicate.GetSql(parameters, this.DatabaseObject.ObjectMap));
            }
        }

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
