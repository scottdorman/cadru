using System.Net.Mail;
using System.Threading.Tasks;

namespace Cadru.Postal
{
    /// <summary>
    /// Creates and send email.
    /// </summary>
    public interface IEmailService
    {
        ///// <summary>
        ///// Saves the email to the specified directory.
        ///// </summary>
        ///// <param name="email">The email to save.</param>
        ///// <param name="path">The directory where the email should be saved.</param>
        //void SaveToFile(Email email, string path);

        /// <summary>
        /// Saves the email to the specified directory as an asynchronous operation.
        /// </summary>
        /// <param name="email">The email to save.</param>
        /// <param name="path">The directory where the email should be saved.</param>
        Task SaveToFileAsync(Email email, string path);

        ///// <summary>
        ///// Sends an email to an SMTP client for delivery.
        ///// </summary>
        ///// <param name="email">The email to send.</param>
        //void Send(Email email);

        /// <summary>
        /// Sends an email to an SMTP client for delivery as an asynchronous operation.
        /// </summary>
        /// <param name="email">The email to send.</param>
        Task SendAsync(Email email);

        /// <summary>
        /// Renders the email view and builds a <see cref="MailMessage"/>.
        /// </summary>
        /// <param name="email">The email to render.</param>
        /// <returns>A <see cref="MailMessage"/> containing the rendered email.</returns>
        Task<MailMessage> CreateMailMessageAsync(Email email);

        /// <summary>
        /// Renders the email view.
        /// </summary>
        /// <param name="email">The email to render.</param>
        /// <param name="viewName">The email view name. If <see langword="null"/> then the
        /// <see cref="Email.ViewName"/> property is used.</param>
        /// <returns>The rendered email view output.</returns>
        string Render(Email email, string viewName = null);

    }
}
