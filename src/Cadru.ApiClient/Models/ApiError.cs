using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Cadru.ApiClient.Models
{
    /// <summary>
    /// Represents an error from an endpoint call.
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiError"/> class.
        /// </summary>
        public ApiError()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiError"/> class
        /// with the given information.
        /// </summary>
        /// <param name="id">The unique identifier for the error.</param>
        /// <param name="httpStatus">The HTTP status code of the error.</param>
        /// <param name="description">The description of the error.</param>
        /// <param name="errorDetails">A collection of <see cref="ErrorDetail"/> instances.</param>
        public ApiError(Guid? id, string? httpStatus, string? description, IEnumerable<ErrorDetail> errorDetails) : this()
        {
            this.Id = id;
            this.HttpStatus = httpStatus;
            this.Description = description;
            this.Details = errorDetails;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ApiError"/> class
        /// with the given information.
        /// </summary>
        /// <param name="id">The unique identifier for the error.</param>
        /// <param name="httpStatusCode">The HTTP status code of the error.</param>
        /// <param name="description">The description of the error.</param>
        /// <param name="errorDetails">A collection of <see cref="ErrorDetail"/> instances.</param>
        public ApiError(Guid? id, HttpStatusCode httpStatusCode, string? description, IEnumerable<ErrorDetail>? errorDetails = null) : this()
        {
            this.Id = id;
            this.HttpStatus = httpStatusCode.ToString("d");
            this.Description = description;
            this.Details = errorDetails ?? Enumerable.Empty<ErrorDetail>();
        }

        /// <summary>
        /// The unique identifier for the error.
        /// </summary>
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue("{00000000-0000-0000-0000-000000000000}")]
        public Guid? Id { get; internal set; }

        /// <summary>
        /// The HTTP status code of the error.
        /// </summary>
        [JsonProperty("statuscode", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string? HttpStatus { get; internal set; }

        /// <summary>
        /// The description of the error.
        /// </summary>

        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string? Description { get; internal set; }

        /// <summary>
        /// A collection of <see cref="ErrorDetail"/> instances.
        /// </summary>
        [JsonProperty("errordetails", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<ErrorDetail> Details { get; internal set; } = Enumerable.Empty<ErrorDetail>();
    }
}
