// ---------------------------------------------------------------------------
// Campari Software
//
// IsoDateTimeResult.cs
//
//
// ---------------------------------------------------------------------------
// Copyright (C) 2006 Campari Software
// All rights reserved.
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
// FITNESS FOR A PARTICULAR PURPOSE.
// ---------------------------------------------------------------------------
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace Campari.Software
{
    #region struct IsoDateTimeResult
    [StructLayout(LayoutKind.Sequential)]
    internal struct IsoDateTimeResult
    {
        #region fields
        internal int Year;
        internal int Month;
        internal int Day;

        internal int Week;
        internal int DayOfYear;
        internal int DayOfWeek;

        internal int Hour;
        internal int Minute;
        internal int Second;
        internal double fraction;

        internal int era;

        internal IsoDateTimeParseFlags flags;
        internal TimeSpan timeZoneOffset;
        internal Calendar calendar;
        internal IsoDateTime parsedDate;
        internal IsoDateTimeParseFailureKind failure;
        internal string failureMessageID;
        internal object failureMessageFormatArgument;
        internal string failureArgumentName;
        #endregion

        #region properties
        #endregion

        #region methods

        #region Init
        internal void Init()
        {
            this.Year = -1;
            this.Month = -1;
            this.Day = -1;
            this.fraction = -1;
            this.era = -1;

            this.Week = -1;
            this.DayOfWeek = -1;
            this.DayOfYear = -1;
            this.Hour = -1;
            this.Minute = -1;
            this.Second = -1;
        }
        #endregion

        #region SetCalendarDate
        internal void SetCalendarDate(ref Calendar cal, DateTimeStyles styles)
        {
            if ((this.Year == -1) || (this.Month == -1) || (this.Day == -1))
            {
                /*
                The following table describes the behaviors of getting the default value
                when a certain year/month/day values are missing.
        
                An "X" means that the value exists.  And "--" means that value is missing.
        
                Year    Month   Day =>  ResultYear  ResultMonth     ResultDay       Note
        
                X       X       X       Parsed year Parsed month    Parsed day
                X       X       --      Parsed Year Parsed month    First day       If we have year and month, assume the first day of that month.
                X       --      X       Parsed year First month     Parsed day      If the month is missing, assume first month of that year.
                X       --      --      Parsed year First month     First day       If we have only the year, assume the first day of that year.
        
                --      X       X       CurrentYear Parsed month    Parsed day      If the year is missing, assume the current year.
                --      X       --      CurrentYear Parsed month    First day       If we have only a month value, assume the current year and current day.
                --      --      X       CurrentYear First month     Parsed day      If we have only a day value, assume current year and first month.
                --      --      --      CurrentYear Current month   Current day     So this means that if the date string only contains time, you will get current date.
                */

                DateTime now = DateTime.Now;
                if (this.Month == -1 && this.Day == -1)
                {
                    if (this.Year == -1)
                    {
                        if ((styles & DateTimeStyles.NoCurrentDateDefault) != 0)
                        {
                            // If there is no year/month/day values, and NoCurrentDateDefault flag is used,
                            // set the year/month/day value to the beginning year/month/day of DateTime().
                            // Note we should be using Gregorian for the year/month/day.
                            cal = new GregorianCalendar();
                            this.Year = this.Month = this.Day = 1;
                        }
                        else
                        {
                            // Year/Month/Day are all missing.
                            this.Year = cal.GetYear(now);
                            this.Month = cal.GetMonth(now);
                            this.Day = cal.GetDayOfMonth(now);
                        }
                    }
                    else
                    {
                        // Month/Day are both missing.
                        this.Month = 1;
                        this.Day = 1;
                    }
                }
                else
                {
                    if (this.Year == -1)
                    {
                        this.Year = cal.GetYear(now);
                    }
                    if (this.Month == -1)
                    {
                        this.Month = 1;
                    }
                    if (this.Day == -1)
                    {
                        this.Day = 1;
                    }
                }
            }

            // Set Hour/Minute/Second to zero if these value are not in str.
            if (this.Hour == -1)
            {
                this.Hour = 0;
            }

            if (this.Minute == -1)
            {
                this.Minute = 0;
            }

            if (this.Second == -1)
            {
                this.Second = 0;
            }

            if (this.era == -1)
            {
                this.era = Calendar.CurrentEra;
            }

            parsedDate = new IsoDateTime(
                                        this.Year,
                                        this.Month,
                                        this.Day,
                                        this.Hour,
                                        this.Minute,
                                        this.Second,
                                        0,
                                        this.timeZoneOffset);

        }
        #endregion

        #region SetDate
        internal void SetDate(DateTimeStyles styles)
        {
            // We return early after setting the date to ensure
            // we don't get any cross-over of the flags.

            // First, test the full representations (or none).
            // We are testing this way because the test will return true
            // only if that flag is set and all others are unset.
            if ((this.flags == IsoDateTimeParseFlags.HaveCalendarDate) || (this.flags == IsoDateTimeParseFlags.HaveCalendarDateTime))
            {
                SetCalendarDate(ref calendar, styles);
                return;
            }

            if ((this.flags == IsoDateTimeParseFlags.HaveWeekDate) || (this.flags == IsoDateTimeParseFlags.HaveWeekDateTime))
            {
                SetWeekDate(ref calendar, styles);
                return;
            }

            if ((this.flags == IsoDateTimeParseFlags.HaveOrdinalDate) || (this.flags == IsoDateTimeParseFlags.HaveOrdinalDateTime))
            {
                SetOrdinalDate(ref calendar, styles);
                return;
            }

            // These are the special cases where we are missing everything, have only the time, or only have a year.
            if (this.flags == IsoDateTimeParseFlags.None)
            {
                SetCalendarDate(ref calendar, styles);
                return;
            }

            if ((this.flags == IsoDateTimeParseFlags.HaveTime) ||
                (this.flags == (IsoDateTimeParseFlags.HaveHour | IsoDateTimeParseFlags.HaveMinute)) ||
                (this.flags == IsoDateTimeParseFlags.HaveHour) ||
                (this.flags == IsoDateTimeParseFlags.HaveMinute) ||
                (this.flags == IsoDateTimeParseFlags.HaveSecond))
            {
                SetCalendarDate(ref calendar, styles);
                return;
            }

            if (this.flags == IsoDateTimeParseFlags.HaveYear)
            {
                SetCalendarDate(ref calendar, styles);
                return;
            }

            // We don't have a full representation (and we're not a special case), so we need
            // to figure out which combination of flags we have.
            if ((this.flags & IsoDateTimeParseFlags.HaveYear) == IsoDateTimeParseFlags.HaveYear)
            {
                // We have a year, so drill down to figure out what date type we have.
                // At this point, we could have a calendar, ordinal, or week date.

                // Calendar date?
                if (((this.flags & IsoDateTimeParseFlags.HaveMonth) == IsoDateTimeParseFlags.HaveMonth) ||
                    ((this.flags & IsoDateTimeParseFlags.HaveDay) == IsoDateTimeParseFlags.HaveDay))
                {
                    // We have a month and/or a day, so we know that we are dealing with a calendar date.
                    // It could be just a date or it could have a date and time
                    SetCalendarDate(ref calendar, styles);
                    return;
                }

                // Ordinal date?
                if (((this.flags & IsoDateTimeParseFlags.HaveDayOfYear) == IsoDateTimeParseFlags.HaveDayOfYear))
                {
                    // we have a day of year, so we know that we are dealing with an ordinal date.
                    // It could be just a date or it could have a date and time
                    SetOrdinalDate(ref calendar, styles);
                    return;
                }

                // Week date?
                if (((this.flags & IsoDateTimeParseFlags.HaveWeek) == IsoDateTimeParseFlags.HaveWeek) ||
                    ((this.flags & IsoDateTimeParseFlags.HaveDayOfWeek) == IsoDateTimeParseFlags.HaveDayOfWeek))
                {
                    // We have a week and/or a day of week, so we know that we are dealinng with a week date.
                    // It could be just a date or it could have a date and time.
                    SetWeekDate(ref calendar, styles);
                    return;
                }
            }
            else
            {
                // We don't have year, but we could still have a calendar, ordinal, or week date.

                // Calendar date?
                if (((this.flags & IsoDateTimeParseFlags.HaveMonth) == IsoDateTimeParseFlags.HaveMonth) ||
                    ((this.flags & IsoDateTimeParseFlags.HaveDay) == IsoDateTimeParseFlags.HaveDay))
                {
                    // We have a month and/or a day, so we know that we are dealing with a calendar date.
                    // It could be just a date or it could have a date and time
                    SetCalendarDate(ref calendar, styles);
                    return;
                }

                // Ordinal date?
                if (((this.flags & IsoDateTimeParseFlags.HaveDayOfYear) == IsoDateTimeParseFlags.HaveDayOfYear))
                {
                    // we have a day of year, so we know that we are dealing with an ordinal date.
                    // It could be just a date or it could have a date and time
                    SetOrdinalDate(ref calendar, styles);
                    return;
                }

                // Week date?
                if (((this.flags & IsoDateTimeParseFlags.HaveWeek) == IsoDateTimeParseFlags.HaveWeek) ||
                    ((this.flags & IsoDateTimeParseFlags.HaveDayOfWeek) == IsoDateTimeParseFlags.HaveDayOfWeek))
                {
                    // We have a week and/or a day of week, so we know that we are dealinng with a week date.
                    // It could be just a date or it could have a date and time.
                    SetWeekDate(ref calendar, styles);
                    return;
                }
            }

            // If we make it here, then we weren't able to parse the date properly. 
            SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
        }
        #endregion

        #region SetFailure

        #region SetFailure(IsoDateTimeParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
        internal void SetFailure(IsoDateTimeParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
        {
            this.failure = failure;
            this.failureMessageID = failureMessageID;
            this.failureMessageFormatArgument = failureMessageFormatArgument;
        }
        #endregion

        #region SetFailure(IsoDateTimeParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument, string failureArgumentName)
        internal void SetFailure(IsoDateTimeParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument, string failureArgumentName)
        {
            this.failure = failure;
            this.failureMessageID = failureMessageID;
            this.failureMessageFormatArgument = failureMessageFormatArgument;
            this.failureArgumentName = failureArgumentName;
        }
        #endregion

        #endregion

        #region SetOrdinalDate
        private void SetOrdinalDate(ref Calendar cal, DateTimeStyles styles)
        {
            /*
			The following table describes the behaviors of getting the default value
			when a certain year/day of year values are missing.
			
			An "X" means that the value exists.  And "--" means that value is missing.
			
           	Year	DayOfYear	=>	ResultYear	    ResultDayOfYear
            X	    X		        Parsed year     Parsed day of year
            --		X		        Current year	Parsed day of year
            */

            DateTime now = DateTime.Now;

            if ((this.Year == -1) || (this.DayOfYear == -1))
            {
                if (this.Year == -1)
                {
                    if ((styles & DateTimeStyles.NoCurrentDateDefault) != 0)
                    {
                        cal = new GregorianCalendar();
                        this.Year = 1;
                    }
                    else
                    {
                        this.Year = cal.GetYear(now);
                    }
                }
                if (this.DayOfYear == -1)
                {
                    if ((styles & DateTimeStyles.NoCurrentDateDefault) != 0)
                    {
                        cal = new GregorianCalendar();
                        this.DayOfYear = 1;
                    }
                    else
                    {
                        this.DayOfYear = cal.GetDayOfYear(now);
                    }
                }
            }

            // Set Hour/Minute/Second to zero if these value are not in str.
            if (this.Hour == -1)
            {
                this.Hour = 0;
            }

            if (this.Minute == -1)
            {
                this.Minute = 0;
            }

            if (this.Second == -1)
            {
                this.Second = 0;
            }

            if (this.era == -1)
            {
                this.era = Calendar.CurrentEra;
            }

            parsedDate = new IsoDateTime(
                                     this.Year,
                                     this.DayOfYear,
                                     this.Hour,
                                     this.Minute,
                                     this.Second,
                                     0,
                                     this.timeZoneOffset);
        }
        #endregion

        #region SetWeekDate
        private void SetWeekDate(ref Calendar cal, DateTimeStyles styles)
        {
            /*
			The following table describes the behaviors of getting the default value
			when a certain year/day of year values are missing.
			
			An "X" means that the value exists.  And "--" means that value is missing.

            Year	Week	DayOfWeek	=>	ResultYear	        ResultWeek	    ResultDayOfWeek
            X		X		X	    		Parsed year	        Parsed week     Parsed day of week
            X		X		--	    		Parsed year			Parsed week	    First day of week
            X		--		X	    		Parsed year			First week	    Parsed day of week
            --		X		X	    		Current year		Parsed week	    Parsed day of week
            --		--		X				Current year		First week		Parsed day of week
            */

            DateTime now = DateTime.Now;
            if ((this.Year == -1) || (this.Week == -1) || (this.DayOfWeek == -1))
            {
                if (this.Week == -1 && this.DayOfWeek == -1)
                {
                    if (this.Year == -1)
                    {
                        if ((styles & DateTimeStyles.NoCurrentDateDefault) != 0)
                        {
                            cal = new GregorianCalendar();
                            this.Year = 1;
                        }
                        else
                        {
                            this.Year = cal.GetYear(now);
                            this.Week = cal.GetWeekOfYear(now, CalendarWeekRule.FirstFourDayWeek, System.DayOfWeek.Monday);
                            this.DayOfWeek = ((now.DayOfWeek == System.DayOfWeek.Sunday) ? 7 : (int)now.DayOfWeek);
                        }
                    }
                    else
                    {
                        this.Week = 1;
                        this.DayOfWeek = 1;
                    }
                }
                else
                {
                    if (this.Year == -1)
                    {
                        this.Year = cal.GetYear(now);
                    }
                    if (this.Week == -1)
                    {
                        this.Week = 1;
                    }
                    if (this.DayOfWeek == -1)
                    {
                        this.DayOfWeek = 1;
                    }
                }
            }
            // Set Hour/Minute/Second to zero if these value are not in str.
            if (this.Hour == -1)
            {
                this.Hour = 0;
            }

            if (this.Minute == -1)
            {
                this.Minute = 0;
            }

            if (this.Second == -1)
            {
                this.Second = 0;
            }

            if (this.era == -1)
            {
                this.era = Calendar.CurrentEra;
            }

            parsedDate = new IsoDateTime(
                                     this.Year,
                                     this.Week,
                                     ((DayOfWeek)((this.DayOfWeek == 7) ? 0 : this.DayOfWeek)),
                                     this.Hour,
                                     this.Minute,
                                     this.Second,
                                     0,
                                     this.timeZoneOffset);
        }
        #endregion

        #endregion
    }
    #endregion
}
