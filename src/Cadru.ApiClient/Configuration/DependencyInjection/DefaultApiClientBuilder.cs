//------------------------------------------------------------------------------
// <copyright file="DefaultApiClientBuilder.cs"
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

using Microsoft.Extensions.DependencyInjection;

using Cadru.ApiClient.Services;

namespace Cadru.ApiClient.Configuration.DependencyInjection
{
    internal sealed class DefaultApiClientBuilder : IApiClientBuilder
    {
        public DefaultApiClientBuilder(IServiceCollection services, string name, IHttpClientBuilder httpClientBuilder)
        {
            this.HttpClientBuilder = httpClientBuilder;
            this.Services = services;
            this.Name = name;
        }

        public string Name { get; }

        public IApiClient? ClientImplementation {get; init; }
        public IHttpClientBuilder HttpClientBuilder { get; }
        public IServiceCollection Services { get; }
    }
}
