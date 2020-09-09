//------------------------------------------------------------------------------
// <copyright file="IFileInfoExtensions.cs"
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
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

using Microsoft.Extensions.FileProviders;

namespace Cadru.Extensions.FileProviders
{
    /// <summary>
    /// Extension methods for working with <see cref="IFileInfo"/> instances.
    /// </summary>
    public static class IFileInfoExtensions
    {
        private static readonly string fqdn = GetFullyQualifiedDomainName();

        /// <summary>
        /// Opens the file, reads all the text in the file into a string, and then closes the file.
        /// </summary>
        /// <param name="fileInfo">The <see cref="IFileInfo"/> representing the file.</param>
        /// <returns>A string containing all the text in the file.</returns>
        public static async Task<string> ReadAllText(this IFileInfo fileInfo)
        {
            using (var streamReader = new StreamReader(fileInfo.CreateReadStream()))
            {
                return await streamReader.ReadToEndAsync();
            }
        }

        /// <summary>
        /// Creates an <see cref="ExtendedFileInfo"/> wrapper around the given <see cref="IFileInfo"/>.
        /// </summary>
        /// <param name="fileInfo">The <see cref="IFileInfo"/> representing the file.</param>
        /// <returns>An <see cref="ExtendedFileInfo"/> wrapper around the given <paramref name="fileInfo"/>.</returns>
        public static ExtendedFileInfo ToExtendedFileInfo(this IFileInfo fileInfo)
        {
            return new ExtendedFileInfo(fileInfo);
        }

        /// <summary>
        /// Returns the path as a uniform resource identifier (URI).
        /// </summary>
        /// <param name="fileInfo">The <see cref="IFileInfo"/> representing the file.</param>
        /// <returns>A <see cref="Uri"/> representing the path.</returns>
        public static Uri ToUri(this IFileInfo fileInfo)
        {
            return new Uri(String.Concat(@"//", fqdn, @"/", fileInfo.PhysicalPath.Substring(Path.GetPathRoot(fileInfo.PhysicalPath).Length)));
        }

        private static string GetFullyQualifiedDomainName()
        {
            var domain = IPGlobalProperties.GetIPGlobalProperties().DomainName;
            var hostName = Dns.GetHostName();
            if (!String.IsNullOrWhiteSpace(domain))
            {
                hostName += "." + domain;
            }

            return hostName;
        }

    }
}