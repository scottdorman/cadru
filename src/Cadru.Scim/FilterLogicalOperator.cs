using System;
using System.ComponentModel;

namespace Cadru.Scim.Filters
{
    /// <summary>
    /// The logical grouping operators for grouping one or more <see cref="IFilterExpression"></see>
    /// instances together in an <see cref="IFilterGroup"></see>.
    /// </summary>
    public readonly struct FilterLogicalOperator : IEquatable<FilterLogicalOperator>
    {
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterLogicalOperator"/> structure.
        /// </summary>
        /// <param name="value">The string value of the instance.</param>
        public FilterLogicalOperator(string value)
        {
            this._value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// The <see cref="IFilterGroup"></see> is only a match if all
        /// <see cref="IFilterExpression"></see> instances evaluate to true.
        /// </summary>
        public static FilterLogicalOperator And => new FilterLogicalOperator("and");

        /// <summary>
        /// The <see cref="IFilterGroup"></see> is only a match if any
        /// <see cref="IFilterExpression"></see> instances evaluate to true.
        /// </summary>
        public static FilterLogicalOperator Or => new FilterLogicalOperator("or");

        /// <summary>
        /// Converts a string to a <see cref="FilterLogicalOperator"/>.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        public static implicit operator FilterLogicalOperator(string value) => new FilterLogicalOperator(value);

        /// <summary>
        /// Determines if two <see cref="FilterLogicalOperator"/> values are different.
        /// </summary>
        /// <param name="left">The first <see cref="FilterLogicalOperator"/> to compare.</param>
        /// <param name="right">The second <see cref="FilterLogicalOperator"/> to compare.</param>
        /// <returns>True if <paramref name="left"/> and <paramref name="right"/> are different; otherwise, false.</returns>
        public static bool operator !=(FilterLogicalOperator left, FilterLogicalOperator right) => !left.Equals(right);

        /// <summary>
        /// Determines if two <see cref="FilterLogicalOperator"/> values are the same.
        /// </summary>
        /// <param name="left">The first <see cref="FilterLogicalOperator"/> to compare.</param>
        /// <param name="right">The second <see cref="FilterLogicalOperator"/> to compare.</param>
        /// <returns>True if <paramref name="left"/> and <paramref name="right"/> are the same; otherwise, false.</returns>
        public static bool operator ==(FilterLogicalOperator left, FilterLogicalOperator right) => left.Equals(right);
        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object? obj) => obj is FilterLogicalOperator other && this.Equals(other);

        /// <inheritdoc/>
        public bool Equals(FilterLogicalOperator other) => string.Equals(this._value, other._value, StringComparison.Ordinal);

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => this._value?.GetHashCode() ?? 0;

        /// <inheritdoc/>
        public override string ToString() => this._value;
    }
}
