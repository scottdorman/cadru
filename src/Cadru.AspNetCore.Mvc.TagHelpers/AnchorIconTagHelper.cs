//------------------------------------------------------------------------------
// <copyright file="AnchorIconTagHelper.cs"
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

using System;

using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cadru.AspNetCore.Mvc.TagHelpers
{
    /// <summary>
    /// <see cref="ITagHelper"/> implementation targeting &lt;a&gt; elements,
    /// adding support for including a CSS icon as part of the content.
    /// </summary>
    [HtmlTargetElement("a", Attributes = IconAttributeName)]
    public class AnchorIconTagHelper : AnchorTagHelper
    {
        private const string IconAttributeName = "icon";

        /// <summary>
        /// Initializes a new instance of the <see cref="AnchorIconTagHelper"/> class.
        /// </summary>
        /// <param name="generator">The <see cref="IHtmlGenerator"/></param>
        public AnchorIconTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        /// <summary>
        /// The CSS classes for the icon element.
        /// </summary>
        [HtmlAttributeName(IconAttributeName)]
        public string? IconCss { get; set; }

        /// <inheritdoc/>
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