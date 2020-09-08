//------------------------------------------------------------------------------
// <copyright file="FieldPredicate.cs"
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
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Cadru.Data.Dapper.Internal;

    using global::Dapper;

    internal class FieldPredicate<T, TFieldType> : ComparePredicate
            where T : class
    {
        public object? Value { get; set; }

        public override string GetSql(DynamicParameters parameters, IObjectMap objectMap)
        {
            if (this.Value != null && IsValidPropertyName(this.PropertyName))
            {
                var columnName = GetColumnName<T>(objectMap, this.PropertyName!, false);

                if (this.Operator == Operator.In)
                {
                    if (this.Value is IEnumerable enumerable)
                    {
                        var @params = new List<string>();
                        foreach (var value in enumerable)
                        {
                            var valueParameterName = parameters.SetParameterName(this.PropertyName!, value, '@');
                            @params.Add(valueParameterName);
                        }

                        if (@params.Any())
                        {
                            var paramStrings = @params.Aggregate(new StringBuilder(), (sb, s) => sb.Append((sb.Length != 0 ? ", " : String.Empty) + s), sb => sb.ToString());
                            return $"{CommandAdapter.LeftParenthesis}{columnName}{this.GetOperatorString()}{CommandAdapter.SpaceLeftParenthesis}{paramStrings}{CommandAdapter.RightParenthesis}{CommandAdapter.RightParenthesis}";
                        }
                        else
                        {
                            return String.Empty;
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Value must be enumerable for IN operations");

                    }
                }

                if (this.Operator == Operator.Between && this.Value is Tuple<TFieldType, TFieldType> values && values.Item1 != null && values.Item2 != null)
                {
                    return $"({columnName} {this.GetOperatorString()} {parameters.SetParameterName(this.PropertyName!, values.Item1, '@')}{CommandAdapter.And}{parameters.SetParameterName(this.PropertyName!, values.Item2, '@')})";
                }

                var parameterName = parameters.SetParameterName(this.PropertyName!, this.Value, '@');
                return $"({columnName} {this.GetOperatorString()} {parameterName})";
            }

            return String.Empty;
        }
    }
}
