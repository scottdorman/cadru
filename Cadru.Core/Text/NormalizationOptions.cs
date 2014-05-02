//------------------------------------------------------------------------------
// <copyright file="NormalizationOptions.cs" 
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
namespace Cadru.Text
{
    using System;

    /// <summary>
    /// Options used by the <see cref="Cadru.Extensions.StringExtensions.Clean(string)"/> methods to
    /// determine how to normalize a string.
    /// </summary>
    [Flags]
    public enum NormalizationOptions
    {
        /// <summary>
        /// Do not remove any characters from the string.
        /// </summary>
        None = 0x000,

        /// <summary>
        /// Remove all control characters from the string.
        /// </summary>
        ControlCharacters = 0x002,

        /// <summary>
        /// Remove all white space characters from the beginning 
        /// and end of the string and collapse all internal white
        /// space characters to a single white space character.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Whitespace", Justification = "Both case forms (Whitespace or WhiteSpace) generate this error, so we are choosing one and ignoring the error.")]
        Whitespace = 0x004,

        /// <summary>
        /// Remove all white space and control characters from 
        /// the beginning and end of the string and collapse
        /// all internal white space characters to a single 
        /// white space character.
        /// </summary>
        All = Whitespace | ControlCharacters,
    }
}
