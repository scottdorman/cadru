//------------------------------------------------------------------------------
// <copyright file="RelativeDateFormatting.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2017 Scott Dorman.
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
    /// <summary>
    /// Specifies how a relative date within the current week
    /// (+/- 5 days from today) should be formatted.
    /// </summary>
    public enum RelativeDateFormatting
    {
        /// <summary>
        /// Format the relative date as a number of days ago if earlier
        /// than today or a number of days from today if later.
        /// </summary>
        Days = 0,

        /// <summary>
        /// Format the relative date as the name of the day of the week.
        /// </summary>
        DayNames
    }
}
