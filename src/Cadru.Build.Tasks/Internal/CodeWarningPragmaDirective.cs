//------------------------------------------------------------------------------
// <copyright file="CodeWarningPragmaDirective.cs"
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
using System.CodeDom;
using System.Collections.Generic;

namespace Cadru.Build.Tasks.Internal
{
    public enum WarningPragmaMode
    {
        None = 0,
        Disable = 1,
        Restore = 2,
    }

    /// <summary>
    /// Represents a code warning pragma code entity
    /// </summary>
    public class CodeWarningPragmaDirective : CodeDirective
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="CodeWarningPragmaDirective"/> class.
        /// </summary>
        public CodeWarningPragmaDirective()
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="CodeWarningPragmaDirective"/> class, specifying its mode
        /// and warning list.
        /// </summary>
        /// <param name="mode">One of the <see cref="WarningPragmaMode"/> values.</param>
        /// <param name="warningList">The list of warnings.</param>
        public CodeWarningPragmaDirective(WarningPragmaMode mode, IList<string> warningList)
        {
            this.WarningList = (IReadOnlyList<string>)warningList;
            this.WarningPragmaMode = mode;
        }

        /// <summary>
        /// Gets the list of warnings for the pragma directive.
        /// </summary>
        public IReadOnlyList<string> WarningList { get; } = new List<string>();

        /// <summary>
        /// Gets the mode for the pragma directive.
        /// </summary>
        public WarningPragmaMode WarningPragmaMode { get; set; }

        /// <summary>
        /// Generates the pragma directive as a line of code for the specified language.
        /// </summary>
        /// <param name="language">The code language</param>
        /// <returns>A string representing the pragma directive.</returns>
        /// <remarks>
        /// <para>
        /// The <see href="https://docs.microsoft.com/en-us/dotnet/framework/reflection-and-codedom/using-the-codedom">CodeDOM</see>
        /// compilers do not work with third-party <see cref="CodeDirective"/> derived classes.
        /// As a result, it is necessary to create the warning pragma as a raw line of code.
        /// </para>
        /// </remarks>
        public string ToPragmaString(string language)
        {
            var text = String.Empty;
            switch (language)
            {
                case "C#" when this.WarningPragmaMode == WarningPragmaMode.Disable:
                    text = $"#pragma warning disable { String.Join(",", this.WarningList) }";
                    break;

                case "C#" when this.WarningPragmaMode == WarningPragmaMode.Restore:
                    text = $"#pragma warning restore { String.Join(",", this.WarningList) }";
                    break;

                case "VisualBasic" when this.WarningPragmaMode == WarningPragmaMode.Disable:
                    text = $"#Disable Warning { String.Join(",", this.WarningList) }";
                    break;

                case "VisualBasic" when this.WarningPragmaMode == WarningPragmaMode.Restore:
                    text = $"#Enable Warning { String.Join(",", this.WarningList) }";
                    break;
            }

            return text;
        }
    }
}