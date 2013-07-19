// ---------------------------------------------------------------------------
// Campari Software
//
// StringTokenizeOptions.cs
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

namespace Campari.Software.Text
{
    #region enum StringTokenizeOptions
    /// <summary>
    /// Description of StringTokenizeOptions.
    /// </summary>
    [Flags]
    public enum StringTokenizeOptions : int
    {
        /// <summary>
        /// Include empty tokens but do not include separator values.
        /// </summary>
        None = 0x000,

        /// <summary>
        /// Omit empty tokens.
        /// </summary>
        RemoveEmptyEntries = 0x002,
    }
    #endregion
}
