using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadru.Data.Dapper
{
    /// <summary>
    /// Specifies how string values should be handled.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class StringHandlingAttribute : Attribute
    {
        /// <summary>
        /// Gets the option for handling string values.
        /// </summary>
        /// <value>The string handling option.</value>
        public StringHandlingOption StringHandlingOption { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringHandlingAttribute"/> class.
        /// </summary>
        /// <param name="stringHandlingOption">
        /// The string handling option.
        /// </param>
        public StringHandlingAttribute(StringHandlingOption stringHandlingOption)
        {
            this.StringHandlingOption = stringHandlingOption;
        }
    }

}
