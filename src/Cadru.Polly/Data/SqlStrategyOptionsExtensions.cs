//------------------------------------------------------------------------------
// <copyright file="SqlStrategyOptionsExtensions.cs"
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

using Polly.Contrib.WaitAndRetry;

namespace Cadru.Polly.Data
{
    /// <summary>
    /// Helper methods for getting strongly typed values from an <see cref="SqlStrategyOptions"/>.
    /// </summary>
    public static class SqlStrategyOptionsExtensions
    {
        /// <summary>
        /// The duration the circuit will stay open before resetting.
        /// </summary>
        /// <param name="sqlStrategyOptions">
        /// The <see cref="SqlStrategyOptions"/> instance.
        /// </param>
        /// <returns>
        /// The configured value if it is not <see langword="null"/>; otherwise,
        /// the default value.
        /// </returns>
        public static TimeSpan DurationOfBreak(this SqlStrategyOptions sqlStrategyOptions)
        {
            return (sqlStrategyOptions.DurationOfBreak ?? SqlStrategyOptions.Defaults.DurationOfBreak)!.Value;
        }

        /// <summary>
        /// The number of exceptions that are allowed before opening the circuit.
        /// </summary>
        /// <param name="sqlStrategyOptions">
        /// The <see cref="SqlStrategyOptions"/> instance.
        /// </param>
        /// <returns>
        /// The configured value if it is not <see langword="null"/>; otherwise,
        /// the default value.
        /// </returns>
        public static int ExceptionsAllowedBeforeBreaking(this SqlStrategyOptions sqlStrategyOptions)
        {
            return (sqlStrategyOptions.ExceptionsAllowedBeforeBreaking ?? SqlStrategyOptions.Defaults.ExceptionsAllowedBeforeBreaking)!.Value;
        }

        /// <summary>
        /// Generates sleep durations in an exponential manner.
        /// </summary>
        /// <param name="sqlStrategyOptions">
        /// The <see cref="SqlStrategyOptions"/> instance.
        /// </param>
        /// <returns>
        /// The configured value if it is not <see langword="null"/> otherwise,
        /// the default value.
        /// </returns>
        public static IEnumerable<TimeSpan> ExponentialBackoff(this SqlStrategyOptions sqlStrategyOptions)
        {
            return Backoff.ExponentialBackoff(TimeSpan.FromSeconds(2), sqlStrategyOptions.RetryCount());
        }

        /// <summary>
        /// The timeout for the overall policy.
        /// </summary>
        /// <param name="sqlStrategyOptions">
        /// The <see cref="SqlStrategyOptions"/> instance.
        /// </param>
        /// <returns>
        /// The configured value if it is not <see langword="null"/>; otherwise,
        /// the default value.
        /// </returns>
        public static TimeSpan OverallTimeout(this SqlStrategyOptions sqlStrategyOptions)
        {
            return (sqlStrategyOptions.OverallTimeout ?? SqlStrategyOptions.Defaults.OverallTimeout)!.Value;
        }

        /// <summary>
        /// The retry count.
        /// </summary>
        /// <param name="sqlStrategyOptions">
        /// The <see cref="SqlStrategyOptions"/> instance.
        /// </param>
        /// <returns>
        /// The configured value if it is not <see langword="null"/>; otherwise,
        /// the default value.
        /// </returns>
        public static int RetryCount(this SqlStrategyOptions sqlStrategyOptions)
        {
            return (sqlStrategyOptions.RetryCount ?? SqlStrategyOptions.Defaults.RetryCount)!.Value;
        }

        /// <summary>
        /// The timeout for each retry.
        /// </summary>
        /// <param name="sqlStrategyOptions">
        /// The <see cref="SqlStrategyOptions"/> instance.
        /// </param>
        /// <returns>
        /// The configured value if it is not <see langword="null"/>; otherwise,
        /// the default value.
        /// </returns>
        public static TimeSpan TimeoutPerRetry(this SqlStrategyOptions sqlStrategyOptions)
        {
            return (sqlStrategyOptions.TimeoutPerRetry ?? SqlStrategyOptions.Defaults.TimeoutPerRetry)!.Value;
        }
    }
}