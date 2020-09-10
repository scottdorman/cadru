//------------------------------------------------------------------------------
// <copyright file="BreadcrumbTagHelper.cs"
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