//------------------------------------------------------------------------------
// <copyright file="AddReleaseNotesRootEntry.cs"
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
using System.Linq;
using System.Xml.Linq;

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Cadru.Build.Tasks
{
    /// <summary>
    /// Adds a release notes root entry to the specified XML file.
    /// </summary>
    /// <remarks>
    /// This takes a fairly opinionated view of what a release notes file should
    /// look like and may not be suitable for everyone.
    /// </remarks>
    public class AddReleaseNotesRootEntry : Task
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not the root node should
        /// be added if it isn't found.
        /// </summary>
        [Required]
        public bool AddIfNotFound { get; set; }

        /// <summary>
        /// The build date.
        /// </summary>
        /// <remarks>
        /// This can be the <see cref="GetVersionProperties.BuildDate"/> property value.
        /// </remarks>
        [Required]
        public string BuildDate { get; set; }

        /// <summary>
        /// The release notes XML file to be updated.
        /// </summary>
        [Required]
        public ITaskItem File { get; set; }

        /// <summary>
        /// An optional milestone value to include in the release notes entry.
        /// </summary>
        public string Milestone { get; set; }

        /// <summary>
        /// The build version.
        /// </summary>
        [Required]
        public string Version { get; set; }

        /// <summary>
        /// Main entry point.
        /// </summary>
        public override bool Execute()
        {
            var document = XDocument.Load(this.File.ItemSpec);
            var entry = document.Root.Descendants("entry").SingleOrDefault(e => e.Attribute("version").Value == "vNext" || String.IsNullOrEmpty(e.Attribute("version").Value) || e.Attribute("version").Value == this.Version);
            if (entry != null)
            {
                this.Log.LogMessage("Build entry found; updating attributes.");
                entry.Attribute("version").Value = this.Version;
                entry.Attribute("build-date").Value = this.BuildDate;

                if (!String.IsNullOrWhiteSpace(this.Milestone))
                {
                    var milestoneAttribute = entry.Attribute("milestone");
                    if (milestoneAttribute != null)
                    {
                        milestoneAttribute.Value = this.Milestone;
                    }
                }

                document.Save(this.File.ItemSpec);
            }
            else
            {
                this.Log.LogMessage("Build entry not found.");
                if (this.AddIfNotFound)
                {
                    this.Log.LogMessage("Adding new build entry.");
                    entry = new XElement("entry",
                        new XAttribute("build-date", this.BuildDate),
                        new XAttribute("version", this.Version),
                        new XElement("content",
                            new XElement("ul")));

                    if (!String.IsNullOrWhiteSpace(this.Milestone))
                    {
                        entry.Add(new XAttribute("milestone", this.Milestone));
                    }

                    var commentTemplate = document.DescendantNodes().OfType<XComment>().FirstOrDefault();
                    if (commentTemplate != null)
                    {
                        commentTemplate.AddAfterSelf(entry);
                    }
                    else
                    {
                        document.Root.AddFirst(entry);
                    }

                    document.Save(this.File.ItemSpec);
                }
            }

            return true;
        }
    }
}