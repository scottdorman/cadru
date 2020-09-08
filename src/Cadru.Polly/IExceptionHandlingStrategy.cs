﻿//------------------------------------------------------------------------------
// <copyright file="ITransientErrorDetectionStrategy.cs"
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
using System.Diagnostics.CodeAnalysis;

namespace Cadru.Polly
{
    /// <summary>
    /// Defines an interface that must be implemented by custom components responsible for detecting specific transient conditions.
    /// </summary>
    public interface IExceptionHandlingStrategy
    {
        /// <summary>
        /// Gets a value indicating whether or not this is the default exception
        /// handling strategy.
        /// </summary>
        bool IsDefaultStrategy { get; }

        /// <summary>
        /// Determines whether the specified exception represents a transient failure that can be compensated by a retry.
        /// </summary>
        /// <param name="exception">The exception object to be verified.</param>
        /// <returns>true if the specified exception is considered as transient; otherwise, false.</returns>
        bool ShouldHandle([NotNull] Exception exception);
    }
}
