//------------------------------------------------------------------------------
// <copyright file="ComparePredicate.cs"
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

namespace Cadru.Data.Dapper.Predicates.Internal
{
    internal abstract partial class ComparePredicate : PredicateBase
    {
        public Operator Operator { get; set; }

        internal string GetOperatorString()
        {
            switch (this.Operator)
            {
                case Operator.GreaterThan:
                    return this.Not ? "<=" : ">";
                case Operator.GreaterThanOrEqual:
                    return this.Not ? "<" : ">=";
                case Operator.LessThan:
                    return this.Not ? ">=" : "<";
                case Operator.LessThanOrEqual:
                    return this.Not ? ">" : "<=";
                case Operator.Like:
                    return this.Not ? CommandAdapter.NotLike : CommandAdapter.Like;
                case Operator.Between:
                    return this.Not ? CommandAdapter.NotBetween : CommandAdapter.Between;
                case Operator.In:
                    return this.Not ? CommandAdapter.NotIn : CommandAdapter.In;
                default:
                    return this.Not ? "<>" : "=";
            }
        }
    }
}
