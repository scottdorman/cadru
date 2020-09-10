//------------------------------------------------------------------------------
// <copyright file="PolicyRegistryExtensions.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2020 Scott Dorman.
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

using Polly;
using Polly.Registry;

namespace Cadru.Polly
{
    /// <summary>
    /// Extension methods for working with a <see cref="IPolicyRegistry{String}"/>.
    /// </summary>
    public static class PolicyRegistryExtensions
    {
        /// <summary>
        /// Adds the specified <paramref name="policy"/> to the registry using
        /// the <see cref="IsPolicy.PolicyKey"/>.
        /// </summary>
        /// <param name="registry">
        /// The <see cref="IPolicyRegistry{String}"/> to add policies to.
        /// </param>
        /// <param name="policy">The <see cref="IsPolicy"/> to add.</param>
        public static void Add(this IPolicyRegistry<string> registry, IsPolicy? policy)
        {
            if (registry != null && policy != null)
            {
                registry.Add(policy.PolicyKey, policy);
            }
        }
    }
}