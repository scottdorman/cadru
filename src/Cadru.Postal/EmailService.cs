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
        private readonly IEmailParser emailParser;
        private readonly IRazorEngineService razorEngineService;
        private readonly Func<SmtpClient> createSmtpClient;


        /// <summary>
        /// Creates a new instance of the <see cref="EmailService"/> class
        /// with the specified Razor Engine configuration and, optionally, a
        /// delegate to create an <see cref="SmtpClient"/>.
        /// </summary>
        /// <param name="configuration">An <see cref="RazorEngine.Configuration.ITemplateServiceConfiguration"/> instance.</param>
        /// <param name="isolated"></param>
        /// <param name="createSmtpClient">A delegate to create an <see cref="SmtpClient"/> instance or <see langword="null"/>
        /// to use a default delegate.</param>
        public static EmailService Create(ITemplateServiceConfiguration configuration, bool isolated = false, Func<SmtpClient> createSmtpClient = null)
        {
            return new EmailService(configuration, isolated, createSmtpClient);
        }

        internal static EmailService Create(bool isolated = false)
        {
            return new EmailService(isolated ? Engine.IsolatedRazor : Engine.Razor);
        }

        private EmailService(ITemplateServiceConfiguration configuration, bool isolated = false, Func<SmtpClient> createSmtpClient = null)
            : this(isolated ? IsolatedRazorEngineService.Create() : RazorEngineService.Create(configuration), createSmtpClient)
        {
        }

        internal EmailService(IRazorEngineService razorEngineService, Func<SmtpClient> createSmtpClient = null)
        {
            this.createSmtpClient = createSmtpClient ?? (() => new SmtpClient());
            this.razorEngineService = razorEngineService;
            this.emailParser = new EmailParser(this.razorEngineService);
        }

        /// <summary>
        /// Saves the email to the specified directory as an asynchronous operation.
        /// </summary>
        /// <param name="email">The email to save.</param>
        /// <param name="path">The directory where the email should be saved.</param>
        public async Task SaveToFileAsync(IEmail email, string path)
        {
            using (var mailMessage = await CreateMailMessageAsync(email))
            using (var client = new SmtpClient("SaveEmailSMTPClient"))
            {
                client.EnableSsl = false;
                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.PickupDirectoryLocation = path;
                await client.SendMailAsync(mailMessage);
                client.Dispose();
            }
        }

        /// <summary>
        /// Sends an email to an SMTP client for delivery as an asynchronous operation.
        /// </summary>
        /// <param name="email">The email to send.</param>
        public async Task SendAsync(IEmail email)
        {
            using (var mailMessage = await CreateMailMessageAsync(email))
            {
                using (var smtp = createSmtpClient())
                {
                    await smtp.SendMailAsync(mailMessage);
                }
            }
        }

        /// <summary>
        /// Renders the email view and builds a <see cref="MailMessage"/>.
        /// </summary>
        /// <param name="email">The email to render.</param>
        /// <returns>A <see cref="MailMessage"/> containing the rendered email.</returns>
        public async Task<MailMessage> CreateMailMessageAsync(IEmail email)
        {
            var templateOutput = this.Render(email);
            var mailMessage = await emailParser.ParseAsync(templateOutput, email);
            return mailMessage;
        }

        public async Task<MailMessage> CreateMailMessageAsync(IEmail email, ITemplateKey key)
        {
            var templateOutput = this.Render(email, key);
            var mailMessage = await emailParser.ParseAsync(templateOutput, email);
            return mailMessage;
        }

        /// <summary>
        /// Renders the email view.
        /// </summary>
        /// <param name="email">The email to render.</param>
        /// <param name="viewName">The email view name. If <see langword="null"/> then the
        /// <see cref="Email.ViewName"/> property is used.</param>
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
        /// <param name="key">The <see cref="ITemplateKey"/> used to resolve the template.</param>
        /// <returns>The rendered email view output.</returns>
        public virtual string Render(IEmail email, ITemplateKey key)
        {
            return this.razorEngineService.RunCompile(key, model: email);
        }
    }
}