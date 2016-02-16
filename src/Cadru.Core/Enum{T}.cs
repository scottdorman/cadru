namespace Cadru
{
    using Cadru.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Cadru.Portability;

    /// <summary>
    /// Provides a class for working with enumerations.
    /// </summary>
    /// <typeparam name="TEnum">The enumeration type.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "Reviewed.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Justification = "Reviewed.")]
    public static class Enum<TEnum> where TEnum : struct
    {
        #region GetDescription

        #region GetDescription(TEnum value)
        /// <summary>
        /// Retrieves the description of the constant in the enumeration
        /// that has the specified value.
        /// </summary>
        /// <param name="value">The value of a particular enumerated constant
        /// in terms of its underlying type.</param>
        /// <returns>A string containing the description of the enumerated constant
        /// in <typeparamref name="TEnum"/> whose value
        /// is <paramref name="value"/>; or <see langword="null"/> if no such
        /// constant is found.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException">
        /// <p><typeparamref name="TEnum"/> is not an <see cref="Enum"/></p>
        /// <p>-or</p>
        /// <p><paramref name="value"/> is neither of type
        /// <typeparamref name="TEnum"/> nor does it have the same underlying
        /// type as <typeparamref name="TEnum"/>.</p>
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static string GetDescription(TEnum value)
        {
            return GetDescription(value, useNameAsFallback: true);
        }
        #endregion

        #region GetDescription(TEnum value, bool useNameAsFallback)
        /// <summary>
        /// Retrieves the description of the constant in the enumeration
        /// that has the specified value.
        /// </summary>
        /// <param name="value">The value of a particular enumerated constant
        /// in terms of its underlying type.</param>
        /// <param name="useNameAsFallback">If <see langword="true"/>, the
        /// name of the enumerated constant is used if no description is found;
        /// otherwise, <see langword="null"/>.</param>
        /// <returns>A string containing the description of the enumerated constant
        /// in <typeparamref name="TEnum"/> whose value
        /// is <paramref name="value"/>; or <see langword="null"/> if no such
        /// constant is found and <paramref name="useNameAsFallback"/> is
        /// <see langword="false"/>; otherwise, the name of the enumerated constant.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException">
        /// <p><typeparamref name="TEnum"/> is not an <see cref="Enum"/></p>
        /// <p>-or</p>
        /// <p><paramref name="value"/> is neither of type
        /// <typeparamref name="TEnum"/> nor does it have the same underlying
        /// type as <typeparamref name="TEnum"/>.</p>
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static string GetDescription(TEnum value, bool useNameAsFallback)
        {
            Contracts.Requires.IsEnum(value, "value");
            var type = value.GetType();

            var fieldValue = value.ToString();
            var attribute = ((EnumDescriptionAttribute[])type.GetDeclaredField(fieldValue)?.GetCustomAttributes(typeof(EnumDescriptionAttribute), false)).FirstOrDefault();
            return attribute?.Description ?? (useNameAsFallback ? fieldValue : null);
        }
        #endregion

        #endregion

        #region GetDescriptions()
        /// <summary>
        /// Retrieves an array of the descriptions of the constants in the enumeration.
        /// </summary>
        /// <returns>A collection of the names of the constants in
        /// <typeparamref name="TEnum"/>.</returns>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="TEnum"/> is not an <see cref="Enum"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static IEnumerable<string> GetDescriptions()
        {
            return GetDescriptions(useNameAsFallback: true);
        }
        #endregion

        #region GetDescriptions(bool useNameAsFallback)
        /// <summary>
        /// Retrieves an array of the descriptions of the constants in the enumeration.
        /// </summary>
        /// <returns>A collection of the names of the constants in
        /// <typeparamref name="TEnum"/>.</returns>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="TEnum"/> is not an <see cref="Enum"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static IEnumerable<string> GetDescriptions(bool useNameAsFallback)
        {
            var type = typeof(TEnum);

            Contracts.Requires.IsTrue(type.IsEnum());
            var fields = type.GetDeclaredFields();
            var descriptions = new string[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                var attribute = ((EnumDescriptionAttribute[])fields[i]?.GetCustomAttributes(typeof(EnumDescriptionAttribute), false)).FirstOrDefault();
                descriptions[i] = attribute?.Description ?? (useNameAsFallback ? fields[i].Name : null);
            }

            return descriptions;
        }
        #endregion

        #region GetName
        /// <summary>
        /// Retrieves the name of the constant in the enumeration
        /// that has the specified value.
        /// </summary>
        /// <param name="value">The value of a particular enumerated constant
        /// in terms of its underlying type.</param>
        /// <returns>A string containing the name of the enumerated constant
        /// in <typeparamref name="TEnum"/> whose value
        /// is <paramref name="value"/>; or <see langword="null"/> if no such
        /// constant is found.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException">
        /// <p><typeparamref name="TEnum"/> is not an <see cref="Enum"/></p>
        /// <p>-or</p>
        /// <p><paramref name="value"/> is neither of type
        /// <typeparamref name="TEnum"/> nor does it have the same underlying
        /// type as <typeparamref name="TEnum"/>.</p>
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static string GetName(TEnum value)
        {
            return Enum.GetName(typeof(TEnum), value);
        }
        #endregion

        #region GetNames
        /// <summary>
        /// Retrieves an array of the names of the constants in the enumeration.
        /// </summary>
        /// <returns>A collection of the names of the constants in
        /// <typeparamref name="TEnum"/>.</returns>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="TEnum"/> is not an <see cref="Enum"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static IEnumerable<string> GetNames()
        {
            return Enum.GetNames(typeof(TEnum));
        }
        #endregion

        #region GetUnderlyingType
        /// <summary>
        /// Returns the underlying type of the enumeration.
        /// </summary>
        /// <returns>The underlying type of <typeparamref name="TEnum"/>.</returns>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="TEnum"/> is not an <see cref="Enum"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static Type GetUnderlyingType()
        {
            return Enum.GetUnderlyingType(typeof(TEnum));
        }
        #endregion

        #region GetValues
        /// <summary>
        /// Retrieves collection of the values of the constants in the
        /// enumeration.
        /// </summary>
        /// <returns>A collection that contains the values of the
        /// constants in <typeparamref name="TEnum"/>.</returns>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="TEnum"/> is not an <see cref="Enum"/>.</exception>
        /// <exception cref="InvalidOperationException">
        /// <p>The method is invoked by reflection in a reflection-only context.</p>
        /// <p>-or-</p>
        /// <p><typeparamref name="TEnum"/> is a type from an assembly loaded
        /// in a reflection-only context.</p>
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Reviewed.")]
        public static IEnumerable<TEnum> GetValues()
        {
            return Enum.GetValues(typeof(TEnum)).OfType<TEnum>();
        }
        #endregion

        #region IsDefined
        /// <summary>
        /// Returns an indication whether a constant with a specified value
        /// exists in the enumeration.
        /// </summary>
        /// <param name="value">The value or name of a constant in
        /// <typeparamref name="TEnum"/>.</param>
        /// <returns><see langword="true"/> if a constant in
        /// <typeparamref name="TEnum"/> has a value equal to
        /// <paramref name="value"/>; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// <p><typeparamref name="TEnum"/> is not an <see cref="Enum"/>.</p>
        /// <p>-or-</p>
        /// <p>The type of <paramref name="value"/> is an enumeration, but it
        /// is not an enumeration of type <typeparamref name="TEnum"/>.</p>
        /// <p>-or-</p>
        /// <p>The type of <paramref name="value"/> is not an underlying type
        /// of <typeparamref name="TEnum"/>.</p>
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="value"/> is not type <see cref="SByte"/>,
        /// <see cref="Int16"/>, <see cref="Int32"/>,
        /// <see cref="Int64"/>, <see cref="Byte"/>,
        /// <see cref="UInt16"/>, <see cref="UInt32"/>,
        /// <see cref="UInt64"/>, or <see cref="String"/>.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static bool IsDefined(object value)
        {
            return Enum.IsDefined(typeof(TEnum), value);
        }
        #endregion

        #region Parse

        #region Parse(string value)
        /// <summary>
        /// Converts the string representation of the name or numeric value of
        /// one or more enumerated constants to an equivalent enumerated object.
        /// </summary>
        /// <param name="value">A string containing the name or value to
        /// convert.</param>
        /// <returns>An instance of <typeparamref name="TEnum"/> whose value
        /// is represented by <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// <p><typeparamref name="TEnum"/> is not an <see cref="Enum"/>.</p>
        /// <p>-or-</p>
        /// <p><paramref name="value"/> is either <see langword="String.Empty"/> or
        /// contains only white space.</p>
        /// <p>-or-</p>
        /// <p><paramref name="value"/> is a name, but not one of the named
        /// constants defined for the enumeration.</p>
        /// </exception>
        /// <exception cref="OverflowException"><paramref name="value"/>
        /// is outside the range of the underlying type of
        /// <typeparamref name="TEnum"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static TEnum Parse(string value)
        {
            return Parse(value, true);
        }
        #endregion

        #region Parse(string value, bool ignoreCase)
        /// <summary>
        /// Converts the string representation of the name or numeric value of
        /// one or more enumerated constants to an equivalent enumerated object.
        /// A parameter specifies whether the operation is case-insensitive.
        /// </summary>
        /// <param name="value">A string containing the name or value to
        /// convert.</param>
        /// <param name="ignoreCase"><see langword="true"/> to ignore case;
        /// <see langword="false"/> to regard case.</param>
        /// <returns>An instance of <typeparamref name="TEnum"/> whose value
        /// is represented by <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// <p><typeparamref name="TEnum"/> is not an <see cref="Enum"/>.</p>
        /// <p>-or-</p>
        /// <p><paramref name="value"/> is either <see langword="String.Empty"/> or
        /// contains only white space.</p>
        /// <p>-or-</p>
        /// <p><paramref name="value"/> is a name, but not one of the named
        /// constants defined for the enumeration.</p>
        /// </exception>
        /// <exception cref="OverflowException"><paramref name="value"/>
        /// is outside the range of the underlying type of
        /// <typeparamref name="TEnum"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static TEnum Parse(string value, bool ignoreCase)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);
        }
        #endregion

        #endregion

        #region ToEnum
        /// <summary>
        /// Converts the specified object with an integer value to an enumeration member.
        /// </summary>
        /// <param name="value">The value convert to an enumeration member.</param>
        /// <returns>An enumeration whose value is <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// <p><typeparamref name="TEnum"/> is not an <see cref="Enum"/>.</p>
        /// <p>-or-</p>
        /// <p><paramref name="value"/> is not type <see cref="SByte"/>,
        /// <see cref="Int16"/>, <see cref="Int32"/>,
        /// <see cref="Int64"/>, <see cref="Byte"/>,
        /// <see cref="UInt16"/>, <see cref="UInt32"/>,
        /// <see cref="UInt64"/>, or <see cref="String"/>.</p>
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static TEnum ToEnum(object value)
        {
            return (TEnum)Enum.ToObject(typeof(TEnum), value);
        }
        #endregion

        #region TryParse

        #region TryParse(string value, out TEnum result)
        /// <summary>
        /// Converts the string representation of the name or numeric value of
        /// one or more enumerated constants to an equivalent enumerated object.
        /// The return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value">The string representation of the enumeration
        /// name or underlying value to convert.</param>
        /// <param name="result">When this method returns, contains an object of
        /// type <typeparamref name="TEnum"/> whose value is represented
        /// by value. This parameter is passed uninitialized.</param>
        /// <returns><see langword="true"/> if the value parameter was
        /// converted successfully; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="TEnum"/> is not an enumeration type.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static bool TryParse(string value, out TEnum result)
        {
            return TryParse(value, true, out result);
        }
        #endregion

        #region TryParse(string value, bool ignoreCase, out TEnum result)
        /// <summary>
        /// Converts the string representation of the name or numeric value of
        /// one or more enumerated constants to an equivalent enumerated object.
        /// A parameter specifies whether the operation is case-sensitive. The
        /// return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value">The string representation of the enumeration
        /// name or underlying value to convert.</param>
        /// <param name="ignoreCase"><see langword="true"/> to ignore case;
        /// <see langword="false"/> to regard case.</param>
        /// <param name="result">When this method returns, contains an object of
        /// type <typeparamref name="TEnum"/> whose value is represented
        /// by value. This parameter is passed uninitialized.</param>
        /// <returns><see langword="true"/> if the value parameter was
        /// converted successfully; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="TEnum"/> is not an enumeration type.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static bool TryParse(string value, bool ignoreCase, out TEnum result)
        {
            return Enum.TryParse<TEnum>(value, ignoreCase, out result);
        }
        #endregion

        #endregion
    }
}