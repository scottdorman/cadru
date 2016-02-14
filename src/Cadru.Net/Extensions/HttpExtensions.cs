using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Cadru.Net.Extensions
{
    /// <summary>
    /// Provides basic routines for common HTTPClient related manipulation.
    /// </summary>
    public static class HttpExtensions
    {
        /// <summary>
        /// Throws an exception if the <see cref="System.Net.Http.HttpResponseMessage.IsSuccessStatusCode"/>
        /// property for the HTTP response is false.
        /// </summary>
        /// <param name="responseMessage">The <see cref="System.Net.Http.HttpResponseMessage"/></param>
        /// <returns>he HTTP response message if the call is successful.</returns>
        [CLSCompliant(false)]
        public static HttpResponseMessage EnsureSuccessStatusCodeWithData(this HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                if (responseMessage.Content != null)
                {
                    responseMessage.Content.Dispose();
                }

                var exception = new HttpRequestException(String.Format(CultureInfo.InvariantCulture, Resources.Strings.net_http_message_not_success_statuscode, (int)responseMessage.StatusCode, responseMessage.ReasonPhrase));
                exception.Data.Add("Status", responseMessage.StatusCode);
                exception.Data.Add("ReasonPhrase", responseMessage.ReasonPhrase);

                throw exception;
            }

            return responseMessage;
        }
    }
}
