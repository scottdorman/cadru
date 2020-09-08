using System;

namespace Cadru.Polly.Data
{
    /// <summary>
    /// The configuration options for a retry strategy.
    /// </summary>
    public partial class SqlStrategyOptions
    {
        /// <summary>
        /// The configuration section key.
        /// </summary>
        public const string SectionKey = "RetryStrategy";

        /// <summary>
        /// The duration the circuit will stay open before resetting.
        /// </summary>
        public TimeSpan? DurationOfBreak { get; set; }

        /// <summary>
        /// The number of exceptions that are allowed before opening the circuit.
        /// </summary>
        public int? ExceptionsAllowedBeforeBreaking { get; set; }

        /// <summary>
        /// The retry count.
        /// </summary>
        public int? RetryCount { get; set; }

        /// <summary>
        /// The timeout for the overall strategy.
        /// </summary>
        public TimeSpan? OverallTimeout { get; set; }

        /// <summary>
        /// The timeout for each retry.
        /// </summary>
        public TimeSpan? TimeoutPerRetry { get; set; }
    }
}
