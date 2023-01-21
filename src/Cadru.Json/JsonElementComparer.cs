//------------------------------------------------------------------------------
// <copyright file="JsonElementComparer.cs"
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
using System.Text.Json;

namespace Cadru.Json
{
    internal class JsonElementComparer : IEqualityComparer<JsonElement>
    {
        public JsonElementComparer() : this(-1) { }

        public JsonElementComparer(int maxHashDepth) => this.MaxHashDepth = maxHashDepth;

        int MaxHashDepth { get; } = -1;

        #region IEqualityComparer<JsonElement> Members

        public bool Equals(JsonElement x, JsonElement y)
        {
             if (x.ValueKind != y.ValueKind)
            {
                return false;
            }

            switch (x.ValueKind)
            {
                case JsonValueKind.Null:
                case JsonValueKind.True:
                case JsonValueKind.False:
                case JsonValueKind.Undefined:
                    return true;

                // Compare the raw values of numbers, and the text of strings.
                // Note this means that 0.0 will differ from 0.00 -- which may be correct as deserializing either to `decimal` will result in subtly different results.
                // Newtonsoft's JValue.Compare(JTokenType valueType, object? objA, object? objB) has logic for detecting "equivalent" values, 
                // you may want to examine it to see if anything there is required here.
                // https://github.com/JamesNK/Newtonsoft.Json/blob/master/Src/Newtonsoft.Json/Linq/JValue.cs#L246
                case JsonValueKind.Number:
                    return x.GetRawText() == y.GetRawText();

                case JsonValueKind.String:
                    return x.GetString() == y.GetString(); // Do not use GetRawText() here, it does not automatically resolve JSON escape sequences to their corresponding characters.

                case JsonValueKind.Array:
                    return x.EnumerateArray().SequenceEqual(y.EnumerateArray(), this);

                case JsonValueKind.Object:
                    {
                        // Surprisingly, JsonDocument fully supports duplicate property names.
                        // I.e. it's perfectly happy to parse {"Value":"a", "Value" : "b"} and will store both
                        // key/value pairs inside the document!
                        // A close reading of https://www.rfc-editor.org/rfc/rfc8259#section-4 seems to indicate that
                        // such objects are allowed but not recommended, and when they arise, interpretation of 
                        // identically-named properties is order-dependent.  
                        // So stably sorting by name then comparing values seems the way to go.
                        var xPropertiesUnsorted = x.EnumerateObject().ToList();
                        var yPropertiesUnsorted = y.EnumerateObject().ToList();
                        if (xPropertiesUnsorted.Count != yPropertiesUnsorted.Count)
                        {
                            return false;
                        }

                        var xProperties = xPropertiesUnsorted.OrderBy(p => p.Name, StringComparer.Ordinal);
                        var yProperties = yPropertiesUnsorted.OrderBy(p => p.Name, StringComparer.Ordinal);
                        foreach (var (px, py) in xProperties.Zip(yProperties))
                        {
                            if (px.Name != py.Name)
                            {
                                return false;
                            }

                            if (!this.Equals(px.Value, py.Value))
                            {
                                return false;
                            }
                        }
                        return true;
                    }

                default:
                    throw new JsonException(String.Format("Unknown JsonValueKind {0}", x.ValueKind));
            }
        }

        public int GetHashCode(JsonElement obj)
        {
            var hash = new HashCode();
            this.ComputeHashCode(obj, ref hash, 0);
            return hash.ToHashCode();
        }

        void ComputeHashCode(JsonElement jsonElement, ref HashCode hash, int depth)
        {
            hash.Add(jsonElement.ValueKind);

            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.Null:
                case JsonValueKind.True:
                case JsonValueKind.False:
                case JsonValueKind.Undefined:
                    break;

                case JsonValueKind.Number:
                    hash.Add(jsonElement.GetRawText());
                    break;

                case JsonValueKind.String:
                    hash.Add(jsonElement.GetString());
                    break;

                case JsonValueKind.Array:
                    if (depth != this.MaxHashDepth)
                    {
                        foreach (var item in jsonElement.EnumerateArray())
                        {
                            this.ComputeHashCode(item, ref hash, depth + 1);
                        }
                    }
                    else
                    {
                        hash.Add(jsonElement.GetArrayLength());
                    }

                    break;

                case JsonValueKind.Object:
                    foreach (var property in jsonElement.EnumerateObject().OrderBy(p => p.Name, StringComparer.Ordinal))
                    {
                        hash.Add(property.Name);
                        if (depth != this.MaxHashDepth)
                        {
                            this.ComputeHashCode(property.Value, ref hash, depth + 1);
                        }
                    }

                    break;

                default:
                    throw new JsonException(String.Format("Unknown JsonValueKind {0}", jsonElement.ValueKind));
            }
        }

        #endregion
    }
}