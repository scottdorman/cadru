//------------------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs"
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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

using Cadru.Internal;
using Cadru.Resources;
using Cadru.Text;

namespace Cadru.Extensions
{
    /// <summary>
    /// Provides basic routines for common DateTime manipulation.
    /// </summary>
    public static class DateTimeExtensions
    {
        private static readonly TimeSpan UtcOffset = new TimeSpan(0, 0, 0);

        /// <summary>
        /// Returns a new <see cref="DateTime"/> that adds the specified number
        /// of quarters to the value of this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="value">
        /// A number of whole and fractional quarters. The
        /// <paramref name="value"/> parameter can be negative or positive.
        /// </param>
        /// <returns>
        /// A <see cref="DateTime"/> whose value is the sum of the date and time
        /// represented by this instance and the number of quarters represented
        /// by <paramref name="value"/>.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The resulting <see cref="DateTime"/> is less than
        /// <see cref="DateTime.MinValue"/> or greater than <see cref="DateTime.MaxValue"/>.
        /// </exception>
        public static DateTime AddQuarters(this DateTime date, double value) => date.AddMonths(checked((int)value * 3));

        /// <summary>
        /// Returns a new <see cref="DateTime"/> that adds the specified number
        /// of weekdays to the value of this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="value">
        /// A number of whole and fractional weekdays. The
        /// <paramref name="value"/> parameter can be negative or positive.
        /// </param>
        /// <returns>
        /// A <see cref="DateTime"/> whose value is the sum of the date and time
        /// represented by this instance and the number of weekdays represented
        /// by <paramref name="value"/>.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The resulting <see cref="DateTime"/> is less than
        /// <see cref="DateTime.MinValue"/> or greater than <see cref="DateTime.MaxValue"/>.
        /// </exception>
        public static DateTime AddWeekdays(this DateTime date, double value)
        {
            var direction = value < 0 ? -1 : 1;

            while (value != 0)
            {
                date = date.AddDays(direction);
                if (!date.IsWeekend())
                {
                    value -= direction;
                }
            }

            return date;
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> that adds the specified number
        /// of weeks to the value of this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="value">
        /// A number of whole and fractional weeks. The <paramref name="value"/>
        /// parameter can be negative or positive.
        /// </param>
        /// <returns>
        /// A <see cref="DateTime"/> whose value is the sum of the date and time
        /// represented by this instance and the number of weeks represented by <paramref name="value"/>.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The resulting <see cref="DateTime"/> is less than
        /// <see cref="DateTime.MinValue"/> or greater than <see cref="DateTime.MaxValue"/>.
        /// </exception>
        public static DateTime AddWeeks(this DateTime date, double value) => date.AddDays(value * 7);

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether the
        /// current <see cref="DateTime"/> instance is between the start and end indicated.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="start">The starting <see cref="DateTime"/>.</param>
        /// <param name="end">The ending <see cref="DateTime"/>.</param>
        /// <returns>
        /// <see langword="true"/> if the current instance is between
        /// <paramref name="start"/> and <paramref name="end"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool Between(this DateTime date, DateTime start, DateTime end) => Between(date, start, end, true);

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether the
        /// current <see cref="DateTime"/> instance is between the start and end indicated.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="start">The starting <see cref="DateTime"/>.</param>
        /// <param name="end">The ending <see cref="DateTime"/>.</param>
        /// <param name="includeTime">
        /// <see langword="true"/> to compare the time portion of the date;
        /// otherwise, <see langword="false"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the current instance is between
        /// <paramref name="start"/> and <paramref name="end"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool Between(this DateTime date, DateTime start, DateTime end, bool includeTime)
        {
            return includeTime ?
               date >= start && date <= end :
               date.Date >= start.Date && date.Date <= end.Date;
        }

        /// <summary>
        /// Returns the number of days in the month for the date represented by
        /// this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// The number of days in the month for the date represented by this instance.
        /// </returns>
        public static int DaysInMonth(this DateTime date) => DateTime.DaysInMonth(date.Year, date.Month);

        /// <summary>
        /// Returns the elapsed time between the date represented by this
        /// instance and the current date and time.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// A <see cref="TimeSpan"/> representing the elapsed time between the
        /// date represented by this instance and the current date and time.
        /// </returns>
        public static TimeSpan Elapsed(this DateTime date) => date.Elapsed(DateTime.Now);

        /// <summary>
        /// Returns the elapsed time between the date represented by this
        /// instance and the given date and time.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="startDate">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// A <see cref="TimeSpan"/> representing the elapsed time between the
        /// date represented by this instance and the current date and time.
        /// </returns>
        public static TimeSpan Elapsed(this DateTime date, DateTime startDate) => startDate - date;

        /// <summary>
        /// Returns a <see cref="DateTime"/> representing the first day of the
        /// month for the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTime"/> representing the first day of the month for
        /// the date represented by this instance.
        /// </returns>
        public static DateTime FirstDayOfMonth(this DateTime date) => new DateTime(date.Year, date.Month, 1);

        /// <summary>
        /// Returns a <see cref="DateTime"/> which represents the first day of
        /// the next quarter of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTime"/> which represents the first day of the next
        /// quarter of the date represented by this instance.
        /// </returns>
        public static DateTime FirstDayOfNextQuarter(this DateTime date) => date.FirstDayOfQuarter().AddMonths(3);

        /// <summary>
        /// Returns a <see cref="DateTime"/> which represents the first day of
        /// the quarter of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTime"/> which represents the first day of the
        /// quarter of the date represented by this instance.
        /// </returns>
        public static DateTime FirstDayOfQuarter(this DateTime date) => new DateTime(date.Year, ((date.Quarter() - 1) * 3) + 1, 1);

        /// <summary>
        /// Returns a <see cref="DateTime"/> which represents the first day of
        /// the week of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTime"/> which represents the first day of the week
        /// of the date represented by this instance.
        /// </returns>
        public static DateTime FirstDayOfWeek(this DateTime date) => date.FirstDayOfWeek(DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);

        /// <summary>
        /// Returns a <see cref="DateTime"/> which represents the first day of
        /// the week of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="startOfWeek">
        /// An enumeration value that represents the first day of the week.
        /// </param>
        /// <returns>
        /// A <see cref="DateTime"/> which represents the first day of the week
        /// of the date represented by this instance.
        /// </returns>
        public static DateTime FirstDayOfWeek(this DateTime date, DayOfWeek startOfWeek)
        {
            var diff = date.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return date.AddDays(-1 * diff).Date;
        }

        /// <summary>
        /// Returns a <see cref="DateTime"/> representing the first day of the
        /// year for the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTime"/> representing the first day of the year for
        /// the date represented by this instance.
        /// </returns>
        public static DateTime FirstDayOfYear(this DateTime date) => new DateTime(date.Year, 1, 1);

        /// <summary>
        /// Returns the culture-specific abbreviated name of the month
        /// represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// The culture-specific abbreviated name of the month represented by
        /// this instance.
        /// </returns>
        public static string GetAbbreviatedMonthName(this DateTime date) => DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(date.Month);

        /// <summary>
        /// Returns the culture-specific abbreviated names of the months.
        /// </summary>
        /// <returns>
        /// A list that contains the culture-specific abbreviated names of the months.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is an extension method.")]
        public static IEnumerable<string> GetAbbreviatedMonthNames() => CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames.Where(m => !String.IsNullOrEmpty(m));

        /// <summary>
        /// Returns a <see cref="DateTime"/> representing the day of the week
        /// from the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="day">
        /// An enumeration value that represents the day of the week for which
        /// the date is to be calculated.
        /// </param>
        /// <returns>
        /// A <see cref="DateTime"/> representing the day of the week from the
        /// date represented by this instance.
        /// </returns>
        public static DateTime GetDayOfWeek(this DateTime date, DayOfWeek day) => date.GetDayOfWeek(day, DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);

        /// <summary>
        /// Returns a <see cref="DateTime"/> representing the day of the week
        /// from the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="day">
        /// An enumeration value that represents the day of the week for which
        /// the date is to be calculated.
        /// </param>
        /// <param name="startOfWeek">
        /// An enumeration value that represents the first day of the week.
        /// </param>
        /// <returns>
        /// A <see cref="DateTime"/> representing the day of the week from the
        /// date represented by this instance.
        /// </returns>
        public static DateTime GetDayOfWeek(this DateTime date, DayOfWeek day, DayOfWeek startOfWeek)
        {
            var current = DaysBetween(date.DayOfWeek, startOfWeek);
            var resultday = DaysBetween(day, startOfWeek);
            return date.AddDays(resultday - current);
        }

        /// <summary>
        /// Returns the culture-specific name of the month represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// The culture-specific name of the month represented by this instance.
        /// </returns>
        public static string GetMonthName(this DateTime date) => DateTimeFormatInfo.CurrentInfo.GetMonthName(date.Month);

        /// <summary>
        /// Returns the culture-specific names of the months.
        /// </summary>
        /// <returns>
        /// A list that contains the culture-specific names of the months.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is an extension method.")]
        public static IEnumerable<string> GetMonthNames() => CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.Where(m => !String.IsNullOrEmpty(m));

        /// <summary>
        /// Returns the month number for the given month name.
        /// </summary>
        /// <param name="name">The month name.</param>
        /// <param name="abbreviated">
        /// <see langword="true"/> if the name is abbreviated; otherwise, <see langword="false"/>.
        /// </param>
        /// <returns>The month number for the given month name.</returns>
        public static int GetMonthNumber(string name, bool abbreviated)
        {
            var months = abbreviated ? GetAbbreviatedMonthNames() : GetMonthNames();
            return months.IndexOf(name) + 1;
        }

        /// <summary>
        /// Returns the week of the year that includes the date in the specified
        /// DateTime value.
        /// </summary>
        /// <param name="time">A date and time value.</param>
        /// <returns>
        /// A positive integer that represents the week of the year that
        /// includes the date in the <paramref name="time"/> parameter.
        /// </returns>
        public static int GetWeekOfYear(this DateTime time) => GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

        /// <summary>
        /// Returns the week of the year that includes the date in the specified
        /// DateTime value.
        /// </summary>
        /// <param name="time">A date and time value.</param>
        /// <param name="rule">
        /// An enumeration value that defines a calendar week.
        /// </param>
        /// <returns>
        /// A positive integer that represents the week of the year that
        /// includes the date in the <paramref name="time"/> parameter.
        /// </returns>
        public static int GetWeekOfYear(this DateTime time, CalendarWeekRule rule) => GetWeekOfYear(time, rule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

        /// <summary>
        /// Returns the week of the year that includes the date in the specified
        /// DateTime value.
        /// </summary>
        /// <param name="time">A date and time value.</param>
        /// <param name="rule">
        /// An enumeration value that defines a calendar week.
        /// </param>
        /// <param name="firstDayOfWeek">
        /// An enumeration value that represents the first day of the week.
        /// </param>
        /// <returns>
        /// A positive integer that represents the week of the year that
        /// includes the date in the <paramref name="time"/> parameter.
        /// </returns>
        public static int GetWeekOfYear(this DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek) => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(time, rule, firstDayOfWeek);

        /// <summary>
        /// Determines whether the specified date is a leap day.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// <see langword="true"/> if the specified date is a leap day;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsLeapDay(this DateTime date) => CultureInfo.CurrentCulture.Calendar.IsLeapDay(date.Year, date.Month, date.Day);

        /// <summary>
        /// Determines whether the specified date is a leap month.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// <see langword="true"/> if the specified date is a leap month;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsLeapMonth(this DateTime date) => CultureInfo.CurrentCulture.Calendar.IsLeapMonth(date.Year, date.Month);

        /// <summary>
        /// Determines whether the specified date is a leap year.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// <see langword="true"/> if the specified date is a leap year;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsLeapYear(this DateTime date) => DateTime.IsLeapYear(date.Year);

        /// <summary>
        /// Determines whether he specified date is a UTC date.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// <see langword="true"/> if the specified date is a UTC date;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsUtcDateTime(this DateTime date)
        {
            return date.Kind == DateTimeKind.Utc && TimeZoneInfo.Utc.GetUtcOffset(date) == UtcOffset;
        }

        /// <summary>
        /// Determines whether the specified date is a week day.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// <see langword="true"/> if the specified date is a week day;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsWeekday(this DateTime date) => !date.IsWeekend();

        /// <summary>
        /// Determines whether the specified date is a weekend.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// <see langword="true"/> if the specified date is a weekend;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsWeekend(this DateTime date) => date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

        /// <summary>
        /// Return a <see cref="DateTime"/> representing the previous day of the week.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="day">
        /// The <see cref="DayOfWeek"/> whose <see cref="DateTime"/>
        /// representation should be returned.
        /// </param>
        /// <returns>
        /// A <see cref="DateTime"/> representing the previous day of the week.
        /// </returns>
        public static DateTime Last(this DateTime date, DayOfWeek day)
        {
            var yesterday = date.Yesterday();
            var diff = (day - yesterday.DayOfWeek - 7) % 7;
            return yesterday.AddDays(diff);
        }

        /// <summary>
        /// Returns a <see cref="DateTime"/> representing the last day of the
        /// month for the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTime"/> representing the last day of the month for
        /// the date represented by this instance.
        /// </returns>
        public static DateTime LastDayOfMonth(this DateTime date) => new DateTime(date.Year, date.Month, date.DaysInMonth());

        /// <summary>
        /// Returns a <see cref="DateTime"/> which represents the last day of
        /// the quarter of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTime"/> which represents the last day of the
        /// quarter of the date represented by this instance.
        /// </returns>
        public static DateTime LastDayOfQuarter(this DateTime date) => date.FirstDayOfNextQuarter().AddDays(-1);

        /// <summary>
        /// Returns a <see cref="DateTime"/> which represents the last day of
        /// the week of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTime"/> which represents the last day of the week
        /// of the date represented by this instance.
        /// </returns>
        public static DateTime LastDayOfWeek(this DateTime date) => date.LastDayOfWeek(DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);

        /// <summary>
        /// Returns a <see cref="DateTime"/> which represents the last day of
        /// the week of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="firstDayOfWeek">
        /// An enumeration value that represents the first day of the week.
        /// </param>
        /// <returns>
        /// A <see cref="DateTime"/> which represents the last day of the week
        /// of the date represented by this instance.
        /// </returns>
        public static DateTime LastDayOfWeek(this DateTime date, DayOfWeek firstDayOfWeek) => date.FirstDayOfWeek(firstDayOfWeek).AddDays(6);

        /// <summary>
        /// Returns a <see cref="DateTime"/> representing the last day of the
        /// year for the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTime"/> representing the last day of the year for
        /// the date represented by this instance.
        /// </returns>
        public static DateTime LastDayOfYear(this DateTime date) => new DateTime(date.Year, 12, 31);

        /// <summary>
        /// Return a <see cref="DateTime"/> representing the next day of the week.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="day">
        /// The <see cref="DayOfWeek"/> whose <see cref="DateTime"/>
        /// representation should be returned.
        /// </param>
        /// <returns>
        /// A <see cref="DateTime"/> representing the next day of the week.
        /// </returns>
        public static DateTime Next(this DateTime date, DayOfWeek day)
        {
            var tomorrow = date.Tomorrow();
            var diff = (day - tomorrow.DayOfWeek + 7) % 7;
            return tomorrow.AddDays(diff <= 0 ? diff + 7 : diff);
        }

        /// <summary>
        /// Returns the quarter component of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// The quarter component of the date represented by this instance.
        /// </returns>
        public static int Quarter(this DateTime date) => ((date.Month - 1) / 3) + 1;

        /// <summary>
        /// Returns a <see cref="DateTime"/> equivalent to the specified serial date.
        /// </summary>
        /// <param name="serialDateValue">A serial date value.</param>
        /// <returns>
        /// A <see cref="DateTime"/> representing the same date and time as <paramref name="serialDateValue"/>.
        /// </returns>
        public static DateTime ToDateTime(this double serialDateValue)
        {
            if (serialDateValue.TryParseFromSerialDate(out var result))
            {
                return result;
            }

            throw new ArgumentOutOfRangeException(nameof(serialDateValue), Strings.ArgumentOutOfRange_DateTimeBadTicks);
        }

        /// <summary>
        /// Returns a <see cref="DateTime"/> representing the day after the date
        /// represented by this instance.
        /// </summary>
        /// <param name="value">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTime"/> representing the day after the date
        /// represented by this instance.
        /// </returns>
        public static DateTime Tomorrow(this DateTime value) => value.AddDays(1);

        /// <summary>
        /// Convert a <see cref="DateTime"/> object to a relative date (e.g.,
        /// Today, tomorrow, yesterday) string format.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> object to convert.</param>
        /// <returns>A relative date/time formatted string.</returns>
        public static string ToRelativeDateString(this DateTime value) => ToRelativeDateString(value, RelativeDateFormatting.DayNames);

        /// <summary>
        /// Convert a <see cref="DateTime"/> object to a relative date (e.g.,
        /// Today, tomorrow, yesterday) string format.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> object to convert.</param>
        /// <param name="options">
        /// One of the <see cref="RelativeDateFormatting"/> values.
        /// </param>
        /// <returns>A relative date/time formatted string.</returns>
        public static string ToRelativeDateString(this DateTime value, RelativeDateFormatting options)
        {
            var days = (value.Date - DateTime.Now.Date).Days;

            return days switch
            {
                0 => String.Format(CultureInfo.CurrentCulture, Strings.RelativeDateFormatStringToday, value),
                1 => Strings.RelativeDateFormatStringTomorrow,
                -1 => Strings.RelativeDateFormatStringYesterday,
                _ when days >= 2 && days <= 5 && options == RelativeDateFormatting.DayNames => value.ToString("dddd", CultureInfo.CurrentCulture),
                _ when days >= 2 && days <= 5 => String.Format(CultureInfo.CurrentCulture, Strings.RelativeDateFormatStringDaysFromNow, days),
                _ when days <= -2 && days >= -5 && options == RelativeDateFormatting.DayNames => value.ToString("dddd", CultureInfo.CurrentCulture),
                _ when days <= -2 && days >= -5 => String.Format(CultureInfo.CurrentCulture, Strings.RelativeDateFormatStringDaysAgo, Math.Abs(days)),
                _ => String.Format(CultureInfo.CurrentCulture, Strings.RelativeDateFormatStringDefault, value)
            };
        }

        /// <summary>
        /// Convert a <see cref="DateTime"/> object to a relative time (e.g.,
        /// now, 2 days ago, 3 days from now) string format.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> object to convert.</param>
        /// <returns>A relative date/time formatted string.</returns>
        public static string ToRelativeTimeString(this DateTime value) => ToRelativeTimeString(value, DateTime.Now);

        /// <summary>
        /// Convert a <see cref="DateTime"/> object to a relative time (e.g.,
        /// now, 2 days ago, 3 days from now) string format.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> object to convert.</param>
        /// <param name="baseDate">
        /// The <see cref="DateTime"/> object to use as the relative date.
        /// </param>
        /// <returns>A relative date/time formatted string.</returns>
        public static string ToRelativeTimeString(this DateTime value, DateTime baseDate)
        {
            var diff = baseDate - value;
            var delta = Math.Round(diff.TotalSeconds, 0);
            var format = "now";

            if (Math.Sign(delta) != 0)
            {
                var baseFormat = Strings.RelativeTimeFormatStringPast;

                if (delta < -0.1)
                {
                    baseFormat = Strings.RelativeTimeFormatStringFuture;
                    delta = -delta;
                    diff = -diff;
                }

                var months = Convert.ToInt32(Math.Floor(diff.Days / 30d));
                var years = Convert.ToInt32(Math.Floor(diff.Days / 365d));

                format = delta switch
                {
                    _ when delta < Constants.SecondsPerMinute && diff.Seconds == 1 => String.Format(CultureInfo.CurrentCulture, baseFormat, diff.Seconds, Strings.RelativeTimeFormatStringSecond),
                    _ when delta < Constants.SecondsPerMinute => String.Format(CultureInfo.CurrentCulture, baseFormat, diff.Seconds, Strings.RelativeTimeFormatStringSeconds),
                    _ when delta < Constants.SecondsPerMinute * 2 => String.Format(CultureInfo.CurrentCulture, baseFormat, diff.Minutes, Strings.RelativeTimeFormatStringMinute),
                    _ when delta < Constants.SecondsPerHour => String.Format(CultureInfo.CurrentCulture, baseFormat, diff.Minutes, Strings.RelativeTimeFormatStringMinutes),
                    _ when delta < Constants.SecondsPerHour * 2 => String.Format(CultureInfo.CurrentCulture, baseFormat, diff.Hours, Strings.RelativeTimeFormatStringHour),
                    _ when delta < Constants.SecondsPerDay => String.Format(CultureInfo.CurrentCulture, baseFormat, diff.Hours, Strings.RelativeTimeFormatStringHours),
                    _ when delta < Constants.SecondsPerDay * 2 => String.Format(CultureInfo.CurrentCulture, baseFormat, diff.Days, Strings.RelativeTimeFormatStringDay),
                    _ when delta < Constants.ApproximateSecondsPerMonth => String.Format(CultureInfo.CurrentCulture, baseFormat, diff.Days, Strings.RelativeTimeFormatStringDays),
                    _ when delta < Constants.ApproximateSecondsPerYear && months <= 1 => String.Format(CultureInfo.CurrentCulture, baseFormat, months, Strings.RelativeTimeFormatStringMonth),
                    _ when delta < Constants.ApproximateSecondsPerYear => String.Format(CultureInfo.CurrentCulture, baseFormat, months, Strings.RelativeTimeFormatStringMonths),
                    _ => String.Format(CultureInfo.CurrentCulture, baseFormat, years, years <= 1 ? Strings.RelativeTimeFormatStringYear : Strings.RelativeTimeFormatStringYears)
                };
            }

            return format;
        }

        /// <summary>
        /// Converts the specified floating-point number into its
        /// <see cref="DateTime"/> equivalent and returns a value that indicates
        /// whether the conversion succeeded.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="result">
        /// When this method returns, contains the <see cref="DateTime"/> value
        /// equivalent to the date and time contained in <paramref name="d"/>,
        /// if the conversion succeeded, or <see cref="DateTime.MinValue"/> if
        /// the conversion failed. The conversion fails if does not contain a
        /// valid floating-point representation of a date and time. This
        /// parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the s parameter was converted
        /// successfully; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The d parameter is a double-precision floating-point number that
        /// represents a date as the number of days before or after the base
        /// date, midnight, 30 December 1899. The sign and integral part of d
        /// encode the date as a positive or negative day displacement from 30
        /// December 1899, and the absolute value of the fractional part of d
        /// encodes the time of day as a fraction of a day displacement from
        /// midnight. d must be a value between negative 657435.0 through
        /// positive 2958465.99999999.
        /// </para>
        /// <para>
        /// Note that because of the way dates are encoded, there are two ways
        /// of representing any time of day on 30 December 1899. For example,
        /// -0.5 and 0.5 both mean noon on 30 December 1899 because a day
        /// displacement of plus or minus zero days from the base date is still
        /// the base date, and a half day displacement from midnight is noon.
        /// </para>
        /// </remarks>
        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public static bool TryParseFromSerialDate(this double d, out DateTime result)
        {
            var num = (long)((d * 86400000.0) + ((d >= 0.0) ? 0.5 : -0.5));
            if (num < 0L)
            {
                num -= (num % 0x5265c00L) * 2L;
            }

            num += 0x3680b5e1fc00L;
            num -= 62135596800000L;
            try
            {
                result = new DateTime(num);
                return true;
            }
            catch
            {
                result = DateTime.MinValue;
                return false;
            }
        }

        /// <summary>
        /// Returns a <see cref="DateTime"/> representing the day before the
        /// date represented by this instance.
        /// </summary>
        /// <param name="value">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTime"/> representing the day before the date
        /// represented by this instance.
        /// </returns>
        public static DateTime Yesterday(this DateTime value) => value.AddDays(-1);

        internal static int DaysBetween(DayOfWeek current, DayOfWeek firstDayOfWeek)
        {
            var days = current - firstDayOfWeek;
            return days < 0 ? days + 7 : days;
        }
    }
}