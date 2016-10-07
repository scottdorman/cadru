using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadru.Data.Dapper
{
    /// <summary>
    /// Denotes that a property should be excluded from being exported.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class ExportableAttribute : Attribute
    {
        /// <summary>
        /// Gets a value that indicates whether a field is exportable.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the field is exportable; otherwise, <see langword="false"/>.
        /// </value>
        public bool AllowExport { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportableAttribute"/> class.
        /// </summary>
        /// <param name="allowExport">
        /// <see langword="true"/> to specify that the field is exportable; otherwise, <see langword="false"/>.
        /// </param>
        public ExportableAttribute(bool allowExport)
        {
            this.AllowExport = allowExport;
        }
    }
}
