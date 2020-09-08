//------------------------------------------------------------------------------
// <copyright file="SqlServerStrategySyntax.cs"
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

using Microsoft.Data.SqlClient;

using Polly;
using Polly.CircuitBreaker;
using Polly.Contrib.WaitAndRetry;
using Polly.Timeout;

namespace Cadru.Polly.Data.SqlServer
{
    /// <summary>
    /// Fluent API for defining a <see cref="SqlStrategy"/>.
    /// </summary>
    public static class SqlServerStrategySyntax
    {
        /// <summary>
        /// Builds a <see cref="SqlStrategy"/> with policies that will function
        /// like a Circuit Breaker.
        /// </summary>
        /// <param name="sqlStrategyBuilder">The SQL strategy.</param>
        /// <param name="sqlStrategyConfiguration">An <see
        /// cref="SqlStrategyOptions"/> containing configuration
        /// parameters.</param>
        /// <returns>The strategy instance.</returns>
        public static SqlStrategyBuilder WithCircuitBreakers(this SqlStrategyBuilder sqlStrategyBuilder, SqlStrategyOptions sqlStrategyConfiguration)
        {
            var exceptionsAllowedBeforeBreaking = sqlStrategyConfiguration.ExceptionsAllowedBeforeBreaking();
            var durationOfBreak = sqlStrategyConfiguration.DurationOfBreak();

            //DatabaseNotCurrentlyAvailable
            sqlStrategyBuilder.Policies.Add(Policy.Handle<SqlException>(e => e.Number == 40613).CircuitBreaker(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlStrategyLoggingDelegates.OnCircuitBreak, SqlStrategyLoggingDelegates.OnCircuitReset).WithPolicyKey($"F1.{SqlServerPolicyKeys.CircuitBreakerPolicy}"));
            sqlStrategyBuilder.Policies.Add(Policy.Handle<SqlException>(e => e.Number == 40613).CircuitBreakerAsync(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlStrategyLoggingDelegates.OnCircuitBreak, SqlStrategyLoggingDelegates.OnCircuitReset).WithPolicyKey($"F1.{SqlServerPolicyKeys.CircuitBreakerPolicyAsync}"));

            //ErrorProcessingRequest
            sqlStrategyBuilder.Policies.Add(Policy.Handle<SqlException>(e => e.Number == 40197).CircuitBreaker(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlStrategyLoggingDelegates.OnCircuitBreak, SqlStrategyLoggingDelegates.OnCircuitReset).WithPolicyKey($"F2.{SqlServerPolicyKeys.CircuitBreakerPolicy}"));
            sqlStrategyBuilder.Policies.Add(Policy.Handle<SqlException>(e => e.Number == 40197).CircuitBreakerAsync(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlStrategyLoggingDelegates.OnCircuitBreak, SqlStrategyLoggingDelegates.OnCircuitReset).WithPolicyKey($"F2.{SqlServerPolicyKeys.CircuitBreakerPolicyAsync}"));

            //ServiceCurrentlyBusy
            sqlStrategyBuilder.Policies.Add(Policy.Handle<SqlException>(e => e.Number == 40501).CircuitBreaker(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlStrategyLoggingDelegates.OnCircuitBreak, SqlStrategyLoggingDelegates.OnCircuitReset).WithPolicyKey($"F3.{SqlServerPolicyKeys.CircuitBreakerPolicy}"));
            sqlStrategyBuilder.Policies.Add(Policy.Handle<SqlException>(e => e.Number == 40501).CircuitBreakerAsync(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlStrategyLoggingDelegates.OnCircuitBreak, SqlStrategyLoggingDelegates.OnCircuitReset).WithPolicyKey($"F3.{SqlServerPolicyKeys.CircuitBreakerPolicyAsync}"));

            //SessionTerminatedLongTransaction
            sqlStrategyBuilder.Policies.Add(Policy.Handle<SqlException>(e => e.Number == 40549).CircuitBreaker(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlStrategyLoggingDelegates.OnCircuitBreak, SqlStrategyLoggingDelegates.OnCircuitReset).WithPolicyKey($"F4.{SqlServerPolicyKeys.CircuitBreakerPolicy}"));
            sqlStrategyBuilder.Policies.Add(Policy.Handle<SqlException>(e => e.Number == 40549).CircuitBreakerAsync(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlStrategyLoggingDelegates.OnCircuitBreak, SqlStrategyLoggingDelegates.OnCircuitReset).WithPolicyKey($"F4.{SqlServerPolicyKeys.CircuitBreakerPolicyAsync}"));

            //NotEnoughResources
            sqlStrategyBuilder.Policies.Add(Policy.Handle<SqlException>(e => e.Number == 49918).CircuitBreaker(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlStrategyLoggingDelegates.OnCircuitBreak, SqlStrategyLoggingDelegates.OnCircuitReset).WithPolicyKey($"F5.{SqlServerPolicyKeys.CircuitBreakerPolicy}"));
            sqlStrategyBuilder.Policies.Add(Policy.Handle<SqlException>(e => e.Number == 49918).CircuitBreakerAsync(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlStrategyLoggingDelegates.OnCircuitBreak, SqlStrategyLoggingDelegates.OnCircuitReset).WithPolicyKey($"F5.{SqlServerPolicyKeys.CircuitBreakerPolicyAsync}"));

            //SessionTerminatedToManyLocks
            sqlStrategyBuilder.Policies.Add(Policy.Handle<SqlException>(e => e.Number == 40550).CircuitBreaker(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlStrategyLoggingDelegates.OnCircuitBreak, SqlStrategyLoggingDelegates.OnCircuitReset).WithPolicyKey($"F6.{SqlServerPolicyKeys.CircuitBreakerPolicy}"));
            sqlStrategyBuilder.Policies.Add(Policy.Handle<SqlException>(e => e.Number == 40550).CircuitBreakerAsync(exceptionsAllowedBeforeBreaking, durationOfBreak, SqlStrategyLoggingDelegates.OnCircuitBreak, SqlStrategyLoggingDelegates.OnCircuitReset).WithPolicyKey($"F6.{SqlServerPolicyKeys.CircuitBreakerPolicyAsync}"));

            return sqlStrategyBuilder;
        }

        /// <summary>
        /// Builds a <see cref="SqlStrategy"/> with a policy which provides a
        /// fallback action if the main execution fails.
        /// </summary>
        /// <param name="sqlStrategy">The SQL strategy.</param>
        /// <param name="action">The fallback action delegate.</param>
        /// <returns>The strategy instance.</returns>
        public static SqlStrategyBuilder WithFallback<T>(this SqlStrategyBuilder sqlStrategy, Func<Task<T>> action)
        {
            sqlStrategy.Policies.Add(sqlStrategy.GetDefaultPolicyBuilder()
                .Or<TimeoutRejectedException>()
                .Or<BrokenCircuitException>().Fallback(() => action()).WithPolicyKey(SqlServerPolicyKeys.FallbackPolicy));

            sqlStrategy.Policies.Add(sqlStrategy.GetDefaultPolicyBuilder()
                .Or<TimeoutRejectedException>()
                .Or<BrokenCircuitException>().FallbackAsync((cancellationToken) => action()).WithPolicyKey(SqlServerPolicyKeys.FallbackPolicyAsync));

            return sqlStrategy;
        }

        /// <summary>
        /// Builds a <see cref="SqlStrategy"/> with a policy which provides a
        /// fallback action if the main execution fails.
        /// </summary>
        /// <param name="sqlStrategy">The SQL strategy.</param>
        /// <param name="action">The fallback action delegate.</param>
        /// <returns>The strategy instance.</returns>
        public static SqlStrategyBuilder WithFallback(this SqlStrategyBuilder sqlStrategy, Func<Task> action)
        {
            sqlStrategy.Policies.Add(sqlStrategy.GetDefaultPolicyBuilder()
                .Or<TimeoutRejectedException>()
                .Or<BrokenCircuitException>().Fallback(() => action()).WithPolicyKey(SqlServerPolicyKeys.FallbackPolicy));

            sqlStrategy.Policies.Add(sqlStrategy.GetDefaultPolicyBuilder()
                .Or<TimeoutRejectedException>()
                .Or<BrokenCircuitException>().FallbackAsync((cancellationToken) => action()).WithPolicyKey(SqlServerPolicyKeys.FallbackPolicyAsync));

            return sqlStrategy;
        }

        /// <summary>
        /// Builds a <see cref="SqlStrategy"/> with policies for both an overall
        /// timeout and a per retry timeout.
        /// </summary>
        /// <param name="sqlStrategy">The SQL strategy.</param>
        /// <param name="sqlStrategyConfiguration">An <see
        /// cref="SqlStrategyOptions"/> containing configuration
        /// parameters.</param>
        /// <returns>The strategy instance.</returns>
        /// <remarks>
        /// This is equivalent to calling <see
        /// cref="WithOverallTimeout(SqlStrategyBuilder, SqlStrategyOptions)"/>,
        /// <see cref="WithTimeoutPerRetry(SqlStrategyBuilder,
        /// SqlStrategyOptions)"/>.
        /// </remarks>
        public static SqlStrategyBuilder WithOverallAndTimeoutPerRetry(this SqlStrategyBuilder sqlStrategy, SqlStrategyOptions sqlStrategyConfiguration)
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
        /// cref="SqlStrategyOptions"/> containing configuration
        /// parameters.</param>
        /// <returns>The strategy instance.</returns>
        /// <remarks>
        /// This is equivalent to calling <see
        /// cref="WithOverallTimeout(SqlStrategyBuilder, SqlStrategyOptions)"/>,
        /// <see cref="WithTimeoutPerRetry(SqlStrategyBuilder,
        /// SqlStrategyOptions)"/>.
        /// </remarks>
        public static SqlStrategyBuilder WithOverallTimeout(this SqlStrategyBuilder sqlStrategy, SqlStrategyOptions sqlStrategyConfiguration)
        {
            var overallTimeout = sqlStrategyConfiguration.OverallTimeout();
            sqlStrategy.Policies.Add(Policy.Timeout(overallTimeout, TimeoutStrategy.Pessimistic, SqlStrategyLoggingDelegates.OnTimeout).WithPolicyKey(SqlServerPolicyKeys.OverallTimeoutPolicy));
            sqlStrategy.Policies.Add(Policy.TimeoutAsync(overallTimeout, TimeoutStrategy.Pessimistic, SqlStrategyLoggingDelegates.OnTimeoutAsync).WithPolicyKey(SqlServerPolicyKeys.OverallTimeoutPolicyAsync));
            return sqlStrategy;
        }

        /// <summary>
        /// Builds a <see cref="SqlStrategy"/> with a policy for a per retry
        /// timeout.
        /// </summary>
        /// <param name="sqlStrategy">The SQL strategy.</param>
        /// <param name="sqlStrategyConfiguration">An <see
        /// cref="SqlStrategyOptions"/> containing configuration
        /// parameters.</param>
        /// <returns>The strategy instance.</returns>
        /// <remarks>
        /// This is equivalent to calling <see
        /// cref="WithOverallTimeout(SqlStrategyBuilder, SqlStrategyOptions)"/>,
        /// <see cref="WithTimeoutPerRetry(SqlStrategyBuilder,
        /// SqlStrategyOptions)"/>.
        /// </remarks>
        public static SqlStrategyBuilder WithTimeoutPerRetry(this SqlStrategyBuilder sqlStrategy, SqlStrategyOptions sqlStrategyConfiguration)
        {
            var timeoutPerRetry = sqlStrategyConfiguration.TimeoutPerRetry();
            sqlStrategy.Policies.Add(Policy.Timeout(timeoutPerRetry, TimeoutStrategy.Pessimistic, SqlStrategyLoggingDelegates.OnTimeout).WithPolicyKey(SqlServerPolicyKeys.TimeoutPerRetryPolicy));
            sqlStrategy.Policies.Add(Policy.TimeoutAsync(timeoutPerRetry, TimeoutStrategy.Pessimistic, SqlStrategyLoggingDelegates.OnTimeoutAsync).WithPolicyKey(SqlServerPolicyKeys.TimeoutPerRetryPolicyAsync));
            return sqlStrategy;
        }

        /// <summary>
        /// Builds a <see cref="SqlStrategy"/> with a policy for retrying
        /// actions on transaction failures.
        /// </summary>
        /// <param name="sqlStrategy">The SQL strategy.</param>
        /// <param name="exceptionHandlingStrategy">The exception handling
        /// strategy used to determine which exceptions should be
        /// retried.</param>
        /// <param name="sqlStrategyConfiguration">An <see
        /// cref="SqlStrategyOptions"/> containing configuration
        /// parameters.</param>
        /// <returns>The strategy instance.</returns>
        public static SqlStrategyBuilder Retry(this SqlStrategyBuilder sqlStrategy, IExceptionHandlingStrategy exceptionHandlingStrategy, SqlStrategyOptions sqlStrategyConfiguration)
        {
            var backoff = Backoff.ExponentialBackoff(TimeSpan.FromSeconds(2), sqlStrategyConfiguration.RetryCount());
            sqlStrategy.Policies.Add(Policy.Handle<SqlException>(exceptionHandlingStrategy.ShouldHandle).WaitAndRetry(backoff, SqlStrategyLoggingDelegates.OnRetry).WithPolicyKey(SqlServerPolicyKeys.TransactionPolicy));
            sqlStrategy.Policies.Add(Policy.Handle<SqlException>(exceptionHandlingStrategy.ShouldHandle).WaitAndRetryAsync(backoff, SqlStrategyLoggingDelegates.OnRetryAsync).WithPolicyKey(SqlServerPolicyKeys.TransactionPolicyAsync));
            return sqlStrategy;
        }
    }
}