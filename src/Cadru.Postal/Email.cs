//------------------------------------------------------------------------------
// <copyright file="Email.cs"
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
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cadru.Contracts;

namespace Cadru.Postal
{
    /// <summary>
    /// An Email object has the name of the MVC view to render and a view data dictionary
    /// to store the data to render. It is best used as a dynamic object, just like the
    /// ViewBag property of a Controller. Any dynamic property access is mapped to the
    /// view data dictionary.
    /// </summary>
    public class Email : DynamicObject, IViewDataContainer, IEmail
    {
        /// <summary>
        /// Creates a new Email, that will render the given view.
        /// </summary>
        /// <param name="viewName">The name of the view to render</param>
        public Email(string viewName)
        {
            Requires.NotNullOrWhiteSpace(viewName, nameof(viewName));

            this.ViewName = viewName;
            this.Attachments = new List<Attachment>();
            this.ViewData = new ViewDataDictionary(this);
            this.ImageEmbedder = new ImageEmbedder();
        }

        /// <summary>
        /// Creates a new Email, that will render the given view.
        /// </summary>
        /// <param name="viewName">The name of the view to render</param>
        /// <param name="areaName">The name of the area containing the view to render</param>
        public Email(string viewName, string areaName)
            : this(viewName)
        {
            this.AreaName = areaName;
        }

        /// <summary>Create an Email where the ViewName is derived from the name of the class.</summary>
        /// <remarks>Used when defining strongly typed Email classes.</remarks>
        protected Email() : this(DeriveViewNameFromClassName())
        {
        }

        /// <summary>
        /// Gets or sets the name of the area containing the email template.
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// Gets or sets the name of the view containing the email template.
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// Gets or sets the view data to pass to the view.
        /// </summary>
        public ViewDataDictionary ViewData { get; set; }

        /// <summary>
        /// Gets the attachments to send with the email.
        /// </summary>
        public List<Attachment> Attachments { get; }

        /// <summary>
        /// Gets the <see cref="ImageEmbedder"/> instance used to the inline
        /// images in the rendered view.
        /// </summary>
        public ImageEmbedder ImageEmbedder { get; }

        /// <summary>
        /// Adds an attachment to the email.
        /// </summary>
        /// <param name="attachment">The attachment to add.</param>
        public void Attach(Attachment attachment)
        {
            this.Attachments.Add(attachment);
        }

        /// <summary>
        /// Convenience method that sends this email asynchronously via a default EmailService.
        /// </summary>
        public async Task SendAsync()
        {
            await EmailService.Create().SendAsync(this);
        }

        /// <summary>
        /// Convenience method that saves this email asynchronously via a default EmailService.
        /// </summary>
        public async Task SaveToFileAsync(string path)
        {
            await EmailService.Create().SaveToFileAsync(this, path);
        }

        /// <summary>
        /// Stores the given value into the <see cref="ViewData"/>.
        /// </summary>
        /// <param name="binder">Provides the name of the view data property.</param>
        /// <param name="value">The value to store.</param>
        /// <returns>Always returns true.</returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            this.ViewData[binder.Name] = value;
            return true;
        }

        /// <summary>
        /// Tries to get a stored value from <see cref="ViewData"/>.
        /// </summary>
        /// <param name="binder">Provides the name of the view data property.</param>
        /// <param name="result">If found, this is the view data property value.</param>
        /// <returns>True if the property was found, otherwise false.</returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return this.ViewData.TryGetValue(binder.Name, out result);
        }

        private static string DeriveViewNameFromClassName()
        {
            var typeName = typeof(Email).Name;
            var viewName = typeName;
            if (viewName.EndsWith("Email"))
            {
                viewName = viewName.Substring(0, viewName.Length - "Email".Length);
            }

            return String.IsNullOrWhiteSpace(viewName) ? typeName : viewName;
        }
    }
}