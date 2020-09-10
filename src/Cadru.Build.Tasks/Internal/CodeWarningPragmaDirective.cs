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

    public class CodeWarningPragmaDirective : CodeDirective
    {
        public CodeWarningPragmaDirective()
        {
        }

        public CodeWarningPragmaDirective(WarningPragmaMode mode, IList<string> warningList)
        {
            this.WarningList = warningList;
            this.WarningPragmaMode = mode;
        }

        public IList<string> WarningList { get; } = new List<string>();

        public WarningPragmaMode WarningPragmaMode { get; set; }

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