//------------------------------------------------------------------------------
// <copyright file="JsonUtilities.cs"
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

using System.Diagnostics;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

using Cadru.Extensions;
using Cadru.Json.DataAnnotations;
using Cadru.Json.Resources;

namespace Cadru.Json
{
    /// <summary>
    /// Utility methods for determining the differences between two JSON
    /// objects.
    /// </summary>
    public static class JsonUtilities
    {
        /// <summary>
        /// Generates JSON representing the difference between two objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <remarks>This only looks at properties which have the <see
        /// cref="JsonComparableAttribute"/> applied.</remarks>
        public static JsonObject? GetDiff<T>([DisallowNull] T original, [DisallowNull] T other)
        {
            var _original = JsonSerializer.SerializeToElement(original);
            var _other = JsonSerializer.SerializeToElement(other);
            JsonObject? result = new();
            JsonElementComparer comparer = new();

            var comparableProperties = typeof(T).GetAllComparableProperties().Where(p => p.PropertyInfo.HasCustomAttribute<JsonComparableAttribute>(true));
            foreach (var (propertyInfo, name) in comparableProperties)
            {
                var comparisonPropertyPath = name;
                JsonElement? originalProperty = _original.SelectToken(comparisonPropertyPath);
                JsonElement? otherProperty = _other.SelectToken(comparisonPropertyPath);

                if (originalProperty != null && otherProperty != null)
                {
                    if (!comparer.Equals(originalProperty.Value, otherProperty.Value))
                    {
                        var value = JsonValue.Create(otherProperty);
                        var index = comparisonPropertyPath.IndexOf(".");
                        if (index > 0)
                        {
                            var parent = comparisonPropertyPath[..index];
                            if (!result.ContainsKey(parent))
                            {
                                result.Add(parent, new JsonObject
                                {
                                    {  comparisonPropertyPath[(index + 1)..], value }
                                });
                            }
                            else
                            {
                                result[parent]?.AsObject().Add(comparisonPropertyPath[(index + 1)..], value);
                            }
                        }
                        else
                        {
                            result.Add(comparisonPropertyPath, value);
                        }
                    }
                }
            }

            return result;
        }

        public static MediaTypeHeaderValue GetDefaultMediaType()
        {
            return new("application/json") { CharSet = "utf-8" };
        }

        public static Encoding? GetEncoding(string? charset)
        {
            Encoding? encoding = null;

            if (charset != null)
            {
                try
                {
                    // Remove at most a single set of quotes.
                    encoding = charset.Length > 2 && charset[0] == '\"' && charset[^1] == '\"'
                        ? Encoding.GetEncoding(charset[1..^1])
                        : Encoding.GetEncoding(charset);
                }
                catch (ArgumentException e)
                {
                    throw new InvalidOperationException(Strings.CharSetInvalid, e);
                }

                Debug.Assert(encoding != null);
            }

            return encoding;
        }
    }
}