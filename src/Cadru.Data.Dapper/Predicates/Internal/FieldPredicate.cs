//------------------------------------------------------------------------------
// <copyright file="FieldPredicate.cs"
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
    using Cadru.Data.Dapper.Internal;
    using global::Dapper;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class FieldPredicate<T, TFieldType> : ComparePredicate
            where T : class
    {
        public TFieldType Value { get; set; }

        public override string GetSql(DynamicParameters parameters)
        {
            var notText = Not ? "NOT " : string.Empty;

            var columnName = GetColumnName<T>(PropertyName, false);
            if (Value == null)
            {
                return $"({columnName} IS {notText}NULL)";
            }

            if (Value is IEnumerable && !(Value is string))
            {
                if (Operator != Operator.Equal)
                {
                    throw new ArgumentException("Operator must be set to Eq for Enumerable types");
                }

                var @params = new List<string>();
                foreach (var value in (IEnumerable)Value)
                {
                    var valueParameterName = parameters.SetParameterName(this.PropertyName, value, '@');
                    @params.Add(valueParameterName);
                }

                var paramStrings = @params.Aggregate(new StringBuilder(), (sb, s) => sb.Append((sb.Length != 0 ? ", " : string.Empty) + s), sb => sb.ToString());
                return $"({columnName} {notText}IN ({paramStrings}))";
            }

            if (this.Operator == Operator.Between)
            {
                var values = this.Value as Tuple<TFieldType, TFieldType>;
                if (values != null)
                {
                    return $"({columnName} {GetOperatorString()} {parameters.SetParameterName(this.PropertyName, values.Item1, '@')} AND {parameters.SetParameterName(this.PropertyName, values.Item2, '@')})";
                }
            }

            var parameterName = parameters.SetParameterName(this.PropertyName, this.Value, '@');
            return $"({columnName} {GetOperatorString()} {parameterName})";
        }
    }
}
