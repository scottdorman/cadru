using RazorEngine;
using RazorEngine.Templating;
using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cadru.Postal
{
    /// <summary>
    /// Renders a preview of an email to display in the browser.
    /// </summary>
    public class EmailViewResult : ViewResult
    {
        const string TextContentType = "text/plain";
        const string HtmlContentType = "text/html";

        //IEmailViewRenderer Renderer { get; set; }
        private readonly IRazorEngineService razorEngineService;

        IEmailParser Parser { get; set; }
        Email Email { get; set; }

        /// <summary>
        /// Creates a new <see cref="EmailViewResult"/>.
        /// </summary>
        public EmailViewResult(Email email, IRazorEngineService razorEngineService, IEmailParser parser)
        {
            Email = email;
            this.razorEngineService = razorEngineService;
            Parser = parser ?? new EmailParser(razorEngineService);
        }

        /// <summary>
        /// Creates a new <see cref="EmailViewResult"/>.
        /// </summary>
        public EmailViewResult(Email email)
            : this(email, null, null)
        {
        }

        /// <summary>
        /// When called by the action invoker, renders the view to the response.
        /// </summary>
        public override async void ExecuteResult(ControllerContext context)
        {
            var httpContext = context.RequestContext.HttpContext;
            var query = httpContext.Request.QueryString;
            var format = query["format"];
            var contentType = await ExecuteResultAsync(context.HttpContext.Response.Output, format);
            httpContext.Response.ContentType = contentType;
        }

        /// <summary>
        /// Writes the email preview in the given format.
        /// </summary>
        /// <returns>The content type for the HTTP response.</returns>
        public async Task<string> ExecuteResultAsync(TextWriter writer, string format = null)
        {
            var result = this.razorEngineService.RunCompile(Email.ViewName, model: Email, viewBag: new DynamicViewBag(Email.ViewData));
            var mailMessage = await Parser.ParseAsync(result, Email);

            // no special requests; render what's in the template
            if (String.IsNullOrEmpty(format))
            {
                if (!mailMessage.IsBodyHtml)
                {
                    writer.Write(result);
                    return TextContentType;
                }

                var template = Extract(result);
                template.Write(writer);
                return HtmlContentType;
            }

            // Check if alternative
            var alternativeContentType = CheckAlternativeViews(writer, mailMessage, format);

            if (!String.IsNullOrEmpty(alternativeContentType))
                return alternativeContentType;

            if (format == "text")
            {
                if (mailMessage.IsBodyHtml)
                    throw new NotSupportedException("No text view available for this email");

                writer.Write(result);
                return TextContentType;
            }

            if (format == "html")
            {
                if (!mailMessage.IsBodyHtml)
                    throw new NotSupportedException("No html view available for this email");

                var template = Extract(result);
                template.Write(writer);
                return HtmlContentType;
            }

            throw new NotSupportedException(String.Format("Unsupported format {0}", format));
        }

        static string CheckAlternativeViews(TextWriter writer, MailMessage mailMessage, string format)
        {
            var contentType = format == "html"
                ? HtmlContentType
                : TextContentType;

            // check for alternative view
            var view = mailMessage.AlternateViews.FirstOrDefault(v => v.ContentType.MediaType == contentType);

            if (view == null)
                return null;

            string content;
            using (var reader = new StreamReader(view.ContentStream))
                content = reader.ReadToEnd();

            content = ReplaceLinkedImagesWithEmbeddedImages(view, content);

            writer.Write(content);
            return contentType;
        }

        class TemplateParts
        {
            readonly string header;
            readonly string body;

            public TemplateParts(string header, string body)
            {
                this.header = header;
                this.body = body;
            }

            public void Write(TextWriter writer)
            {
                writer.WriteLine("<!--");
                writer.WriteLine(header);
                writer.WriteLine("-->");
                writer.WriteLine(body);
            }
        }

        static TemplateParts Extract(string template)
        {
            var headerBuilder = new StringBuilder();

            using (var reader = new StringReader(template))
            {
                // try to read until we passed headers
                var line = reader.ReadLine();

                while (line != null)
                {
                    if (String.IsNullOrEmpty(line))
                    {
                        return new TemplateParts(headerBuilder.ToString(), reader.ReadToEnd());
                    }

                    headerBuilder.AppendLine(line);
                    line = reader.ReadLine();
                }
            }

            return null;
        }

        static TemplateParts Extract(string template, MailMessage mailMessage)
        {
            var headerBuilder = new StringBuilder();

            using (var reader = new StringReader(template))
            {
                // try to read until we passed headers
                var line = reader.ReadLine();

                while (line != null)
                {
                    if (String.IsNullOrEmpty(line))
                    {
                        return new TemplateParts(headerBuilder.ToString(), mailMessage.Body);
                    }

                    headerBuilder.AppendLine(line);
                    line = reader.ReadLine();
                }
            }

            return null;
        }

        internal static string ReplaceLinkedImagesWithEmbeddedImages(AlternateView view, string content)
        {
            var resources = view.LinkedResources;

            if (!resources.Any())
                return content;

            foreach (var resource in resources)
            {
                var find = "src=\"cid:" + resource.ContentId + "\"";
                var imageData = ComposeImageData(resource);
                content = content.Replace(find, "src=\"" + imageData + "\"");
            }

            return content;
        }

        static string ComposeImageData(LinkedResource resource)
        {
            var contentType = resource.ContentType.MediaType;
            var bytes = ReadFully(resource.ContentStream);
            return String.Format("data:{0};base64,{1}",
                contentType,
                Convert.ToBase64String(bytes));
        }

        static byte[] ReadFully(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}