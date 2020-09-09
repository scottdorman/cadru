//------------------------------------------------------------------------------
// <copyright file="PredicateGroup.cs"
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
using System.Linq;

using Dapper;

namespace Cadru.Data.Dapper.Predicates.Internal
{
    /// <summary>
    /// Groups IPredicates together using the specified group operator.
    /// </summary>
    internal class PredicateGroup : IPredicateGroup
    {
        private readonly List<IPredicate> predicates;

        public PredicateGroup()
        {
            this.predicates = new List<IPredicate>();
        }

        public IList<IPredicate> Predicates => this.predicates;
        internal GroupOperator Operator { get; set; }

        public string GetSql(DynamicParameters parameters, IObjectMap objectMap)
        {
            var seperator = this.Operator == GroupOperator.And ? CommandAdapter.And : CommandAdapter.Or;
            var predicateList = new List<string>();
            foreach (var predicate in this.Predicates)
            {
                var sql = predicate.GetSql(parameters, objectMap);
                if (!String.IsNullOrWhiteSpace(sql))
                {
                    predicateList.Add(sql);
                }
            }

            return predicateList.Any() ? $"{CommandAdapter.LeftParenthesis}{ String.Join(seperator, predicateList) }{CommandAdapter.RightParenthesis}" : String.Empty;
        }

        internal void AddRange(IEnumerable<IPredicate> predicates)
        {
            this.predicates.AddRange(predicates);
        }
    }
}