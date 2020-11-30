//------------------------------------------------------------------------------
// <copyright file="IEmailService.cs"
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

using System.Net.Mail;
using System.Threading.Tasks;

namespace Cadru.Postal
{
    /// <summary>
    /// Creates and sends an email.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Renders the email view and builds a <see cref="MailMessage"/>.
        /// </summary>
        /// <param name="email">The email to render.</param>
        /// <returns>
        /// A <see cref="MailMessage"/> containing the rendered email.
        /// </returns>
        Task<MailMessage> CreateMailMessageAsync(IEmail email);

        /// <summary>
        /// Renders the email view.
        /// </summary>
        /// <param name="email">The email to render.</param>
        /// <param name="viewName">
        /// The email view name. If <see langword="null"/> then the
        /// <see cref="Email.ViewName"/> property is used.
        /// </param>
        /// <returns>The rendered email view output.</returns>
        string Render(IEmail email, string viewName = null);

        /// <summary>
        /// Saves the email to the specified directory as an asynchronous operation.
        /// </summary>
        /// <param name="email">The email to save.</param>
        /// <param name="path">The directory where the email should be saved.</param>
        Task SaveToFileAsync(IEmail email, string path);

        /// <summary>
        /// Sends an email to an SMTP client for delivery as an asynchronous operation.
        /// </summary>
        /// <param name="email">The email to send.</param>
        Task SendAsync(IEmail email);
    }
}