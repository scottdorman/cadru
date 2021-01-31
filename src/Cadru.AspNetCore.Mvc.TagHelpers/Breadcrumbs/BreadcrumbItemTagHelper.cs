//------------------------------------------------------------------------------
// <copyright file="BreadcrumbItemTagHelper.cs"
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

using System.Text.Encodings.Web;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cadru.AspNetCore.Mvc.TagHelpers
{
    /// <summary>
    /// <see cref="ITagHelper"/> implementation creating a
    /// <see href="https://getbootstrap.com/">Bootstrap</see><c>breadcrumb-item</c> component.
    /// </summary>

    [HtmlTargetElement("cadru-breadcrumb-item", ParentTag = "cadru-breadcrumb", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement("cadru-breadcrumb-item", Attributes = ActiveAttributeName)]
    [OutputElementHint("li")]
    public partial class BreadcrumbItemTagHelper : TagHelper
    {
        private const string ActiveAttributeName = "active";
        private readonly HtmlEncoder _htmlEncoder;

        /// <summary>
        /// Creates a new <see cref="BreadcrumbItemTagHelper"/>.
        /// </summary>
        /// <param name="generator">The <see cref="IHtmlGenerator"/>.</param>
        /// <param name="htmlEncoder">The HTML character encoding.</param>
        public BreadcrumbItemTagHelper(IHtmlGenerator generator, HtmlEncoder htmlEncoder) : base()
        {
            this._htmlEncoder = htmlEncoder;
            this.Generator = generator;
        }

        /// <summary>
        /// Gets or sets a value indicating if this is the active breadcrumb item.
        /// </summary>
        [HtmlAttributeName(ActiveAttributeName)]
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ViewContext"/> for the current request.
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext? ViewContext { get; set; }

        /// <summary>
        /// Gets the <see cref="IHtmlGenerator"/> used to generate the
        /// <see cref="AnchorTagHelper"/>'s output.
        /// </summary>
        protected IHtmlGenerator Generator { get; }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            output.Content.AppendHtml(childContent);
            await base.ProcessAsync(context, output);
        }
    }
}