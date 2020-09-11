using System;
using System.Diagnostics;

using Cadru.Extensions;
using Cadru.Resources;

namespace Cadru
{
    /// <summary>
    /// Represents a type whose value is constrained to be within an upper and lower bound.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DebuggerDisplay("{Value}, ({LowerBound}..{UpperBound})")]
    public struct ConstrainedNumeric<T> : IComparable, IComparable<ConstrainedNumeric<T>>, IComparable<T>, IEquatable<ConstrainedNumeric<T>>, IEquatable<T> where T : unmanaged, IComparable<T>
    {
        private T value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstrainedNumeric{T}"/> structure.
        /// </summary>
        /// <param name="lowerBound">The lower bound of the constraint.</param>
        /// <param name="upperBound">The upper bound of the constraint. </param>
        public ConstrainedNumeric(T lowerBound, T upperBound)
        {
            this.LowerBound = lowerBound;
            this.UpperBound = upperBound;
            this.value = lowerBound;
        }

        /// <summary>
        /// Gets the lower bound of the constraint.
        /// </summary>
        public T LowerBound { get; }

        /// <summary>
        /// Gets the upper bound of the constraint.
        /// </summary>
        public T UpperBound { get; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public T Value
        {
            get => this.value;
            set => this.value = value.Clamp(this.LowerBound, this.UpperBound);
        }

        /// <summary>
        /// Defines an implicit conversion from <see cref="ConstrainedNumeric{T}"/> to <typeparamref name="T"/>.
        /// </summary>
        /// <param name="constrainedNumeric">The object to convert.</param>
        /// <returns>The converted object.</returns>
        public static implicit operator T(ConstrainedNumeric<T> constrainedNumeric)
        {
            return constrainedNumeric.Value;
        }

        /// <inheritdoc/>
        public static bool operator !=(ConstrainedNumeric<T> left, ConstrainedNumeric<T> right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public static bool operator <(ConstrainedNumeric<T> left, ConstrainedNumeric<T> right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <inheritdoc/>
        public static bool operator <=(ConstrainedNumeric<T> left, ConstrainedNumeric<T> right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <inheritdoc/>
        public static bool operator ==(ConstrainedNumeric<T> left, ConstrainedNumeric<T> right)
        {
            return left.Value.Equals(right.Value);
        }

        /// <inheritdoc/>
        public static bool operator >(ConstrainedNumeric<T> left, ConstrainedNumeric<T> right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <inheritdoc/>
        public static bool operator >=(ConstrainedNumeric<T> left, ConstrainedNumeric<T> right)
        {
            return left.CompareTo(right) >= 0;
        }

        /// <inheritdoc/>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (obj is ConstrainedNumeric<T> cn)
            {
                return this.CompareTo(cn);
            }
            else if (obj is T v)
            {
                return this.CompareTo(v);
            }

            throw new ArgumentException(Strings.Arg_MustBeConstrainedNumeric);
        }

        /// <inheritdoc/>
        public int CompareTo(ConstrainedNumeric<T> other) => this.Value.CompareTo(other.Value);

        /// <inheritdoc/>
        public int CompareTo(T other) => this.Value.CompareTo(other);

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (!(obj is ConstrainedNumeric<T> cn))
            {
                return false;
            }
            else
            {
                if (obj is T v)
                {
                    return this.Equals(v);
                }

                return this.Equals(cn);
            }
        }

        /// <inheritdoc/>
        public bool Equals(ConstrainedNumeric<T> other) => this.Value.Equals(other.Value);

        /// <inheritdoc/>
        public bool Equals(T other) => this.Value.Equals(other);


        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }
    }
}
