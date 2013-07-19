// ---------------------------------------------------------------------------
// Campari Software
//
// IsoDateTimeFormat.cs
//
// Encapsulate all of the formatting rules used by the IsoDateTime.ToString
// methods.
//
// Customized format patterns:
// (Format in the table below is the internal number format used to display the pattern.)
//
//     Patterns   	Format      Description                           Example
//     =========  	==========  ===================================== ========
//        "h*"     	"00"        hour (24-hour clock)with leading zero 08
//
//        "H*"     	"00"        hour (24-hour clock)with leading zero 08
//
//        "m*"     	"00"        minute with leading zero
//
//        "s*"     	"00"        second with leading zero
//
//        "f"     	"0"         second fraction (1 digit)
//        "ff"    	"00"        second fraction (2 digit)
//        "fff"   	"000"       second fraction (3 digit)
//        "ffff"  	"0000"      second fraction (4 digit)
//        "fffff" 	"00000"     second fraction (5 digit)
//        "ffffff"  "000000"    second fraction (6 digit)
//        "fffffff"	"0000000"   second fraction (7 digit)
//
//        "F"     	"0"         second fraction (up to 1 digit)
//        "FF"    	"00"        second fraction (up to 2 digit)
//        "FFF"   	"000"       second fraction (up to 3 digit)
//        "FFFF"  	"0000"      second fraction (up to 4 digit)
//        "FFFFF" 	"00000"     second fraction (up to 5 digit)
//        "FFFFFF"  "000000"    second fraction (up to 6 digit)
//        "FFFFFFF" "0000000"   second fraction (up to 7 digit)
//
//        "d*"     	"00"        day with leading zero                 01
//
//        "M*"     	"00"        month with leading zero               02
//       
//        "y"     	"0"         two digit year (year % 100) w/o leading zero           0
//        "yy"    	"00"        two digit year (year % 100) with leading zero          00
//        "yyy"   	"D3"        year                                  2000
//        "yyyy"  	"D4"        year                                  2000
//        "yyyyy" 	"D5"        year                                  2000
//        ...
//
//        "w*"      "00"        week of year with leading zero        06
//
//        "n*"      "000"       day of year with leading zero         045
//
//        "D*"      "0"         day of week
//
//        "z"     	"+00;-00"   timezone offset with leading zero     -08
//        "zz"    	"+00;-00"   timezone offset with leading zero     -08
//        "zzz"   	"+00;-00" 	for hour offset, "00" for minute offset   full timezone offset   -08:00
//        "zzz*"  	"+00;-00" 	for hour offset, "00" for minute offset   full timezone offset   -08:00
//        
//        ":"                 	time separator                        :
//        "/"                 	date separator                        /
//        "'"                 	quoted string                         'ABC' will insert ABC into the formatted string.
//        '"'                 	quoted string                         "ABC" will insert ABC into the formatted string.
//        "%"                 	used to quote a single pattern characters      E.g.The format character "%y" is to print two digit year.
//        "\"                 	escaped character                     E.g. '\d' insert the character 'd' into the format string.
//        other characters    	insert the character into the format string. 
//
// Pre-defined format characters: 
//    
//        Format              Description                             Real format                             Example
//        =========           =================================       ======================                  =======================
//        "d"                 ordinal date                            "yyyy'-'nnn"                            1993-045
//        "D"                 long data                               "yyyy'-'MM'-'dd"                        1993-02-14
//        "f"                 full date (long date + short time)      "yyyy'-'MM'-'dd' 'HH':'mm"              1993-02-14 18:10
//        "F"                 full date (long date + long time)       "yyyy'-'MM'-'dd' 'HH':'mm':'ss";        1993-02-14 18:10:30
//        "L"                 local date (long date + long time)      "yyyy'-'MM'-'dd' 'HH':'mm':'sszzzz"     1993-02-14 13:10:30-05:00
//        "s"                 sortable format                         "yyyy'-'MM'-'dd'T'HH':'mm':'ss"         1993-02-14T18:10:30
//        "t"                 short time                              "HH':'mm"                               18:10
//        "T"                 long time                               "HH':'mm':'ss"                          18:10:30
//        "U"                 Universal time (long date + long time)  "yyyy'-'MM'-'dd' 'HH':'mm':'ss'Z'"      1993-02-14 18:10:30Z
//        "w"                 short week date                         "yyyy'-''W'ww"                          1993-W06
//        "W"                 long week date                          "yyyy'-''W'ww'-'D"                      1993-W06-7
//        "y"/"Y"             Year/Month date                         "yyyy'-'MM"                             1993-02
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
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace Campari.Software
{
    #region class IsoDateTimeFormat
    internal static class IsoDateTimeFormat
    {
        #region events

        #endregion

        #region class-wide fields
        private const int DEFAULT_ALL_DATETIMEFORMATS_SIZE = 26;

        internal const int MaxSecondsFractionDigits = 7;
        internal const long TicksPerDay = 0xc92a69c000;
        internal const long TicksPerSecond = 0x989680;

        internal static char[] allStandardFormats = new char[] 
        { 
        	'd', 'D', 'f', 'F', 'L', 's', 
        	't', 'T', 'U', 'w', 'W', 'y', 'Y' 
        };

        private static String[] fixedNumberFormats = new String[] 
      	{
            "0",
            "00",
            "000",
            "0000",
            "00000",
            "000000",
            "0000000",
        };

        #endregion

        #region private and internal properties and methods

        #region properties

        #endregion

        #region methods

        #region ExpandPredefinedFormat
        private static string ExpandPredefinedFormat(string format, ref DateTimeFormatInfo dtfi, ref IsoDateTime dateTime)
        {
            switch (format[0])
            {
                case 'L':
                    if (dateTime.Date.Kind == DateTimeKind.Local)
                    {
                        InvalidFormatForLocal(format, dateTime);
                    }
                    dateTime = new IsoDateTime(dateTime.ToLocalTime());
                    break;

                case 'U':
                    dateTime = new IsoDateTime(dateTime.ToUniversalTime());
                    break;
            }

            // Universal time is always in Greogrian calendar.
            //
            // Change the Calendar to be Gregorian Calendar.
            //
            dtfi = (DateTimeFormatInfo)dtfi.Clone();
            if (dtfi.Calendar.GetType() != typeof(GregorianCalendar))
            {
                dtfi.Calendar = new GregorianCalendar();
            }

            format = IsoDateTimeFormat.GetRealFormat(format);
            return format;
        }
        #endregion

        #region Format
        internal static string Format(IsoDateTime dateTime, string format, DateTimeFormatInfo dtfi)
        {
            if (String.IsNullOrEmpty(format))
            {
                format = "F";
            }

            if (format.Length == 1)
            {
                format = IsoDateTimeFormat.ExpandPredefinedFormat(format, ref dtfi, ref dateTime);
            }
            return IsoDateTimeFormat.FormatCustomized(dateTime, format, dtfi);
        }
        #endregion

        #region FormatCustomized
        private static string FormatCustomized(IsoDateTime dateTime, string format, DateTimeFormatInfo dtfi)
        {
            Calendar cal = dtfi.Calendar;
            StringBuilder result = new StringBuilder();

            // This is a flag to indicate if we are formating hour/minute/second only.
            bool bTimeOnly = true;

            int i = 0;
            int tokenLen;

            while (i < format.Length)
            {
                char ch = format[i];
                int nextChar;

                switch (ch)
                {
                    case 'g': // era, not used.
                    case 't': // time designator (AM/PM), not used.
                    case 'K': // kind, not used.
                        tokenLen = 1;
                        break;

                    case 'h':
                    case 'H':
                        tokenLen = ParseRepeatPattern(format, i, ch);
                        FormatDigits(result, dateTime.Hour, 2);
                        break;

                    case 'm':
                        tokenLen = ParseRepeatPattern(format, i, ch);
                        FormatDigits(result, dateTime.Minute, 2);
                        break;

                    case 's':
                        tokenLen = ParseRepeatPattern(format, i, ch);
                        FormatDigits(result, dateTime.Second, 2);
                        break;

                    case 'f':
                    case 'F':
                        tokenLen = ParseRepeatPattern(format, i, ch);
                        if (tokenLen <= MaxSecondsFractionDigits)
                        {
                            long fraction = (dateTime.Ticks % IsoDateTimeFormat.TicksPerSecond);
                            fraction = fraction / (long)Math.Pow(10, 7 - tokenLen);
                            if (ch == 'f')
                            {
                                result.Append(((int)fraction).ToString(fixedNumberFormats[tokenLen - 1], CultureInfo.InvariantCulture));
                            }
                            else
                            {
                                int effectiveDigits = tokenLen;
                                while (effectiveDigits > 0)
                                {
                                    if (fraction % 10 == 0)
                                    {
                                        fraction = fraction / 10;
                                        effectiveDigits--;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (effectiveDigits > 0)
                                {
                                    result.Append(((int)fraction).ToString(fixedNumberFormats[effectiveDigits - 1], CultureInfo.InvariantCulture));
                                }
                                else
                                {
                                    // No fraction to emit, so see if we should remove decimal also.
                                    if (result.Length > 0 && result[result.Length - 1] == '.')
                                    {
                                        result.Remove(result.Length - 1, 1);
                                    }
                                }
                            }
                        }
                        else
                        {
                            throw new FormatException(Properties.Resources.ResourceManager.GetString("Format_InvalidString"));
                        }
                        break;

                    case 'd':
                        tokenLen = ParseRepeatPattern(format, i, ch);
                        // we aren't interested in trying to print anything other than the numeric
                        // value for the day of the month, and we always want it formatted with
                        // a leading 0 if needed.
                        IsoDateTimeFormat.FormatDigits(result, dateTime.Day, 2);
                        bTimeOnly = false;
                        break;

                    case 'M':
                        tokenLen = ParseRepeatPattern(format, i, ch);
                        int month = cal.GetMonth(dateTime.Date);
                        IsoDateTimeFormat.FormatDigits(result, month, 2);
                        bTimeOnly = false;
                        break;

                    case 'y':
                        // Notes about OS behavior:
                        // y: Always print (year % 100). No leading zero.
                        // yy: Always print (year % 100) with leading zero.
                        // yyy/yyyy/yyyyy/... : Print year value.  No leading zero.

                        int year = cal.GetYear(dateTime.Date);
                        tokenLen = ParseRepeatPattern(format, i, ch);
                        if (tokenLen <= 2)
                        {
                            FormatDigits(result, year % 100, 2);
                        }
                        else
                        {
                            String fmtPattern = "D" + tokenLen;
                            result.Append(year.ToString(fmtPattern, CultureInfo.InvariantCulture));
                        }
                        bTimeOnly = false;
                        break;

                    case 'z':
                        //
                        // Output the offset of the timezone according to the system timezone setting.
                        //
                        tokenLen = ParseRepeatPattern(format, i, ch);
                        TimeSpan offset;

                        //if (bTimeOnly && dateTime.Ticks < IsoDateTimeFormat.TicksPerDay) 
                        if (bTimeOnly && dateTime.UtcOffset == TimeSpan.Zero)
                        {
                            offset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
                        }
                        else
                        {
                            if (dateTime.UtcOffset == TimeSpan.Zero)
                            {
                                // The CLR DateTime class calls an MDA at this point. However, since we
                                // don't have access to the internals to call it ourself, there isn't
                                // much we can do about this case.
                                InvalidFormatForUtc(format, dateTime);
                                offset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.SpecifyKind(dateTime.Date, DateTimeKind.Local));
                            }
                            else
                            {
                                offset = dateTime.UtcOffset;
                            }
                        }

                        switch (tokenLen)
                        {
                            case 1:
                                result.Append((offset.Hours).ToString("+0;-0", CultureInfo.InvariantCulture));
                                break;

                            case 2:
                                result.Append((offset.Hours).ToString("+00;-00", CultureInfo.InvariantCulture));
                                break;

                            default:
                                if (offset.Ticks >= 0)
                                {
                                    result.Append(String.Format(CultureInfo.InvariantCulture, "+{0:00}:{1:00}", offset.Hours, offset.Minutes));
                                }
                                else
                                {
                                    // When the offset is negative, note that the offset.Minute is also negative.
                                    // So use should use -offset.Minute to get the postive value.
                                    result.Append(String.Format(CultureInfo.InvariantCulture, "-{0:00}:{1:00}", -offset.Hours, -offset.Minutes));
                                }
                                break;
                        }
                        break;

                    case 'D':
                        tokenLen = IsoDateTimeFormat.ParseRepeatPattern(format, i, ch);
                        result.Append(IsoDateTimeFormat.FormatDayOfWeek(dateTime.DayOfWeek, 1));
                        break;

                    case 'n':
                        tokenLen = IsoDateTimeFormat.ParseRepeatPattern(format, i, ch);
                        IsoDateTimeFormat.FormatDigits(result, dateTime.DayOfYear, 3);
                        break;

                    case 'w':
                        tokenLen = IsoDateTimeFormat.ParseRepeatPattern(format, i, ch);
                        IsoDateTimeFormat.FormatDigits(result, dateTime.Week, 2);
                        break;

                    case ':':
                        result.Append(ch);
                        tokenLen = 1;
                        break;

                    case '/':
                        result.Append(ch);
                        tokenLen = 1;
                        break;

                    case '\'':
                    case '\"':
                        StringBuilder enquotedString = new StringBuilder();
                        tokenLen = ParseQuoteString(format, i, enquotedString);
                        result.Append(enquotedString);
                        break;

                    case '%':
                        // Optional format character.
                        // For example, format string "%d" will print day of month 
                        // without leading zero.  Most of the cases, "%" can be ignored.
                        nextChar = ParseNextChar(format, i);
                        // nextChar will be -1 if we already reach the end of the format string.
                        // Besides, we will not allow "%%" appear in the pattern.
                        if (nextChar >= 0 && nextChar != (int)'%')
                        {
                            result.Append(FormatCustomized(dateTime, ((char)nextChar).ToString(), dtfi));
                            tokenLen = 2;
                        }
                        else
                        {
                            //
                            // This means that '%' is at the end of the format string or
                            // "%%" appears in the format string.
                            //
                            throw new FormatException(Properties.Resources.ResourceManager.GetString("Format_InvalidString"));
                        }
                        break;

                    case '\\':
                        nextChar = ParseNextChar(format, i);
                        if (nextChar >= 0)
                        {
                            result.Append(((char)nextChar));
                            tokenLen = 2;
                        }
                        else
                        {
                            //
                            // This means that '\' is at the end of the formatting string.
                            //
                            throw new FormatException(Properties.Resources.Format_InvalidString);
                        }
                        break;

                    default:
                        result.Append(ch);
                        tokenLen = 1;
                        break;
                }
                i += tokenLen;
            }
            return result.ToString();
        }
        #endregion

        #region FormatDayOfWeek
        private static string FormatDayOfWeek(DayOfWeek dayOfWeek, int repeat)
        {
            int d = (dayOfWeek == DayOfWeek.Sunday) ? 7 : (int)dayOfWeek;
            return d.ToString("D" + repeat, CultureInfo.InvariantCulture);
        }
        #endregion

        #region FormatDigits
        private static void FormatDigits(StringBuilder outputBuffer, int value, int len)
        {
            outputBuffer.Append(value.ToString(IsoDateTimeFormat.fixedNumberFormats[len - 1], CultureInfo.InvariantCulture));
        }
        #endregion

        #region GetAllDateTimeFormats
        internal static String[] GetAllDateTimeFormats()
        {
            ArrayList results = new ArrayList(DEFAULT_ALL_DATETIMEFORMATS_SIZE);

            for (int i = 0; i < allStandardFormats.Length; i++)
            {
                String format = GetRealFormat(allStandardFormats[i]);
                results.Add(format);
                results.Add(format.Replace("'-'", "").Replace(":", ""));
            }
            String[] value = new String[results.Count];
            results.CopyTo(0, value, 0, results.Count);
            return (value);
        }
        #endregion

        #region GetRealFormat

        #region GetRealFormat(char format)
        internal static string GetRealFormat(char format)
        {
            string realFormat = "";

            switch (format)
            {
                case 'd':
                    realFormat = "yyyy'-'nnn";
                    break;

                case 'D':
                    realFormat = "yyyy'-'MM'-'dd";
                    break;

                case 'f':
                    realFormat = "yyyy'-'MM'-'dd' 'HH':'mm";
                    break;

                case 'F':
                    realFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
                    break;

                case 's':
                    realFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
                    break;

                case 't':
                    realFormat = "HH':'mm";
                    break;

                case 'T':
                    realFormat = "HH':'mm':'ss";
                    break;

                case 'w':
                    realFormat = "yyyy'-''W'ww";
                    break;

                case 'W':
                    realFormat = "yyyy'-''W'ww'-'D";
                    break;

                case 'y':
                case 'Y':
                    realFormat = "yyyy'-'MM";
                    break;

                case 'L':
                    realFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'sszzzz";
                    break;

                case 'U':
                    realFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss'Z'";
                    break;

                default:
                    throw new FormatException(Properties.Resources.Format_BadFormatSpecifier);
            }
            return realFormat;
        }
        #endregion

        #region GetRealFormat(string format)
        internal static string GetRealFormat(string format)
        {
            return GetRealFormat(format[0]);
        }
        #endregion

        #endregion

        #region InvalidFormatForLocal
        // This is a placeholder for an MDA to detect when the user is using a
        // local DateTime with a format that will be interpreted as UTC.
        [System.Diagnostics.Conditional("Debug")]
        internal static void InvalidFormatForLocal(String format, IsoDateTime dateTime)
        {
            System.Diagnostics.Debug.WriteLine(String.Format(CultureInfo.CurrentUICulture, Properties.Resources.MDA_InvalidFormatForLocal, format, dateTime));
        }
        #endregion

        #region InvalidFormatForUtc
        // This is an MDA for cases when the user is using a local format with
        // a Utc DateTime.
        [System.Diagnostics.Conditional("Debug")]
        internal static void InvalidFormatForUtc(String format, IsoDateTime dateTime)
        {
            System.Diagnostics.Debug.WriteLine(String.Format(CultureInfo.CurrentUICulture, Properties.Resources.MDA_InvalidFormatForUtc, format, dateTime));
        }
        #endregion

        #region ParseNextChar
        private static int ParseNextChar(string format, int pos)
        {
            if (pos >= format.Length - 1)
            {
                return (-1);
            }
            return ((int)format[pos + 1]);
        }
        #endregion

        #region ParseQuoteString
        internal static int ParseQuoteString(string format, int pos, StringBuilder result)
        {
            //
            // NOTE : pos will be the index of the quote character in the 'format' string.
            //
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
                        throw new FormatException(Properties.Resources.ResourceManager.GetString("Format_InvalidString"));
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
                throw new FormatException(
                        String.Format(
                            CultureInfo.CurrentCulture,
                            Properties.Resources.ResourceManager.GetString("Format_BadQuote"), quoteChar));
            }

            //
            // Return the character count including the begin/end quote characters and enclosed string.
            //
            return (pos - beginPos);
        }
        #endregion

        #region ParseRepeatPattern
        internal static int ParseRepeatPattern(string format, int pos, char patternChar)
        {
            int length = format.Length;
            int index = pos + 1;

            while ((index < length) && (format[index] == patternChar))
            {
                index++;
            }
            return (index - pos);
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
