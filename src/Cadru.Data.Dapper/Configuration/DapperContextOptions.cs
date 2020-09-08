
using Microsoft.Extensions.Options;

namespace Cadru.Data.Dapper.Configuration
{
    public class DapperContextOptions : IOptions<DapperContextOptions>
    {
        public const string SectionKey = "DapperContext";

        public TimeoutOptions Timeout { get; set; } = new TimeoutOptions();
        public LoggingOptions Logging { get; set; } = new LoggingOptions();
        public DapperContextOptions Value => this;
    }

    public class TimeoutOptions
    {
        public int DefaultCommandTimeout { get; set; }
        public int ExtendedCommandTimeout { get; set; }
    }

    public class LoggingOptions
    {
        public bool CommandDefinitionLoggingEnabled { get; set; }
    }
}
