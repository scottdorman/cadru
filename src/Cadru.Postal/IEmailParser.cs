using System.Net.Mail;
using System.Threading.Tasks;

namespace Cadru.Postal
{
    /// <summary>
    /// Parses raw string output of email views into <see cref="MailMessage"/>.
    /// </summary>
    public interface IEmailParser
    {
        /// <summary>
        /// Parses the email view output into a <see cref="MailMessage"/>.
        /// </summary>
        /// <param name="template">The email view template.</param>
        /// <param name="email">The <see cref="Email"/> used to generate the output.</param>
        /// <returns>A <see cref="MailMessage"/> containing the email headers and content.</returns>
        Task<MailMessage> ParseAsync(string template, IEmail email);
    }
}
