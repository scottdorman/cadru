//------------------------------------------------------------------------------
// <copyright file="InputPlaceholderTagHelper.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2017 Scott Dorman.
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

    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement("input", Attributes = PlaceholderAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class InputPlaceholderTagHelper : InputTagHelper
    {
        private const string PlaceholderAttributeName = "asp-placeholder-for";

        public InputPlaceholderTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        /// <summary>
        /// An expression to be evaluated against the current model.
        /// </summary>
        [HtmlAttributeName(PlaceholderAttributeName)]
        public ModelExpression Placeholder { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            var placeholder = this.GetPlaceholder(this.Placeholder.ModelExplorer);
            if (!output.Attributes.TryGetAttribute("placeholder", out _))
            {
                output.Attributes.Add(new TagHelperAttribute("placeholder", placeholder));
            }
        }

        private string GetPlaceholder(ModelExplorer modelExplorer)
        {
            string placeholder;
            placeholder = modelExplorer.Metadata.Placeholder;

            if (String.IsNullOrWhiteSpace(placeholder))
            {
                placeholder = modelExplorer.Metadata.GetDisplayName();
            }

            return placeholder;
        }
    }
}
