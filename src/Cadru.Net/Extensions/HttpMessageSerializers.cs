using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Cadru.Net.Extensions
{
    public static class HttpMessageSerializers
    {
        public static async Task<string> SerializeHttpResponseMessageAsync(this HttpResponseMessage response)
        {
            string result;

            try
            {
                var jsonData = new
                {
                    response.StatusCode,
                    response.ReasonPhrase,
                    response.Version,
                    Headers = new List<string>(),
                    TrailingHeaders = new List<string>(),
                    Content = new
                    {
                        Headers = new List<string>(),
                        Data = await response.Content?.TryDeserializeContent()
                    }
                };

                foreach (var header in response.Headers)
                {
                    foreach (var headerValue in header.Value)
                    {
                        if (header.Key == "Location")
                        {
                            jsonData.Headers.Add($"{ header.Key } = { WebUtility.UrlDecode(headerValue) }");

                        }
                        else
                        {
                            jsonData.Headers.Add($"{ header.Key } = { headerValue }");
                        }
                    }
                }

                if (response.Content != null)
                {
                    foreach (var header in response.Content.Headers)
                    {
                        foreach (var headerValue in header.Value)
                        {
                            jsonData.Content.Headers.Add($"{ header.Key } = { headerValue }");
                        }
                    }
                }

                result = JsonConvert.SerializeObject(jsonData);
            }
            catch (Exception e)
            {
                result = e.ToString();
            }

            return result;
        }

        private static async Task<object> TryDeserializeContent(this HttpContent httpContent)
        {
            object content = String.Empty;
            if (httpContent != null)
            {
                var data = await httpContent.ReadAsStringAsync();
                try
                {
                    content = JsonConvert.DeserializeObject(data);
                }
                catch
                {
                    content = data;
                }
            }

            return content;
        }

        public static async Task<string> SerializeHttpRequestMessageAsync(this HttpRequestMessage request)
        {
            string result;
            try
            {
                var jsonData = new
                {
                    HttpMethod = request.Method.ToString(),
                    RequestUri = request.RequestUri.ToString(),
                    request.Version,
                    Headers = new List<string>(),
                    Properties = new List<string>(),
                    Content = new
                    {
                        Headers = new List<string>(),
                        Data = await request?.Content.TryDeserializeContent()
                    }
                };

                foreach (var header in request.Headers)
                {
                    foreach (var headerValue in header.Value)
                    {
                        if (header.Key == "Location")
                        {
                            jsonData.Headers.Add($"{ header.Key } = { WebUtility.UrlDecode(headerValue) }");

                        }
                        else
                        {
                            jsonData.Headers.Add($"{ header.Key } = { headerValue }");
                        }
                    }
                }

                foreach (var prop in request.Properties)
                {
                    jsonData.Properties.Add($"{ prop.Key } = { prop.Value }");
                }

                if (request.Content != null)
                {
                    foreach (var header in request.Content.Headers)
                    {
                        foreach (var headerValue in header.Value)
                        {
                            jsonData.Content.Headers.Add($"{ header.Key } = { headerValue }");
                        }
                    }
                }

                result = JsonConvert.SerializeObject(jsonData);
            }
            catch (Exception e)
            {
                result = e.ToString();
            }

            return result;
        }
    }
}