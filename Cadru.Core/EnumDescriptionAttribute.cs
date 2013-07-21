using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cadru
{
    /// <summary>
    /// Provides a description for an enumerated type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class EnumDescriptionAttribute : Attribute
    {
        #region events
        #endregion

        #region class-wide fields
        private string description;
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="EnumDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="description">The description to store in this attribute.</param>
        public EnumDescriptionAttribute(string description)
            : base()
        {
            this.description = description;
        }
        #endregion

        #region properties

        #region Description
        /// <summary>
        /// Gets the description stored in this attribute.
        /// </summary>
        /// <value>The description stored in the attribute.</value>
        public string Description
        {
            get
            {
                return this.description;
            }
        }
        #endregion

        #endregion

        #region methods
        #endregion
    }
}
