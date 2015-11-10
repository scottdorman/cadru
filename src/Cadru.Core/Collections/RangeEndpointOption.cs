//------------------------------------------------------------------------------
// <copyright file="RangeEndpointOption.cs" 
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

namespace Cadru.Collections
{
    /// <summary>
    /// Specifies the endpoint inclusion option for a <see cref="Range{T}"/>.
    /// </summary>
    public enum RangeEndpointOption
    {
        /// <summary>
        /// The range includes both the lower and upper bounds.
        /// </summary>
        Open = 0,

        /// <summary>
        /// The range includes the lower bound but excludes the upper bound.
        /// </summary>
        LeftHalfOpen = 1,

        /// <summary>
        /// The range excludes the lower bound but includes the upper bound.
        /// </summary>
        RightHalfOpen = 2,

        /// <summary>
        /// The range excludes both the lower and upper bounds.
        /// </summary>
        Closed = 3,
    }
}
