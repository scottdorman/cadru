// ---------------------------------------------------------------------------
// Campari Software
//
// IsoDateTimeParseFailureKind.cs
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
    #region enum IsoDateTimeParseFailureKind
    internal enum IsoDateTimeParseFailureKind
    {
        None,
        ArgumentNull,
        Format,
        FormatWithParameter,
        FormatBadDateTimeCalendar
    }
    #endregion
}
