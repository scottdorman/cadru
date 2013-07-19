// ---------------------------------------------------------------------------
// Campari Software
//
// IsoDateTimeParsingInfo.cs
//
// This is a helper class used by IsoDateTimeParse.
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

namespace Campari.Software
{
    #region struct IsoDateTimeParsingInfo
    internal struct IsoDateTimeParsingInfo
    {
        #region fields
        internal Calendar calendar;
        internal int dayOfWeek;
        internal bool fAllowInnerWhite;
        internal bool fAllowTrailingWhite;
        #endregion

        #region properties
        #endregion

        #region methods

        #region Init
        internal void Init()
        {
            dayOfWeek = -1;
        }
        #endregion

        #endregion
    }
    #endregion
}
