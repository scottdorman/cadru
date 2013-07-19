//------------------------------------------------------------------------------
// <copyright file="ReadOnlyNameValueCollection.cs" 
//  company="Scott Dorman" 
//  library="Cadru">
//    Copyright (C) 2001-2013 Scott Dorman.
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
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a read only sorted collection of associated <see cref="System.String"/> keys and 
    /// <b>String</b> values that can be accessed either with the key or with the index.
    /// </summary>
    public class NameValueCollection : List<NameValuePair>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameValueCollection"/> class
        /// that is empty, has the default initial capacity and uses the specified hash code 
        /// provider and the specified comparer.
        /// </summary>
        /// <param name="list">The list to wrap.</param>
        public NameValueCollection(IList<NameValuePair> list)
            : base(list)
        {
        }
    }
}
