//------------------------------------------------------------------------------
// <copyright file="DapperContextBuilder.cs"
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