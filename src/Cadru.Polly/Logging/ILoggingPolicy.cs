//------------------------------------------------------------------------------
// <copyright file="ILoggingPolicy.cs"
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

namespace Cadru.Polly.Logging
{
    /// <summary>
    /// Defines properties common to synchronous and asynchronous Logging policies.
    /// </summary>
    public interface ILoggingPolicy : IsPolicy
    {
    }

    /// <summary>
    /// Defines properties common to generic, synchronous and asynchronous Logging policies.
    /// </summary>
    public interface ILoggingPolicy<TResult> : ILoggingPolicy
    {
    }
}