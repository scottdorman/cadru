using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Cadru.Polly;
using Cadru.Polly.Data.SqlServer;

using Dapper;

using Microsoft.Data.SqlClient;

namespace Cadru.Data.Dapper.Configuration
{
    /// <summary>
    /// Provides a simple API surface for configuring <see cref="DapperContext"/>.
    /// </summary>
    public static class DapperContextBuilderExtensions
    {
        /// <summary>
        /// Configure the specified type to be processed by a custom <see
        /// href="https://stackexchange.github.io/Dapper/">Dapper</see> handler.
        /// </summary>
        /// <typeparam name="T">The type to handle.</typeparam>
        /// <param name="contextBuilder">The builder being used to configure the context.</param>
        /// <param name="handler">The handler for the type <typeparamref name="T"/>.</param>
        /// <returns>The context builder so that further configuration can be chained.</returns>
        public static DapperContextBuilder AddTypeHandler<T>(this DapperContextBuilder contextBuilder, SqlMapper.TypeHandler<T> handler)
        {
            SqlMapper.AddTypeHandler(handler);
            return contextBuilder;
        }

        /// <summary>
        /// Configure the specified type to be processed by a custom <see
        /// href="https://stackexchange.github.io/Dapper/">Dapper</see> handler.
        /// </summary>
        /// <param name="contextBuilder">The builder being used to configure the context.</param>
        /// <param name="type">The type to handle.</param>
        /// <param name="handler">The handler for the type <paramref name="type"/>.</param>
        /// <returns>The context builder so that further configuration can be chained.</returns>
        public static DapperContextBuilder AddTypeHandler(this DapperContextBuilder contextBuilder, Type type, SqlMapper.ITypeHandler handler)
        {
            SqlMapper.AddTypeHandler(type, handler);
            return contextBuilder;
        }

        /// <summary>
        /// Configure the specified type to be processed by a custom <see
        /// href="https://stackexchange.github.io/Dapper/">Dapper</see> handler.
        /// </summary>
        /// <param name="contextBuilder">The builder being used to configure the context.</param>
        /// <param name="type">The type to handle.</param>
        /// <param name="handler">The handler for the type <paramref name="type"/>.</param>
        /// <param name="clone">Whether to clone the current type handler map.</param>
        /// <returns>The context builder so that further configuration can be chained.</returns>
        public static DapperContextBuilder AddTypeHandlerImpl(this DapperContextBuilder contextBuilder, Type type, SqlMapper.ITypeHandler handler, bool clone)
        {
            SqlMapper.AddTypeHandlerImpl(type, handler, clone);
            return contextBuilder;
        }

        /// <summary>
        /// Configures the specified type to be mapped to a given db-type.
        /// </summary>
        /// <param name="contextBuilder">The builder being used to configure the context.</param>
        /// <param name="type">The type to map from.</param>
        /// <param name="dbType">The database type to map to.</param>
        /// <returns>The context builder so that further configuration can be chained.</returns>
        public static DapperContextBuilder AddTypeMap(this DapperContextBuilder contextBuilder, Type type, DbType dbType)
        {
            SqlMapper.AddTypeMap(type, dbType);
            return contextBuilder;
        }

        /// <summary>
        /// Configures the context to enable retries on transient failures.
        /// </summary>
        /// <param name="contextBuilder">The builder being used to configure the context.</param>
        /// <param name="exceptionHandlingStrategies">The collection of exception handling strategies used to determine if an error is transient.</param>
        /// <returns>The context builder so that further configuration can be chained.</returns>
        public static DapperContextBuilder EnableRetryOnFailure(this DapperContextBuilder contextBuilder, IEnumerable<IExceptionHandlingStrategy>? exceptionHandlingStrategies)
        {
            if (contextBuilder is DapperContextBuilder policyContextBuilder)
            {
                policyContextBuilder.RetryOnFailureEnabled = true;
                policyContextBuilder.ExceptionHandlingStrategies = exceptionHandlingStrategies ?? Enumerable.Empty<IExceptionHandlingStrategy>();
            }

            return contextBuilder;
        }

        /// <summary>
        /// Configures the context to connect to a Microsoft SQL Server database.
        /// </summary>
        /// <param name="contextBuilder">The builder being used to configure the context.</param>
        /// <param name="connectionString">The connection string of the database to connect to.</param>
        /// <returns>The context builder so that further configuration can be chained.</returns>
        public static DapperContextBuilder UseSqlServer(this DapperContextBuilder contextBuilder, string connectionString)
        {
            if (contextBuilder is DapperContextBuilder connectionContextBuilder)
            {
                connectionContextBuilder.DbProviderFactory = SqlClientFactory.Instance;
                connectionContextBuilder.ConnectionString = connectionString;
                connectionContextBuilder.CommandAdapter = new SqlCommandAdapter();
                connectionContextBuilder.SqlStrategyFactory = SqlServerStrategyFactory.Instance;
            }

            return contextBuilder;
        }
    }
}
