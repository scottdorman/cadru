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