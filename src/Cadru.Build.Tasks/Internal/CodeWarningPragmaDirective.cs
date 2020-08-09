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
            string text = String.Empty;
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
