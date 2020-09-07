//------------------------------------------------------------------------------
// <copyright file="SqlServerStrategyConfiguration.cs"
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

using Cadru.Polly.Utilities;

namespace Cadru.Polly.Sql.SqlServer
{
    /// <summary>
    /// Helper methods for getting strongly typed values from an <see cref="ISqlStrategyConfiguration"/>.
    /// </summary>
    public static class SqlServerStrategyConfiguration
    {
        /// <summary>
        /// The retry count.
        /// </summary>
        /// <param name="sqlStrategyConfiguration"></param>
        /// <returns></returns>
        public static int MaxRetries(this ISqlStrategyConfiguration sqlStrategyConfiguration)
        {
            sqlStrategyConfiguration.TryGetInt32("MaxRetries", out var value);
            return value ?? 5;
        }

        /// <summary>
        /// The number of exceptions that are allowed before opening the circuit.
        /// </summary>
        /// <param name="sqlStrategyConfiguration"></param>
        /// <returns></returns>
        public static int ExceptionsAllowedBeforeBreaking(this ISqlStrategyConfiguration sqlStrategyConfiguration)
        {
            sqlStrategyConfiguration.TryGetInt32("ExceptionsAllowedBeforeBreaking", out var value);
            return value ?? 3;
        }

        /// <summary>
        /// The duration the circuit will stay open before resetting.
        /// </summary>
        /// <param name="sqlStrategyConfiguration"></param>
        /// <returns></returns>
        public static TimeSpan DurationOfBreak(this ISqlStrategyConfiguration sqlStrategyConfiguration)
        {
            sqlStrategyConfiguration.TryGetTimeSpan("DurationOfBreak", out var value);
            return value ?? TimeSpan.FromSeconds(30);
        }

        /// <summary>
        /// The timeout for the overall policy.
        /// </summary>
        /// <param name="sqlStrategyConfiguration"></param>
        /// <returns></returns>
        public static TimeSpan OverallTimeout(this ISqlStrategyConfiguration sqlStrategyConfiguration)
        {
            sqlStrategyConfiguration.TryGetTimeSpan("OverallTimeout", out var value);
            return value ?? TimeoutHelper.GetTimeout(TimeSpan.Zero, sqlStrategyConfiguration.MaxRetries());
        }

        /// <summary>
        /// The timeout for each retry.
        /// </summary>
        /// <param name="sqlStrategyConfiguration"></param>
        /// <returns></returns>
        public static TimeSpan TimeoutPerRetry(this ISqlStrategyConfiguration sqlStrategyConfiguration)
        {
            sqlStrategyConfiguration.TryGetTimeSpan("TimeoutPerRetry", out var value);
            return value ?? TimeoutHelper.GetTimeout(TimeSpan.Zero, sqlStrategyConfiguration.MaxRetries());
        }
    }
}