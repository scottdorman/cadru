using System;

using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cadru.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement("a", Attributes = IconAttributeName)]
    public class AnchorIconTagHelper : AnchorTagHelper
    {
        private const string IconAttributeName = "icon";

        public AnchorIconTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        /// <summary>
        /// An expression to be evaluated against the current model.
        /// </summary>
        [HtmlAttributeName(IconAttributeName)]
        public string IconCss { get; set; }

        /// <inheritdoc />
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!String.IsNullOrWhiteSpace(this.IconCss))
            {
                var iconTagBuilder = this.Generator.GenerateTag("i");
                iconTagBuilder.AddCssClass(this.IconCss);
                output.PreContent.AppendHtml(iconTagBuilder);
            }
        }
    }
}