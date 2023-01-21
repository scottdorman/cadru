//------------------------------------------------------------------------------
// <copyright file="ApiClient.cs"
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

using System.Net.Http;
using System.Net.Http.Headers;

namespace Cadru.ApiClient.Services
{
    public static class ApiClientExtensions
    {
        public const string BearerToken = "Bearer";

        public static TApiClient WithBearerToken<TApiClient>(this TApiClient apiClient, string accessToken)
            where TApiClient : IApiClient
        {
            apiClient.AuthenticationHeaderValue = new AuthenticationHeaderValue(BearerToken, accessToken);
            return apiClient;
        }

        public static void TrySetAuthorization(this HttpRequestMessage requestMessage, IApiClient apiClient)
        {
            requestMessage.Headers.Authorization = apiClient.AuthenticationHeaderValue;
        }
    }
}
