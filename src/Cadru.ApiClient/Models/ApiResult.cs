using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

namespace Cadru.ApiClient.Models
{
    /// <summary>
    /// Represents the response of an endpoint call.
    /// </summary>
    public abstract class ApiResult : IApiResult
    {
        /// <inheritdoc/>
        bool IApiResult.IsError => this.Error != null;

        /// <inheritdoc/>
        [JsonProperty("error", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public ApiError? Error { get; internal set; } = null;

        /// <summary>
        /// Creates
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="apiError"></param>
        /// <returns></returns>
        public static ApiResult<TData> ErrorResult<TData>(ApiError apiError) where TData : class
        {
            return new ApiResult<TData>
            {
                Error = apiError
            };
        }

        /// <summary>
        /// Deserializes the JSON to the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T FromString<T>(string value) where T : ApiResult
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Creates a new <see cref="IApiResult{TData}"/> from the given object instance.
        /// </summary>
        /// <typeparam name="TData">The type of payload model.</typeparam>
        /// <param name="value">The object instance.</param>
        /// <returns>A new <see cref="IApiResult{TData}"/> representing the response.</returns>
        public static IApiResult<TData> FromObject<TData>(TData value) where TData : class
        {
            return new ApiResult<TData>
            {
                Data = value
            };
        }

        /// <summary>
        /// Serializes the object to a JSON string.
        /// </summary>
        /// <returns></returns>
        public string ToString(Formatting formatting = Formatting.None, JsonSerializerSettings? serializerSettings = null)
        {
            return JsonConvert.SerializeObject(this, formatting, serializerSettings);
        }
    }

    /// <summary>
    /// Represents the response of an endpoint call.
    /// </summary>
    /// <typeparam name="TData">The type of payload model.</typeparam>
    public class ApiResult<TData> : ApiResult, IApiResult<TData> where TData : class
    {
        /// <inheritdoc/>
        [JsonProperty("data", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public TData? Data { get; internal set; }
    }
}
