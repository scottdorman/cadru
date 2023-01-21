//------------------------------------------------------------------------------
// <copyright file="MalformedLineException.cs"
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
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

using Cadru.Data.Resources;

namespace Cadru.Data.IO
{
    /// <summary>
    /// Indicates a line cannot be parsed into fields
    /// </summary>
    [Serializable]
    public partial class MalformedLineException : Exception
    {
        private const string LINE_NUMBER_PROPERTY = "LineNumber";

        /// <summary>
        /// Creates a new exception with no properties set
        /// </summary>
        /// <remarks></remarks>
        public MalformedLineException() : base()
        {
        }

        /// <summary>
        /// Creates a new exception, setting Message and LineNumber
        /// </summary>
        /// <param name="message">The message for the exception</param>
        /// <param name="lineNumber">The number of the line that is malformed</param>
        /// <remarks></remarks>
        public MalformedLineException(string message, long lineNumber) : base(message)
        {
            this.LineNumber = lineNumber;
        }

        /// <summary>
        /// Creates a new exception, setting Message
        /// </summary>
        /// <param name="message">The message for the exception</param>
        /// <remarks></remarks>
        public MalformedLineException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new exception, setting Message, LineNumber, and InnerException
        /// </summary>
        /// <param name="message">The message for the exception</param>
        /// <param name="lineNumber">The number of the line that is malformed</param>
        /// <param name="innerException">The inner exception for the exception</param>
        /// <remarks></remarks>
        public MalformedLineException(string message, long lineNumber, Exception innerException) : base(message, innerException)
        {
            this.LineNumber = lineNumber;
        }

        /// <summary>
        /// Creates a new exception, setting Message and InnerException
        /// </summary>
        /// <param name="message">The message for the exception</param>
        /// <param name="innerException">The inner exception for the exception</param>
        /// <remarks></remarks>
        public MalformedLineException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor used for serialization
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected MalformedLineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info is not null)
            {
                this.LineNumber = info.GetInt32(LINE_NUMBER_PROPERTY);
            }
            else
            {
                this.LineNumber = -1;
            }
        }

        /// <summary>
        /// The number of the offending line
        /// </summary>
        /// <value>The line number</value>
        /// <remarks></remarks>
        [EditorBrowsable(EditorBrowsableState.Always)]
        public long LineNumber { get; set; }

        /// <summary>
        /// Supports serialization
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        /// <remarks></remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info?.AddValue(LINE_NUMBER_PROPERTY, this.LineNumber, typeof(long));
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// Appends extra data to string so that it's available when the exception is caught as an Exception
        /// </summary>
        /// <returns>The base ToString plus the Line Number</returns>
        /// <remarks></remarks>
        public override string ToString()
        {
            return base.ToString() + " " + String.Format(Strings.TextFieldParser_MalformedExtraData, this.LineNumber.ToString(CultureInfo.InvariantCulture));
        }
    }
}