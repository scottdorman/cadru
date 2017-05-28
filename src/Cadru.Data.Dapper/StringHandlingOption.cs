//------------------------------------------------------------------------------
// <copyright file="StringHandlingOption.cs"
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadru.Data.Dapper
{
    /// <summary>
    /// Represents the string handling option.
    /// </summary>
    public enum StringHandlingOption
    {
        /// <summary>
        /// The string value should not be modified.
        /// </summary>
        None = 0,

        /// <summary>
        /// The string value should be trimmed to remove leading and trailing spaces.
        /// </summary>
        Trim = 1,

        /// <summary>
        /// The string value should be truncated.
        /// </summary>
        Truncate = 2
    }
}
