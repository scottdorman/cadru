using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cadru.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement("cadru-breadcrumb")]
    [RestrictChildren("cadru-breadcrumb-item", "li")]
    [OutputElementHint("ol")]
    public partial class BreadcrumbTagHelper : TagHelper
    {
        public BreadcrumbTagHelper(IHtmlGenerator generator) : base()
        {
            this.Generator = generator;
        }

        [HtmlAttributeNotBound]
        protected IHtmlGenerator Generator { get; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "nav";
            output.Attributes.SetAttribute("aria-label", "breadcrumb");
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var itemsListTagBuilder = this.Generator.GenerateTag("ol");
            itemsListTagBuilder.AddCssClass("breadcrumb");
            if (output.Attributes.TryGetAttribute("class", out var classAttribute))
            {
                itemsListTagBuilder.AddCssClass(classAttribute.Value.ToString());
                output.Attributes.Remove(classAttribute);
            }

            itemsListTagBuilder.InnerHtml.AppendHtml(await output.GetChildContentAsync());
            output.Content.AppendHtml(itemsListTagBuilder);
            await base.ProcessAsync(context, output);
        }
    }
}