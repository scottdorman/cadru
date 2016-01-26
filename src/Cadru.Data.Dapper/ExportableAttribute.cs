using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadru.Data.Dapper
{
    public sealed class ExportableAttribute : Attribute
    {
        /// <summary>
        /// Indicates whether or not the field/property allows exporting of the
        /// value.
        /// </summary>
        /// <value>
        /// When <c>true</c>, the field/property is exportable.
        /// <para>
        /// When <c>false</c>, the field/property is not exportable.
        /// </para>
        /// </value>
        public bool AllowExport { get; private set; }

        /// <summary>
        /// Indicate whether or not a field/property is exportable.
        /// </summary>
        /// <param name="allowEdit">
        /// Indicates whether the field/property is editable.
        /// </param>
        public ExportableAttribute(bool allowEdit)
        {
            this.AllowExport = allowEdit;
        }
    }
}
