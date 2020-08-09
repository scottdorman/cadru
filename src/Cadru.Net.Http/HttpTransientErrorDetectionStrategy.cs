//------------------------------------------------------------------------------
// <copyright file="HttpTransientErrorDetectionStrategy.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2017 Scott Dorman.
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

namespace Cadru.Net.Http
{
    using System;
    using System.Net;
    using System.Net.Http;
    using Cadru.Extensions;
    using Cadru.TransientFaultHandling.DetectionStrategies;

    /// <summary>
    /// Represents a strategy that determines whether or not a given <see cref="HttpRequestException"/> should
    /// be considered as a transient error.
    /// </summary>ry>
    public class HttpTransientErrorDetectionStrategy : ITransientErrorDetectionStrategy
    {
        private bool IsTransientWebException(HttpRequestException httpException)
        {
            var isTransient = false;

            if (httpException.InnerException is WebException webException)
            {
                isTransient = true;

                // The PCL version of WebExceptionStatus does not include
                // all of the possible values, so we're switching on
                // the raw integer values instead.
                switch ((int)webException.Status)
                {
                    case 1: // NameResolutionFailure
                    case 2: // ConnectFailure
                    case 3: // RecieveFailure
                    case 4: // SendFailure
                    case 14: // Timeout
                    case 15: // ProxyNameResolutionFailure
                        isTransient = true;
                        break;

                    default:
                        isTransient = false;
                        break;
                }
            }

            return isTransient;
        }

        private bool IsTransientHttpException(HttpRequestException exception)
        {
            var isTransient = false;
            if (exception.Data.Count > 0)
            {
                var statusCode = exception.Data.GetValueOrDefault("Status", (HttpStatusCode)0);
                switch (statusCode)
                {
                    case HttpStatusCode.GatewayTimeout:
                    case HttpStatusCode.InternalServerError:
                    case HttpStatusCode.RequestTimeout:
                    case HttpStatusCode.ServiceUnavailable:
                        isTransient = true;
                        break;

                    default:
                        isTransient = false;
                        break;
                }
            }

            return isTransient;
        }

        /// <summary>
        /// Determines whether the specified exception represents a transient failure that can be compensated by a retry.
        /// </summary>
        /// <param name="ex">The exception object to be verified.</param>
        /// <returns>true if the specified exception is considered as transient; otherwise, false.</returns>
        public bool IsTransient(Exception ex)
        {
            var isTransient = false;
            if (ex is HttpRequestException httpException)
            {
                isTransient = IsTransientWebException(httpException) || IsTransientHttpException(httpException);
            }

            return isTransient;
        }
    }
}