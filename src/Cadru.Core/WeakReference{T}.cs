//------------------------------------------------------------------------------
// <copyright file="WeakReference{T}.cs" 
//  company="Scott Dorman" 
//  library="Cadru">
//    Copyright (C) 2001-2014 Scott Dorman.
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

namespace Cadru
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Represents a weak reference, which references an object while still allowing
    /// that object to be reclaimed by garbage collection.
    /// </summary>
    /// <typeparam name="T">The type of object referenced.</typeparam>
    public class WeakReference<T> : WeakReference
        where T : class
    {
        #region fields
        #endregion

        #region events
        #endregion

        #region constructors

        #region WeakReference(T target)
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakReference{T}"/> class, referencing
        /// the specified object.
        /// </summary>
        /// <param name="target">An object to track or <see langword="null"/>.</param>
        public WeakReference(T target)
            : base(target)
        {
        }
        #endregion

        #region WeakReference(T target, bool trackResurrection)
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakReference{T}"/> class, referencing
        /// the specified object and using the specified resurrection tracking.
        /// </summary>
        /// <param name="target">An object to track.</param>
        /// <param name="trackResurrection">Indicates when to stop tracking the object. 
        /// If <see langword="true"/>, the object is tracked after finalization; if 
        /// <see langword="false"/>, the object is only tracked until finalization.
        /// </param>
        public WeakReference(T target, bool trackResurrection)
            : base(target, trackResurrection)
        {
        }
        #endregion

        #endregion

        #region properties

        #region Target
        /// <summary>
        /// Gets or sets the object (the target) referenced by the current
        /// <see cref="WeakReference{T}"/> object.
        /// </summary>
        /// <value><see langword="null"/> if the object referenced by the current
        /// <see cref="WeakReference{T}"/> object has been garbage collected;
        /// otherwise, a reference to the object referenced by the current
        /// <see cref="WeakReference{T}"/> object.</value>
        /// <exception cref="InvalidOperationException">
        /// The reference to the target object is invalid. This exception can be thrown
        /// while setting this property if the value is a null reference or if the object
        /// has been finalized during the set operation.
        /// </exception>
        public new T Target
        {
            get
            {
                return (T)base.Target;
            }

            set
            {
                base.Target = value;
            }
        }
        #endregion

        #endregion

        #region methods
        #endregion
    }
}
