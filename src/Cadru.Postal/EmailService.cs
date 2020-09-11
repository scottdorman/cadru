//------------------------------------------------------------------------------
// <copyright file="EmailService.cs"
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
using System.Net.Mail;
using System.Threading.Tasks;

using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace Cadru.Postal
{
    /// <summary>
    /// Sends email using the default <see cref="SmtpClient"/>.
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly Func<SmtpClient> createSmtpClient;
        private readonly IEmailParser emailParser;
        private readonly IRazorEngineService razorEngineService;

        internal EmailService(IRazorEngineService razorEngineService, Func<SmtpClient> createSmtpClient = null)
        {
            this.createSmtpClient = createSmtpClient ?? (() => new SmtpClient());
            this.razorEngineService = razorEngineService;
            this.emailParser = new EmailParser(this.razorEngineService);
        }

        private EmailService(ITemplateServiceConfiguration configuration, bool isolated = false, Func<SmtpClient> createSmtpClient = null)
                    : this(isolated ? IsolatedRazorEngineService.Create() : RazorEngineService.Create(configuration), createSmtpClient)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class
        /// with the specified Razor Engine configuration and, optionally, a
        /// delegate to create an <see cref="SmtpClient"/>.
        /// </summary>
        /// <param name="configuration">
        /// An
        /// <see cref="RazorEngine.Configuration.ITemplateServiceConfiguration"/> instance.
        /// </param>
        /// <param name="isolated"></param>
        /// <param name="createSmtpClient">
        /// A delegate to create an <see cref="SmtpClient"/> instance or
        /// <see langword="null"/> to use a default delegate.
        /// </param>
        public static EmailService Create(ITemplateServiceConfiguration configuration, bool isolated = false, Func<SmtpClient> createSmtpClient = null)
        {
            return new EmailService(configuration, isolated, createSmtpClient);
        }

        /// <summary>
        /// Renders the email view and builds a <see cref="MailMessage"/>.
        /// </summary>
        /// <param name="email">The email to render.</param>
        /// <returns>
        /// A <see cref="MailMessage"/> containing the rendered email.
        /// </returns>
        public async Task<MailMessage> CreateMailMessageAsync(IEmail email)
        {
            var templateOutput = this.Render(email);
            var mailMessage = await this.emailParser.ParseAsync(templateOutput, email);
            return mailMessage;
        }

        public async Task<MailMessage> CreateMailMessageAsync(IEmail email, ITemplateKey key)
        {
            var templateOutput = this.Render(email, key);
            var mailMessage = await this.emailParser.ParseAsync(templateOutput, email);
            return mailMessage;
        }

        /// <summary>
        /// Renders the email view.
        /// </summary>
        /// <param name="email">The email to render.</param>
        /// <param name="viewName">
        /// The email view name. If <see langword="null"/> then the
        /// <see cref="Email.ViewName"/> property is used.
        /// </param>
        /// <returns>The rendered email view output.</returns>
        public virtual string Render(IEmail email, string viewName = null)
        {
            viewName = viewName ?? email.ViewName;
            var viewOutput = this.razorEngineService.RunCompile(viewName, model: email);
            return viewOutput;
        }

        /// <summary>
        /// Renders the email view.
        /// </summary>
        /// <param name="email">The email to render.</param>
        /// <param name="key">
        /// The <see cref="ITemplateKey"/> used to resolve the template.
        /// </param>
        /// <returns>The rendered email view output.</returns>
        public virtual string Render(IEmail email, ITemplateKey key)
        {
            return this.razorEngineService.RunCompile(key, model: email);
        }

        /// <summary>
        /// Saves the email to the specified directory as an asynchronous operation.
        /// </summary>
        /// <param name="email">The email to save.</param>
        /// <param name="path">The directory where the email should be saved.</param>
        public async Task SaveToFileAsync(IEmail email, string path)
        {
            using (var mailMessage = await this.CreateMailMessageAsync(email))
            using (var client = new SmtpClient("SaveEmailSMTPClient"))
            {
                client.EnableSsl = false;
                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.PickupDirectoryLocation = path;
                await client.SendMailAsync(mailMessage);
            }
        }

        /// <summary>
        /// Sends an email to an SMTP client for delivery as an asynchronous operation.
        /// </summary>
        /// <param name="email">The email to send.</param>
        public async Task SendAsync(IEmail email)
        {
            using (var mailMessage = await this.CreateMailMessageAsync(email))
            {
                using (var smtp = this.createSmtpClient())
                {
                    await smtp.SendMailAsync(mailMessage);
                }
            }
        }

        internal static EmailService Create(bool isolated = false)
        {
            return new EmailService(isolated ? Engine.IsolatedRazor : Engine.Razor);
        }
    }
}