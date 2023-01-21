////------------------------------------------------------------------------------
//// <copyright file="ApiErrorConverter.cs"
////  company="Scott Dorman"
////  library="Cadru">
////    Copyright (C) 2001-2021 Scott Dorman.
//// </copyright>
////
//// <license>
////    Licensed under the Microsoft Public License (Ms-PL) (the "License");
////    you may not use this file except in compliance with the License.
////    You may obtain a copy of the License at
////
////    http://opensource.org/licenses/Ms-PL.html
////
////    Unless required by applicable law or agreed to in writing, software
////    distributed under the License is distributed on an "AS IS" BASIS,
////    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
////    See the License for the specific language governing permissions and
////    limitations under the License.
//// </license>
////------------------------------------------------------------------------------

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text.Json;
//using System.Text.Json.Serialization;

//using Cadru.ApiClient.Models;

//namespace Cadru.ApiClient.Serialization
//{
//    /// <summary>
//    ///  Converts an <see cref="ApiError"/> to or from JSON.
//    /// </summary>
//    internal class ApiErrorConverter : JsonConverter<ApiError>
//    {
//        /// <inheritdoc/>
//        public override ApiError? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//        {
//            var value = new ApiError();

//            if (reader.TokenType != JsonTokenType.StartObject)
//            {
//                throw new JsonException();
//            }

//            while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
//            {
//                if (reader.TokenType == JsonTokenType.PropertyName)
//                {
//                    var tokenName = reader.GetString();
//                    reader.Read();

//                    switch (tokenName)
//                    {
//                        case JsonSerializationPropertyNames.Id:
//                            value.Id = reader.GetGuid();
//                            break;

//                        case JsonSerializationPropertyNames.Description:
//                            value.Description = reader.GetString();
//                            break;

//                        case JsonSerializationPropertyNames.ErrorCode:
//                            value.ErrorCode = reader.GetString();
//                            break;

//                        case JsonSerializationPropertyNames.StatusCode:
//                            if (Enum.TryParse<HttpStatusCode>(reader.GetString(), out var httpStatusCode))
//                            {
//                                value.HttpStatusCode = httpStatusCode;
//                            }

//                            break;

//                        case JsonSerializationPropertyNames.ErrorDetails:
//                            var errorDetails = JsonSerializer.Deserialize<IEnumerable<ErrorDetail>>(ref reader, options);
//                            value.Details = errorDetails ?? Enumerable.Empty<ErrorDetail>();
//                            break;
//                    }
//                }
//            }

//            if (reader.TokenType == JsonTokenType.EndObject)
//            {
//                return value;
//            }

//            throw new JsonException();
//        }

//        /// <inheritdoc/>
//        public override void Write(Utf8JsonWriter writer, ApiError value, JsonSerializerOptions options)
//        {
//            writer.WriteStartObject();
//            writer.WriteString(JsonSerializationPropertyNames.Id, value.Id);
//            writer.WriteString(JsonSerializationPropertyNames.ErrorCode, value.ErrorCode);
//            writer.WriteString(JsonSerializationPropertyNames.StatusCode, value.HttpStatusCode.ToString());
//            writer.WriteString(JsonSerializationPropertyNames.Description, value.Description);
//            writer.WritePropertyName(JsonSerializationPropertyNames.ErrorDetails);
//            JsonSerializer.Serialize(writer, value.Details, options);
//            writer.WriteEndObject();
//        }
//    }
//}
