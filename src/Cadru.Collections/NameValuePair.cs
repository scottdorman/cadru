//------------------------------------------------------------------------------
// <copyright file="NameValuePair.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2017 Scott Dorman.
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

namespace Cadru.Collections
{
    using System.Collections.Generic;
    using System.Text;
    using Cadru.Extensions;

    /// <summary>Defines a key/value pair that can be set or retrieved.</summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <filterpriority>1</filterpriority>
    public struct NameValuePair<TValue>
    {
        #region fields
        private string key;
        private IList<TValue> value;
        #endregion

        #region constructors
        /// <summary>Initializes a new instance of the <see cref="NameValuePair{TValue}" />
        /// structure with the specified key.</summary>
        /// <param name="key">The object defined in each key/value pair.</param>
        public NameValuePair(string key)
        {
            this.key = key;
            this.value = new List<TValue>();
        }
        #endregion

        #region events
        #endregion

        #region properties

        #region Key
        /// <summary>Gets the key in the key/value pair.</summary>
        /// <value>A <see cref="string"/> that is the key of the <see cref="NameValuePair{TValue}" />. </value>
        public string Key
        {
            get
            {
                return this.key;
            }
        }
        #endregion

        #region Value
        /// <summary>Gets the value in the key/value pair.</summary>
        /// <value>A <see cref="IList{TValue}"/> that is the value of the <see cref="NameValuePair{TValue}" />. </value>
        public IList<TValue> Value
        {
            get
            {
                return this.value;
            }
        }
        #endregion

        #endregion

        #region operators

        #region op_Equality
        /// <summary>
        /// Determines whether two specified instances of <see cref="NameValuePair{TValue}"/> are equal.
        /// </summary>
        /// <param name="left">An <see cref="NameValuePair{TValue}"/>.</param>
        /// <param name="right">An <see cref="NameValuePair{TValue}"/>.</param>
        /// <returns><see langword="true"/> if left and right represent the same server; otherwise <see langword="false"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed.")]
        public static bool operator ==(NameValuePair<TValue> left, NameValuePair<TValue> right)
        {
            return left.Equals(right);
        }
        #endregion

        #region op_Inequality
        /// <summary>
        /// Determines whether two specified instances of <see cref="NameValuePair{TValue}"/> are not equal.
        /// </summary>
        /// <param name="left">An <see cref="NameValuePair{TValue}"/>.</param>
        /// <param name="right">An <see cref="NameValuePair{TValue}"/>.</param>
        /// <returns><see langword="true"/> if left and right do note represent the same server;
        /// otherwise <see langword="false"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed.")]
        public static bool operator !=(NameValuePair<TValue> left, NameValuePair<TValue> right)
        {
            return !left.Equals(right);
        }
        #endregion

        #endregion

        #region methods

        #region Equals

        #region Equals(NameValuePair<TValue> other)
        /// <summary>
        /// Returns a value indicating whether this instance is equal to the specified <see cref="NameValuePair{TValue}"/> instance.
        /// </summary>
        /// <param name="other">An <see cref="NameValuePair{TValue}"/> instance to compare to this instance.</param>
        /// <returns><see langword="true"/> if the other parameter equals the value of this instance; otherwise, <see langword="false"/>. </returns>
        /// <remarks>This method implements the <see cref="System.IEquatable{T}"/> interface and performs slightly
        /// better than the <see cref="NameValuePair{TValue}.Equals(Object)"/> method because it does not have to convert
        /// the other parameter to an object.</remarks>
        public bool Equals(NameValuePair<TValue> other)
        {
            if (other.key != this.key)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Equals(object obj)
        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare to this instance.</param>
        /// <returns><see langword="true"/> if value is an instance of <see cref="NameValuePair{TValue}"/>
        /// equals the value of this instance; otherwise, <see langword="false"/>. </returns>
        public override bool Equals(object obj)
        {
            if (obj.IsNull() || !(obj is NameValuePair<TValue>))
            {
                return false;
            }
            else
            {
                return ((NameValuePair<TValue>)obj).Equals(this);
            }
        }
        #endregion

        #endregion

        #region GetHashCode
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return this.key.GetHashCode();
        }
        #endregion

        #region ToString
        /// <summary>Returns a string representation of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />, using the string representations of the key and value.</summary>
        /// <returns>A string representation of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />, which includes the string representations of the key and value.</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(16);
            stringBuilder.Append("[");
            if (this.Key.IsNotNull())
            {
                stringBuilder.Append(this.Key.ToString());
            }

            stringBuilder.Append(": ");
            var total = this.Value.Count;

            for (int i = 0; i < total; i++)
            {
                TValue v = this.Value[i];
                if (v.IsNotNull())
                {
                    stringBuilder.Append(v);
                    if (i < total - 1)
                    {
                        stringBuilder.Append(", ");
                    }
                }
            }

            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }

        #endregion

        #endregion
    }
}
