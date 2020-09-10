using System.Text.Encodings.Web;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cadru.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement("cadru-breadcrumb-item", ParentTag = "cadru-breadcrumb", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement("cadru-breadcrumb-item", Attributes = ActiveAttributeName)]
    [OutputElementHint("li")]
    public partial class BreadcrumbItemTagHelper : TagHelper
    {
        private const string ActiveAttributeName = "active";
        private readonly HtmlEncoder _htmlEncoder;

        /// <summary>
        /// Creates a new <see cref="AnchorTagHelper"/>.
        /// </summary>
        /// <param name="generator">The <see cref="IHtmlGenerator"/>.</param>
        public BreadcrumbItemTagHelper(IHtmlGenerator generator, HtmlEncoder htmlEncoder) : base()
        {
            this._htmlEncoder = htmlEncoder;
            this.Generator = generator;
        }

        [HtmlAttributeName(ActiveAttributeName)]
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Rendering.ViewContext"/> for the current request.
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// Gets the <see cref="IHtmlGenerator"/> used to generate the <see cref="AnchorTagHelper"/>'s output.
        /// </summary>
        protected IHtmlGenerator Generator { get; }

        /// <inheritdoc />
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "li";
            output.Attributes.Clear();
            output.AddClass("breadcrumb-item", this._htmlEncoder);

            if (this.Active)
            {
                output.AddClass("active", this._htmlEncoder);
                output.Attributes.SetAttribute("aria-current", "page");
            }
        }

        /// <summary>
        /// The name of the action method.
        /// </summary
        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            output.Content.AppendHtml(childContent);
            await base.ProcessAsync(context, output);
        }
    }
}