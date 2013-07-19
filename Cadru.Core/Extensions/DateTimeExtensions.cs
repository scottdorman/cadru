//------------------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" 
//  company="Scott Dorman" 
//  library="Cadru">
//    Copyright (C) 2001-2013 Scott Dorman.
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
    using System.Globalization;
    using Cadru.Properties;

    /// <summary>
    /// Provides basic routines for common DateTime manipulation.
    /// </summary>
    public static class DateTimeExtensions
    {
        #region Quarter
        /// <summary>
        /// Gets the quarter component of the date represented by this instance.
        /// </summary>
        /// <param name="date">A valid <see cref="DateTime"/> instance.</param>
        /// <returns>The quarter component of the date represented by this instance.</returns>
        public static int Quarter(this DateTime date)
        {
            return ((date.Month - 1) / 3) + 1;
        }
        #endregion

        #region FirstDayOfQuarter
        /// <summary>
        /// Gets a <see cref="DateTime"/> which represents the 
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

        #region LastDayOfQuarter
        /// <summary>
        /// Gets a <see cref="DateTime"/> which represents the 
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

        #region FirstDayOfNextQuarter
        /// <summary>
        /// Gets a <see cref="DateTime"/> which represents the 
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
            int num = 0;

            if (firstDayOfWeek < DayOfWeek.Sunday || firstDayOfWeek > DayOfWeek.Saturday)
            {
                throw new ArgumentOutOfRangeException("firstDayOfWeek", String.Format(CultureInfo.CurrentUICulture, Resources.ArgumentOutOfRange_Range, DayOfWeek.Sunday, DayOfWeek.Saturday));
            }

            switch (rule)
            {
                case CalendarWeekRule.FirstDay:
                    num = GetFirstDayWeekOfYear(time, (int)firstDayOfWeek);
                    break;

                case CalendarWeekRule.FirstFullWeek:
                    num = GetWeekOfYearFullDays(time, (int)firstDayOfWeek, 7);
                    break;

                case CalendarWeekRule.FirstFourDayWeek:
                    num = GetWeekOfYearFullDays(time, (int)firstDayOfWeek, 4);
                    break;

                default:
                    throw new ArgumentOutOfRangeException("rule", String.Format(CultureInfo.CurrentUICulture, Resources.ArgumentOutOfRange_Range, CalendarWeekRule.FirstDay, CalendarWeekRule.FirstFourDayWeek));
            }

            return num;
        }
        #endregion

        #endregion

        #region GetFirstDayWeekOfYear
        internal static int GetFirstDayWeekOfYear(DateTime time, int firstDayOfWeek)
        {
            int dayOfYear = time.DayOfYear - 1;
            int dayOfWeek = ((int)time.DayOfWeek - dayOfYear) % 7;
            int num = (dayOfWeek - firstDayOfWeek + 14) % 7;
            return ((dayOfYear + num) / 7) + 1;
        }
        #endregion

        #region GetWeekOfYearFullDays
        internal static int GetWeekOfYearFullDays(DateTime time, int firstDayOfWeek, int fullDays)
        {
            int dayOfYear = time.DayOfYear - 1;
            int dayOfWeek = ((int)time.DayOfWeek - dayOfYear) % 7;
            int num = (firstDayOfWeek - dayOfWeek + 14) % 7;

            if (num != 0 && num >= fullDays)
            {
                num = num - 7;
            }

            int num1 = dayOfYear - num;
            if (num1 >= 0)
            {
                return (num1 / 7) + 1;
            }

            return GetWeekOfYearFullDays(time.AddDays((double)(-(dayOfYear + 1))), firstDayOfWeek, fullDays);
        }
        #endregion
    }
}
