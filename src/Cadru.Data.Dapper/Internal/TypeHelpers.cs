//------------------------------------------------------------------------------
// <copyright file="TypeHelpers.cs"
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

namespace Cadru.Data.Dapper.Internal
{
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using global::Dapper;

    internal static class TypeHelpers
    {
        public static PropertyInfo GetProperty(this LambdaExpression lambda)
        {
            Expression expr = lambda;
            for (;;)
            {
                switch (expr.NodeType)
                {
                    case ExpressionType.Lambda:
                        expr = ((LambdaExpression)expr).Body;
                        break;
                    case ExpressionType.Convert:
                        expr = ((UnaryExpression)expr).Operand;
                        break;
                    case ExpressionType.MemberAccess:
                        return (expr as MemberExpression)?.Member as PropertyInfo;
                    default:
                        return null;
                }
            }
        }

        public static string SetParameterName(this DynamicParameters parameters, string name, object value, char prefix)
        {
            var parameterName = $"{prefix}{name}_{parameters.ParameterNames.Count()}";
            parameters.Add(parameterName, value);
            return parameterName;
        }
    }
}
