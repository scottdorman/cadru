//------------------------------------------------------------------------------
// <copyright file="IHtmlGeneratorExtensions.cs"
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

using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Cadru.AspNetCore.Mvc.TagHelpers
{
    public static class IHtmlGeneratorExtensions
    {
        public static TagBuilder GenerateTag(this IHtmlGenerator generator, string tagName)
        {
            var tagBuilder = new TagBuilder(tagName);
            return tagBuilder;
        }

        public static TagBuilder GenerateTag(this IHtmlGenerator generator, string tagName, IDictionary<string, object> htmlAttributes)
        {
            var tagBuilder = new TagBuilder(tagName);
            tagBuilder.MergeAttributes(htmlAttributes, replaceExisting: true);
            return tagBuilder;
        }

        public static TagBuilder GenerateTag(this IHtmlGenerator generator, string tagName, object htmlAttributes)
        {
            return GenerateTag(generator, tagName, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static TagBuilder GenerateTag(this IHtmlGenerator generator, string tagName, object htmlAttributes, params IHtmlContent[] innerContents)
        {
            var tagBuilder = new TagBuilder(tagName);
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), replaceExisting: true);
            foreach (var innerContent in innerContents ?? Enumerable.Empty<IHtmlContent>())
            {
                tagBuilder.InnerHtml.AppendHtml(innerContent);
            }

            return tagBuilder;
        }

        public static TagBuilder GenerateTag(this IHtmlGenerator generator, string tagName, object htmlAttributes, string innerContent)
        {
            var tagBuilder = new TagBuilder(tagName);
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), replaceExisting: true);
            tagBuilder.InnerHtml.AppendHtml(innerContent);
            return tagBuilder;
        }

        public static TagBuilder GenerateTag(this IHtmlGenerator generator, string tagName, params IHtmlContent[] innerContents)
        {
            var tagBuilder = new TagBuilder(tagName);
            foreach (var innerContent in innerContents ?? Enumerable.Empty<IHtmlContent>())
            {
                tagBuilder.InnerHtml.AppendHtml(innerContent);
            }

            return tagBuilder;
        }

        //public static TagBuilder GenerateTag(this IHtmlGenerator generator, string tagName, string id, string name, object htmlAttributes)
        //{
        //    return GenerateTag(generator, tagName, id, name, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        //}

        //public static TagBuilder GenerateTag(this IHtmlGenerator generator, string tagName, string id, string name, IDictionary<string, object> htmlAttributes)
        //{
        //    var tagBuilder = new TagBuilder(tagName);
        //    tagBuilder.MergeAttribute("id", id);
        //    tagBuilder.MergeAttribute("name", name);
        //    tagBuilder.MergeAttributes(htmlAttributes, replaceExisting: true);

        //    return tagBuilder;
        //}
    }
}