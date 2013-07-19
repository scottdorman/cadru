// ---------------------------------------------------------------------------
// Campari Software
//
// IsoDateTimeParse.cs
//
// Encapsulate all of the parsing rules used by the IsoDateTime.Parse
// and IsoDateTime.TryParse methods.
//
//      Date Formats
//      Calendar Date Formats
//      The following complete, abbreviated or truncated formats are permissible: 
//
//      "19930214" or "1993-02-14" (complete representation) 
//      "1993-02" (reduced precision) 
//      "1993" 
//      "19" 
//      "930214" or "93-02-14" (truncated, current century assumed) 
//      "-9302" or "-93-02" 
//      "-93" 
//      "--0214" or "--02-14" 
//      "--02" 
//      "---14" 
//
//      Ordinal Date Formats
//      The day number within a given year can be expressed as: 
//
//      "1993045" or "1993-045" (complete representation) 
//      "93045" or "93-045" 
//      "-045" 
//
//      Week/Day Formats
//      Dates with a given week number may be expressed as: 
//
//      "1993W067" or "1993-W06-7" (complete representation) 
//      "1993W06" or "1993-W06" 
//      "93W067" or "93-W06-7" 
//      "93W06" or "93-W06" 
//      "-3W067" or "-3-W06-7" 
//      "-W067" or "-W06-7" 
//      "-W06" 
//      "-W-7" (day of current week) 
//      "---7" (day of any week) 
//
//		Time Formats
//      The following complete, abbreviated or turncated formats are permissible:
//      23:59:59 or 235959 (complete representation)
//      23:59 or 2359 (reduced precision)
//      23
//
//      23:59:59.9942 or 23.59.59.9942 (additional precision, time 5.8 ms before midnight
//
//      As every day both starts and ends with midnight, the two notations 00:00 and
//      24:00 are available to distinguish the two midnights that can be associated
//      with one date. This means that the following two notations refer to exactly the
//      same point in time:
//
//      1995-02-04T24:00 == 1995-02-05T00:00
//      In case an unambiguous representation of time is required, 00:00 is usually the
//      preferred notation for midnight and not 24:00. Digital clocks display 00:00 and
//      not 24:00.
//
//      Without any further additions, a date and time as written above is assumed to be
//      in some local time zone. In order to indicate that a time is measured in 
//      Universal Time (UTC), you can append a capital letter Z to a time as in
//
//      23:59:59Z or 2359Z
//
//      The strings
//
//		  +hh:mm, +hhmm, or +hh
//
//		  can be added to the time to indicate that the used local time zone is hh hours and
//      mm minutes ahead of UTC. For time zones west of the zero meridian, which are behind
//      UTC, the notation
//
//      -hh:mm, -hhmm, or -hh
//
//      is used instead. For example, Central European Time (CET) is +0100 and 
//      U.S./Canadian Eastern Standard Time (EST) is -0500. The following strings
//      all indicate the same point of time:
//
//		  12:00Z = 13:00+01:00 = 0700-0500
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
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

using Campari.Software;
using Campari.Software.Text;

namespace Campari.Software
{
    #region class IsoDateTimeParse
    internal static class IsoDateTimeParse
    {
        #region events

        #endregion

        #region class-wide fields
        internal const String GMTName = "GMT";
        internal const int MaxSecondsFractionDigits = 7;
        internal const string MonthDayPattern = "yyyy'-'n";
        internal const string SortableDateTimePattern = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
        internal const string StandardDatePattern = "yyyy'-'MM'-'dd";
        internal const string StandardTimePattern = "hh':'mm':'ss";
        internal const long TicksPerSecond = ((long)0x989680);
        internal const String ZuluName = "Z";

        internal static char[] allStandardFormats = new char[] { 'd', 'D', 'f', 'F', 'L', 's', 't', 'T', 'U', 'w', 'W', 'y', 'Y' };

        private static string[] fixedNumberFormats = new string[] { "0", "00", "000", "0000", "00000", "000000", "0000000" };

        #endregion

        #region private and internal properties and methods

        #region properties

        #endregion

        #region methods

        #region CheckNewValue
        /*=================================CheckNewValue==================================
        **Action: Check if currentValue is initialized.  If not, return the newValue.
        **        If yes, check if the current value is equal to newValue.  Return false
        **        if they are not equal.  This is used to check the case like "d" and "dd" are both
        **        used to format a string.
        **Returns: the correct value for currentValue.
        **Arguments:
        **Exceptions:
        ==============================================================================*/
        private static bool CheckNewValue(ref int currentValue, int newValue, char patternChar, ref IsoDateTimeResult result)
        {
            if (currentValue == -1)
            {
                currentValue = newValue;
                return (true);
            }
            else
            {
                if (newValue != currentValue)
                {
                    result.SetFailure(IsoDateTimeParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", patternChar);
                    return (false);
                }
            }
            return (true);
        }
        #endregion

        #region ExpandPredefinedFormat
        // Expand a pre-defined format string to the real format that we
        // are going to use in the date time parsing. This method also
        // converts the dateTime if necessary.
        private static string ExpandPredefinedFormat(string format, DateTimeFormatInfo dtfi, ref IsoDateTimeParsingInfo parseInfo, ref IsoDateTimeResult result)
        {
            switch (format[0])
            {
                case 'L':
                    dtfi = DateTimeFormatInfo.InvariantInfo;
                    break;

                case 'U':
                    parseInfo.calendar = new GregorianCalendar();
                    result.flags |= IsoDateTimeParseFlags.TimeZoneUsed;
                    result.timeZoneOffset = new TimeSpan(0);
                    result.flags |= IsoDateTimeParseFlags.TimeZoneUtc;
                    break;
            }
            if (dtfi.Calendar.GetType() != typeof(GregorianCalendar))
            {
                dtfi = (DateTimeFormatInfo)dtfi.Clone();
                dtfi.Calendar = new GregorianCalendar();
            }

            format = IsoDateTimeFormat.GetRealFormat(format);
            return format;
        }
        #endregion

        #region GetDateTimeParseException
        private static Exception GetDateTimeParseException(ref IsoDateTimeResult result)
        {
            switch (result.failure)
            {
                case IsoDateTimeParseFailureKind.ArgumentNull:
                    return new ArgumentNullException(result.failureArgumentName, Properties.Resources.ResourceManager.GetString(result.failureMessageID));
                case IsoDateTimeParseFailureKind.Format:
                    return new FormatException(Properties.Resources.ResourceManager.GetString(result.failureMessageID));
                case IsoDateTimeParseFailureKind.FormatWithParameter:
                    return new FormatException(String.Format(CultureInfo.CurrentUICulture, Properties.Resources.ResourceManager.GetString(result.failureMessageID), result.failureMessageFormatArgument));
                case IsoDateTimeParseFailureKind.FormatBadDateTimeCalendar:
                    return new FormatException(String.Format(CultureInfo.CurrentUICulture, Properties.Resources.ResourceManager.GetString(result.failureMessageID), result.calendar));
                default:
                    Debug.Assert(false, "Unkown DateTimeParseFailure: " + result);
                    return null;
            }
        }
        #endregion

        #region GetTimeZoneName
        //
        // Check the word at the current index to see if it matches GMT name or Zulu name.
        //
        private static bool GetTimeZoneName(ref DateTimeString str)
        {
            //
            //
            if (MatchWord(ref str, GMTName))
            {
                return (true);
            }

            if (MatchWord(ref str, ZuluName))
            {
                return (true);
            }

            return (false);
        }
        #endregion

        #region IsDigit
        internal static bool IsDigit(char ch)
        {
            return (ch >= '0' && ch <= '9');
        }
        #endregion

        #region MatchWord
        //
        // Search from the index of str at str.Index to see if the target string exists in the str.
        //
        private static bool MatchWord(ref DateTimeString str, String target)
        {
            int length = target.Length;
            if (length > (str.Value.Length - str.Index))
            {
                return false;
            }

            if (str.CompareInfo.Compare(str.Value, str.Index, length,
                                        target, 0, length, CompareOptions.IgnoreCase) != 0)
            {
                return (false);
            }

            int nextCharIndex = str.Index + target.Length;

            if (nextCharIndex < str.Value.Length)
            {
                char nextCh = str.Value[nextCharIndex];
                if (Char.IsLetter(nextCh))
                {
                    return (false);
                }
            }
            str.Index = nextCharIndex;
            if (str.Index < str.Length)
            {
                str.CurrentCharacter = str.Value[str.Index];
            }

            return (true);
        }
        #endregion

        #region Parse
        internal static IsoDateTime Parse(string s, DateTimeFormatInfo dtfi, DateTimeStyles styles)
        {
            IsoDateTimeResult result = new IsoDateTimeResult();       // The buffer to store the parsing result.
            result.Init();
            result.calendar = dtfi.Calendar;

            if (TryParse(s, dtfi, styles, ref result))
            {
                return result.parsedDate;
            }
            else
            {
                throw GetDateTimeParseException(ref result);
            }
        }
        #endregion

        #region ParseDigits
        internal static bool ParseDigits(ref DateTimeString str, int minDigitLen, int maxDigitLen, out int result)
        {
            result = 0;
            int startingIndex = str.Index;
            int tokenLength = 0;
            while (tokenLength < maxDigitLen)
            {
                if (!str.GetNextDigit())
                {
                    str.Index--;
                    break;
                }
                result = result * 10 + str.GetDigit();
                tokenLength++;
            }
            if (tokenLength < minDigitLen)
            {
                str.Index = startingIndex;
                return false;
            }
            return true;
        }
        #endregion

        #region ParseDigits
        /*=================================ParseDigits==================================
        **Action: Parse the number string in __DTString that are formatted using
        **        the following patterns:
        **        "0", "00", and "000..0"
        **Returns: the integer value
        **Arguments:    str: a __DTString.  The parsing will start from the
        **              next character after str.Index.
        **Exceptions: FormatException if error in parsing number.
        ==============================================================================*/
        internal static bool ParseDigits(ref DateTimeString str, int digitLen, out int result)
        {
            if (digitLen == 1)
            {
                // 1 really means 1 or 2 for this call
                return ParseDigits(ref str, 1, 2, out result);
            }
            else
            {
                return ParseDigits(ref str, digitLen, digitLen, out result);
            }
        }
        #endregion

        #region ParseExact
        internal static IsoDateTime ParseExact(string s, string format, DateTimeFormatInfo dtfi, DateTimeStyles styles)
        {
            IsoDateTimeResult result = new IsoDateTimeResult();       // The buffer to store the parsing result.
            result.Init();
            result.calendar = dtfi.Calendar;

            if (TryParseExact(s, format, dtfi, styles, ref result))
            {
                return result.parsedDate;
            }
            else
            {
                throw GetDateTimeParseException(ref result);
            }
        }
        #endregion

        #region ParseExactMultiple
        internal static IsoDateTime ParseExactMultiple(String s, String[] formats, DateTimeFormatInfo dtfi, DateTimeStyles styles)
        {
            IsoDateTimeResult result = new IsoDateTimeResult();       // The buffer to store the parsing result.
            result.Init();
            result.calendar = dtfi.Calendar;

            if (TryParseExactMultiple(s, formats, dtfi, styles, ref result))
            {
                return result.parsedDate;
            }
            else
            {
                throw GetDateTimeParseException(ref result);
            }
        }
        #endregion

        #region ParseFormat

        #region ParseFormat(ref DateTimeString str, ref DateTimeString format, ref IsoDateTimeParsingInfo parseInfo, DateTimeFormatInfo dtfi, ref IsoDateTimeResult result)
        private static bool ParseFormat(ref DateTimeString str, ref DateTimeString format, ref IsoDateTimeParsingInfo parseInfo, DateTimeFormatInfo dtfi, ref IsoDateTimeResult result)
        {
            int tokenLen = 0;
            int tempWeek = 0, tempDayOfYear = 0, tempYear = 0, tempMonth = 0, tempDay = 0, tempDayOfWeek = 0, tempHour = 0, tempMinute = 0, tempSecond = 0;
            double tempFraction = 0;

            char ch = format.GetChar();

            switch (ch)
            {
                case 'g': // era, not used.
                case 't': // time designator (AM/PM), not used.
                case 'K': // kind, not used.
                    break;

                case 'h':
                case 'H':
                    tokenLen = format.GetRepeatCount();
                    if (!ParseDigits(ref str, 2, out tempHour))
                    {
                        result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                        return (false);
                    }
                    if (!CheckNewValue(ref result.Hour, tempHour, ch, ref result))
                    {
                        return (false);
                    }
                    result.flags |= IsoDateTimeParseFlags.HaveHour;
                    break;

                case 's':
                    tokenLen = format.GetRepeatCount();
                    if (!ParseDigits(ref str, 2, out tempSecond))
                    {
                        result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                        return (false);
                    }
                    if (!CheckNewValue(ref result.Second, tempSecond, ch, ref result))
                    {
                        return (false);
                    }
                    result.flags |= IsoDateTimeParseFlags.HaveSecond;
                    break;

                case 'm':
                    tokenLen = format.GetRepeatCount();
                    if (!ParseDigits(ref str, 2, out tempMinute))
                    {
                        result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                        return (false);
                    }
                    if (!CheckNewValue(ref result.Minute, tempMinute, ch, ref result))
                    {
                        return (false);
                    }
                    result.flags |= IsoDateTimeParseFlags.HaveMinute;
                    break;

                case 'f':
                case 'F':
                    tokenLen = format.GetRepeatCount();
                    if (tokenLen <= IsoDateTimeFormat.MaxSecondsFractionDigits)
                    {
                        if (!ParseFractionExact(ref str, tokenLen, ref tempFraction))
                        {
                            if (ch == 'f')
                            {
                                result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                                return (false);
                            }
                        }
                        if (result.fraction < 0)
                        {
                            result.fraction = tempFraction;
                        }
                        else
                        {
                            if (tempFraction != result.fraction)
                            {
                                result.SetFailure(IsoDateTimeParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", ch);
                                return (false);
                            }
                        }
                    }
                    else
                    {
                        result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                        return (false);
                    }
                    break;

                case 'd':
                    tokenLen = format.GetRepeatCount();
                    if (!ParseDigits(ref str, 2, out tempDay))
                    {
                        result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                        return (false);
                    }
                    if (!CheckNewValue(ref result.Day, tempDay, ch, ref result))
                    {
                        return (false);
                    }
                    result.flags |= IsoDateTimeParseFlags.HaveDay;
                    break;

                case 'M':
                    tokenLen = format.GetRepeatCount();
                    if (!ParseDigits(ref str, 2, out tempMonth))
                    {
                        result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                        return (false);
                    }
                    if (!CheckNewValue(ref result.Month, tempMonth, ch, ref result))
                    {
                        return (false);
                    }
                    result.flags |= IsoDateTimeParseFlags.HaveMonth;
                    break;

                case 'y':
                    tokenLen = format.GetRepeatCount();
                    if (!ParseDigits(ref str, 4, out tempYear))
                    {
                        result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                        return (false);
                    }
                    if (!CheckNewValue(ref result.Year, tempYear, ch, ref result))
                    {
                        return (false);
                    }
                    result.flags |= IsoDateTimeParseFlags.HaveYear;
                    break;


                case 'D':
                    tokenLen = format.GetRepeatCount();
                    if (!ParseDigits(ref str, 1, out tempDayOfWeek))
                    {
                        result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                        return (false);
                    }
                    if (!CheckNewValue(ref result.DayOfWeek, tempDayOfWeek, ch, ref result))
                    {
                        return (false);
                    }
                    result.flags |= IsoDateTimeParseFlags.HaveDayOfWeek;
                    break;

                case 'n':
                    tokenLen = format.GetRepeatCount();
                    if (!ParseDigits(ref str, 3, out tempDayOfYear))
                    {
                        result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                        return (false);
                    }
                    if (!CheckNewValue(ref result.DayOfYear, tempDayOfYear, ch, ref result))
                    {
                        return (false);
                    }
                    result.flags |= IsoDateTimeParseFlags.HaveDayOfYear;
                    break;

                case 'w':
                    tokenLen = format.GetRepeatCount();
                    if (!ParseDigits(ref str, 2, out tempWeek))
                    {
                        result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                        return (false);
                    }
                    if (!CheckNewValue(ref result.Week, tempWeek, ch, ref result))
                    {
                        return (false);
                    }
                    result.flags |= IsoDateTimeParseFlags.HaveWeek;
                    break;

                case 'z':
                    // timezone offset
                    tokenLen = format.GetRepeatCount();
                    {
                        TimeSpan tempTimeZoneOffset = new TimeSpan(0);
                        if (!ParseTimeZoneOffset(ref str, tokenLen, ref tempTimeZoneOffset))
                        {
                            result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                            return (false);
                        }
                        if ((result.flags & IsoDateTimeParseFlags.TimeZoneUsed) != 0 && tempTimeZoneOffset != result.timeZoneOffset)
                        {
                            result.SetFailure(IsoDateTimeParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", 'z');
                            return (false);
                        }
                        result.timeZoneOffset = tempTimeZoneOffset;
                        result.flags |= IsoDateTimeParseFlags.TimeZoneUsed;
                    }
                    break;

                case 'Z':
                    if ((result.flags & IsoDateTimeParseFlags.TimeZoneUsed) != 0 && result.timeZoneOffset != TimeSpan.Zero)
                    {
                        result.SetFailure(IsoDateTimeParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", 'Z');
                        return (false);
                    }
                    result.flags |= IsoDateTimeParseFlags.TimeZoneUsed;
                    result.timeZoneOffset = new TimeSpan(0);
                    result.flags |= IsoDateTimeParseFlags.TimeZoneUtc;

                    // The updating of the indexes is to reflect that ParseExact MatchXXX methods assume that
                    // they need to increment the index and Parse GetXXX do not. Since we are calling a Parse
                    // method from inside ParseExact we need to adjust this. Long term, we should try to
                    // eliminate this discrepancy.
                    str.Index++;
                    if (!GetTimeZoneName(ref str))
                    {
                        result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                        return false;
                    }
                    str.Index--;
                    break;

                case ':':
                    if (!str.Match(ch))
                    {
                        // A time separator is expected.
                        result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                        return false;
                    }
                    break;

                case '-':
                    if (!str.Match(ch))
                    {
                        // A date separator is expected.
                        result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                        return false;
                    }
                    break;

                case '\"':
                case '\'':
                    StringBuilder enquotedString = new StringBuilder();
                    // Use ParseQuoteString so that we can handle escape characters within the quoted string.
                    if (!TryParseQuoteString(format.Value, format.Index, enquotedString, out tokenLen))
                    {
                        result.SetFailure(IsoDateTimeParseFailureKind.FormatWithParameter, "Format_BadQuote", ch);
                        return (false);
                    }
                    format.Index += tokenLen - 1;

                    // Some cultures uses space in the quoted string.  E.g. Spanish has long date format as:
                    // "dddd, dd' de 'MMMM' de 'yyyy".  When inner spaces flag is set, we should skip whitespaces if there is space
                    // in the quoted string.
                    String quotedStr = enquotedString.ToString();
                    for (int i = 0; i < quotedStr.Length; i++)
                    {
                        if (!str.Match(quotedStr[i]))
                        {
                            // Can not find the matching quoted string.
                            result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                            return false;
                        }
                    }
                    break;
                case '%':
                    // Skip this so we can get to the next pattern character.
                    // Used in case like "%d", "%y"

                    // Make sure the next character is not a '%' again.
                    if (format.Index >= format.Value.Length - 1 || format.Value[format.Index + 1] == '%')
                    {
                        result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadFormatSpecifier", null);
                        return false;
                    }
                    break;

                case '\\':
                    // Escape character. For example, "\d".
                    // Get the next character in format, and see if we can
                    // find a match in str.
                    if (format.GetNext())
                    {
                        if (!str.Match(format.GetChar()))
                        {
                            // Can not find a match for the escaped character.
                            result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                            return false;
                        }
                    }
                    else
                    {
                        result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadFormatSpecifier", null);
                        return false;
                    }
                    break;

                case '.':
                    if (!str.Match(ch))
                    {
                        if (format.GetNext())
                        {
                            // If we encounter the pattern ".F", and the dot is not present, it is an optional
                            // second fraction and we can skip this format.
                            if (format.Match('F'))
                            {
                                format.GetRepeatCount();
                                break;
                            }
                        }
                        result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                        return false;
                    }
                    break;

                default:
                    if (ch == ' ')
                    {
                        if (parseInfo.fAllowInnerWhite)
                        {
                            // Skip whitespaces if AllowInnerWhite.
                            // Do nothing here.
                        }
                        else
                        {
                            if (!str.Match(ch))
                            {
                                // If the space does not match, and trailing space is allowed, we do
                                // one more step to see if the next format character can lead to
                                // successful parsing.
                                // This is used to deal with special case that a empty string can match
                                // a specific pattern.
                                // The example here is af-ZA, which has a time format like "hh:mm:ss tt".  However,
                                // its AM symbol is "" (empty string).  If fAllowTrailingWhite is used, and time is in
                                // the AM, we will trim the whitespaces at the end, which will lead to a failure
                                // when we are trying to match the space before "tt".
                                if (parseInfo.fAllowTrailingWhite)
                                {
                                    if (format.GetNext())
                                    {
                                        if (ParseFormat(ref str, ref format, ref parseInfo, dtfi, ref result))
                                        {
                                            return (true);
                                        }
                                    }
                                }
                                result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                                return false;
                            }
                            // Found a macth.
                        }
                    }
                    else
                    {
                        if (format.MatchSpecifiedWord(GMTName))
                        {
                            format.Index += (GMTName.Length - 1);
                            // Found GMT string in format.  This means the IsoDateTime string
                            // is in GMT timezone.
                            result.flags |= IsoDateTimeParseFlags.TimeZoneUsed;
                            result.timeZoneOffset = TimeSpan.Zero;
                            if (!str.Match(GMTName))
                            {
                                result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                                return false;
                            }
                        }
                        else if (!str.Match(ch))
                        {
                            // ch is expected.
                            result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                            return false;
                        }
                    }
                    break;
            } // switch
            return (true);
        }
        #endregion

        #region ParseFormat(string str, string formatParam, DateTimeFormatInfo dtfi, DateTimeStyles styles, ref IsoDateTimeResult result)
        private static bool ParseFormat(string str, string formatParam, DateTimeFormatInfo dtfi, DateTimeStyles styles, ref IsoDateTimeResult result)
        {
            // since we don't have a format, assume it is one of the predefined formats           
            if (String.IsNullOrEmpty(formatParam))
            {
                formatParam = "F";
            }

            IsoDateTimeParsingInfo parseInfo = new IsoDateTimeParsingInfo();
            parseInfo.Init();

            parseInfo.calendar = dtfi.Calendar;
            parseInfo.fAllowInnerWhite = ((styles & DateTimeStyles.AllowInnerWhite) != 0);
            parseInfo.fAllowTrailingWhite = ((styles & DateTimeStyles.AllowTrailingWhite) != 0);

            if (formatParam.Length == 1)
            {
                formatParam = IsoDateTimeParse.ExpandPredefinedFormat(formatParam, dtfi, ref parseInfo, ref result);
            }

            result.calendar = parseInfo.calendar;

            // Reset these values to negative one so that we could throw exception
            // if we have parsed every item twice.
            result.Hour = result.Minute = result.Second = -1;

            DateTimeString format = new DateTimeString(formatParam);
            DateTimeString dateTime = new DateTimeString(str);

            if (parseInfo.fAllowTrailingWhite)
            {
                // Trim trailing spaces if AllowTrailingWhite.
                format.TrimTail();
                format.RemoveTrailingInQuoteSpaces();
                dateTime.TrimTail();
            }

            if ((styles & DateTimeStyles.AllowLeadingWhite) != 0)
            {
                format.SkipWhiteSpaces();
                format.RemoveLeadingInQuoteSpaces();
                dateTime.SkipWhiteSpaces();
            }

            //
            // Scan every character in format and match the pattern in str.
            //
            while (format.GetNext())
            {
                // We trim inner spaces here, so that we will not eat trailing spaces when
                // AllowTrailingWhite is not used.
                if (parseInfo.fAllowInnerWhite)
                {
                    dateTime.SkipWhiteSpaces();
                }
                if (!ParseFormat(ref dateTime, ref format, ref parseInfo, dtfi, ref result))
                {
                    return (false);
                }
            }

            if (dateTime.Index < dateTime.Value.Length - 1)
            {
                // There are still remaining character in str.
                result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                return false;
            }
            result.SetDate(styles);
            return true;
        }
        #endregion

        #endregion

        #region ParseFractionExact
        /*=================================ParseFractionExact==================================
        **Action: Parse the number string in __DTString that are formatted using
        **        the following patterns:
        **        "0", "00", and "000..0"
        **Returns: the fraction value
        **Arguments:    str: a __DTString.  The parsing will start from the
        **              next character after str.Index.
        **Exceptions: FormatException if error in parsing number.
        ==============================================================================*/
        private static bool ParseFractionExact(ref DateTimeString str, int maxDigitLen, ref double result)
        {
            if (!str.GetNextDigit())
            {
                str.Index--;
                return false;
            }
            result = str.GetDigit();

            int digitLen = 1;
            for (; digitLen < maxDigitLen; digitLen++)
            {
                if (!str.GetNextDigit())
                {
                    str.Index--;
                    break;
                }
                result = result * 10 + str.GetDigit();
            }

            result = ((double)result / Math.Pow(10, digitLen));
            return (digitLen == maxDigitLen);
        }
        #endregion

        #region ParseSign
        /*=================================ParseSign==================================
        **Action: Parse a positive or a negative sign.
        **Returns:      true if postive sign.  flase if negative sign.
        **Arguments:    str: a __DTString.  The parsing will start from the
        **              next character after str.Index.
        **Exceptions:   FormatException if end of string is encountered or a sign
        **              symbol is not found.
        ==============================================================================*/
        private static bool ParseSign(ref DateTimeString str, ref bool result)
        {
            if (!str.GetNext())
            {
                // A sign symbol ('+' or '-') is expected. However, end of string is encountered.
                return false;
            }
            char ch = str.GetChar();
            if (ch == '+')
            {
                result = true;
                return (true);
            }
            else if (ch == '-')
            {
                result = false;
                return (true);
            }
            // A sign symbol ('+' or '-') is expected.
            return false;
        }
        #endregion

        #region ParseTimeZoneOffset
        /*=================================ParseTimeZoneOffset==================================
        **Action: Parse the string formatted using "z", "zz", "zzz" in IsoDateTime.Format().
        **Returns: the TimeSpan for the parsed timezone offset.
        **Arguments:    str: a __DTString.  The parsing will start from the
        **              next character after str.Index.
        **              len: the repeated number of the "z"
        **Exceptions: FormatException if errors in parsing.
        ==============================================================================*/
        private static bool ParseTimeZoneOffset(ref DateTimeString str, int len, ref TimeSpan result)
        {
            bool isPositive = true;
            int hourOffset;
            int minuteOffset = 0;

            switch (len)
            {
                case 1:
                case 2:
                    if (!ParseSign(ref str, ref isPositive))
                    {
                        return (false);
                    }
                    if (!ParseDigits(ref str, len, out hourOffset))
                    {
                        return (false);
                    }
                    break;
                default:
                    if (!ParseSign(ref str, ref isPositive))
                    {
                        return (false);
                    }

                    // Parsing 1 digit will actually parse 1 or 2.
                    if (!ParseDigits(ref str, 1, out hourOffset))
                    {
                        return (false);
                    }
                    // ':' is optional.
                    if (str.Match(":"))
                    {
                        // Found ':'
                        if (!ParseDigits(ref str, 2, out minuteOffset))
                        {
                            return (false);
                        }
                    }
                    else
                    {
                        // Since we can not match ':', put the char back.
                        str.Index--;
                        if (!ParseDigits(ref str, 2, out minuteOffset))
                        {
                            return (false);
                        }
                    }
                    break;
            }
            if (minuteOffset < 0 || minuteOffset >= 60)
            {
                return false;
            }
            result = (new TimeSpan(hourOffset, minuteOffset, 0));
            if (!isPositive)
            {
                result = result.Negate();
            }
            return (true);
        }
        #endregion

        #region TryParse

        #region TryParse(string s, DateTimeFormatInfo dtfi, DateTimeStyles styles, ref IsoDateTimeResult result)
        internal static bool TryParse(string s, DateTimeFormatInfo dtfi, DateTimeStyles styles, ref IsoDateTimeResult result)
        {
            if (s == null)
            {
                result.SetFailure(IsoDateTimeParseFailureKind.ArgumentNull, "ArgumentNull_String", null, "s");
                return false;
            }
            if (s.Length == 0)
            {
                result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                return false;
            }

            // TODO: Implement a full state machine to handle parsing non standard format strings.
            // HACK: We parse based on the standard formats only.
            // Do a loop through the standard formats and see if we can parse succesfully in
            // one of the formats.
            //
            string[] formats = IsoDateTimeFormat.GetAllDateTimeFormats();
            for (int i = 0; i < formats.Length; i++)
            {
                if (formats[i] == null || formats[i].Length == 0)
                {
                    result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadFormatSpecifier", null);
                    return false;
                }
                if (TryParseExact(s, formats[i], dtfi, styles, out result.parsedDate))
                {
                    return (true);
                }
            }
            result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
            return (false);
        }
        #endregion

        #region TryParse(string s, IFormatProvider provider, DateTimeStyles styles, out IsoDateTime result)
        internal static bool TryParse(string s, DateTimeFormatInfo dtfi, DateTimeStyles styles, out IsoDateTime result)
        {
            result = IsoDateTime.MinValue;
            IsoDateTimeResult resultData = new IsoDateTimeResult();       // The buffer to store the parsing result.
            resultData.Init();
            resultData.calendar = dtfi.Calendar;

            if (TryParse(s, dtfi, styles, ref resultData))
            {
                result = resultData.parsedDate;
                return true;
            }
            return false;
        }
        #endregion

        #endregion

        #region TryParseExact

        #region TryParseExact(string s, string format, DateTimeFormatInfo dtfi, DateTimeStyles styles, out IsoDateTime result)
        internal static bool TryParseExact(string s, string format, DateTimeFormatInfo dtfi, DateTimeStyles styles, out IsoDateTime result)
        {
            result = IsoDateTime.MinValue;
            IsoDateTimeResult resultData = new IsoDateTimeResult();       // The buffer to store the parsing result.
            resultData.Init();
            resultData.calendar = dtfi.Calendar;

            if (TryParseExact(s, format, dtfi, styles, ref resultData))
            {
                result = resultData.parsedDate;
                return true;
            }
            return false;
        }
        #endregion

        #region TryParseExact(string s, string formatString, DateTimeFormatInfo dtfi, DateTimeStyles styles, ref IsoDateTimeResult result)
        internal static bool TryParseExact(string s, string format, DateTimeFormatInfo dtfi, DateTimeStyles styles, ref IsoDateTimeResult result)
        {
            if (s == null)
            {
                result.SetFailure(IsoDateTimeParseFailureKind.ArgumentNull, "ArgumentNull_String", null, "s");
                return false;
            }
            if (s.Length == 0)
            {
                result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                return false;
            }
            if (format.Length == 0)
            {
                result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadFormatSpecifier", null);
                return false;
            }

            bool parseResult = ParseFormat(s, format, dtfi, styles, ref result);
            return parseResult;
        }
        #endregion

        #endregion

        #region TryParseExactMultiple

        #region TryParseExactMultiple(String s, String[] formats, DateTimeFormatInfo dtfi, DateTimeStyles styles, out IsoDateTime result)
        internal static bool TryParseExactMultiple(String s, String[] formats, DateTimeFormatInfo dtfi, DateTimeStyles styles, out IsoDateTime result)
        {
            result = IsoDateTime.MinValue;
            IsoDateTimeResult resultData = new IsoDateTimeResult();       // The buffer to store the parsing result.
            resultData.Init();
            resultData.calendar = dtfi.Calendar;

            if (TryParseExactMultiple(s, formats, dtfi, styles, ref resultData))
            {
                result = resultData.parsedDate;
                return true;
            }
            return false;
        }
        #endregion

        #region TryParseExactMultiple(String s, String[] formats, DateTimeFormatInfo dtfi, DateTimeStyles styles, ref DateTimeResult result)
        internal static bool TryParseExactMultiple(String s, String[] formats, DateTimeFormatInfo dtfi, DateTimeStyles styles, ref IsoDateTimeResult result)
        {
            if (s == null)
            {
                result.SetFailure(IsoDateTimeParseFailureKind.ArgumentNull, "ArgumentNull_String", null, "s");
                return false;
            }
            if (formats == null)
            {
                result.SetFailure(IsoDateTimeParseFailureKind.ArgumentNull, "ArgumentNull_String", null, "formats");
                return false;
            }

            if (s.Length == 0)
            {
                result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
                return false;
            }

            if (formats.Length == 0)
            {
                result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadFormatSpecifier", null);
                return false;
            }

            Debug.Assert(dtfi != null, "dtfi == null");

            //
            // Do a loop through the provided formats and see if we can parse succesfully in
            // one of the formats.
            //
            for (int i = 0; i < formats.Length; i++)
            {
                if (formats[i] == null || formats[i].Length == 0)
                {
                    result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadFormatSpecifier", null);
                    return false;
                }
                if (TryParseExact(s, formats[i], dtfi, styles, out result.parsedDate))
                {
                    return (true);
                }
            }
            result.SetFailure(IsoDateTimeParseFailureKind.Format, "Format_BadDateTime", null);
            return (false);
        }
        #endregion

        #endregion

        #region TryParseQuoteString
        //
        // The pos should point to a quote character. This method will
        // get the string encloed by the quote character.
        //
        internal static bool TryParseQuoteString(String format, int pos, StringBuilder result, out int returnValue)
        {
            //
            // NOTE : pos will be the index of the quote character in the 'format' string.
            //
            returnValue = 0;
            int formatLen = format.Length;
            int beginPos = pos;
            char quoteChar = format[pos++]; // Get the character used to quote the following string.

            bool foundQuote = false;
            while (pos < formatLen)
            {
                char ch = format[pos++];
                if (ch == quoteChar)
                {
                    foundQuote = true;
                    break;
                }
                else if (ch == '\\')
                {
                    // The following are used to support escaped character.
                    // Escaped character is also supported in the quoted string.
                    // Therefore, someone can use a format like "'minute:' mm\"" to display:
                    //  minute: 45"
                    // because the second double quote is escaped.
                    if (pos < formatLen)
                    {
                        result.Append(format[pos++]);
                    }
                    else
                    {
                        //
                        // This means that '\' is at the end of the formatting string.
                        //
                        return false;
                    }
                }
                else
                {
                    result.Append(ch);
                }
            }

            if (!foundQuote)
            {
                // Here we can't find the matching quote.
                return false;
            }

            //
            // Return the character count including the begin/end quote characters and enclosed string.
            //
            returnValue = (pos - beginPos);
            return true;
        }
        #endregion

        #endregion

        #endregion

        #region public properties and methods

        #region properties

        #endregion

        #region methods

        #endregion

        #endregion
    }
    #endregion
}
