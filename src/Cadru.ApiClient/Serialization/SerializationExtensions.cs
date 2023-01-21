//------------------------------------------------------------------------------
// <copyright file="SerializationExtensions.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2021 Scott Dorman.
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

using System.Text.Json;

using Cadru.ApiClient.Models;

namespace Cadru.ApiClient.Serialization
{
    /// <summary>
    /// Extension methods to help serialize an <see cref="IApiResult{TData}"/>.
    /// </summary>
    public static class SerializationExtensions
    {
        /// <summary>
        /// Converts the value of the <paramref name="apiResult"/> into a JSON
        /// string.
        /// </summary>
        /// <param name="apiResult">The <see cref="IApiResult{TData}"/> to convert.</param>
        /// <returns>A JSON string that represents the <paramref name="apiResult"/>.</returns>
        public static string ToJson<TData>(this IApiResult<TData> apiResult)
            where TData : class
        {
            return JsonSerializer.Serialize(apiResult, DefaultJsonSerializerOptions.Create());
        }
    }
}
