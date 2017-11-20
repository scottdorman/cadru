﻿using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RazorEngine.Templating;

namespace Cadru.Postal
{
    /// <summary>
    /// Converts the raw string output of a view into a <see cref="MailMessage"/>.
    /// </summary>
    public class EmailParser : IEmailParser
    {
        private readonly IRazorEngineService razorEngineService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailParser"/> class
        /// with the specified <see cref="IRazorEngineService"/> instance.
        /// </summary>
        /// <param name="razorEngineService"></param>
        public EmailParser(IRazorEngineService razorEngineService)
        {
            this.razorEngineService = razorEngineService;
        }

        private async Task<AlternateView> CreateAlternativeViewAsync(IEmail email, string alternativeViewName)
        {
            var fullViewName = GetAlternativeViewName(email, alternativeViewName);
            var output = this.razorEngineService.RunCompile(fullViewName, model: email, viewBag: new DynamicViewBag(email.ViewData));

            string contentType;
            string body;
            using (var reader = new StringReader(output))
            {
                contentType = await ParseHeadersForContentType(reader);
                body = reader.ReadToEnd();
            }

            if (string.IsNullOrWhiteSpace(contentType))
            {
                if (alternativeViewName.Equals("text", StringComparison.OrdinalIgnoreCase))
                {
                    contentType = "text/plain";
                }
                else if (alternativeViewName.Equals("html", StringComparison.OrdinalIgnoreCase))
                {
                    contentType = "text/html";
                }
                else
                {
                    throw new Exception("The 'Content-Type' header is missing from the alternative view '" + fullViewName + "'.");
                }
            }

            var stream = CreateStreamOfBody(body);
            var alternativeView = new AlternateView(stream, contentType);
            if (alternativeView.ContentType.CharSet == null)
            {
                // Must set a charset otherwise mail readers seem to guess the wrong one!
                // Strings are unicode by default in .net.
                alternativeView.ContentType.CharSet = System.Text.Encoding.Unicode.WebName;
                // A different charset can be specified in the Content-Type header.
                // e.g. Content-Type: text/html; charset=utf-8
            }
            email.ImageEmbedder.AddImagesToView(alternativeView);
            return alternativeView;
        }

        private static string GetAlternativeViewName(IEmail email, string alternativeViewName)
        {
            if (email.ViewName.StartsWith("~"))
            {
                var index = email.ViewName.LastIndexOf('.');
                return email.ViewName.Insert(index + 1, alternativeViewName + ".");
            }
            else
            {
                return email.ViewName + "." + alternativeViewName;
            }
        }

        MemoryStream CreateStreamOfBody(string body)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(body);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private async Task<string> ParseHeadersForContentType(StringReader reader)
        {
            string contentType = null;
            await ParserUtils.ParseHeadersAsync(reader, (key, value) =>
            {
                if (key.Equals("content-type", StringComparison.OrdinalIgnoreCase))
                {
                    contentType = value;
                }
            });

            return contentType;
        }

        private bool IsAlternativeViewsHeader(string headerName)
        {
            return headerName.Equals("views", StringComparison.OrdinalIgnoreCase);
        }

        private void AssignEmailHeaderToMailMessage(string key, string value, MailMessage message)
        {
            switch (key)
            {
                case "to":
                    message.To.Add(value);
                    break;
                case "from":
                    message.From = new MailAddress(value);
                    break;
                case "subject":
                    message.Subject = value;
                    break;
                case "cc":
                    message.CC.Add(value);
                    break;
                case "bcc":
                    message.Bcc.Add(value);
                    break;
                case "reply-to":
                    message.ReplyToList.Add(value);
                    break;
                case "sender":
                    message.Sender = new MailAddress(value);
                    break;
                case "priority":
                    MailPriority priority;
                    if (Enum.TryParse(value, true, out priority))
                    {
                        message.Priority = priority;
                    }
                    else
                    {
                        throw new ArgumentException(string.Format("Invalid email priority: {0}. It must be High, Medium or Low.", value));
                    }
                    break;
                case "content-type":
                    var charsetMatch = Regex.Match(value, @"\bcharset\s*=\s*(.*)$");
                    if (charsetMatch.Success)
                    {
                        message.BodyEncoding = System.Text.Encoding.GetEncoding(charsetMatch.Groups[1].Value);
                    }
                    break;
                default:
                    message.Headers[key] = value;
                    break;
            }
        }

        private void AddAttachments(MailMessage message, IEmail email)
        {
            foreach (var attachment in email.Attachments)
            {
                message.Attachments.Add(attachment);
            }
        }

        private void AssignCommonHeaders(MailMessage message, IEmail email)
        {
            if (message.To.Count == 0)
            {
                AssignCommonHeader<string>(email, "to", to => message.To.Add(to));
                AssignCommonHeader<MailAddress>(email, "to", to => message.To.Add(to));
            }

            if (message.From == null)
            {
                AssignCommonHeader<string>(email, "from", from => message.From = new MailAddress(from));
                AssignCommonHeader<MailAddress>(email, "from", from => message.From = from);
            }
            if (message.CC.Count == 0)
            {
                AssignCommonHeader<string>(email, "cc", cc => message.CC.Add(cc));
                AssignCommonHeader<MailAddress>(email, "cc", cc => message.CC.Add(cc));
            }
            if (message.Bcc.Count == 0)
            {
                AssignCommonHeader<string>(email, "bcc", bcc => message.Bcc.Add(bcc));
                AssignCommonHeader<MailAddress>(email, "bcc", bcc => message.Bcc.Add(bcc));
            }
            if (message.ReplyToList.Count == 0)
            {
                AssignCommonHeader<string>(email, "replyto", replyTo => message.ReplyToList.Add(replyTo));
                AssignCommonHeader<MailAddress>(email, "replyto", replyTo => message.ReplyToList.Add(replyTo));
            }
            if (message.Sender == null)
            {
                AssignCommonHeader<string>(email, "sender", sender => message.Sender = new MailAddress(sender));
                AssignCommonHeader<MailAddress>(email, "sender", sender => message.Sender = sender);
            }
            if (string.IsNullOrEmpty(message.Subject))
            {
                AssignCommonHeader<string>(email, "subject", subject => message.Subject = subject);
            }
        }

        private void AssignCommonHeader<T>(IEmail email, string header, Action<T> assign)
            where T : class
        {
            if (email.ViewData.TryGetValue(header, out object value))
            {
                if (value is T typedValue) assign(typedValue);
            }
        }

        private async Task ProcessHeaderAsync(string key, string value, MailMessage message, IEmail email)
        {
            if (IsAlternativeViewsHeader(key))
            {
                var viewNames = value.Split(new[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var viewName in viewNames)
                {
                    var view = await CreateAlternativeViewAsync(email, viewName);
                    message.AlternateViews.Add(view);
                }
            }
            else
            {
                AssignEmailHeaderToMailMessage(key, value, message);
            }
        }

        /// <summary>
        /// Parses the email view output into a <see cref="MailMessage"/>.
        /// </summary>
        /// <param name="template">The email view template.</param>
        /// <param name="email">The <see cref="Email"/> used to generate the output.</param>
        /// <returns>A <see cref="MailMessage"/> containing the email headers and content.</returns>
        public async Task<MailMessage> ParseAsync(string template, IEmail email)
        {
            var message = new MailMessage();
            using (var reader = new StringReader(template))
            {
                await ParserUtils.ParseHeadersAsync(reader, async (key, value) => await ProcessHeaderAsync(key, value, message, email));
                AssignCommonHeaders(message, email);
                if (message.AlternateViews.Count == 0)
                {
                    var messageBody = (await reader.ReadToEndAsync()).Trim();
                    if (email.ImageEmbedder.HasImages)
                    {
                        var view = AlternateView.CreateAlternateViewFromString(messageBody, new ContentType("text/html"));
                        email.ImageEmbedder.AddImagesToView(view);
                        message.AlternateViews.Add(view);
                        message.Body = "Plain text not available.";
                        message.IsBodyHtml = false;
                    }
                    else
                    {
                        message.Body = messageBody;
                        if (message.Body.StartsWith("<")) message.IsBodyHtml = true;
                    }
                }

                AddAttachments(message, email);
            }

            return message;
        }
    }
}