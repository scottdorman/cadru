//------------------------------------------------------------------------------
// <copyright file="PredicateGroup.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2015 Scott Dorman.
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

namespace Cadru.Data.Dapper.Predicates.Internal
{
    using global::Dapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Groups IPredicates together using the specified group operator.
    /// </summary>
    internal class PredicateGroup : IPredicateGroup
    {
        private List<IPredicate> predicates;

        public PredicateGroup()
        {
            this.predicates = new List<IPredicate>();
        }

        internal GroupOperator Operator { get; set; }

        public IList<IPredicate> Predicates => predicates;

        internal void AddRange(IEnumerable<IPredicate> predicates)
        {
            this.predicates.AddRange(predicates);
        }
        //public static IPredicateGroup And()
        //{
        //    var group = new PredicateGroup
        //    {
        //        Operator = GroupOperator.And,
        //    };

        //    return group;
        //}

        //public static IPredicateGroup And(IList<IPredicate> predicates)
        //{
        //    var group = (PredicateGroup)PredicateGroup.And();
        //    group.predicates.AddRange(predicates);
        //    return group;
        //}

        //public static IPredicateGroup Or()
        //{
        //    var group = new PredicateGroup
        //    {
        //        Operator = GroupOperator.Or,
        //    };

        //    return group;
        //}

        //public static IPredicateGroup Or(IList<IPredicate> predicates)
        //{
        //    var group = (PredicateGroup)PredicateGroup.Or();
        //    group.predicates.AddRange(predicates);
        //    return group;
        //}

        public string GetSql(DynamicParameters parameters)
        {
            var seperator = Operator == GroupOperator.And ? " AND " : " OR ";
            var seed = new StringBuilder();
            Func<StringBuilder, IPredicate, StringBuilder> func = (sb, p) => (sb.Length == 0 ? sb : sb.Append(seperator)).Append(p.GetSql(parameters));
            Func<StringBuilder, string> result = sb =>
            {
                var s = sb.ToString();
                if (s.Length == 0) return "1 = 1";
                return s;
            };

            return $"( {Predicates.Aggregate(seed, func, result)})";
        }
    }
}
