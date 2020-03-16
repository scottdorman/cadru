//------------------------------------------------------------------------------
// <copyright file="PropertyPredicate.cs"
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
    using global::Dapper;

    internal class PropertyPredicate<T, T2> : ComparePredicate
        where T : class
        where T2 : class
    {
        public string PropertyName2 { get; set; }

        public override string GetSql(DynamicParameters parameters)
        {
            var columnName = GetColumnName<T>(this.PropertyName, false);
            var columnName2 = GetColumnName<T2>(this.PropertyName2, false);
            return $"{CommandAdapter.LeftParenthesis}{columnName}{this.GetOperatorString()}{columnName2}{CommandAdapter.RightParenthesis}";
        }
    }
}
