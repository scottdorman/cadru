//------------------------------------------------------------------------------
// <copyright file="ParseErrorEventArgs.cs"
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

namespace Cadru.Data.Csv
{
    /// <summary>
    /// Provides data for the <see cref="M:CsvReader.ParseError" /> event.
    /// </summary>
    public class ParseErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Contains the error that occured.
        /// </summary>
        private readonly MalformedCsvException _error;

        /// <summary>
        /// Contains the action to take.
        /// </summary>
        private ParseErrorAction _action;

        /// <summary>
        /// Initializes a new instance of the ParseErrorEventArgs class.
        /// </summary>
        /// <param name="error">The error that occured.</param>
        /// <param name="defaultAction">The default action to take.</param>
        public ParseErrorEventArgs(MalformedCsvException error, ParseErrorAction defaultAction)
        {
            this._error = error;
            this._action = defaultAction;
        }

        /// <summary>
        /// Gets or sets the action to take.
        /// </summary>
        /// <value>The action to take.</value>
        public ParseErrorAction Action
        {
            get { return this._action; }
            set { this._action = value; }
        }

        /// <summary>
        /// Gets the error that occured.
        /// </summary>
        /// <value>The error that occured.</value>
        public MalformedCsvException Error => this._error;
    }
}