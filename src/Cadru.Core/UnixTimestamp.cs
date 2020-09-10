//------------------------------------------------------------------------------
// <copyright file="UnixTimestamp.cs"
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
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

using Cadru.Internal;
using Cadru.Resources;

namespace Cadru
{
    /// <summary>
    /// Represents an instant in time, defined as the number of seconds that
    /// have elapsed since 00:00:00 Coordinated Universal Time (UTC),
    /// Thursday, 1 January 1970, not counting leap seconds.
    /// </summary>
    /// <remarks>The date and time range that can be represented by a
    /// <see cref="UnixTimestamp"/> is constrained to the same date and
    /// time range as <see cref="DateTime"/>.</remarks>
    [StructLayout(LayoutKind.Auto)]
    public partial struct UnixTimestamp : IFormattable, IComparable, IComparable<UnixTimestamp>, IEquatable<UnixTimestamp>
    {
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
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        private readonly long seconds;

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
                throw new ArgumentOutOfRangeException(nameof(seconds), Strings.ArgumentOutOfRange_UnixTimestampBadSeconds);
            }

            Contract.EndContractBlock();

            this.seconds = seconds;
        }

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
            var sec = DateToSeconds(year, month, day, hour, minute, second);
            if (sec < UnixTimestamp.MinSeconds || sec > UnixTimestamp.MaxSeconds)
            {
                throw new ArgumentException(Strings.Arg_UnixTimestampRange);
            }

            this.seconds = sec;
        }

        /// <summary>
        /// Gets a <see cref="UnixTimestamp"/> object that is set to the current date and time on this computer.
        /// </summary>
        /// <value>
        /// An object whose value is the current local date and time.
        /// </value>
        public static UnixTimestamp Now => new UnixTimestamp(DateTime.Now);

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
        public DateTime DateTime => UnixTimestamp.Epoch.AddSeconds(this.seconds);

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
        public long Days => this.seconds / Constants.SecondsPerDay;

        /// <summary>
        /// Gets the number of seconds that represent the date and time of
        /// this instance.
        /// </summary>
        /// <value>The number of seconds that represent the date and time of
        /// this instance. The value is between
        /// <see cref="P:UnixTimestamp.MinValue.Seconds"/> and
        /// <see cref="P:UnixTimestamp.MaxValue.Seconds"/>.</value>
        public long Seconds => this.seconds;

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
            return this.AddSeconds(value * Constants.SecondsPerDay);
        }

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
            return this.AddSeconds(value * Constants.SecondsPerHour);
        }

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
            return this.AddSeconds(value * Constants.SecondsPerMinute);
        }

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
                throw new ArgumentOutOfRangeException(nameof(months), Strings.ArgumentOutOfRange_UnixTimestampBadMonths);
            }

            Contract.EndContractBlock();

            var datePart = this.DateTime;

            var y = datePart.Year;
            var m = datePart.Month;
            var d = datePart.Day;
            var i = m - 1 + months;
            if (i >= 0)
            {
                m = (i % 12) + 1;
                y += (i / 12);
            }
            else
            {
                m = 12 + ((i + 1) % 12);
                y += ((i - 11) / 12);
            }

            if (y < 1 || y > 9999)
            {
                throw new ArgumentOutOfRangeException(nameof(months), Strings.ArgumentOutOfRange_DateArithmetic);
            }

            var daysInMonth = DateTime.DaysInMonth(y, m);
            if (d > daysInMonth)
            {
                d = daysInMonth;
            }

            return new UnixTimestamp(y, m, d, datePart.Hour, datePart.Minute, datePart.Second);
        }

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
                throw ExceptionBuilder.CreateArgumentOutOfRangeException(nameof(years), Strings.ArgumentOutOfRange_UnixTimestampBadYears);
            }

            Contract.EndContractBlock();

            return this.AddMonths(years * 12);
        }

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

            if (obj is UnixTimestamp timestamp)
            {
                return this.CompareTo(timestamp);
            }

            throw new ArgumentException(Strings.Arg_MustBeUnixTimestamp);
        }

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
        public override bool Equals(object obj)
        {
            if (obj is UnixTimestamp timestamp)
            {
                return this.seconds == timestamp.seconds;
            }

            return false;
        }

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

        /// <summary>
        /// Converts the value of the current <see cref="UnixTimestamp"/> object
        /// to its equivalent <see cref="DateTime"/>.
        /// </summary>
        /// <returns>A <see cref="DateTime"/> representing the current <see
        /// cref="UnixTimestamp"/> object.</returns>
        public DateTime ToDateTime()
        {
            return UnixTimestamp.Epoch.AddSeconds(this.seconds);
        }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        private static long DateToSeconds(int year, int month, int day, int hour, int minute, int second)
        {
            long sec;
            try
            {
                var dateTime = new DateTime(year, month, day, hour, minute, second);
                sec = (dateTime.Ticks - Epoch.Ticks) / TimeSpan.TicksPerSecond;
            }
            catch
            {
                sec = (DateTime.MaxValue.Ticks - Epoch.Ticks) / TimeSpan.TicksPerSecond;
            }

            return sec;
        }
    }
}