//------------------------------------------------------------------------------
// <copyright file="LoggingScope.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2020 Scott Dorman.
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

namespace Cadru.Core.Logging
{
    /// <summary>
    /// An object that can be used to provide additional logging data through a
    /// logging scope.
    /// </summary>
    public class LoggingScope
    {
        /// <summary>
        /// Additional key-value pairs to be added to the logging scope.
        /// </summary>
        public IDictionary<string, string> AdditionalItems { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Returns a collection of key-value pairs to be used in the logging scope.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<KeyValuePair<string, string>> AsEnumerable()
        {
            foreach (var additionalItem in this.AdditionalItems)
            {
                yield return additionalItem;
            }
        }
    }
}