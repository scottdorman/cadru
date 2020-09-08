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
    public static class DapperContextBuilderExtensions
    {
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

        public static DapperContextBuilder EnableRetryOnFailure(this DapperContextBuilder contextBuilder, IEnumerable<IExceptionHandlingStrategy>? exceptionHandlingStrategies)
        {
            if (contextBuilder is DapperContextBuilder policyContextBuilder)
            {
                policyContextBuilder.RetryOnFailureEnabled = true;
                policyContextBuilder.ExceptionHandlingStrategies = exceptionHandlingStrategies ?? Enumerable.Empty<IExceptionHandlingStrategy>();
            }

            return contextBuilder;
        }

        //
        // Summary:
        //     Configure the specified type to be processed by a custom handler.
        //
        // Parameters:
        //   handler:
        //     The handler for the type T.
        //
        // Type parameters:
        //   T:
        //     The type to handle.
        public static DapperContextBuilder AddTypeHandler<T>(this DapperContextBuilder contextBuilder, SqlMapper.TypeHandler<T> handler)
        {
            SqlMapper.AddTypeHandler(handler);
            return contextBuilder;
        }

        //
        // Summary:
        //     Configure the specified type to be processed by a custom handler.
        //
        // Parameters:
        //   type:
        //     The type to handle.
        //
        //   handler:
        //     The handler to process the type.
        public static DapperContextBuilder AddTypeHandler(this DapperContextBuilder contextBuilder, Type type, SqlMapper.ITypeHandler handler)
        {
            SqlMapper.AddTypeHandler(type, handler);
            return contextBuilder;
        }

        //
        // Summary:
        //     Configure the specified type to be processed by a custom handler.
        //
        // Parameters:
        //   type:
        //     The type to handle.
        //
        //   handler:
        //     The handler to process the type.
        //
        //   clone:
        //     Whether to clone the current type handler map.
        public static DapperContextBuilder AddTypeHandlerImpl(this DapperContextBuilder contextBuilder, Type type, SqlMapper.ITypeHandler handler, bool clone)
        {
            SqlMapper.AddTypeHandlerImpl(type, handler, clone);
            return contextBuilder;
        }

        //
        // Summary:
        //     Configure the specified type to be mapped to a given db-type.
        //
        // Parameters:
        //   type:
        //     The type to map from.
        //
        //   dbType:
        //     The database type to map to.
        public static DapperContextBuilder AddTypeMap(this DapperContextBuilder contextBuilder, Type type, DbType dbType)
        {
            SqlMapper.AddTypeMap(type, dbType);
            return contextBuilder;
        }
    }
}
