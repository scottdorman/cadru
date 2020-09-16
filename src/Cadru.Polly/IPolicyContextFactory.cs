//------------------------------------------------------------------------------
// <copyright file="IPolicyContextFactory.cs"
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

using System;
using System.Threading.Tasks;

using Polly;

namespace Cadru.Polly
{
    /// <summary>
    /// Represents a set of methods for creating instances of a
    /// <see cref="Context"/> which has an <see cref="IServiceProvider"/> item
    /// automatically added.
    /// </summary>
    public interface IPolicyContextFactory
    {
        /// <summary>
        /// Create a new <see cref="Context"/> instance.
        /// </summary>
        /// <returns>
        /// A new <see cref="Context"/> which has an
        /// <see cref="IServiceProvider"/> item automatically added.
        /// </returns>
        Context Create();

        /// <summary>
        /// Create a new <see cref="Context"/> instance.
        /// </summary>
        /// <returns>
        /// A new <see cref="Context"/> which has an
        /// <see cref="IServiceProvider"/> item automatically added.
        /// </returns>
        Task<Context> CreateAsync();
    }
}