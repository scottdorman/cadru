//------------------------------------------------------------------------------
// <copyright file="QueryStringParametersDictionary.cs"
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
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Cadru.Extensions;

    /// <summary>
    /// Represents a collection of query string parameters and values.
    /// </summary>
    public class QueryStringParametersDictionary : Dictionary<string, string>
    {
        #region fields
        #endregion

        #region constructors
        #endregion

        #region events
        #endregion

        #region properties
        #endregion

        #region methods

        #region ToQueryString
        /// <summary>
        /// Returns a string representation of the query parameters and
        /// values.
        /// </summary>
        /// <returns>A string representation of the query parameters and
        /// values.</returns>
        public string ToQueryString()
        {
            var queryString = String.Empty;
            int index = 0;

            if (this.Count > 0)
            {
                int count = this.Count - 1;
                var builder = new StringBuilder();
                foreach (var item in this)
                {
                    builder.AppendFormat(CultureInfo.InvariantCulture, "{0}={1}{2}", item.Key, item.Value, index++ < count ? "&" : String.Empty);
                }

                queryString = builder.ToString();
            }

            return queryString;
        }
        #endregion

        #endregion
    }
}
