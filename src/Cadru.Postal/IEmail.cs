//------------------------------------------------------------------------------
// <copyright file="IEmail.cs"
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

using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cadru.Postal
{
    /// <summary>
    /// An Email object has the name of the MVC view to render and a view data
    /// dictionary to store the data to render. It is best used as a dynamic
    /// object, just like the ViewBag property of a Controller. Any dynamic
    /// property access is mapped to the view data dictionary.
    /// </summary>
    public interface IEmail
    {
        /// <summary>
        /// Gets or sets the name of the area containing the email template.
        /// </summary>
        string AreaName { get; set; }

        /// <summary>
        /// Gets the attachments to send with the email.
        /// </summary>
        List<Attachment> Attachments { get; }

        /// <summary>
        /// Gets the <see cref="ImageEmbedder"/> instance used to the inline
        /// images in the rendered view.
        /// </summary>
        ImageEmbedder ImageEmbedder { get; }

        /// <summary>
        /// Gets or sets the view data to pass to the view.
        /// </summary>
        ViewDataDictionary ViewData { get; set; }

        /// <summary>
        /// Gets or sets the name of the view containing the email template.
        /// </summary>
        string ViewName { get; set; }

        /// <summary>
        /// Adds an attachment to the email.
        /// </summary>
        /// <param name="attachment">The attachment to add.</param>
        void Attach(Attachment attachment);

        /// <summary>
        /// Convenience method that saves this email asynchronously via a
        /// default EmailService.
        /// </summary>
        Task SaveToFileAsync(string path);

        /// <summary>
        /// Convenience method that sends this email asynchronously via a
        /// default EmailService.
        /// </summary>
        Task SendAsync();
    }
}