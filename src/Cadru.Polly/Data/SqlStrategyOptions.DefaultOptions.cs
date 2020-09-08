using System;

using Cadru.Polly.Utilities;

namespace Cadru.Polly.Data
{
    public partial class SqlStrategyOptions
    {
        internal const int retryCountDefault = 5;
        internal const int exeptionsAllowedBeforeBreakingDefault = 3;
        internal const int durationOfBreakSecondsDefault = 30;

        /// <summary>
        /// Gets the default options values.
        /// </summary>
        public static SqlStrategyOptions Defaults => new SqlStrategyOptions
        {
            DurationOfBreak = TimeSpan.FromSeconds(durationOfBreakSecondsDefault),
            ExceptionsAllowedBeforeBreaking = exeptionsAllowedBeforeBreakingDefault,
            RetryCount = retryCountDefault,
            OverallTimeout = TimeoutHelper.GetTimeout(TimeSpan.Zero, retryCountDefault),
            TimeoutPerRetry = TimeoutHelper.GetTimeout(TimeSpan.Zero, retryCountDefault),
        };
    }
}
