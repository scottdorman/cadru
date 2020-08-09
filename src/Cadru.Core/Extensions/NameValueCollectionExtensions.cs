//------------------------------------------------------------------------------
// <copyright file="NameValueCollectionExtensions.cs"
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
using System.Collections.Specialized;

namespace Cadru.Extensions
{
    public static class NameValueCollectionExtensions
    {
        public static int GetKeyIndex(this NameValueCollection nameValueCollection, string key)
        {
            return nameValueCollection.AllKeys.FindIndex(e => e.Equals(key, StringComparison.OrdinalIgnoreCase));
        }
    }
}
