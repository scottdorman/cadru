//------------------------------------------------------------------------------
// <copyright file="StopwatchTimer.cs"
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
using System.Diagnostics;

namespace Cadru.Diagnostics
{
    /// <summary>
    /// Provides a set of methods and properties that you can use to accurate measure
    /// elapsed time using the scope syntax provided by the <c>using</c> statement.
    /// </summary>
    public sealed class StopwatchTimer : IDisposable
    {
        private readonly Action<Stopwatch>? stopAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="StopwatchTimer"/> class.
        /// </summary>
        /// <param name="startAction">An optional action to run when the <see cref="Stopwatch"/> is started.</param>
        /// <param name="stopAction">An optional action to run when the <see cref="Stopwatch"/> is stopped.</param>
        public StopwatchTimer(Action<Stopwatch>? startAction = null, Action<Stopwatch>? stopAction = null)
        {
            this.stopAction = stopAction;
            this.Stopwatch = Stopwatch.StartNew();
            startAction?.Invoke(this.Stopwatch);
        }

        /// <summary>
        /// Gets the <see cref="Stopwatch"/> instance.
        /// </summary>
        public Stopwatch Stopwatch { get; }

        /// <summary>
        /// Stops the internal <see cref="Stopwatch"/> and performs
        /// the stop action, if it was provided.
        /// </summary>
        public void Dispose()
        {
            this.Stopwatch.Stop();
            this.stopAction?.Invoke(this.Stopwatch);
        }
    }
}