//------------------------------------------------------------------------------
// <copyright file="TypeExtensions.cs"
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
using System.Reflection;
using System.Text.Json.Serialization;

using Cadru.Extensions;
using Cadru.Json.DataAnnotations;

namespace Cadru.Json
{
    internal static class TypeExtensions
    {
        internal static IEnumerable<(PropertyInfo PropertyInfo, string Name)> GetAllComparableProperties(this Type t)
        {
            return GetAllRuntimeProperties(t).Where(p => p.PropertyInfo.HasCustomAttribute<JsonComparableAttribute>(true));
        }

        internal static string GetJsonPropertyName(this MemberInfo type)
        {
            return type.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? type.Name;
        }

        private static IEnumerable<(PropertyInfo PropertyInfo, string Name)> GetAllRuntimeProperties(Type t, string? prefix = null)
        {
            foreach (var p in t.GetRuntimeProperties())
            {
                if (p.IsEmbeddedObject())
                {
                    var parentName = p.GetJsonPropertyName();
                    foreach (var childProperty in GetAllRuntimeProperties(p.PropertyType, parentName))
                    {
                        yield return childProperty;
                    }
                }
                else
                {
                    yield return (p, prefix == null ? p.GetJsonPropertyName() : $"{prefix}.{p.GetJsonPropertyName()}");
                }
            }
        }

        private static bool IsEmbeddedObject(this PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.IsClass && !propertyInfo.PropertyType.IsValueType && !propertyInfo.PropertyType.IsPrimitive && propertyInfo.PropertyType.FullName != "System.String";
        }
    }
}