//------------------------------------------------------------------------------
// <copyright file="EventArgs{T}.cs"
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
using System.Runtime.Serialization;

namespace Cadru
{
    /// <summary>
    /// <see cref="EventArgs{T}"/> is the base class for classes containing
    /// event data.
    /// </summary>
    /// <typeparam name="T">The type of the event data.</typeparam>
    [DataContract]
    public class EventArgs<T> : EventArgs
    {
        private readonly T data;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventArgs{T}"/> class.
        /// </summary>
        /// <param name="data">The event data.</param>
        public EventArgs(T data)
        {
            this.data = data;
        }

        /// <summary>
        /// Gets the event data.
        /// </summary>
        /// <value>The event data.</value>
        [DataMember]
        public T Data => this.data;
    }
}