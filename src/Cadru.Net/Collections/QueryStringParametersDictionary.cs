//------------------------------------------------------------------------------
// <copyright file="QueryStringParametersDictionary.cs"
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

namespace Cadru.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using Cadru.Internal;

    /// <summary>
    /// Represents a collection of query string parameters and values.
    /// </summary>
    public class QueryStringParametersDictionary : Dictionary<string, string>
    {
        #region fields
        #endregion

        #region constructors

        #region QueryStringParametersDictionary()
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryStringParametersDictionary"/>
        /// class that is empty and has the default initial capacity.
        /// </summary>
        public QueryStringParametersDictionary()
            : base()
        {
        }
        #endregion

        #region QueryStringParametersDictionary(int capacity)
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryStringParametersDictionary"/>
        /// class that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the
        /// <see cref="QueryStringParametersDictionary"/> can contain.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="capacity"/> is less than 0.</exception>
        public QueryStringParametersDictionary(int capacity)
            : base(capacity)
        {
        }
        #endregion

        #region QueryStringParametersDictionary(IDictionary<string, string> dictionary)
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryStringParametersDictionary"/>
        /// class that contains elements copied from the specified
        /// <see cref="System.Collections.Generic.IDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <param name="dictionary">
        /// The <see cref="System.Collections.Generic.IDictionary{TKey,TValue}"/>
        /// whose elements are copied to the new
        /// <see cref="QueryStringParametersDictionary"/>.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="dictionary"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="dictionary"/> contains one or more duplicate keys.
        /// </exception>
        public QueryStringParametersDictionary(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
        }
        #endregion

        #region QueryStringParametersDictionary(string query)
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryStringParametersDictionary"/>
        /// class that contains elements parsed from the given query.
        /// </summary>
        /// <remarks>Multiple occurrences of the same query string variable are not supported.</remarks>
        /// <param name="query">The string to be parsed.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="query"/> contains one or more keys whose value
        /// is <see langword="null"/>.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="query"/> contains one or more duplicate keys.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// <paramref name="query"/> is not properly formed.
        /// </exception>
        public QueryStringParametersDictionary(string query)
            : base()
        {
            this.FillFromString(query);
        }
        #endregion

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

        internal void FillFromString(string query)
        {
            int length = (query != null) ? query.Length : 0;

            if (length > 0)
            {
                if (query[0] == '?')
                {
                    query = query.Substring(1);
                }

                foreach (var pair in query.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    int pairLength = pair.Length;
                    var index = pair.IndexOf('=');
                    if (index == -1)
                    {
                        throw ExceptionBuilder.CreateInvalidOperation(Net.Resources.Strings.InvalidOperation_QueryStringParameterDictionaryParsing);
                    }
                    else
                    {
                        this.Add(pair.Substring(0, index), pair.Substring(index + 1, pairLength - index - 1));
                    }
                }
            }
        }

        #endregion
    }
}
