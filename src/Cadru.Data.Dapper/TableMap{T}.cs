//------------------------------------------------------------------------------
// <copyright file="TableMap{T}.cs"
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

namespace Cadru.Data.Dapper
{
    using System;

#pragma warning disable 1591

    [Obsolete("Use ObjectMap<T> directly instead.")]
    public class TableMap<T> : ObjectMap<T> where T : class
    {
        internal TableMap(CommandAdapter commandAdapter) : base(commandAdapter, DatabaseObjectType.Table)
        {
        }
    }

#pragma warning restore 1591
}