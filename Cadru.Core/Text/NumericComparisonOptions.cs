//------------------------------------------------------------------------------
// <copyright file="NumericComparisonOptions.cs" 
//  company="Scott Dorman" 
//  library="Cadru">
//    Copyright (C) 2001-2014 Scott Dorman.
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
namespace Cadru.Text
{
    using System;

    /// <summary>
    /// Specifies the comparison rules to be used by certain overloads of the
    /// <see cref="Cadru.Extensions.StringExtensions">StringExtensions.LengthBetween</see> and
    /// <see cref="Cadru.Extensions.NumericExtensions">NumericExtensions.Between</see> methods. 
    /// </summary>
    [Flags]
    public enum NumericComparisonOptions
    {
        /// <summary>
        /// The comparison includes neither the minimum and maximum value.
        /// </summary>
        None = 0x000,

        /// <summary>
        /// The comparison includes the minimum value.
        /// </summary>
        IncludeMinimum = 0x002,

        /// <summary>
        /// The comparison includes the maximum value.
        /// </summary>
        IncludeMaximum = 0x004,

        /// <summary>
        /// The comparison includes both the minimum and maximum value.
        /// </summary>
        IncludeBoth = IncludeMinimum | IncludeMaximum,
    }
}
