using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Cadru.AspNetCore.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Cadru.AspNetCore.Http.Internal
{
    internal static class Extensions
    {
        private const string Location = "Location";

        public static string GetRawTarget(this HttpRequest request)
        {
            var httpContext = request.HttpContext;
            var requestFeature = httpContext.Features.Get<IHttpRequestFeature>();
            return requestFeature.RawTarget;
        }

        public static async Task<string> ReadToEndAsync(this MemoryStream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            string result;
            using (var reader = new StreamReader(stream))
            {
                result = await reader.ReadToEndAsync();
            }

            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public static async Task<string> SerializeHttpResponseAsync(this HttpResponse response, MemoryStream responseStream)
        {
            string? result;

            try
            {
                var data = new StringBuilder(String.Format(Strings.Debugging_HttpMessage_Response, response.StatusCode));
                if (response.Headers.TryGetValue(Location, out var location))
                {
                    data.AppendFormat(Strings.Debugging_HttpMessages_Location, location.ToString());
                }

                var originalBody = response.Body;
                if (originalBody != null)
                {
                    response.Body = responseStream;
                    await responseStream.CopyToAsync(originalBody);
                    data.Append(await ReadToEndAsync(responseStream));
                    response.Body = originalBody;
                }

                result = data.ToString();
            }
            catch (Exception e)
            {
                result = e.ToString();
            }

            return result ?? Strings.Debugging_HttpMessages_EmptyResponse;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public static async Task<string> SerializeHttpResponseMessageAsync(this HttpResponseMessage? response)
        {
            string? result = null;
            if (response != null)
            {
                try
                {
                    var data = new StringBuilder(String.Format(Strings.Debugging_HttpMessage_Response, response.StatusCode, (int)response.StatusCode));
                    if (response.Headers.Location != null)
                    {
                        data.AppendFormat(Strings.Debugging_HttpMessages_Location, response.Headers.Location);
                    }

                    if (response.Content != null)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        if (!String.IsNullOrWhiteSpace(responseBody))
                        {
                            data.AppendFormat(Strings.Debugging_HttpMessages_Body, responseBody);
                        }
                    }

                    result = data.ToString();
                }
                catch (Exception e)
                {
                    result = e.ToString();
                }
            }

            return result ?? Strings.Debugging_HttpMessages_EmptyResponse;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public static string SerializeHttpRequest(this HttpRequest request)
        {
            string result;
            try
            {
                var data = new StringBuilder($"{request.Method} {request.GetRawTarget()} ");
                data.AppendFormat(Strings.Debugging_HttpMessages_ContentType, request.ContentType);
                result = data.ToString();
            }
            catch (Exception e)
            {
                result = e.ToString();
            }

            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public static string SerializeHttpRequestMessage(this HttpRequestMessage request)
        {
            string result;
            try
            {
                var data = new StringBuilder($"{request.Method} {request.RequestUri} ");
                if (request.Content?.Headers.ContentType != null)
                {
                    data.AppendFormat(Strings.Debugging_HttpMessages_ContentType, request.Content?.Headers.ContentType);
                }

                result = data.ToString();
            }
            catch (Exception e)
            {
                result = e.ToString();
            }

            return result;
        }
    }
}
