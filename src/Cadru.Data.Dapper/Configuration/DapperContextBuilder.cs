using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using Cadru.Polly;
using Cadru.Polly.Data;

namespace Cadru.Data.Dapper.Configuration
{
    public partial class DapperContextBuilder
    {
        internal DapperContextBuilder()
        {
        }

        public ICommandAdapter CommandAdapter { get; internal set; } = new CommandAdapter();
        public string? ConnectionString { get; set; }
        public DbProviderFactory? DbProviderFactory { get; internal set; }
        public bool RetryOnFailureEnabled { get; internal set; }
        public ISqlStrategyFactory? SqlStrategyFactory { get; internal set; }
        public IEnumerable<IExceptionHandlingStrategy> ExceptionHandlingStrategies { get; internal set; }

        internal IDbConnection? CreateConnection()
        {
            IDbConnection? connection = this.DbProviderFactory?.CreateConnection();
            if (connection != null)
            {
                connection!.ConnectionString = this.ConnectionString;
            }

            return connection;
        }
    }
}
