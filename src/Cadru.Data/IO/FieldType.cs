// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

//using Microsoft.VisualBasic.CompilerServices; // Install-Package Microsoft.VisualBasic

namespace Cadru.Data.IO
{
    /// <summary>
    /// Indicate the kind of file being read, either delimited or fixed length
    /// </summary>
    public enum FieldType
    {
        /// <summary>
        /// The file being read has delimited records.
        /// </summary>
        Delimited,

        /// <summary>
        /// The file being read has fixed length records.
        /// </summary>
        FixedWidth
    }
}