//------------------------------------------------------------------------------
// <copyright file="BootstrapNavLinkTagHelper.cs"
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

namespace Cadru.AspNetCore.Mvc.TagHelpers
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [OutputElementHint("li")]
    public class BootstrapNavLinkTagHelper : AnchorTagHelper
    {
        private const string IconAttributeName = "bootstrap-icon";

        public BootstrapNavLinkTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        /// <summary>
        /// An expression to be evaluated against the current model.
        /// </summary>
        [HtmlAttributeName(IconAttributeName)]
        public string IconCss { get; set; }

        public async override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            var childContent = await output.GetChildContentAsync();
            var content = childContent.GetContent();
            output.TagName = "li";

            var href = output.Attributes.FirstOrDefault(a => a.Name == "href");
            if (href != null)
            {
                var tagBuilder = new TagBuilder("a");
                tagBuilder.Attributes.Add("href", href.Value.ToString());

                if (!String.IsNullOrWhiteSpace(this.IconCss))
                {
                    var iconTagBuilder = new TagBuilder("div");
                    iconTagBuilder.AddCssClass(this.IconCss);
                    tagBuilder.InnerHtml.AppendHtml(iconTagBuilder);
                }

                tagBuilder.InnerHtml.AppendHtml(content);
                output.Content.SetHtmlContent(tagBuilder);
            }
            else
            {
                output.Content.SetHtmlContent(content);
            }

            if (this.ShouldBeActive())
            {
                this.MakeActive(output);
            }
        }

        private void MakeActive(TagHelperOutput output)
        {
            if (output.Attributes.TryGetAttribute("class", out var classAttribute))
            {
                output.Attributes.SetAttribute("class", classAttribute.Value + " active");
            }
            else
            {
                output.Attributes.Add(new TagHelperAttribute("class", "active"));
            }
        }

        private bool ShouldBeActive()
        {
            var routeData = this.ViewContext.RouteData.Values;
            var currentController = routeData["controller"] as string;
            var currentAction = routeData["action"] as string;
            var result = false;

            if (!String.IsNullOrWhiteSpace(this.Controller) && !String.IsNullOrWhiteSpace(this.Action))
            {
                result = String.Equals(this.Action, currentAction, StringComparison.OrdinalIgnoreCase) &&
                    String.Equals(this.Controller, currentController, StringComparison.OrdinalIgnoreCase);
            }
            else if (!String.IsNullOrWhiteSpace(this.Action))
            {
                result = String.Equals(this.Action, currentAction, StringComparison.OrdinalIgnoreCase);
            }
            else if (!String.IsNullOrWhiteSpace(this.Controller))
            {
                result = String.Equals(this.Controller, currentController, StringComparison.OrdinalIgnoreCase);
            }

            return result;
        }
    }
}