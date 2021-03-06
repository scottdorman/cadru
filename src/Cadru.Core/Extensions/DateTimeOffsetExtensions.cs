﻿//------------------------------------------------------------------------------
// <copyright file="DateTimeOffsetExtensions.cs"
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
    /// Provides basic routines for common DateTimeOffset manipulation.
    /// </summary>
    public static class DateTimeOffsetExtensions
    {
        private static readonly TimeSpan UtcOffset = new TimeSpan(0, 0, 0);

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> that adds the specified
        /// number of quarters to the value of this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <param name="value">
        /// A number of whole and fractional quarters. The
        /// <paramref name="value"/> parameter can be negative or positive.
        /// </param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> whose value is the sum of the date
        /// and time represented by this instance and the number of quarters
        /// represented by <paramref name="value"/>.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The resulting <see cref="DateTimeOffset"/> is less than
        /// <see cref="DateTimeOffset.MinValue"/> or greater than <see cref="DateTimeOffset.MaxValue"/>.
        /// </exception>
        public static DateTimeOffset AddQuarters(this DateTimeOffset date, double value) => date.AddMonths(checked((int)value * 3));

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> that adds the specified
        /// number of weekdays to the value of this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <param name="value">
        /// A number of whole and fractional weekdays. The
        /// <paramref name="value"/> parameter can be negative or positive.
        /// </param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> whose value is the sum of the date
        /// and time represented by this instance and the number of weekdays
        /// represented by <paramref name="value"/>.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The resulting <see cref="DateTimeOffset"/> is less than
        /// <see cref="DateTimeOffset.MinValue"/> or greater than <see cref="DateTimeOffset.MaxValue"/>.
        /// </exception>
        public static DateTimeOffset AddWeekdays(this DateTimeOffset date, double value)
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
        /// Returns a new <see cref="DateTimeOffset"/> that adds the specified
        /// number of weeks to the value of this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <param name="value">
        /// A number of whole and fractional weeks. The <paramref name="value"/>
        /// parameter can be negative or positive.
        /// </param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> whose value is the sum of the date
        /// and time represented by this instance and the number of weeks
        /// represented by <paramref name="value"/>.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The resulting <see cref="DateTimeOffset"/> is less than
        /// <see cref="DateTimeOffset.MinValue"/> or greater than <see cref="DateTimeOffset.MaxValue"/>.
        /// </exception>
        public static DateTimeOffset AddWeeks(this DateTimeOffset date, double value) => date.AddDays(value * 7);

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether the
        /// current <see cref="DateTimeOffset"/> instance is between the start
        /// and end indicated.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <param name="start">The starting <see cref="DateTimeOffset"/>.</param>
        /// <param name="end">The ending <see cref="DateTimeOffset"/>.</param>
        /// <returns>
        /// <see langword="true"/> if the current instance is between
        /// <paramref name="start"/> and <paramref name="end"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool Between(this DateTimeOffset date, DateTimeOffset start, DateTimeOffset end) => Between(date, start, end, true);

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether the
        /// current <see cref="DateTimeOffset"/> instance is between the start
        /// and end indicated.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <param name="start">The starting <see cref="DateTimeOffset"/>.</param>
        /// <param name="end">The ending <see cref="DateTimeOffset"/>.</param>
        /// <param name="includeTime">
        /// <see langword="true"/> to compare the time portion of the date;
        /// otherwise, <see langword="false"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the current instance is between
        /// <paramref name="start"/> and <paramref name="end"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool Between(this DateTimeOffset date, DateTimeOffset start, DateTimeOffset end, bool includeTime)
        {
            return includeTime ?
               date >= start && date <= end :
               date.Date >= start.Date && date.Date <= end.Date;
        }

        /// <summary>
        /// Returns the number of days in the month for the date represented by
        /// this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// The number of days in the month for the date represented by this instance.
        /// </returns>
        public static int DaysInMonth(this DateTimeOffset date) => DateTime.DaysInMonth(date.Year, date.Month);

        /// <summary>
        /// Returns the elapsed time between the date represented by this
        /// instance and the current date and time.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// A <see cref="TimeSpan"/> representing the elapsed time between the
        /// date represented by this instance and the current date and time.
        /// </returns>
        public static TimeSpan Elapsed(this DateTimeOffset date) => date.Elapsed(DateTimeOffset.Now);

        /// <summary>
        /// Returns the elapsed time between the date represented by this
        /// instance and the given date and time.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <param name="startDate">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// A <see cref="TimeSpan"/> representing the elapsed time between the
        /// date represented by this instance and the current date and time.
        /// </returns>
        public static TimeSpan Elapsed(this DateTimeOffset date, DateTimeOffset startDate) => startDate - date;

        /// <summary>
        /// Returns a <see cref="DateTimeOffset"/> representing the first day of
        /// the month for the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> representing the first day of the
        /// month for the date represented by this instance.
        /// </returns>
        public static DateTimeOffset FirstDayOfMonth(this DateTimeOffset date) => new DateTimeOffset(date.Year, date.Month, 1, date.Hour, date.Minute, date.Second, date.Offset);

        /// <summary>
        /// Returns a <see cref="DateTimeOffset"/> which represents the first
        /// day of the next quarter of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> which represents the first day of the
        /// next quarter of the date represented by this instance.
        /// </returns>
        public static DateTimeOffset FirstDayOfNextQuarter(this DateTimeOffset date) => date.FirstDayOfQuarter().AddMonths(3);

        /// <summary>
        /// Returns a <see cref="DateTimeOffset"/> which represents the first
        /// day of the quarter of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> which represents the first day of the
        /// quarter of the date represented by this instance.
        /// </returns>
        public static DateTimeOffset FirstDayOfQuarter(this DateTimeOffset date) => new DateTimeOffset(date.Year, ((date.Quarter() - 1) * 3) + 1, 1, date.Hour, date.Minute, date.Second, date.Offset);

        /// <summary>
        /// Returns a <see cref="DateTimeOffset"/> which represents the first
        /// day of the week of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> which represents the first day of the
        /// week of the date represented by this instance.
        /// </returns>
        public static DateTimeOffset FirstDayOfWeek(this DateTimeOffset date) => date.FirstDayOfWeek(DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);

        /// <summary>
        /// Returns a <see cref="DateTimeOffset"/> which represents the first
        /// day of the week of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <param name="startOfWeek">
        /// An enumeration value that represents the first day of the week.
        /// </param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> which represents the first day of the
        /// week of the date represented by this instance.
        /// </returns>
        public static DateTimeOffset FirstDayOfWeek(this DateTimeOffset date, DayOfWeek startOfWeek)
        {
            var diff = date.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return date.AddDays(-1 * diff);
        }

        /// <summary>
        /// Returns a <see cref="DateTimeOffset"/> representing the first day of
        /// the year for the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> representing the first day of the
        /// year for the date represented by this instance.
        /// </returns>
        public static DateTimeOffset FirstDayOfYear(this DateTimeOffset date) => new DateTimeOffset(date.Year, 1, 1, date.Hour, date.Minute, date.Second, date.Offset);

        /// <summary>
        /// Returns the culture-specific abbreviated name of the month
        /// represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// The culture-specific abbreviated name of the month represented by
        /// this instance.
        /// </returns>
        public static string GetAbbreviatedMonthName(this DateTimeOffset date) => DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(date.Month);

        /// <summary>
        /// Returns the culture-specific abbreviated names of the months.
        /// </summary>
        /// <returns>
        /// A list that contains the culture-specific abbreviated names of the months.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is an extension method.")]
        public static IEnumerable<string> GetAbbreviatedMonthNames() => CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames.Where(m => !String.IsNullOrEmpty(m));

        /// <summary>
        /// Returns a <see cref="DateTimeOffset"/> representing the day of the
        /// week from the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <param name="day">
        /// An enumeration value that represents the day of the week for which
        /// the date is to be calculated.
        /// </param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> representing the day of the week from
        /// the date represented by this instance.
        /// </returns>
        public static DateTimeOffset GetDayOfWeek(this DateTimeOffset date, DayOfWeek day) => date.GetDayOfWeek(day, DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);

        /// <summary>
        /// Returns a <see cref="DateTimeOffset"/> representing the day of the
        /// week from the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <param name="day">
        /// An enumeration value that represents the day of the week for which
        /// the date is to be calculated.
        /// </param>
        /// <param name="startOfWeek">
        /// An enumeration value that represents the first day of the week.
        /// </param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> representing the day of the week from
        /// the date represented by this instance.
        /// </returns>
        public static DateTimeOffset GetDayOfWeek(this DateTimeOffset date, DayOfWeek day, DayOfWeek startOfWeek)
        {
            var current = DateTimeExtensions.DaysBetween(date.DayOfWeek, startOfWeek);
            var resultday = DateTimeExtensions.DaysBetween(day, startOfWeek);
            return date.AddDays(resultday - current);
        }

        /// <summary>
        /// Returns the culture-specific name of the month represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// The culture-specific name of the month represented by this instance.
        /// </returns>
        public static string GetMonthName(this DateTimeOffset date) => DateTimeFormatInfo.CurrentInfo.GetMonthName(date.Month);

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
        /// DateTimeOffset value.
        /// </summary>
        /// <param name="time">A date and time value.</param>
        /// <returns>
        /// A positive integer that represents the week of the year that
        /// includes the date in the <paramref name="time"/> parameter.
        /// </returns>
        public static int GetWeekOfYear(this DateTimeOffset time) => GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

        /// <summary>
        /// Returns the week of the year that includes the date in the specified
        /// DateTimeOffset value.
        /// </summary>
        /// <param name="time">A date and time value.</param>
        /// <param name="rule">
        /// An enumeration value that defines a calendar week.
        /// </param>
        /// <returns>
        /// A positive integer that represents the week of the year that
        /// includes the date in the <paramref name="time"/> parameter.
        /// </returns>
        public static int GetWeekOfYear(this DateTimeOffset time, CalendarWeekRule rule) => GetWeekOfYear(time, rule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

        /// <summary>
        /// Returns the week of the year that includes the date in the specified
        /// DateTimeOffset value.
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
        public static int GetWeekOfYear(this DateTimeOffset time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek) => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(time.DateTime, rule, firstDayOfWeek);

        /// <summary>
        /// Determines whether the specified date is a leap day.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// <see langword="true"/> if the specified date is a leap day;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsLeapDay(this DateTimeOffset date) => CultureInfo.CurrentCulture.Calendar.IsLeapDay(date.Year, date.Month, date.Day);

        /// <summary>
        /// Determines whether the specified date is a leap month.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// <see langword="true"/> if the specified date is a leap month;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsLeapMonth(this DateTimeOffset date) => CultureInfo.CurrentCulture.Calendar.IsLeapMonth(date.Year, date.Month);

        /// <summary>
        /// Determines whether the specified date is a leap year.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// <see langword="true"/> if the specified date is a leap year;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsLeapYear(this DateTimeOffset date) => DateTime.IsLeapYear(date.Year);

        /// <summary>
        /// Determines whether he specified date is a UTC date.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// <see langword="true"/> if the specified date is a UTC date;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsUtcDateTime(this DateTimeOffset date)
        {
            return date.UtcDateTime == date.DateTime && date.Offset == UtcOffset && date.Offset == TimeZoneInfo.Utc.GetUtcOffset(date);
        }

        /// <summary>
        /// Determines whether the specified date is a week day.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// <see langword="true"/> if the specified date is a week day;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsWeekday(this DateTimeOffset date) => !date.IsWeekend();

        /// <summary>
        /// Determines whether the specified date is a weekend.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// <see langword="true"/> if the specified date is a weekend;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsWeekend(this DateTimeOffset date) => date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

        /// <summary>
        /// Return a <see cref="DateTimeOffset"/> representing the previous day
        /// of the week.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <param name="day">
        /// The <see cref="DayOfWeek"/> whose <see cref="DateTime"/>
        /// representation should be returned.
        /// </param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> representing the previous day of the week.
        /// </returns>
        public static DateTimeOffset Last(this DateTimeOffset date, DayOfWeek day)
        {
            var yesterday = date.Yesterday();
            var diff = (day - yesterday.DayOfWeek - 7) % 7;
            return yesterday.AddDays(diff);
        }

        /// <summary>
        /// Returns a <see cref="DateTimeOffset"/> representing the last day of
        /// the month for the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> representing the last day of the
        /// month for the date represented by this instance.
        /// </returns>
        public static DateTimeOffset LastDayOfMonth(this DateTimeOffset date) => new DateTimeOffset(date.Year, date.Month, date.DaysInMonth(), date.Hour, date.Minute, date.Second, date.Offset);

        /// <summary>
        /// Returns a <see cref="DateTimeOffset"/> which represents the last day
        /// of the quarter of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> which represents the last day of the
        /// quarter of the date represented by this instance.
        /// </returns>
        public static DateTimeOffset LastDayOfQuarter(this DateTimeOffset date) => date.FirstDayOfNextQuarter().AddDays(-1);

        /// <summary>
        /// Returns a <see cref="DateTimeOffset"/> which represents the last day
        /// of the week of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> which represents the last day of the
        /// week of the date represented by this instance.
        /// </returns>
        public static DateTimeOffset LastDayOfWeek(this DateTimeOffset date) => date.LastDayOfWeek(DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);

        /// <summary>
        /// Returns a <see cref="DateTimeOffset"/> which represents the last day
        /// of the week of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <param name="firstDayOfWeek">
        /// An enumeration value that represents the first day of the week.
        /// </param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> which represents the last day of the
        /// week of the date represented by this instance.
        /// </returns>
        public static DateTimeOffset LastDayOfWeek(this DateTimeOffset date, DayOfWeek firstDayOfWeek) => date.FirstDayOfWeek(firstDayOfWeek).AddDays(6);

        /// <summary>
        /// Returns a <see cref="DateTimeOffset"/> representing the last day of
        /// the year for the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> representing the last day of the year
        /// for the date represented by this instance.
        /// </returns>
        public static DateTimeOffset LastDayOfYear(this DateTimeOffset date) => new DateTimeOffset(date.Year, 12, 31, date.Hour, date.Minute, date.Second, date.Offset);

        /// <summary>
        /// Return a <see cref="DateTimeOffset"/> representing the next day of
        /// the week.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <param name="day">
        /// The <see cref="DayOfWeek"/> whose <see cref="DateTime"/>
        /// representation should be returned.
        /// </param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> representing the next day of the week.
        /// </returns>
        public static DateTimeOffset Next(this DateTimeOffset date, DayOfWeek day)
        {
            var tomorrow = date.Tomorrow();
            var diff = (day - tomorrow.DayOfWeek + 7) % 7;
            return tomorrow.AddDays(diff <= 0 ? diff + 7 : diff);
        }

        /// <summary>
        /// Returns the quarter component of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// The quarter component of the date represented by this instance.
        /// </returns>
        public static int Quarter(this DateTimeOffset date) => ((date.Month - 1) / 3) + 1;

        /// <summary>
        /// Returns a <see cref="DateTimeOffset"/> representing the day after
        /// the date represented by this instance.
        /// </summary>
        /// <param name="value">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> representing the day after the date
        /// represented by this instance.
        /// </returns>
        public static DateTimeOffset Tomorrow(this DateTimeOffset value) => value.AddDays(1);

        /// <summary>
        /// Convert a <see cref="DateTimeOffset"/> object to a relative date
        /// (e.g., Today, tomorrow, yesterday) string format.
        /// </summary>
        /// <param name="value">
        /// The <see cref="DateTimeOffset"/> object to convert.
        /// </param>
        /// <returns>A relative date/time formatted string.</returns>
        public static string ToRelativeDateString(this DateTimeOffset value) => ToRelativeDateString(value, RelativeDateFormatting.DayNames);

        /// <summary>
        /// Convert a <see cref="DateTimeOffset"/> object to a relative date
        /// (e.g., Today, tomorrow, yesterday) string format.
        /// </summary>
        /// <param name="value">
        /// The <see cref="DateTimeOffset"/> object to convert.
        /// </param>
        /// <param name="options">
        /// One of the <see cref="RelativeDateFormatting"/> values.
        /// </param>
        /// <returns>A relative date/time formatted string.</returns>
        public static string ToRelativeDateString(this DateTimeOffset value, RelativeDateFormatting options)
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
        /// Convert a <see cref="DateTimeOffset"/> object to a relative time
        /// (e.g., now, 2 days ago, 3 days from now) string format.
        /// </summary>
        /// <param name="value">
        /// The <see cref="DateTimeOffset"/> object to convert.
        /// </param>
        /// <returns>A relative date/time formatted string.</returns>
        public static string ToRelativeTimeString(this DateTimeOffset value) => ToRelativeTimeString(value, DateTimeOffset.Now);

        /// <summary>
        /// Convert a <see cref="DateTimeOffset"/> object to a relative time
        /// (e.g., now, 2 days ago, 3 days from now) string format.
        /// </summary>
        /// <param name="value">
        /// The <see cref="DateTimeOffset"/> object to convert.
        /// </param>
        /// <param name="baseDate">
        /// The <see cref="DateTimeOffset"/> object to use as the relative date.
        /// </param>
        /// <returns>A relative date/time formatted string.</returns>
        public static string ToRelativeTimeString(this DateTimeOffset value, DateTimeOffset baseDate)
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
        /// Returns a <see cref="DateTimeOffset"/> representing the day before
        /// the date represented by this instance.
        /// </summary>
        /// <param name="value">A valid <see cref="DateTimeOffset"/> instance.</param>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> representing the day before the date
        /// represented by this instance.
        /// </returns>
        public static DateTimeOffset Yesterday(this DateTimeOffset value) => value.AddDays(-1);
    }
}