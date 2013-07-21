using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cadru.Extensions
{
    public static class EnumExtensions
    {
        #region GetDescription
        /// <summary>
        /// Gets the <see cref="DescriptionAttribute"/> of an <see cref="Enum"/> type value.
        /// </summary>
        /// <param name="value">The <see cref="Enum"/> type value.</param>
        /// <returns>A string containing the text of the <see cref="DescriptionAttribute"/>.</returns>
        public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            Type type = value.GetType();
            string description = Enum.GetName(type, value);
            FieldInfo fieldInfo = value.GetType().GetField(description);
            if (fieldInfo != null)
            {
                EnumDescriptionAttribute attribute = ((EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false)).FirstOrDefault();
                if (attribute != null)
                {
                    description = attribute.Description;
                }
            }

            return description;
        }
        #endregion
    }
}