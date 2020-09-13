//------------------------------------------------------------------------------
// <copyright file="SqlServerStrategyBuilder.cs"
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
using System.Collections.Generic;
using System.Linq;

using Cadru.Extensions;
using Cadru.Polly.Resources;

using Microsoft.Extensions.Options;

using Polly;

namespace Cadru.Polly.Data.SqlServer
{
    /// <summary>
    /// Represents the policies used for performing SQL Server database operations.
    /// </summary>
    public class SqlServerStrategyBuilder : SqlStrategyBuilder
    {
        /// <summary>
        /// Creates a <see cref="SqlServerStrategyBuilder"/> with default policies already included.
        /// </summary>
        public static SqlServerStrategyBuilder Default
        {
            get
            {
                var exceptionHandlingStrategies = new IExceptionHandlingStrategy[]
                {
                    new SqlServerTransientExceptionHandlingStrategy(),
                    new NetworkConnectivityExceptionHandlingStrategy(),
                    new SqlServerTransientTransactionExceptionHandlingStrategy(),
                    new SqlServerTimeoutExceptionHandlingStrategy()
                };

                var strategy = new SqlServerStrategyBuilder(exceptionHandlingStrategies, SqlStrategyOptions.Defaults);
                return strategy;
            }
        }

        /// <summary>
        /// Creates a <see cref="SqlServerStrategyBuilder"/> with no policies.
        /// </summary>
        public static SqlServerStrategyBuilder Empty
        {
            get
            {
                var exceptionHandlingStrategies = new IExceptionHandlingStrategy[]
                {
                    new SqlServerTransientExceptionHandlingStrategy(),
                    new NetworkConnectivityExceptionHandlingStrategy(),
                    new SqlServerTransientTransactionExceptionHandlingStrategy(),
                    new SqlServerTimeoutExceptionHandlingStrategy()
                };

                var strategy = new SqlServerStrategyBuilder(exceptionHandlingStrategies, SqlStrategyOptions.Defaults);
                strategy.Policies.Clear();
                return strategy;
            }
        }

        private SqlServerStrategyBuilder(IEnumerable<IExceptionHandlingStrategy> exceptionHandlingStrategies, SqlStrategyOptions? strategyOptions)
                   : base(exceptionHandlingStrategies, strategyOptions)
        {
            var backoff = this.StrategyOptions.ExponentialBackoff();
            var defaultPolicyHandler = this.GetDefaultPolicyBuilder();
            this.Policies.Add(defaultPolicyHandler.WaitAndRetry(backoff, SqlStrategyLoggingDelegates.OnRetry).WithPolicyKey(SqlServerPolicyKeys.CommonTransientErrorsPolicy));
            this.Policies.Add(defaultPolicyHandler.WaitAndRetryAsync(backoff, SqlStrategyLoggingDelegates.OnRetryAsync).WithPolicyKey(SqlServerPolicyKeys.CommonTransientErrorsPolicyAsync));
            this.WithOverallTimeout(this.StrategyOptions);
            this.WithCircuitBreakers(this.StrategyOptions);
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SqlServerStrategyBuilder"/> class.
        /// </summary>
        /// <param name="exceptionHandlingStrategies">
        /// The collection of <see cref="IExceptionHandlingStrategy"/>
        /// strategies to use.
        /// </param>
        /// <param name="strategyOptionsAccessor">
        /// The strategy configuration. If
        /// <paramref name="strategyOptionsAccessor"/> is
        /// <see langword="null"/>, a default configuration will be used.
        /// </param>
        public SqlServerStrategyBuilder(IEnumerable<IExceptionHandlingStrategy> exceptionHandlingStrategies, IOptions<SqlStrategyOptions>? strategyOptionsAccessor)
            : this(exceptionHandlingStrategies, strategyOptionsAccessor?.Value)
        {
        }

        /// <inheritdoc/>
        protected override void Validate()
        {
            if (this.Policies.Any(x => x.PolicyKey.StartsWith(SqlServerPolicyKeys.TimeoutPerRetryPolicy)
                && !this.Policies.Any(x => x.PolicyKey.StartsWithAny(new[] { SqlServerPolicyKeys.CommonTransientErrorsPolicy, SqlServerPolicyKeys.TransactionPolicy }))))
            {
                throw new InvalidOperationException(Strings.SqlServer_InvalidTimeoutConfiguration);
            }
        }
    }
}