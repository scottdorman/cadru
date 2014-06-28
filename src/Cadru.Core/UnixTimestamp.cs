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
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using Cadru.Internal;
    using Cadru.Properties;

    /// <summary>
    /// Represents an instant in time, defined as the number of seconds that 
    /// have elapsed since 00:00:00 Coordinated Universal Time (UTC), 
    /// Thursday, 1 January 1970, not counting leap seconds.
    /// </summary>
    /// <remarks>The date and time range that can be represented by a
    /// <see cref="UnixTimestamp"/> is constrained to the same date and
    /// time range as <see cref="DateTime"/>.</remarks>
    [StructLayout(LayoutKind.Auto)]
    public struct UnixTimestamp : IComparable, IFormattable, IConvertible, IComparable<UnixTimestamp>, IEquatable<UnixTimestamp>
    {
        #region fields
        /// <summary>
        /// Represents the largest possible value of <see cref="UnixTimestamp"/>. This field is read-only.
        /// </summary>
        /// <remarks>The value of this constant is equivalent to 23:59:59, December 31, 9999.</remarks>
        public static readonly UnixTimestamp MaxValue = new UnixTimestamp(UnixTimestamp.MaxSeconds);

        /// <summary>
        /// Represents the smallest possible value of <see cref="UnixTimestamp"/>. This field is read-only.
        /// </summary>
        /// <remarks>The value of this constant is equivalent to 00:00:00, January 01, 0001.</remarks>
        public static readonly UnixTimestamp MinValue = new UnixTimestamp(UnixTimestamp.MinSeconds);

        private const long MaxSeconds = 253402300799L;
        private const long MinSeconds = -62135596800L;
        private const int SecondsPerMinute = 60;
        private const int SecondsPerHour = SecondsPerMinute * 60;
        private const int SecondsPerDay = SecondsPerHour * 24;
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        private long seconds;
        #endregion

        #region constructors

        #region UnixTimestamp(long seconds)
        /// <summary>
        /// Initializes a new instance of the <see cref="UnixTimestamp"/> 
        /// structure to the specified number of seconds.
        /// </summary>
        /// <param name="seconds">A date and time expressed in the number of 
        /// seconds that have elapsed since January 1, 1970 at 00:00:00.000 
        /// in the Gregorian calendar.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="seconds"/> is less than 
        /// <see cref="UnixTimestamp.MinValue"/> or greater than
        /// <see cref="UnixTimestamp.MaxValue"/>.
        /// </exception>
        public UnixTimestamp(long seconds)
        {
            if (seconds < UnixTimestamp.MinSeconds || seconds > UnixTimestamp.MaxSeconds)
            {
                throw new ArgumentOutOfRangeException("seconds", Resources.ArgumentOutOfRange_UnixTimestampBadSeconds);
            }

            Contract.EndContractBlock();

            this.seconds = seconds;
        }
        #endregion

        #region UnixTimestamp(DateTime date)
        /// <summary>
        /// Initializes a new instance of the <see cref="UnixTimestamp"/> 
        /// structure to the specified <see cref="DateTime"/> value.
        /// </summary>
        /// <param name="date">A date and time.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="date"/> is less than 
        /// <see cref="UnixTimestamp.MinValue"/> or greater than
        /// <see cref="UnixTimestamp.MaxValue"/>.
        /// </exception>
        public UnixTimestamp(DateTime date)
            : this(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second)
        {
        }
        #endregion

        #region UnixTimestamp(int year, int month, int day)
        /// <summary>
        /// Initializes a new instance of the <see cref="UnixTimestamp"/>
        /// structure to the specified year, month and day.
        /// </summary>
        /// <param name="year">The year (0 through 9999).</param>
        /// <param name="month">The month (0 through 12).</param>
        /// <param name="day">The day (1 through the number of days in <paramref name="month"/>).</param>
        public UnixTimestamp(int year, int month, int day)
            : this(year, month, day, 0, 0, 0)
        {
        }
        #endregion

        #region UnixTimestamp(int year, int month, int day, int hour, int minute, int second)
        /// <summary>
        /// Initializes a new instance of the <see cref="UnixTimestamp"/> 
        /// structure to the specified year, month, day, hour, minute,
        /// and second.
        /// </summary>
        /// <param name="year">The year (0 through 9999).</param>
        /// <param name="month">The month (0 through 12).</param>
        /// <param name="day">The day (1 through the number of days in <paramref name="month"/>).</param>
        /// <param name="hour">The hours (0 through 23).</param>
        /// <param name="minute">The minutes (0 through 59).</param>
        /// <param name="second">The seconds (0 through 59).</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="second"/> is less than 
        /// <see cref="UnixTimestamp.MinValue"/> or greater than
        /// <see cref="UnixTimestamp.MaxValue"/>.
        /// </exception>
        public UnixTimestamp(int year, int month, int day, int hour, int minute, int second)
        {
            var seconds = DateToSeconds(year, month, day, hour, minute, second);
            if (seconds < UnixTimestamp.MinSeconds || seconds > UnixTimestamp.MaxSeconds)
            {
                throw new ArgumentException(Resources.Arg_UnixTimestampRange);
            }

            this.seconds = DateToSeconds(year, month, day, hour, minute, second);
        }
        #endregion

        #endregion

        #region events
        #endregion

        #region properties

        #region Now
        /// <summary>
        /// Gets a <see cref="UnixTimestamp"/> object that is set to the current date and time on this computer.
        /// </summary>
        /// <value>
        /// An object whose value is the current local date and time.
        /// </value>
        public static UnixTimestamp Now
        {
            get
            {
                return new UnixTimestamp(DateTime.Now);
            }
        }
        #endregion

        #region DateTime
        /// <summary>
        /// Gets a <see cref="DateTime"/> value that represents the date and 
        /// time of the current <see cref="UnixTimestamp"/> object.
        /// </summary>
        /// <value>
        /// The date and time of the current <see cref="UnixTimestamp"/> object.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The resulting <see cref="DateTime"/> is less than <see cref="P:DateTime.MinValue"/>
        /// or greater than <see cref="P:DateTime.MaxValue"/>.</exception>
        public DateTime DateTime
        {
            get
            {
                return UnixTimestamp.Epoch.AddSeconds(this.seconds);
            }
        }
        #endregion

        #region Days
        /// <summary>
        /// Gets the number of days since 00:00:00 Coordinated Universal Time (UTC), 
        /// Thursday, 1 January 1970 represented by current 
        /// <see cref="UnixTimestamp"/>.
        /// </summary>
        /// <value>
        /// The number of days since 00:00:00 Coordinated Universal Time (UTC), 
        /// Thursday, 1 January 1970 represented by current 
        /// <see cref="UnixTimestamp"/>.
        /// </value>
        public long Days
        {
            get
            {
                return this.seconds / UnixTimestamp.SecondsPerDay;
            }
        }
        #endregion

        #region Seconds
        /// <summary>
        /// Gets the number of seconds that represent the date and time of 
        /// this instance.
        /// </summary>
        /// <value>The number of seconds that represent the date and time of
        /// this instance. The value is between
        /// <see cref="P:UnixTimestamp.MinValue.Seconds"/> and 
        /// <see cref="P:UnixTimestamp.MaxValue.Seconds"/>.</value>
        public long Seconds
        {
            get
            {
                return this.seconds;
            }
        }
        #endregion

        #endregion

        #region operators

        #region Implicit(UnixTimetamp to long)
        /// <summary>
        /// Defines an implicit conversion from <see cref="UnixTimestamp"/> to <see cref="System.Int64"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>
        /// The converted object.
        /// </returns>
        public static implicit operator long(UnixTimestamp value)
        {
            return value.seconds;
        }
        #endregion

        #region Implicit(long to UnixTimestamp)
        /// <summary>
        /// Defines an implicit conversion from <see cref="System.Int64"/> to <see cref="UnixTimestamp"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>
        /// The converted object.
        /// </returns>
        public static implicit operator UnixTimestamp(long value)
        {
            return new UnixTimestamp(value);
        }
        #endregion

        #region Equality
        /// <summary>
        /// Determines whether two specified <see cref="UnixTimestamp"/>
        /// objects represent the same point in time.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>
        /// <see langword="true"/> if both objects represent the same point
        /// in time; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(UnixTimestamp left, UnixTimestamp right)
        {
            return left.seconds == right.seconds;
        }
        #endregion

        #region Inequality
        /// <summary>
        /// Determines whether two specified <see cref="UnixTimestamp"/>
        /// objects represent different points in time.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> and
        /// <paramref name="right"/> do not represent the same point
        /// in time; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(UnixTimestamp left, UnixTimestamp right)
        {
            return left.seconds != right.seconds;
        }
        #endregion

        #region GreaterThan
        /// <summary>
        /// Determines whether one specified <see cref="UnixTimestamp"/> object
        /// is earlier than another specified <see cref="UnixTimestamp"/> object.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is earlier than
        /// <paramref name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <(UnixTimestamp left, UnixTimestamp right)
        {
            return left.seconds < right.seconds;
        }
        #endregion

        #region GreaterThanOrEqual
        /// <summary>
        /// Determines whether one specified <see cref="UnixTimestamp"/> object
        /// is the same as or earlier than another specified 
        /// <see cref="UnixTimestamp"/> object.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is the same as or
        /// earlier than <paramref name="right"/>; otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        public static bool operator <=(UnixTimestamp left, UnixTimestamp right)
        {
            return left.seconds <= right.seconds;
        }
        #endregion

        #region LessThan
        /// <summary>
        /// Determines whether one specified <see cref="UnixTimestamp"/> object
        /// is later than another specified <see cref="UnixTimestamp"/> object.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is later than
        /// <paramref name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >(UnixTimestamp left, UnixTimestamp right)
        {
            return left.seconds > right.seconds;
        }
        #endregion

        #region LessThanOrEqual
        /// <summary>
        /// Determines whether one specified <see cref="UnixTimestamp"/> object
        /// is the same as or later than another specified 
        /// <see cref="UnixTimestamp"/> object.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is the same as or
        /// later than <paramref name="right"/>; otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        public static bool operator >=(UnixTimestamp left, UnixTimestamp right)
        {
            return left.seconds >= right.seconds;
        }
        #endregion

        #endregion

        #region methods

        #region Equals (static)
        /// <summary>
        /// Returns a value indicating whether two <see cref="UnixTimestamp"/>
        /// instances represent the same point in time.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><see langword="true"/> if the two 
        /// values are equal; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool Equals(UnixTimestamp left, UnixTimestamp right)
        {
            return left.seconds == right.seconds;
        }
        #endregion

        #region Add
        /// <summary>
        /// Returns a new <see cref="UnixTimestamp"/> that adds the value of 
        /// the specified <see cref="TimeSpan"/> to the value of this instance.
        /// </summary>
        /// <param name="value">The valueA positive or negative time interval.</param>
        /// <returns>An object whose value is the sum of the date and time
        /// represented by this instance and the time interval represented by
        /// <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The resulting <see cref="UnixTimestamp"/> is less than 
        /// <see cref="UnixTimestamp.MinValue"/> or greater than
        /// <see cref="UnixTimestamp.MaxValue"/>.
        /// </exception>
        public UnixTimestamp Add(TimeSpan value)
        {
            return new UnixTimestamp(this.DateTime + value);
        }
        #endregion

        #region AddDays
        /// <summary>
        /// Returns a new <see cref="UnixTimestamp"/> that adds the specified
        /// number of days to the value of this instance.
        /// </summary>
        /// <param name="value">A number of whole and fractional days. 
        /// The <paramref name="value"/> parameter can be negative or positive.</param>
        /// <returns>An object whose value is the sum of the date and time 
        /// represented by this instance and the number of days represented by
        /// <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The resulting <see cref="UnixTimestamp"/> is less than 
        /// <see cref="UnixTimestamp.MinValue"/> or greater than
        /// <see cref="UnixTimestamp.MaxValue"/>.
        /// </exception>
        public UnixTimestamp AddDays(double value)
        {
            return this.AddSeconds(value * UnixTimestamp.SecondsPerDay);
        }
        #endregion

        #region AddHours
        /// <summary>
        /// Returns a new <see cref="UnixTimestamp"/> that adds the specified
        /// number of hours to the value of this instance.
        /// </summary>
        /// <param name="value">A number of whole and fractional hours. 
        /// The <paramref name="value"/> parameter can be negative or positive.</param>
        /// <returns>An object whose value is the sum of the date and time 
        /// represented by this instance and the number of hours represented by
        /// <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The resulting <see cref="UnixTimestamp"/> is less than 
        /// <see cref="UnixTimestamp.MinValue"/> or greater than
        /// <see cref="UnixTimestamp.MaxValue"/>.
        /// </exception>
        public UnixTimestamp AddHours(double value)
        {
            return this.AddSeconds(value * UnixTimestamp.SecondsPerHour);
        }
        #endregion

        #region AddMinutes
        /// <summary>
        /// Returns a new <see cref="UnixTimestamp"/> that adds the specified
        /// number of minutes to the value of this instance.
        /// </summary>
        /// <param name="value">A number of whole and fractional minutes. 
        /// The <paramref name="value"/> parameter can be negative or positive.</param>
        /// <returns>An object whose value is the sum of the date and time 
        /// represented by this instance and the number of minutes represented by
        /// <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The resulting <see cref="UnixTimestamp"/> is less than 
        /// <see cref="UnixTimestamp.MinValue"/> or greater than
        /// <see cref="UnixTimestamp.MaxValue"/>.
        /// </exception>
        public UnixTimestamp AddMinutes(double value)
        {
            return this.AddSeconds(value * UnixTimestamp.SecondsPerMinute);
        }
        #endregion

        #region AddMonths
        /// <summary>
        /// Returns a new <see cref="UnixTimestamp"/> that adds the specified
        /// number of months to the value of this instance.
        /// </summary>
        /// <param name="months">A number of months. 
        /// The <paramref name="months"/> parameter can be negative or positive.</param>
        /// <returns>An object whose value is the sum of the date and time 
        /// represented by this instance and the number of months represented by
        /// <paramref name="months"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The resulting <see cref="UnixTimestamp"/> is less than 
        /// <see cref="UnixTimestamp.MinValue"/> or greater than
        /// <see cref="UnixTimestamp.MaxValue"/>.
        /// </exception>
        public UnixTimestamp AddMonths(int months)
        {
            if (months < -120000 || months > 120000)
            {
                throw new ArgumentOutOfRangeException("months", Resources.ArgumentOutOfRange_UnixTimestampBadMonths);
            }

            Contract.EndContractBlock();
            
            var datePart = this.DateTime;

            int y = datePart.Year;
            int m = datePart.Month;
            int d = datePart.Day;
            int i = m - 1 + months;
            if (i >= 0)
            {
                m = (i % 12) + 1;
                y = y + (i / 12);
            }
            else
            {
                m = 12 + ((i + 1) % 12);
                y = y + ((i - 11) / 12);
            }

            if (y < 1 || y > 9999)
            {
                throw new ArgumentOutOfRangeException("months", Resources.ArgumentOutOfRange_DateArithmetic);
            }
            
            int daysInMonth = DateTime.DaysInMonth(y, m);
            if (d > daysInMonth)
            {
                d = daysInMonth;
            }

            return new UnixTimestamp(y, m, d, datePart.Hour, datePart.Minute, datePart.Second);
        }
        #endregion

        #region AddSeconds
        /// <summary>
        /// Returns a new <see cref="UnixTimestamp"/> that adds the specified
        /// number of seconds to the value of this instance.
        /// </summary>
        /// <param name="value">A number of whole and fractional seconds. 
        /// The <paramref name="value"/> parameter can be negative or positive.</param>
        /// <returns>An object whose value is the sum of the date and time 
        /// represented by this instance and the number of seconds represented by
        /// <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The resulting <see cref="UnixTimestamp"/> is less than 
        /// <see cref="UnixTimestamp.MinValue"/> or greater than
        /// <see cref="UnixTimestamp.MaxValue"/>.
        /// </exception>
        public UnixTimestamp AddSeconds(double value)
        {
            return new UnixTimestamp((long)(this.seconds + value));
        }
        #endregion

        #region AddYears
        /// <summary>
        /// Returns a new <see cref="UnixTimestamp"/> that adds the specified
        /// number of years to the value of this instance.
        /// </summary>
        /// <param name="years">A number of years. 
        /// The <paramref name="years"/> parameter can be negative or positive.</param>
        /// <returns>An object whose value is the sum of the date and time 
        /// represented by this instance and the number of years represented by
        /// <paramref name="years"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para>The resulting <see cref="UnixTimestamp"/> is less than 
        /// <see cref="UnixTimestamp.MinValue"/> or greater than
        /// <see cref="UnixTimestamp.MaxValue"/>.</para>
        /// <para>-or-</para>
        /// <para><paramref name="years"/> is not in the valid range.</para>
        /// </exception>
        public UnixTimestamp AddYears(int years)
        {
            if (years < -10000 || years > 10000)
            {
                throw ExceptionBuilder.CreateArgumentOutOfRangeException("years", Resources.ArgumentOutOfRange_UnixTimestampBadYears);
            }

            Contract.EndContractBlock();

            return this.AddMonths(years * 12);
        }
        #endregion

        #region CompareTo

        #region CompareTo(object obj)
        /// <summary>
        /// Compares the value of this instance to a specified object that
        /// contains a specified <see cref="UnixTimestamp"/> value, and returns
        /// an integer that indicates whether this instance is earlier than, 
        /// the same as, or later than the specified <see cref="UnixTimestamp"/> value.
        /// </summary>
        /// <param name="obj">A boxed object to compare, or <see langword="null"/>.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and the 
        /// <paramref name="obj"/> parameter.
        /// <list type="table">
        /// <listheader>
        /// <term>Value</term>
        /// <term>Condition</term>
        /// </listheader>
        /// <item>
        /// <term>Less than zero</term>
        /// <description>
        /// This instance is earlier than <paramref name="obj"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Zero</term>
        /// <description>
        /// This instance is the same as <paramref name="obj"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Greater than zero</term>
        /// <description>
        /// This instance is later than <paramref name="obj"/>,
        /// or <paramref name="obj"/> is <see langword="null"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="obj"/> is not a <see cref="UnixTimestamp"/>.
        /// </exception>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (obj is UnixTimestamp)
            {
                return this.CompareTo((UnixTimestamp)obj);
            }

            throw new ArgumentException(Resources.Arg_MustBeUnixTimestamp);
        }
        #endregion

        #region CompareTo(UnixTimestamp value)
        /// <summary>
        /// Compares the value of this instance to a specified 
        /// <see cref="UnixTimestamp"/> value and returns an integer that 
        /// indicates whether this instance is earlier than, the same as, or
        /// later than the specified <see cref="UnixTimestamp"/> value. 
        /// </summary>
        /// <param name="other">The object to compare to the current instance.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and the 
        /// <paramref name="other"/> parameter.
        /// <list type="table">
        /// <listheader>
        /// <term>Value</term>
        /// <term>Condition</term>
        /// </listheader>
        /// <item>
        /// <term>Less than zero</term>
        /// <description>
        /// This instance is earlier than <paramref name="other"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Zero</term>
        /// <description>
        /// This instance is the same as <paramref name="other"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Greater than zero</term>
        /// <description>
        /// This instance is later than <paramref name="other"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        public int CompareTo(UnixTimestamp other)
        {
            return this.seconds.CompareTo(other.seconds);
        }
        #endregion

        #endregion

        #region Equals

        #region Equals(Object obj)
        /// <summary>
        /// Returns a value indicating whether the value of this instance is 
        /// equal to the value of the specified <see cref="UnixTimestamp"/> 
        /// instance.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="obj"/> parameter 
        /// equals the value of this instance; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>The current instance and <paramref name="obj"/> are equal 
        /// if their <see cref="Seconds"/> property values are equal.</remarks>
        public override bool Equals(Object obj)
        {
            if (obj is UnixTimestamp)
            {
                return this.seconds == ((UnixTimestamp)obj).seconds;
            }

            return false;
        }
        #endregion

        #region Equals(UnixTimestamp other)
        /// <summary>
        /// Returns a value indicating whether the value of this instance is
        /// equal to the value of the specified <see cref="UnixTimestamp"/>
        /// instance.
        /// </summary>
        /// <param name="other">The object to compare to this instance.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="other"/> parameter 
        /// equals the value of this instance; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>The current instance and <paramref name="other"/> are equal 
        /// if their <see cref="Seconds"/> property values are equal.</remarks>
        public bool Equals(UnixTimestamp other)
        {
            return this.seconds == other.seconds;
        }
        #endregion

        #endregion

        #region GetHashCode
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer hash code. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.seconds.GetHashCode();
        }
        #endregion

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

        #region Subtract

        #region Subtract(UnixTimestamp value)
        /// <summary>
        /// Subtracts the specified date and time from this instance.
        /// </summary>
        /// <param name="value">The date and time value to subtract.</param>
        /// <returns>A time interval that is equal to the date and time 
        /// represented by this instance minus the date and time represented
        /// by <paramref name="value"/>.</returns>
        /// <remarks>
        /// If the date and time of the current instance is earlier than value,
        /// the method returns a <see cref="TimeSpan"/> object that represents a
        /// negative time span. That is, the value of all of its non-zero properties
        /// (such as Days or Ticks) is negative.
        /// </remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The result is less than <see cref="MinValue"/> or greater than 
        /// <see cref="MaxValue"/>.</exception>
        public TimeSpan Subtract(UnixTimestamp value)
        {
            return TimeSpan.FromSeconds(this.Seconds - value.Seconds);
        }
        #endregion

        #region Subtract(TimeSpan value)
        /// <summary>
        /// Subtracts the specified duration from this instance.
        /// </summary>
        /// <param name="value">The time interval to subtract.</param>
        /// <returns>An object that is equal to the date and time represented
        /// by this instance minus the time interval represented by 
        /// <paramref name="value"/>.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The result is less than <see cref="MinValue"/> or greater than 
        /// <see cref="MaxValue"/>.</exception>
        public UnixTimestamp Subtract(TimeSpan value)
        {
            return new UnixTimestamp(this.DateTime - value);
        }
        #endregion

        #endregion

        #region ToString

        #region ToString()
        /// <summary>
        /// Converts the value of the current <see cref="UnixTimestamp"/>
        /// object to its equivalent string representation.
        /// </summary>
        /// <returns>
        /// A string representation of the value of the current <see cref="UnixTimestamp"/> object.
        /// </returns>
        public override string ToString()
        {
            return this.seconds.ToString(NumberFormatInfo.CurrentInfo);
        }
        #endregion

        #region ToString(string format, IFormatProvider formatProvider)
        /// <summary>
        /// Converts the value of the current <see cref="UnixTimestamp"/>
        /// object to its equivalent string representation using the specified
        /// format and culture-specific format information.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A string representation of value of the current <see cref="UnixTimestamp"/> object as specified by
        /// <paramref name="format"/> and <paramref name="formatProvider"/>.
        /// </returns>
        /// <exception cref="FormatException">
        /// <paramref name="format"/> is invalid or not supported.
        /// </exception>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return this.seconds.ToString(format, formatProvider);
        }
        #endregion

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
            throw new InvalidCastException(ExceptionBuilder.Format(Resources.InvalidCast_FromTo, "UnixTimestamp", "Boolean"));
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
            throw new InvalidCastException(ExceptionBuilder.Format(Resources.InvalidCast_FromTo, "UnixTimestamp", "Byte"));
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
            throw new InvalidCastException(ExceptionBuilder.Format(Resources.InvalidCast_FromTo, "UnixTimestamp", "Char"));
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
            throw new InvalidCastException(ExceptionBuilder.Format(Resources.InvalidCast_FromTo, "UnixTimestamp", "Decimal"));
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
            throw new InvalidCastException(ExceptionBuilder.Format(Resources.InvalidCast_FromTo, "UnixTimestamp", "Double"));
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
            throw new InvalidCastException(ExceptionBuilder.Format(Resources.InvalidCast_FromTo, "UnixTimestamp", "Int16"));
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
            throw new InvalidCastException(ExceptionBuilder.Format(Resources.InvalidCast_FromTo, "UnixTimestamp", "Int32"));
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
            throw new InvalidCastException(ExceptionBuilder.Format(Resources.InvalidCast_FromTo, "UnixTimestamp", "SByte"));
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
            throw new InvalidCastException(ExceptionBuilder.Format(Resources.InvalidCast_FromTo, "UnixTimestamp", "Single"));
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
            throw new InvalidCastException(ExceptionBuilder.Format(Resources.InvalidCast_FromTo, "UnixTimestamp", "String"));
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
            throw new InvalidCastException(ExceptionBuilder.Format(Resources.InvalidCast_FromTo, "UnixTimestamp", "UInt16"));
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
            throw new InvalidCastException(ExceptionBuilder.Format(Resources.InvalidCast_FromTo, "UnixTimestamp", "UInt32"));
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
            throw new InvalidCastException(ExceptionBuilder.Format(Resources.InvalidCast_FromTo, "UnixTimestamp", "UInt64"));
        }
        #endregion

        #region DateToSeconds
        private static long DateToSeconds(int year, int month, int day, int hour, int minute, int second)
        {
            var ticks = new DateTime(year, month, day, hour, minute, second).Ticks;
            var seconds = TicksToSeconds(ticks - UnixTimestamp.Epoch.Ticks);
            return seconds;
        }
        #endregion

        #region TicksToSeconds
        private static long TicksToSeconds(long ticks)
        {
            return ticks / TimeSpan.TicksPerSecond;
        }
        #endregion

        #endregion
    }
}