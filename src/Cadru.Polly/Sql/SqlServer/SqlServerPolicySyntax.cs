//------------------------------------------------------------------------------
// <copyright file="SqlServerPolicySyntax.cs"
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
using System.Threading.Tasks;

using Polly;
using Polly.CircuitBreaker;
using Polly.Contrib.WaitAndRetry;
using Polly.Timeout;

namespace Cadru.Polly.Sql.SqlServer
{
    /// <summary>
    /// Fluent API for defining a <see cref="SqlStrategy"/>.
    /// </summary>
    public static class SqlServerPolicySyntax
    {
        /// <summary>
        /// Builds a <see cref="SqlStrategy"/> with policies that will function
        /// like a Circuit Breaker.
        /// </summary>
        /// <param name="sqlStrategy">The SQL strategy.</param>
        /// <param name="sqlStrategyConfiguration">An <see
        /// cref="ISqlStrategyConfiguration"/> containing configuration
        /// parameters.</param>
        /// <returns>The strategy instance.</returns>
        public static SqlStrategy WithCircuitBreaker(this SqlStrategy sqlStrategy, ISqlStrategyConfiguration sqlStrategyConfiguration)
        {
            var exceptionsAllowedBeforeBreaking = sqlStrategyConfiguration.ExceptionsAllowedBeforeBreaking();
            var durationOfBreak = sqlStrategyConfiguration.DurationOfBreak();

            //DatabaseNotCurrentlyAvailable
            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleSqlError(40613).CircuitBreaker(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlPolicyDelegates.OnCircuitBreak, SqlPolicyDelegates.OnCircuitReset).WithPolicyKey($"F1.{SqlServerPolicyKeys.CircuitBreakerPolicy}"));
            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleSqlError(40613).CircuitBreakerAsync(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlPolicyDelegates.OnCircuitBreak, SqlPolicyDelegates.OnCircuitReset).WithPolicyKey($"F1.{SqlServerPolicyKeys.CircuitBreakerPolicyAsync}"));

            //ErrorProcessingRequest
            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleSqlError(40197).CircuitBreaker(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlPolicyDelegates.OnCircuitBreak, SqlPolicyDelegates.OnCircuitReset).WithPolicyKey($"F2.{SqlServerPolicyKeys.CircuitBreakerPolicy}"));
            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleSqlError(40197).CircuitBreakerAsync(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlPolicyDelegates.OnCircuitBreak, SqlPolicyDelegates.OnCircuitReset).WithPolicyKey($"F2.{SqlServerPolicyKeys.CircuitBreakerPolicyAsync}"));

            //ServiceCurrentlyBusy
            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleSqlError(40501).CircuitBreaker(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlPolicyDelegates.OnCircuitBreak, SqlPolicyDelegates.OnCircuitReset).WithPolicyKey($"F3.{SqlServerPolicyKeys.CircuitBreakerPolicy}"));
            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleSqlError(40501).CircuitBreakerAsync(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlPolicyDelegates.OnCircuitBreak, SqlPolicyDelegates.OnCircuitReset).WithPolicyKey($"F3.{SqlServerPolicyKeys.CircuitBreakerPolicyAsync}"));

            //SessionTerminatedLongTransaction
            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleSqlError(40549).CircuitBreaker(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlPolicyDelegates.OnCircuitBreak, SqlPolicyDelegates.OnCircuitReset).WithPolicyKey($"F4.{SqlServerPolicyKeys.CircuitBreakerPolicy}"));
            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleSqlError(40549).CircuitBreakerAsync(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlPolicyDelegates.OnCircuitBreak, SqlPolicyDelegates.OnCircuitReset).WithPolicyKey($"F4.{SqlServerPolicyKeys.CircuitBreakerPolicyAsync}"));

            //NotEnoughResources
            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleSqlError(49918).CircuitBreaker(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlPolicyDelegates.OnCircuitBreak, SqlPolicyDelegates.OnCircuitReset).WithPolicyKey($"F5.{SqlServerPolicyKeys.CircuitBreakerPolicy}"));
            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleSqlError(49918).CircuitBreakerAsync(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlPolicyDelegates.OnCircuitBreak, SqlPolicyDelegates.OnCircuitReset).WithPolicyKey($"F5.{SqlServerPolicyKeys.CircuitBreakerPolicyAsync}"));

            //SessionTerminatedToManyLocks
            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleSqlError(40550).CircuitBreaker(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlPolicyDelegates.OnCircuitBreak, SqlPolicyDelegates.OnCircuitReset).WithPolicyKey($"F6.{SqlServerPolicyKeys.CircuitBreakerPolicy}"));
            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleSqlError(40550).CircuitBreakerAsync(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlPolicyDelegates.OnCircuitBreak, SqlPolicyDelegates.OnCircuitReset).WithPolicyKey($"F6.{SqlServerPolicyKeys.CircuitBreakerPolicyAsync}"));

            return sqlStrategy;
        }

        /// <summary>
        /// Builds a <see cref="SqlStrategy"/> with a default set of policies.
        /// </summary>
        /// <param name="sqlStrategy">The SQL strategy.</param>
        /// <returns>The strategy instance.</returns>
        /// <remarks>
        /// This is equivalent to calling <see
        /// cref="WithOverallTimeout(SqlStrategy, ISqlStrategyConfiguration)"/>, <see
        /// cref="WithTransientErrors(SqlStrategy, ISqlStrategyConfiguration)"/>, and <see
        /// cref="WithCircuitBreaker(SqlStrategy, ISqlStrategyConfiguration)"/>.
        /// </remarks>
        public static SqlStrategy WithDefaults(this SqlStrategy sqlStrategy) => sqlStrategy.WithDefaults(new SqlStrategyConfiguration());

        /// <summary>
        /// Builds a <see cref="SqlStrategy"/> with a default set of policies.
        /// </summary>
        /// <param name="sqlStrategy">The SQL strategy.</param>
        /// <param name="sqlStrategyConfiguration">An <see
        /// cref="ISqlStrategyConfiguration"/> containing configuration
        /// parameters.</param>
        /// <returns>The strategy instance.</returns>
        /// <remarks>
        /// This is equivalent to calling <see
        /// cref="WithOverallTimeout(SqlStrategy, ISqlStrategyConfiguration)"/>, <see
        /// cref="WithTransientErrors(SqlStrategy, ISqlStrategyConfiguration)"/>, and <see
        /// cref="WithCircuitBreaker(SqlStrategy, ISqlStrategyConfiguration)"/>.
        /// </remarks>
        public static SqlStrategy WithDefaults(this SqlStrategy sqlStrategy, ISqlStrategyConfiguration? sqlStrategyConfiguration)
        {
            sqlStrategyConfiguration ??= new SqlStrategyConfiguration();
            sqlStrategy.WithOverallTimeout(sqlStrategyConfiguration);
            sqlStrategy.WithTransientErrors(sqlStrategyConfiguration);
            sqlStrategy.WithCircuitBreaker(sqlStrategyConfiguration);
            return sqlStrategy;
        }

        /// <summary>
        /// Builds a <see cref="SqlStrategy"/> with a policy which provides a
        /// fallback action if the main execution fails.
        /// </summary>
        /// <param name="sqlStrategy">The SQL strategy.</param>
        /// <param name="action">The fallback action delegate.</param>
        /// <returns>The strategy instance.</returns>
        public static SqlStrategy WithFallback<T>(this SqlStrategy sqlStrategy, Func<Task<T>> action)
        {
            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleAllTransientSqlErrors().Or<TimeoutRejectedException>().Or<BrokenCircuitException>().Fallback(() => action()).WithPolicyKey(SqlServerPolicyKeys.FallbackPolicy));
            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleAllTransientSqlErrors().Or<TimeoutRejectedException>().Or<BrokenCircuitException>().FallbackAsync((cancellationToken) => action()).WithPolicyKey(SqlServerPolicyKeys.FallbackPolicyAsync));
            return sqlStrategy;
        }

        /// <summary>
        /// Builds a <see cref="SqlStrategy"/> with a policy which provides a
        /// fallback action if the main execution fails.
        /// </summary>
        /// <param name="sqlStrategy">The SQL strategy.</param>
        /// <param name="action">The fallback action delegate.</param>
        /// <returns>The strategy instance.</returns>
        public static SqlStrategy WithFallback(this SqlStrategy sqlStrategy, Func<Task> action)
        {
            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleAllTransientSqlErrors().Or<TimeoutRejectedException>().Or<BrokenCircuitException>().Fallback(() => action()).WithPolicyKey(SqlServerPolicyKeys.FallbackPolicy));
            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleAllTransientSqlErrors().Or<TimeoutRejectedException>().Or<BrokenCircuitException>().FallbackAsync((cancellationToken) => action()).WithPolicyKey(SqlServerPolicyKeys.FallbackPolicyAsync));
            return sqlStrategy;
        }

        /// <summary>
        /// Builds a <see cref="SqlStrategy"/> with policies for both an overall
        /// timeout and a per retry timeout.
        /// </summary>
        /// <param name="sqlStrategy">The SQL strategy.</param>
        /// <param name="sqlStrategyConfiguration">An <see
        /// cref="ISqlStrategyConfiguration"/> containing configuration
        /// parameters.</param>
        /// <returns>The strategy instance.</returns>
        /// <remarks>
        /// This is equivalent to calling <see
        /// cref="WithOverallTimeout(SqlStrategy, ISqlStrategyConfiguration)"/>,
        /// <see cref="WithTimeoutPerRetry(SqlStrategy,
        /// ISqlStrategyConfiguration)"/>.
        /// </remarks>
        public static SqlStrategy WithOverallAndTimeoutPerRetry(this SqlStrategy sqlStrategy, ISqlStrategyConfiguration sqlStrategyConfiguration)
        {
            sqlStrategy.WithOverallTimeout(sqlStrategyConfiguration);
            sqlStrategy.WithTimeoutPerRetry(sqlStrategyConfiguration);
            return sqlStrategy;
        }

        /// <summary>
        /// Builds a <see cref="SqlStrategy"/> with a policy for an overall
        /// timeout.
        /// </summary>
        /// <param name="sqlStrategy">The SQL strategy.</param>
        /// <param name="sqlStrategyConfiguration">An <see
        /// cref="ISqlStrategyConfiguration"/> containing configuration
        /// parameters.</param>
        /// <returns>The strategy instance.</returns>
        /// <remarks>
        /// This is equivalent to calling <see
        /// cref="WithOverallTimeout(SqlStrategy, ISqlStrategyConfiguration)"/>,
        /// <see cref="WithTimeoutPerRetry(SqlStrategy,
        /// ISqlStrategyConfiguration)"/>.
        /// </remarks>
        public static SqlStrategy WithOverallTimeout(this SqlStrategy sqlStrategy, ISqlStrategyConfiguration sqlStrategyConfiguration)
        {
            var overallTimeout = sqlStrategyConfiguration.OverallTimeout();
            sqlStrategy.Policies.Add(Policy.Timeout(overallTimeout, TimeoutStrategy.Pessimistic, SqlPolicyDelegates.OnTimeout).WithPolicyKey(SqlServerPolicyKeys.OverallTimeoutPolicy));
            sqlStrategy.Policies.Add(Policy.TimeoutAsync(overallTimeout, TimeoutStrategy.Pessimistic, SqlPolicyDelegates.OnTimeoutAsync).WithPolicyKey(SqlServerPolicyKeys.OverallTimeoutPolicyAsync));
            return sqlStrategy;
        }

        /// <summary>
        /// Builds a <see cref="SqlStrategy"/> with a policy for a per retry
        /// timeout.
        /// </summary>
        /// <param name="sqlStrategy">The SQL strategy.</param>
        /// <param name="sqlStrategyConfiguration">An <see
        /// cref="ISqlStrategyConfiguration"/> containing configuration
        /// parameters.</param>
        /// <returns>The strategy instance.</returns>
        /// <remarks>
        /// This is equivalent to calling <see
        /// cref="WithOverallTimeout(SqlStrategy, ISqlStrategyConfiguration)"/>,
        /// <see cref="WithTimeoutPerRetry(SqlStrategy,
        /// ISqlStrategyConfiguration)"/>.
        /// </remarks>
        public static SqlStrategy WithTimeoutPerRetry(this SqlStrategy sqlStrategy, ISqlStrategyConfiguration sqlStrategyConfiguration)
        {
            var timeoutPerRetry = sqlStrategyConfiguration.TimeoutPerRetry();
            sqlStrategy.Policies.Add(Policy.Timeout(timeoutPerRetry, TimeoutStrategy.Pessimistic, SqlPolicyDelegates.OnTimeout).WithPolicyKey(SqlServerPolicyKeys.TimeoutPerRetryPolicy));
            sqlStrategy.Policies.Add(Policy.TimeoutAsync(timeoutPerRetry, TimeoutStrategy.Pessimistic, SqlPolicyDelegates.OnTimeoutAsync).WithPolicyKey(SqlServerPolicyKeys.TimeoutPerRetryPolicyAsync));
            return sqlStrategy;
        }

        /// <summary>
        /// Builds a <see cref="SqlStrategy"/> with a policy for retrying
        /// actions on transaction failures.
        /// </summary>
        /// <param name="sqlStrategy">The SQL strategy.</param>
        /// <param name="sqlStrategyConfiguration">An <see
        /// cref="ISqlStrategyConfiguration"/> containing configuration
        /// parameters.</param>
        /// <returns>The strategy instance.</returns>
        public static SqlStrategy WithTransaction(this SqlStrategy sqlStrategy, ISqlStrategyConfiguration sqlStrategyConfiguration)
        {
            var backoff = Backoff.ExponentialBackoff(TimeSpan.FromSeconds(2), sqlStrategyConfiguration.MaxRetries());
            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleTransientSqlError().WaitAndRetry(backoff, SqlPolicyDelegates.OnRetry).WithPolicyKey(SqlServerPolicyKeys.TransactionPolicy));
            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleTransientSqlError().WaitAndRetryAsync(backoff, SqlPolicyDelegates.OnRetryAsync).WithPolicyKey(SqlServerPolicyKeys.TransactionPolicyAsync));
            return sqlStrategy;
        }

        /// <summary>
        /// Builds a <see cref="SqlStrategy"/> with a policy for retrying
        /// actions on transient failures.
        /// </summary>
        /// <param name="sqlStrategy">The SQL strategy.</param>
        /// <param name="sqlStrategyConfiguration">An <see
        /// cref="ISqlStrategyConfiguration"/> containing configuration
        /// parameters.</param>
        /// <returns>The strategy instance.</returns>
        public static SqlStrategy WithTransientErrors(this SqlStrategy sqlStrategy, ISqlStrategyConfiguration sqlStrategyConfiguration)
        {
            var backoff = Backoff.ExponentialBackoff(TimeSpan.FromSeconds(2), sqlStrategyConfiguration.MaxRetries());

            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleTransientSqlError().WaitAndRetry(backoff, SqlPolicyDelegates.OnRetry).WithPolicyKey(SqlServerPolicyKeys.CommonTransientErrorsPolicy));
            sqlStrategy.Policies.Add(SqlServerPolicyExtensions.HandleTransientSqlError().WaitAndRetryAsync(backoff, SqlPolicyDelegates.OnRetryAsync).WithPolicyKey(SqlServerPolicyKeys.CommonTransientErrorsPolicyAsync));
            return sqlStrategy;
        }
    }
}