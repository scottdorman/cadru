using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadru.Data.Dapper
{
    /// <summary>
    /// Represents the string handling option.
    /// </summary>
    public enum StringHandlingOption
    {
        /// <summary>
        /// The string value should not be modified.
        /// </summary>
        None = 0,

        /// <summary>
        /// The string value should be trimmed to remove leading and trailing spaces.
        /// </summary>
        Trim = 1,

        /// <summary>
        /// The string value should be truncated.
        /// </summary>
        Truncate = 2
    }
}
