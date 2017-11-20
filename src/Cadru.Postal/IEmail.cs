using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cadru.Postal
{
    public interface IEmail
    {
        string AreaName { get; set; }
        List<Attachment> Attachments { get; }
        ViewDataDictionary ViewData { get; set; }
        string ViewName { get; set; }
        ImageEmbedder ImageEmbedder { get; }
        void Attach(Attachment attachment);
        Task SaveToFileAsync(string path);
        Task SendAsync();
    }
}