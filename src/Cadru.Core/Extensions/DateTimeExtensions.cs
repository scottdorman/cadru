//------------------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs"
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

namespace Cadru.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Cadru.Properties;
    using Cadru.Text;
    /// <summary>
    /// Provides basic routines for common DateTime manipulation.
    /// </summary>
    public static class DateTimeExtensions
    {
        #region fields
        private static readonly TimeSpan UtcOffset = new TimeSpan(0, 0, 0);
        #endregion

        #region constructors
        #endregion

        #region events
        #endregion

        #region properties
        #endregion

        #region methods

        #region AddWeekdays
        /// <summary>
        /// Returns a new <see cref="DateTime"/> that adds the specified number of
        /// weekdays to the value of this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="value">A number of whole and fractional weekdays.
        /// The <paramref name="value"/> parameter can be negative or positive.</param>
        /// <returns>A <see cref="DateTime"/> whose value is the sum of the
        /// date and time represented by this instance and the number of weekdays
        /// represented by <paramref name="value"/>.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The resulting <see cref="DateTime"/> is less than
        /// <see cref="DateTime.MinValue"/> or greater than
        /// <see cref="DateTime.MaxValue"/>.
        /// </exception>
        public static DateTime AddWeekdays(this DateTime date, double value)
        {
            int direction = value < 0 ? -1 : 1;

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
        #endregion

        #region AddQuarters
        /// <summary>
        /// Returns a new <see cref="DateTime"/> that adds the specified number of
        /// quarters to the value of this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="value">A number of whole and fractional quarters.
        /// The <paramref name="value"/> parameter can be negative or positive.</param>
        /// <returns>A <see cref="DateTime"/> whose value is the sum of the
        /// date and time represented by this instance and the number of quarters
        /// represented by <paramref name="value"/>.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The resulting <see cref="DateTime"/> is less than
        /// <see cref="DateTime.MinValue"/> or greater than
        /// <see cref="DateTime.MaxValue"/>.
        /// </exception>
        public static DateTime AddQuarters(this DateTime date, double value)
        {
            return date.AddMonths(checked((int)value * 3));
        }
        #endregion

        #region AddWeeks
        /// <summary>
        /// Returns a new <see cref="DateTime"/> that adds the specified number of
        /// weeks to the value of this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="value">A number of whole and fractional weeks.
        /// The <paramref name="value"/> parameter can be negative or positive.</param>
        /// <returns>A <see cref="DateTime"/> whose value is the sum of the
        /// date and time represented by this instance and the number of weeks
        /// represented by <paramref name="value"/>.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The resulting <see cref="DateTime"/> is less than
        /// <see cref="DateTime.MinValue"/> or greater than
        /// <see cref="DateTime.MaxValue"/>.
        /// </exception>
        public static DateTime AddWeeks(this DateTime date, double value)
        {
            return date.AddDays(value * 7);
        }
        #endregion

        #region DaysInMonth
        /// <summary>
        /// Returns the number of days in the month for the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>The number of days in the month for the date represented by this instance.</returns>
        public static int DaysInMonth(this DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month);
        }
        #endregion

        #region Elapsed
        /// <summary>
        /// Returns the elapsed time between the date represented by this instance
        /// and the current date and time.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>A <see cref="TimeSpan"/> representing the elapsed
        /// time between the date represented by this instance and the
        /// current date and time.</returns>
        public static TimeSpan Elapsed(this DateTime date)
        {
            return DateTime.Now - date;
        }
        #endregion

        #region FirstDayOfMonth
        /// <summary>
        /// Returns a <see cref="DateTime"/> representing the
        /// first day of the month for the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>A <see cref="DateTime"/> representing the
        /// first day of the month for the date represented by this instance.</returns>
        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            var firstDate = new DateTime(date.Year, date.Month, 1);
            return firstDate;
        }
        #endregion

        #region FirstDayOfNextQuarter
        /// <summary>
        /// Returns a <see cref="DateTime"/> which represents the
        /// first day of the next quarter of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>A <see cref="DateTime"/> which represents the
        /// first day of the next quarter of the date represented by this instance.</returns>
        public static DateTime FirstDayOfNextQuarter(this DateTime date)
        {
            return date.FirstDayOfQuarter().AddMonths(3);
        }
        #endregion

        #region FirstDayOfQuarter
        /// <summary>
        /// Returns a <see cref="DateTime"/> which represents the
        /// first day of the quarter of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>A <see cref="DateTime"/> which represents the
        /// first day of the quarter of the date represented by this instance.</returns>
        public static DateTime FirstDayOfQuarter(this DateTime date)
        {
            return new DateTime(date.Year, ((date.Quarter() - 1) * 3) + 1, 1);
        }
        #endregion

        #region FirstDayOfWeek
        /// <summary>
        /// Returns a <see cref="DateTime"/> which represents the
        /// first day of the week of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>A <see cref="DateTime"/> which represents the
        /// first day of the week of the date represented by this instance.</returns>
        public static DateTime FirstDayOfWeek(this DateTime date)
        {
            return date.FirstDayOfWeek(DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
        }

        /// <summary>
        /// Returns a <see cref="DateTime"/> which represents the
        /// first day of the week of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="startOfWeek">An enumeration value that represents the first day of the week.</param>
        /// <returns>A <see cref="DateTime"/> which represents the
        /// first day of the week of the date represented by this instance.</returns>
        public static DateTime FirstDayOfWeek(this DateTime date, DayOfWeek startOfWeek)
        {
            int diff = date.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return date.AddDays(-1 * diff).Date;
        }
        #endregion

        #region FirstDayOfYear
        /// <summary>
        /// Returns a <see cref="DateTime"/> representing the
        /// first day of the year for the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>A <see cref="DateTime"/> representing the
        /// first day of the year for the date represented by this instance.</returns>
        public static DateTime FirstDayOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }
        #endregion

        #region GetAbbreviatedMonthName
        /// <summary>
        /// Returns the culture-specific abbreviated name of the month represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>The culture-specific abbreviated name of the month represented by this instance.</returns>
        public static string GetAbbreviatedMonthName(this DateTime date)
        {
            return DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(date.Month);
        }
        #endregion

        #region GetAbbreviatedMonthNames
        /// <summary>
        /// Returns the culture-specific abbreviated names of the months.
        /// </summary>
        /// <returns>A list that contains the culture-specific abbreviated names of the months.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is an extension method.")]
        public static IList<string> GetAbbreviatedMonthNames()
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames.Where(m => !string.IsNullOrEmpty(m)).ToList();
        }
        #endregion

        #region GetDayOfWeek
        /// <summary>
        /// Returns a <see cref="DateTime"/> representing the
        /// day of the week from the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="day">An enumeration value that represents the day of
        /// the week for which the date is to be calculated.</param>
        /// <returns>A <see cref="DateTime"/> representing the
        /// day of the week from the date represented by this instance.</returns>
        public static DateTime GetDayOfWeek(this DateTime date, DayOfWeek day)
        {
            return date.GetDayOfWeek(day, DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
        }

        /// <summary>
        /// Returns a <see cref="DateTime"/> representing the
        /// day of the week from the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="day">An enumeration value that represents the day of
        /// the week for which the date is to be calculated.</param>
        /// <param name="startOfWeek">An enumeration value that represents the first day of the week.</param>
        /// <returns>A <see cref="DateTime"/> representing the
        /// day of the week from the date represented by this instance.</returns>
        public static DateTime GetDayOfWeek(this DateTime date, DayOfWeek day, DayOfWeek startOfWeek)
        {
            int current = DaysBetween(date.DayOfWeek, startOfWeek);
            int resultday = DaysBetween(day, startOfWeek);
            return date.AddDays(resultday - current);
        }
        #endregion

        #region GetMonthName
        /// <summary>
        /// Returns the culture-specific name of the month represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>The culture-specific name of the month represented by this instance.</returns>
        public static string GetMonthName(this DateTime date)
        {
            return DateTimeFormatInfo.CurrentInfo.GetMonthName(date.Month);
        }
        #endregion

        #region GetMonthNames
        /// <summary>
        /// Returns the culture-specific names of the months.
        /// </summary>
        /// <returns>A list that contains the culture-specific names of the months.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is an extension method.")]
        public static IList<string> GetMonthNames()
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.Where(m => !string.IsNullOrEmpty(m)).ToList();
        }
        #endregion

        #region GetMonthNumber
        /// <summary>
        /// Returns the month number for the given month name.
        /// </summary>
        /// <param name="name">The month name.</param>
        /// <param name="abbreviated"><see langword="true"/> if the name is abbreviated;
        /// otherwise, <see langword="false"/>.</param>
        /// <returns>The month number for the given month name.</returns>
        public static int GetMonthNumber(string name, bool abbreviated)
        {
            var months = abbreviated ? GetAbbreviatedMonthNames() : GetMonthNames();
            return months.IndexOf(name) + 1;
        }
        #endregion

        #region GetWeekOfYear

        #region GetWeekOfYear(this DateTime time)
        /// <summary>
        /// Returns the week of the year that includes the date in the specified DateTime value.
        /// </summary>
        /// <param name="time">A date and time value.</param>
        /// <returns>A positive integer that represents the week of the year
        /// that includes the date in the <paramref name="time"/> parameter.</returns>
        public static int GetWeekOfYear(this DateTime time)
        {
            return GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }
        #endregion

        #region GetWeekOfYear(this DateTime time, CalendarWeekRule rule)
        /// <summary>
        /// Returns the week of the year that includes the date in the specified DateTime value.
        /// </summary>
        /// <param name="time">A date and time value.</param>
        /// <param name="rule">An enumeration value that defines a calendar week.</param>
        /// <returns>A positive integer that represents the week of the year
        /// that includes the date in the <paramref name="time"/> parameter.</returns>
        public static int GetWeekOfYear(this DateTime time, CalendarWeekRule rule)
        {
            return GetWeekOfYear(time, rule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }
        #endregion

        #region GetWeekOfYear(this DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
        /// <summary>
        /// Returns the week of the year that includes the date in the specified DateTime value.
        /// </summary>
        /// <param name="time">A date and time value.</param>
        /// <param name="rule">An enumeration value that defines a calendar week.</param>
        /// <param name="firstDayOfWeek">An enumeration value that represents the first day of the week.</param>
        /// <returns>A positive integer that represents the week of the year
        /// that includes the date in the <paramref name="time"/> parameter.</returns>
        public static int GetWeekOfYear(this DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
        {
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(time, rule, firstDayOfWeek);
        }
        #endregion

        #endregion

        #region IsLeapYear
        /// <summary>
        /// Determines whether the specified date is a leap year.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns><see langword="true"/> if the specified date is a leap year;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool IsLeapYear(this DateTime date)
        {
            return DateTime.IsLeapYear(date.Year);
        }
        #endregion

        #region IsLeapMonth
        /// <summary>
        /// Determines whether the specified date is a leap month.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns><see langword="true"/> if the specified date is a leap month;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool IsLeapMonth(this DateTime date)
        {
            return CultureInfo.CurrentCulture.Calendar.IsLeapMonth(date.Year, date.Month);
        }
        #endregion

        #region IsLeapDay
        /// <summary>
        /// Determines whether the specified date is a leap day.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns><see langword="true"/> if the specified date is a leap day;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool IsLeapDay(this DateTime date)
        {
            return CultureInfo.CurrentCulture.Calendar.IsLeapDay(date.Year, date.Month, date.Day);
        }
        #endregion

        #region IsWeekday
        /// <summary>
        /// Determines whether the specified date is a week day.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns><see langword="true"/> if the specified date is a week day;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool IsWeekday(this DateTime date)
        {
            return !date.IsWeekend();
        }
        #endregion

        #region IsWeekend
        /// <summary>
        /// Determines whether the specified date is a weekend.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns><see langword="true"/> if the specified date is a weekend;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }
        #endregion

        #region IsUtcDateTime
        /// <summary>
        /// Determines whether he specified date is a UTC date.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns><see langword="true"/> if the specified date is a UTC date;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool IsUtcDateTime(this DateTime date)
        {
            return date.Kind == DateTimeKind.Utc && TimeZoneInfo.Utc.GetUtcOffset(date) == UtcOffset;
        }
        #endregion

        #region Last
        /// <summary>
        /// Return a <see cref="DateTime"/> representing the previous day of
        /// the week.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="day">The <see cref="DayOfWeek"/> whose <see cref="DateTime"/>
        /// representation should be returned.</param>
        /// <returns>A <see cref="DateTime"/> representing the
        /// previous day of the week.</returns>
        public static DateTime Last(this DateTime date, DayOfWeek day)
        {
            var yesterday = date.Yesterday();
            var diff = (day - yesterday.DayOfWeek - 7) % 7;
            return yesterday.AddDays(diff);
        }
        #endregion

        #region LastDayOfMonth
        /// <summary>
        /// Returns a <see cref="DateTime"/> representing the
        /// last day of the month for the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>A <see cref="DateTime"/> representing the
        /// last day of the month for the date represented by this instance.</returns>
        public static DateTime LastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.DaysInMonth());
        }
        #endregion

        #region LastDayOfQuarter
        /// <summary>
        /// Returns a <see cref="DateTime"/> which represents the
        /// last day of the quarter of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>A <see cref="DateTime"/> which represents the
        /// last day of the quarter of the date represented by this instance.</returns>
        public static DateTime LastDayOfQuarter(this DateTime date)
        {
            return date.FirstDayOfNextQuarter().AddDays(-1);
        }
        #endregion

        #region LastDayOfWeek
        /// <summary>
        /// Returns a <see cref="DateTime"/> which represents the
        /// last day of the week of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>A <see cref="DateTime"/> which represents the
        /// last day of the week of the date represented by this instance.</returns>
        public static DateTime LastDayOfWeek(this DateTime date)
        {
            return date.LastDayOfWeek(DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
        }

        /// <summary>
        /// Returns a <see cref="DateTime"/> which represents the
        /// last day of the week of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="firstDayOfWeek">An enumeration value that represents the first day of the week.</param>
        /// <returns>A <see cref="DateTime"/> which represents the
        /// last day of the week of the date represented by this instance.</returns>
        public static DateTime LastDayOfWeek(this DateTime date, DayOfWeek firstDayOfWeek)
        {
            return date.FirstDayOfWeek(firstDayOfWeek).AddDays(6);
        }
        #endregion

        #region LastDayOfYear
        /// <summary>
        /// Returns a <see cref="DateTime"/> representing the
        /// last day of the year for the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>A <see cref="DateTime"/> representing the
        /// last day of the year for the date represented by this instance.</returns>
        public static DateTime LastDayOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 12, 31);
        }
        #endregion

        #region Next
        /// <summary>
        /// Return a <see cref="DateTime"/> representing the next day of
        /// the week.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <param name="day">The <see cref="DayOfWeek"/> whose <see cref="DateTime"/>
        /// representation should be returned.</param>
        /// <returns>A <see cref="DateTime"/> representing the
        /// next day of the week.</returns>
        public static DateTime Next(this DateTime date, DayOfWeek day)
        {
            var tomorrow = date.Tomorrow();
            var diff = (day - tomorrow.DayOfWeek + 7) % 7;
            return tomorrow.AddDays(diff <= 0 ? diff + 7 : diff);
        }
        #endregion

        #region Quarter
        /// <summary>
        /// Returns the quarter component of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>The quarter component of the date represented by this instance.</returns>
        public static int Quarter(this DateTime date)
        {
            return ((date.Month - 1) / 3) + 1;
        }
        #endregion

        #region Tomorrow
        /// <summary>
        /// Returns a <see cref="DateTime"/> representing the day after
        /// the date represented by this instance.
        /// </summary>
        /// <param name="value">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>A <see cref="DateTime"/> representing the day after
        /// the date represented by this instance.</returns>
        public static DateTime Tomorrow(this DateTime value)
        {
            return value.AddDays(1);
        }
        #endregion

        #region ToRelativeDateString

        #region ToRelativeDateString(this DateTime value)
        /// <summary>
        /// Convert a <see cref="DateTime"/> object to a relative date
        /// (e.g., Today, tomorrow, yesterday) string format.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> object to convert.</param>
        /// <returns>A relative date/time formatted string.</returns>
        public static string ToRelativeDateString(this DateTime value)
        {
            return ToRelativeDateString(value, RelativeDateFormattingOptions.DayNames);
        }
        #endregion

        #region ToRelativeDateString(this DateTime value, RelativeDateFormattingOptions options)
        /// <summary>
        /// Convert a <see cref="DateTime"/> object to a relative date
        /// (e.g., Today, tomorrow, yesterday) string format.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> object to convert.</param>
        /// <param name="options">One of the <see cref="RelativeDateFormattingOptions"/> values.</param>
        /// <returns>A relative date/time formatted string.</returns>
        public static string ToRelativeDateString(this DateTime value, RelativeDateFormattingOptions options)
        {
            var diff = value.Date - DateTime.Now.Date;
            var days = diff.Days;
            string format;

            switch (days)
            {
                case 0:
                    format = String.Format(Resources.RelativeDateFormatStringToday, value);
                    break;

                case 1:
                    format = Resources.RelativeDateFormatStringTomorrow;
                    break;

                case -1:
                    format = Resources.RelativeDateFormatStringYesterday;
                    break;

                case 2:
                case 3:
                case 4:
                case 5:
                    format = options == RelativeDateFormattingOptions.DayNames ? value.ToString("dddd") : String.Format(Resources.RelativeDateFormatStringDaysFromNow, days);
                    break;

                case -2:
                case -3:
                case -4:
                case -5:
                    format = options == RelativeDateFormattingOptions.DayNames ? value.ToString("dddd") : String.Format(Resources.RelativeDateFormatStringDaysAgo, Math.Abs(days));
                    break;

                default:
                    format = String.Format(Resources.RelativeDateFormatStringDefault, value);
                    break;
            }

            return format;
        }
        #endregion

        #endregion

        #region Yesterday
        /// <summary>
        /// Returns a <see cref="DateTime"/> representing the day before
        /// the date represented by this instance.
        /// </summary>
        /// <param name="value">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>A <see cref="DateTime"/> representing the day before
        /// the date represented by this instance.</returns>
        public static DateTime Yesterday(this DateTime value)
        {
            return value.AddDays(-1);
        }
        #endregion

        #region DaysBetween
        private static int DaysBetween(DayOfWeek current, DayOfWeek firstDayOfWeek)
        {
            int days = current - firstDayOfWeek;
            return days < 0 ? days + 7 : days;
        }
        #endregion

        #endregion
    }
}
