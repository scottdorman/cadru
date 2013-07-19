//------------------------------------------------------------------------------
// <copyright file="AssumptionException.cs" 
//  company="Scott Dorman" 
//  library="Cadru">
//    Copyright (C) 2001-2013 Scott Dorman.
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

namespace Cadru.UnitTest.Framework.Internal.Contracts
{
    using System;
    using System.Runtime.Serialization;
    using Cadru.UnitTest.Framework.Properties;

    /// <summary>
    /// The exception that is thrown when an assumption fails.
    /// </summary>
    [Serializable]
    public sealed class AssumptionException : Exception
    {
        #region events
        #endregion

        #region class-wide fields
        #endregion

        #region constructors

        #region AssumptionException()
        /// <summary>
        /// Initializes a new instance of the <see cref="AssumptionException"/> class.
        /// </summary>
        public AssumptionException()
            : base(Resources.AssumptionException_EmptyMessage)
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

        #region AssumptionException(SerializationInfo info, StreamingContext context)
        /// <summary>
        /// Initializes a new instance of the <see cref="AssumptionException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        /// <exception cref="System.ArgumentNullException">The info parameter is null.</exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException">The class name is <see langword="null"/> or System.Exception.HResult is zero (0).</exception>
        private AssumptionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endregion

        #endregion

        #region private and internal properties and methods

        #region properties
        #endregion

        #region methods
        #endregion

        #endregion

        #region public and protected properties and methods

        #region properties
        #endregion

        #region methods
        #endregion

        #endregion
    }
}
