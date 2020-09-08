//------------------------------------------------------------------------------
// <copyright file="FilterExpressionOperator.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2020 Scott Dorman.
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

using System;
using System.ComponentModel;

namespace Cadru.Scim.Filters
{
    /// <summary>
    /// The expression operators.
    /// </summary>
    public readonly struct FilterExpressionOperator : IEquatable<FilterExpressionOperator>
    {
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterExpressionOperator"/> structure.
        /// </summary>
        /// <param name="value">The string value of the instance.</param>
        public FilterExpressionOperator(string value)
        {
            this._value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// The entire operator value must be a substring of the attribute value for a match.
        /// </summary>
        public static FilterExpressionOperator Contains => new FilterExpressionOperator("co");

        /// <summary>
        /// The entire operator value must be a substring of the attribute value,
        /// matching at the end of the attribute value.
        /// </summary>
        /// <remarks>
        /// This criterion is satisfied if the two strings are identical.
        /// </remarks>
        public static FilterExpressionOperator EndsWith => new FilterExpressionOperator("ew");

        /// <summary>
        /// The attribute and operator values must be identical for a match.
        /// </summary>
        public static FilterExpressionOperator Equal => new FilterExpressionOperator("eq");

        /// <summary>
        /// If the attribute value is greater than the operator value, there is
        /// a match.
        /// </summary>
        /// <remarks>
        /// The actual comparison is dependent on the attribute type. For string
        /// attribute types, this is a lexicographical comparison, and for
        /// DateTime types, it is a chronological comparison. For integer
        /// attributes, it is a comparison by numeric value. Boolean and Binary
        /// attributes will cause a failed response (HTTP status code 400) with
        /// "scimType" of "invalidFilter".
        /// </remarks>
        public static FilterExpressionOperator GreaterThan => new FilterExpressionOperator("gt");

        /// <summary>
        /// If the attribute value is greater than or equal to the operator
        /// value, there is a match.
        /// </summary>
        /// <remarks>
        /// The actual comparison is dependent on the attribute type. For string
        /// attribute types, this is a lexicographical comparison, and for
        /// DateTime types, it is a chronological comparison. For integer
        /// attributes, it is a comparison by numeric value. Boolean and Binary
        /// attributes will cause a failed response (HTTP status code 400) with
        /// "scimType" of "invalidFilter".
        /// </remarks>
        public static FilterExpressionOperator GreaterThanOrEqual => new FilterExpressionOperator("ge");

        /// <summary>
        /// If the attribute has a non-empty or non-null value, or if it
        /// contains a  non-empty node for complex attributes, there is a match.
        /// </summary>
        public static FilterExpressionOperator HasValue => new FilterExpressionOperator("pr");

        /// <summary>
        /// If the attribute value is less than the operator value, there is
        /// a match.
        /// </summary>
        /// <remarks>
        /// The actual comparison is dependent on the attribute type. For string
        /// attribute types, this is a lexicographical comparison, and for
        /// DateTime types, it is a chronological comparison. For integer
        /// attributes, it is a comparison by numeric value. Boolean and Binary
        /// attributes will cause a failed response (HTTP status code 400) with
        /// "scimType" of "invalidFilter".
        /// </remarks>
        public static FilterExpressionOperator LessThan => new FilterExpressionOperator("lt");

        /// <summary>
        /// If the attribute value is less than or equal to the operator
        /// value, there is a match.
        /// </summary>
        /// <remarks>
        /// The actual comparison is dependent on the attribute type. For string
        /// attribute types, this is a lexicographical comparison, and for
        /// DateTime types, it is a chronological comparison. For integer
        /// attributes, it is a comparison by numeric value. Boolean and Binary
        /// attributes will cause a failed response (HTTP status code 400) with
        /// "scimType" of "invalidFilter".
        /// </remarks>
        public static FilterExpressionOperator LessThanOrEqual => new FilterExpressionOperator("le");

        /// <summary>
        /// The attribute and operator values are not identical.
        /// </summary>
        public static FilterExpressionOperator NotEqual => new FilterExpressionOperator("ne");

        /// <summary>
        /// If the attribute has a non-empty or non-null value, or if it
        /// contains a  non-empty node for complex attributes, there is a match.
        /// </summary>
        /// <remarks>
        /// This is synonymous with <see cref="HasValue"></see>.
        /// </remarks>
        public static FilterExpressionOperator Present => new FilterExpressionOperator("pr");

        /// <summary>
        /// The entire operator value must be a substring of the attribute value,
        /// matching at the beginning of the attribute value.
        /// </summary>
        /// <remarks>
        /// This criterion is satisfied if the two strings are identical.
        /// </remarks>
        public static FilterExpressionOperator StartsWith => new FilterExpressionOperator("sw");

        /// <summary>
        /// Converts a string to a <see cref="FilterExpressionOperator"/>.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        public static implicit operator FilterExpressionOperator(string value) => new FilterExpressionOperator(value);

        /// <summary>
        /// Determines if two <see cref="FilterExpressionOperator"/> values are different.
        /// </summary>
        /// <param name="left">The first <see cref="FilterExpressionOperator"/> to compare.</param>
        /// <param name="right">The second <see cref="FilterExpressionOperator"/> to compare.</param>
        /// <returns>True if <paramref name="left"/> and <paramref name="right"/> are different; otherwise, false.</returns>
        public static bool operator !=(FilterExpressionOperator left, FilterExpressionOperator right) => !left.Equals(right);

        /// <summary>
        /// Determines if two <see cref="FilterExpressionOperator"/> values are the same.
        /// </summary>
        /// <param name="left">The first <see cref="FilterExpressionOperator"/> to compare.</param>
        /// <param name="right">The second <see cref="FilterExpressionOperator"/> to compare.</param>
        /// <returns>True if <paramref name="left"/> and <paramref name="right"/> are the same; otherwise, false.</returns>
        public static bool operator ==(FilterExpressionOperator left, FilterExpressionOperator right) => left.Equals(right);

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object? obj) => obj is FilterExpressionOperator other && this.Equals(other);

        /// <inheritdoc/>
        public bool Equals(FilterExpressionOperator other) => String.Equals(this._value, other._value, StringComparison.Ordinal);

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => this._value?.GetHashCode() ?? 0;

        /// <inheritdoc/>
        public override string ToString() => this._value;
    }
}