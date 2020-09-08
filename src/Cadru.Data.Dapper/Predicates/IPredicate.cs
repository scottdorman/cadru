﻿//------------------------------------------------------------------------------
// <copyright file="IPredicate.cs"
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
    using global::Dapper;

    /// <summary>
    /// Represents a basic predicate.
    /// </summary>
    public interface IPredicate
    {
        /// <summary>
        /// Gets the SQL text representing the given predicate.
        /// </summary>
        /// <param name="parameters">A <see cref="DynamicParameters"/>
        /// collection to which the parameters and values for the predicate
        /// will be added.</param>
        /// <param name="objectMap"></param>
        /// <returns>A string containing the SQL representation of the predicate.</returns>
        string GetSql(DynamicParameters parameters, IObjectMap objectMap);
    }
}
