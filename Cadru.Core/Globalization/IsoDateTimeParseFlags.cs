// ---------------------------------------------------------------------------
// Campari Software
//
// IsoDateTimeParseFlags.cs
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

namespace Campari.Software
{
    #region enum IsoDateTimeParseFlags
    [Flags]
    internal enum IsoDateTimeParseFlags
    {
        None = 0,

        HaveDay = 0x0002,
        HaveMonth = 0x0004,
        HaveYear = 0x0008,
        HaveWeek = 0x0010,
        HaveDayOfWeek = 0x0020,
        HaveDayOfYear = 0x0040,

        HaveHour = 0x0080,
        HaveMinute = 0x0100,
        HaveSecond = 0x0200,

        TimeZoneUsed = 0x0400,
        TimeZoneUtc = 0x0800,

        HaveCalendarDate = (HaveYear | HaveMonth | HaveDay),
        HaveOrdinalDate = (HaveYear | HaveDayOfYear),
        HaveWeekDate = (HaveYear | HaveWeek | HaveDayOfWeek),
        HaveTime = (HaveHour | HaveMinute | HaveSecond),

        HaveCalendarDateTime = (HaveCalendarDate | HaveTime),
        HaveOrdinalDateTime = (HaveOrdinalDate | HaveTime),
        HaveWeekDateTime = (HaveWeekDate | HaveTime),
    }
    #endregion
}
