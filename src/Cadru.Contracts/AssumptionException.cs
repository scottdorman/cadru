//------------------------------------------------------------------------------
// <copyright file="AssumptionException.cs" 
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

namespace Cadru.Contracts
{
    using System;
    using System.Runtime.Serialization;
 

    /// <summary>
    /// The exception that is thrown when an assumption fails.
    /// </summary>
    [DataContract]
    public sealed class AssumptionException : Exception
    {
        #region fields
        #endregion

        #region constructors

        #region AssumptionException()
        /// <summary>
        /// Initializes a new instance of the <see cref="AssumptionException"/> class.
        /// </summary>
        public AssumptionException()
            : base(Resources.Strings.AssumptionException_EmptyMessage)
        {
        }
        #endregion

        #region AssumptionException(string message)
        /// <summary>
        /// Initializes a new instance of the <see cref="AssumptionException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AssumptionException(string message)
            : base(message)
        {
        }
        #endregion

        #region AssumptionException(string message, Exception inner)
        /// <summary>
        /// Initializes a new instance of the <see cref="AssumptionException"/> class with a specified
        /// error message and a reference to the inner exception that is the cause of
        /// this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a <see langword="null"/> if no inner exception is specified.</param>
        public AssumptionException(string message, Exception inner)
            : base(message, inner)
        {
        }
        #endregion

        #endregion

        #region events
        #endregion

        #region properties
        #endregion

        #region methods
        #endregion
    }
}
