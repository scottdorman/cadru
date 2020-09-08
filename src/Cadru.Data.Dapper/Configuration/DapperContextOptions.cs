namespace Cadru.Data.Dapper.Configuration
{
    /// <summary>
    /// Configuration options for a <see cref="DapperContext"/>
    /// </summary>
    public class DapperContextOptions
    {
        /// <summary>
        /// The configuration section key.
        /// </summary>
        public const string SectionKey = "DapperContext";

        /// <summary>
        /// Command logging options for a <see cref="DapperContext"/>
        /// </summary>
        public LoggingOptions Logging { get; set; } = new LoggingOptions();

        /// <summary>
        /// Command timeout options for a <see cref="DapperContext"/>
        /// </summary>
        public TimeoutOptions Timeout { get; set; } = new TimeoutOptions();
    }

    /// <summary>
    /// Command logging options for a <see cref="DapperContext"/>
    /// </summary>
    public class LoggingOptions
    {
        /// <summary>
        /// Gets a value indicating whether command definitions should be logged.
        /// </summary>
        public bool CommandDefinitionLoggingEnabled { get; set; }
    }

    /// <summary>
    /// Command timeout options for a <see cref="DapperContext"/>
    /// </summary>
    public class TimeoutOptions
    {
        /// <summary>
        /// Gets the default command timeout.
        /// </summary>
        public int DefaultCommandTimeout { get; set; }

        /// <summary>
        /// Gets an extended command timeout.
        /// </summary>
        public int ExtendedCommandTimeout { get; set; }
    }
}
