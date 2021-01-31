//------------------------------------------------------------------------------
// <copyright file="GetAssemblyVersion.cs"
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
using System.Reflection;

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Cadru.Build.Tasks
{
    /// <summary>
    /// Generates the value for the
    /// <see cref="AssemblyVersionAttribute.Version"/> property from the
    /// specified NuGet version.
    /// </summary>
    public class GetAssemblyVersion : Task
    {
        /// <summary>
        /// Gets the <see cref="AssemblyVersionAttribute.Version"/> value.
        /// </summary>
        [Output]
        public string? AssemblyVersion { get; private set; }

        /// <summary>
        /// The NuGet version used to derive the assembly version from.
        /// </summary>
        [Required]
        public string? NuGetVersion { get; set; }

        /// <summary>
        /// Main entry point.
        /// </summary>
        public override bool Execute()
        {
            if (!String.IsNullOrWhiteSpace(this.NuGetVersion))
            {
                var args = this.NuGetVersion!.Split('-');
                this.AssemblyVersion = args[0];
                return true;
            }

            return false;
        }
    }
}