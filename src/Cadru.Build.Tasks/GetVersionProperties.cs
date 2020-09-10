//------------------------------------------------------------------------------
// <copyright file="GetVersionProperties.cs"
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
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Cadru.Build.Tasks
{
    public enum VersionStrategy
    {
        ShortDate = 0,
        DayOfYear,
        VisualStudio
    }

    public class GetVersionProperties : Task
    {
        private const int VersionBaseShortDate = 19000;
        private readonly DateTime VisualStudioBaseDate = new DateTime(2000, 1, 1);

        [Output]
        public int Build { get; private set; }

        [Output]
        public string BuildDate { get; private set; }

        [Required]
        public ITaskItem PropertiesFile { get; set; }

        [Output]
        public int Revision { get; private set; }

        public string Strategy { get; set; } = VersionStrategy.ShortDate.ToString();

        public override bool Execute()
        {
            var now = DateTimeOffset.UtcNow;
            this.BuildDate = now.ToString();

            Enum.TryParse<VersionStrategy>(this.Strategy, out var versionStrategy);

            switch (versionStrategy)
            {
                case VersionStrategy.ShortDate:
                    this.ShortDateStrategy(now);
                    break;

                case VersionStrategy.VisualStudio:
                    this.VisualStudioStrategy(now);
                    break;

                default:
                case VersionStrategy.DayOfYear:
                    this.DayOfYearStrategy(now);
                    break;
            }

            XDocument document;

            if (!File.Exists(this.PropertiesFile.ItemSpec))
            {
                document = new XDocument(new XComment("This file may be overwritten by automation."),
                    new XElement("Project",
                        new XElement("PropertyGroup",
                            new XElement("BuildDate", this.BuildDate),
                            new XElement("VersionBuild", this.Build),
                            new XElement("VersionRevision", this.Revision))));
            }
            else
            {
                document = XDocument.Load(this.PropertiesFile.ItemSpec);
                document.Root.XPathSelectElement("//BuildDate").SetValue(this.BuildDate);
                document.Root.XPathSelectElement("//VersionBuild").SetValue(this.Build);
                document.Root.XPathSelectElement("//VersionRevision").SetValue(this.Revision);
            }

            document.Save(this.PropertiesFile.ItemSpec);
            return true;
        }

        private void DayOfYearStrategy(DateTimeOffset now)
        {
            this.Build = Int32.Parse(String.Format("{0}{1}", now.Year % 100, now.DayOfYear));
            this.Revision = ((int)((now.Date - now).TotalSeconds / 2));
        }

        private void ShortDateStrategy(DateTimeOffset now)
        {
            var shortDate = ((now.Year % 100) * 1000 + 50 * now.Month + now.Day);
            this.Build = (shortDate % 100) * 100 + now.Year % 100;
            this.Revision = (shortDate - VersionBaseShortDate) * 100 + now.Millisecond;

            this.Build = shortDate;
            this.Revision = (((shortDate + VersionBaseShortDate) * 100) + (int)(now - now.Date).TotalSeconds / 2) % 50000;
        }

        private void VisualStudioStrategy(DateTimeOffset now)
        {
            this.Build = (this.VisualStudioBaseDate - now).Days;
            this.Revision = ((int)((now.Date - now).TotalSeconds / 2));
        }
    }
}