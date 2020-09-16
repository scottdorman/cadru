﻿//------------------------------------------------------------------------------
// <copyright file="PolicyContextItems.cs"
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

using Microsoft.Extensions.Logging;

using Polly;

namespace Cadru.Polly
{
    /// <summary>
    /// Standard <see cref="Context"/> item keys.
    /// </summary>
    public static class PolicyContextItems
    {
        /// <summary>
        /// The key for an <see cref="ILogger"/> item.
        /// </summary>
        public static readonly string Logger = $"Logger";

        /// <summary>
        /// The key for an <see cref="IServiceProvider"/> item.
        /// </summary>
        public static readonly string Services = "Services";
    }
}