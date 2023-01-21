using System;
using System.Collections.Generic;
using System.Text;

using Cadru.ApiClient.Resources;

namespace Cadru.ApiClient.Models
{
    partial class ApiError
    {
        /// <summary>
        /// Represents an <see cref="ApiError"/> with no content.
        /// </summary>
        public static IApiError Empty => new ApiError();

        /// <summary>
        /// An <see cref="ApiError"/> representing an unexpected error condition.
        /// </summary>
        public static IApiError Unexpected => 
            new ApiError(Guid.Empty, System.Net.HttpStatusCode.InternalServerError, errorCode: null, description: Strings.Error_UnexpectedApiError);
    }
}
