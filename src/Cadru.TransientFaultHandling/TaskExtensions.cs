//------------------------------------------------------------------------------
// <copyright file="TaskExtensions.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2016 Scott Dorman.
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cadru.Extensions
{
    public static class TaskExtensions
    {
        public static Task Delay(TimeSpan delay)
        {
#if SL50 || NET40  || PCL
            var tcs = new TaskCompletionSource<object>();
            new Timer(_ => tcs.SetResult(null), null, 0, Timeout.Infinite).Change(delay, TimeSpan.FromMilliseconds(Timeout.Infinite));
            return tcs.Task;
#else
            return Task.Delay(delay);
#endif
        }
    }
}
