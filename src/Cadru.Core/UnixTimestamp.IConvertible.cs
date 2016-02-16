//------------------------------------------------------------------------------
// <copyright file="UnixTimestamp.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2014 Scott Dorman.
// </copyright>
//
// <license>
//    Licensed under the Microsoft Public License (Ms-PL) (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//    http://opensource.org/licenses/Ms-PL.html
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </license>
//------------------------------------------------------------------------------

namespace Cadru
{
#if !(NETCORE || WP || DOTNET)
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Cadru.Internal;


    public partial struct UnixTimestamp : IConvertible
    {
    #region fields
    #endregion

    #region constructors
    #endregion

    #region events
    #endregion

    #region properties
    #endregion

    #region operators
    #endregion

    #region methods

    #region GetTypeCode
        /// <summary>
        /// Returns the <see cref="T:System.TypeCode" /> for this instance.
        /// </summary>
        /// <returns>
        /// The enumerated constant <see cref="T:System.TypeCode">TypeCode.Object</see>.
        /// </returns>
        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }
    #endregion

    #region IConvertible.ToBoolean
        /// <summary>
        /// Infrastructure. This conversion is not supported. Attempting to
        /// use this method throws an <see cref="InvalidCastException"/>.
        /// </summary>
        /// <param name="provider">
        /// An object that implements the
        /// <see cref="T:System.IFormatProvider" /> interface.
        /// (This parameter is not used; specify <see langword="null"/>.)
        /// </param>
        /// <returns>
        /// The return value for this member is not used.
        /// </returns>
        /// <exception cref="System.InvalidCastException">In all cases.</exception>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed.")]
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            throw new InvalidCastException(ExceptionBuilder.Format(Core.Resources.Strings.InvalidCast_FromTo, "UnixTimestamp", "Boolean"));
        }
    #endregion

    #region IConvertible.ToByte
        /// <summary>
        /// Infrastructure. This conversion is not supported. Attempting to
        /// use this method throws an <see cref="InvalidCastException"/>.
        /// </summary>
        /// <param name="provider">
        /// An object that implements the
        /// <see cref="T:System.IFormatProvider" /> interface.
        /// (This parameter is not used; specify <see langword="null"/>.)
        /// </param>
        /// <returns>
        /// The return value for this member is not used.
        /// </returns>
        /// <exception cref="System.InvalidCastException">In all cases.</exception>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed.")]
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            throw new InvalidCastException(ExceptionBuilder.Format(Core.Resources.Strings.InvalidCast_FromTo, "UnixTimestamp", "Byte"));
        }
    #endregion

    #region IConvertible.ToChar
        /// <summary>
        /// Infrastructure. This conversion is not supported. Attempting to
        /// use this method throws an <see cref="InvalidCastException"/>.
        /// </summary>
        /// <param name="provider">
        /// An object that implements the
        /// <see cref="T:System.IFormatProvider" /> interface.
        /// (This parameter is not used; specify <see langword="null"/>.)
        /// </param>
        /// <returns>
        /// The return value for this member is not used.
        /// </returns>
        /// <exception cref="System.InvalidCastException">In all cases.</exception>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed.")]
        char IConvertible.ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException(ExceptionBuilder.Format(Core.Resources.Strings.InvalidCast_FromTo, "UnixTimestamp", "Char"));
        }
    #endregion

    #region IConvertible.ToDateTime
        /// <summary>
        /// Returns a <see cref="DateTime"/> object representing the current instance.
        /// </summary>
        /// <param name="provider">
        /// An object that implements the
        /// <see cref="T:System.IFormatProvider" /> interface.
        /// (This parameter is not used; specify <see langword="null"/>.)
        /// </param>
        /// <returns>
        /// A <see cref="T:System.DateTime" /> representing the current instance.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed.")]
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return UnixTimestamp.Epoch.AddSeconds(this.seconds);
        }
    #endregion

    #region IConvertible.ToDecimal
        /// <summary>
        /// Infrastructure. This conversion is not supported. Attempting to
        /// use this method throws an <see cref="InvalidCastException"/>.
        /// </summary>
        /// <param name="provider">
        /// An object that implements the
        /// <see cref="T:System.IFormatProvider" /> interface.
        /// (This parameter is not used; specify <see langword="null"/>.)
        /// </param>
        /// <returns>
        /// The return value for this member is not used.
        /// </returns>
        /// <exception cref="System.InvalidCastException">In all cases.</exception>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed.")]
        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            throw new InvalidCastException(ExceptionBuilder.Format(Core.Resources.Strings.InvalidCast_FromTo, "UnixTimestamp", "Decimal"));
        }
    #endregion

    #region IConvertible.ToDouble
        /// <summary>
        /// Infrastructure. This conversion is not supported. Attempting to
        /// use this method throws an <see cref="InvalidCastException"/>.
        /// </summary>
        /// <param name="provider">
        /// An object that implements the
        /// <see cref="T:System.IFormatProvider" /> interface.
        /// (This parameter is not used; specify <see langword="null"/>.)
        /// </param>
        /// <returns>
        /// The return value for this member is not used.
        /// </returns>
        /// <exception cref="System.InvalidCastException">In all cases.</exception>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed.")]
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            throw new InvalidCastException(ExceptionBuilder.Format(Core.Resources.Strings.InvalidCast_FromTo, "UnixTimestamp", "Double"));
        }
    #endregion

    #region IConvertible.ToInt16
        /// <summary>
        /// Infrastructure. This conversion is not supported. Attempting to
        /// use this method throws an <see cref="InvalidCastException"/>.
        /// </summary>
        /// <param name="provider">
        /// An object that implements the
        /// <see cref="T:System.IFormatProvider" /> interface.
        /// (This parameter is not used; specify <see langword="null"/>.)
        /// </param>
        /// <returns>
        /// The return value for this member is not used.
        /// </returns>
        /// <exception cref="System.InvalidCastException">In all cases.</exception>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed.")]
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            throw new InvalidCastException(ExceptionBuilder.Format(Core.Resources.Strings.InvalidCast_FromTo, "UnixTimestamp", "Int16"));
        }
    #endregion

    #region IConvertible.ToInt32
        /// <summary>
        /// Infrastructure. This conversion is not supported. Attempting to
        /// use this method throws an <see cref="InvalidCastException"/>.
        /// </summary>
        /// <param name="provider">
        /// An object that implements the
        /// <see cref="T:System.IFormatProvider" /> interface.
        /// (This parameter is not used; specify <see langword="null"/>.)
        /// </param>
        /// <returns>
        /// The return value for this member is not used.
        /// </returns>
        /// <exception cref="System.InvalidCastException">In all cases.</exception>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed.")]
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            throw new InvalidCastException(ExceptionBuilder.Format(Core.Resources.Strings.InvalidCast_FromTo, "UnixTimestamp", "Int32"));
        }
    #endregion

    #region IConvertible.ToInt64
        /// <summary>
        /// Returns a <see cref="T:System.Int64"/> representing the seconds of the current instance.
        /// </summary>
        /// <param name="provider">
        /// An object that implements the
        /// <see cref="T:System.IFormatProvider" /> interface.
        /// (This parameter is not used; specify <see langword="null"/>.)
        /// </param>
        /// <returns>
        /// A <see cref="T:System.Int64" /> representing the seconds of the current instance.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed.")]
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return this.seconds;
        }
    #endregion

    #region IConvertible.ToSByte
        /// <summary>
        /// Infrastructure. This conversion is not supported. Attempting to
        /// use this method throws an <see cref="InvalidCastException"/>.
        /// </summary>
        /// <param name="provider">
        /// An object that implements the
        /// <see cref="T:System.IFormatProvider" /> interface.
        /// (This parameter is not used; specify <see langword="null"/>.)
        /// </param>
        /// <returns>
        /// The return value for this member is not used.
        /// </returns>
        /// <exception cref="System.InvalidCastException">In all cases.</exception>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed.")]

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            throw new InvalidCastException(ExceptionBuilder.Format(Core.Resources.Strings.InvalidCast_FromTo, "UnixTimestamp", "SByte"));
        }
    #endregion

    #region IConvertible.ToSingle
        /// <summary>
        /// Infrastructure. This conversion is not supported. Attempting to
        /// use this method throws an <see cref="InvalidCastException"/>.
        /// </summary>
        /// <param name="provider">
        /// An object that implements the
        /// <see cref="T:System.IFormatProvider" /> interface.
        /// (This parameter is not used; specify <see langword="null"/>.)
        /// </param>
        /// <returns>
        /// The return value for this member is not used.
        /// </returns>
        /// <exception cref="System.InvalidCastException">In all cases.</exception>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed.")]
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            throw new InvalidCastException(ExceptionBuilder.Format(Core.Resources.Strings.InvalidCast_FromTo, "UnixTimestamp", "Single"));
        }
    #endregion

    #region IConvertible.ToString
        /// <summary>
        /// Infrastructure. This conversion is not supported. Attempting to
        /// use this method throws an <see cref="InvalidCastException"/>.
        /// </summary>
        /// <param name="provider">
        /// An object that implements the
        /// <see cref="T:System.IFormatProvider" /> interface.
        /// (This parameter is not used; specify <see langword="null"/>.)
        /// </param>
        /// <returns>
        /// The return value for this member is not used.
        /// </returns>
        /// <exception cref="System.InvalidCastException">In all cases.</exception>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed.")]
        string IConvertible.ToString(IFormatProvider provider)
        {
            throw new InvalidCastException(ExceptionBuilder.Format(Core.Resources.Strings.InvalidCast_FromTo, "UnixTimestamp", "String"));
        }
    #endregion

    #region IConvertible.ToType
        /// <summary>
        /// Converts the current <see cref="UnixTimestamp"/> object to an object of a specified type.
        /// </summary>
        /// <param name="type">The desired type.</param>
        /// <param name="provider">
        /// An object that implements the
        /// <see cref="T:System.IFormatProvider" /> interface.
        /// (This parameter is not used; specify <see langword="null"/>.)
        /// </param>
        /// <returns>
        /// An object of the type specified by the <paramref name="type"/> parameter,
        /// with a value equivalent to the current <see cref="UnixTimestamp"/> object.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <see langword="null"/>.</exception>
        /// <exception cref="System.InvalidCastException">This conversion is not supported for the Da<see cref="UnixTimestamp"/> type.</exception>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed.")]
        object IConvertible.ToType(Type type, IFormatProvider provider)
        {
            return Convert.ChangeType((IConvertible)this, type, provider);
        }
    #endregion

    #region IConvertible.ToUInt16
        /// <summary>
        /// Infrastructure. This conversion is not supported. Attempting to
        /// use this method throws an <see cref="InvalidCastException"/>.
        /// </summary>
        /// <param name="provider">
        /// An object that implements the
        /// <see cref="T:System.IFormatProvider" /> interface.
        /// (This parameter is not used; specify <see langword="null"/>.)
        /// </param>
        /// <returns>
        /// The return value for this member is not used.
        /// </returns>
        /// <exception cref="System.InvalidCastException">In all cases.</exception>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed.")]
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            throw new InvalidCastException(ExceptionBuilder.Format(Core.Resources.Strings.InvalidCast_FromTo, "UnixTimestamp", "UInt16"));
        }
    #endregion

    #region IConvertible.ToUInt32
        /// <summary>
        /// Infrastructure. This conversion is not supported. Attempting to
        /// use this method throws an <see cref="InvalidCastException"/>.
        /// </summary>
        /// <param name="provider">
        /// An object that implements the
        /// <see cref="T:System.IFormatProvider" /> interface.
        /// (This parameter is not used; specify <see langword="null"/>.)
        /// </param>
        /// <returns>
        /// The return value for this member is not used.
        /// </returns>
        /// <exception cref="System.InvalidCastException">In all cases.</exception>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed.")]
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            throw new InvalidCastException(ExceptionBuilder.Format(Core.Resources.Strings.InvalidCast_FromTo, "UnixTimestamp", "UInt32"));
        }
    #endregion

    #region IConvertible.ToUInt64
        /// <summary>
        /// Infrastructure. This conversion is not supported. Attempting to
        /// use this method throws an <see cref="InvalidCastException"/>.
        /// </summary>
        /// <param name="provider">
        /// An object that implements the
        /// <see cref="T:System.IFormatProvider" /> interface.
        /// (This parameter is not used; specify <see langword="null"/>.)
        /// </param>
        /// <returns>
        /// The return value for this member is not used.
        /// </returns>
        /// <exception cref="System.InvalidCastException">In all cases.</exception>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed.")]
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            throw new InvalidCastException(ExceptionBuilder.Format(Core.Resources.Strings.InvalidCast_FromTo, "UnixTimestamp", "UInt64"));
        }
    #endregion

    #endregion
    }
#endif
}