using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

using Cadru.Polly;
using Cadru.Polly.Data;

namespace Cadru.Data.Dapper.Configuration
{
    /// <summary>
    /// Provides a simple API surface for configuring <see cref="DapperContext"/>.
    /// </summary>
    public partial class DapperContextBuilder
    {
        internal DapperContextBuilder()
        {
        }

        /// <summary>
        /// Gets a <see cref="CommandAdapter"/> that is used to create SQL statements.
        /// </summary>
        public ICommandAdapter CommandAdapter { get; internal set; } = new CommandAdapter();

        /// <summary>
        /// Gets the database connection string.
        /// </summary>
        public string? ConnectionString { get; set; }

        /// <summary>
        /// Gets the <see cref="DbProviderFactory"/> used to create data source classes.
        /// </summary>
        public DbProviderFactory? DbProviderFactory { get; internal set; }

        /// <summary>
        /// Gets the collection of exception handling strategies.
        /// </summary>
        public IEnumerable<IExceptionHandlingStrategy> ExceptionHandlingStrategies { get; internal set; } = Enumerable.Empty<IExceptionHandlingStrategy>();

        /// <summary>
        /// Gets a value indicating if retry on failures is enabled.
        /// </summary>
        public bool RetryOnFailureEnabled { get; internal set; }

        /// <summary>
        /// Gets the <see cref="ISqlStrategyFactory"/> used for creating a retry on failure strategy.
        /// </summary>
        /// <remarks><see cref="SqlStrategy.Default"/> is used if <see cref="RetryOnFailureEnabled"/> is <see langword="false"/> or
        /// <see cref="SqlStrategyFactory"/> is <see langword="null"/>.
        /// </remarks>
        public ISqlStrategyFactory? SqlStrategyFactory { get; internal set; }

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
