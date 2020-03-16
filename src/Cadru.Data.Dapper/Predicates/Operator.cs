//------------------------------------------------------------------------------
// <copyright file="Operator.cs"
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

namespace Cadru.Data.Dapper.Predicates
{
    /// <summary>
    /// Comparison operator for predicates.
    /// </summary>
    public enum Operator
    {
        /// <summary>
        /// Equal to
        /// </summary>
        Equal,

        /// <summary>
        /// Greater than
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Greater than or equal to
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// Less than
        /// </summary>
        LessThan,

        /// <summary>
        /// Less than or equal to
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// Like (You can use % in the value to do wilcard searching)
        /// </summary>
        Like,

        /// <summary>
        /// Between
        /// </summary>
        Between,

        /// <summary>
        /// In
        /// </summary>
        In,
    }
}
