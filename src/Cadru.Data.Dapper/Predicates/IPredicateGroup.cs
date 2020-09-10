//------------------------------------------------------------------------------
// <copyright file="IPredicateGroup.cs"
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

using System.Collections.Generic;

namespace Cadru.Data.Dapper.Predicates
{
    /// <summary>
    /// Represents a collection of <see cref="IPredicate"/> instances to be
    /// joined together.
    /// </summary>
    public interface IPredicateGroup : IPredicate
    {
        /// <summary>
        /// Gets the collection containing the <see cref="IPredicate"/> instances.
        /// </summary>
        IList<IPredicate> Predicates { get; }
    }
}