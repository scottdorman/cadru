//------------------------------------------------------------------------------
// <copyright file="TagBuilderExtensions.cs"
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
    /// <summary>
    /// Extension methods for working with <see cref="TagBuilder"/> as a more
    /// fluent API.
    /// </summary>
    public static class TagBuilderExtensions
    {
        /// <summary>
        /// Creates a new HTML tag that has the specified tag name.
        /// </summary>
        /// <param name="generator">The <see cref="IHtmlGenerator"/> service.</param>
        /// <param name="tagName">An HTML tag name.</param>
        /// <returns>
        /// A new <see cref="TagBuilder"/> representing the HTML tag.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required for the extension method.")]
        public static TagBuilder GenerateTag(this IHtmlGenerator generator, string tagName)
        {
            var tagBuilder = new TagBuilder(tagName);
            return tagBuilder;
        }

        /// <summary>
        /// Creates a new HTML tag that has the specified tag name.
        /// </summary>
        /// <param name="generator">The <see cref="IHtmlGenerator"/> service.</param>
        /// <param name="tagName">An HTML tag name.</param>
        /// <param name="htmlAttributes">
        /// A dictionary of HTML attributes to merge into the generated tag.
        /// </param>
        /// <returns>
        /// A new <see cref="TagBuilder"/> representing the HTML tag.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required for the extension method.")]
        public static TagBuilder GenerateTag(this IHtmlGenerator generator, string tagName, IDictionary<string, object> htmlAttributes)
        {
            var tagBuilder = new TagBuilder(tagName);
            tagBuilder.MergeAttributes(htmlAttributes, replaceExisting: true);
            return tagBuilder;
        }

        /// <summary>
        /// Creates a new HTML tag that has the specified tag name.
        /// </summary>
        /// <param name="generator">The <see cref="IHtmlGenerator"/> service.</param>
        /// <param name="tagName">An HTML tag name.</param>
        /// <param name="htmlAttributes">
        /// An object of HTML attributes to merge into the generated tag. The
        /// value can be of type <see cref="IDictionary{TKey,TValue}"/> or
        /// <see cref="IReadOnlyDictionary{TKey,TValue}"/> or an object with
        /// public properties as key-value pairs.
        /// </param>
        /// <returns>
        /// A new <see cref="TagBuilder"/> representing the HTML tag.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required for the extension method.")]
        public static TagBuilder GenerateTag(this IHtmlGenerator generator, string tagName, object htmlAttributes)
        {
            return GenerateTag(generator, tagName, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Creates a new HTML tag that has the specified tag name.
        /// </summary>
        /// <param name="generator">The <see cref="IHtmlGenerator"/> service.</param>
        /// <param name="tagName">An HTML tag name.</param>
        /// <param name="htmlAttributes">
        /// An object of HTML attributes to merge into the generated tag. The
        /// value can be of type <see cref="IDictionary{TKey,TValue}"/> or
        /// <see cref="IReadOnlyDictionary{TKey,TValue}"/> or an object with
        /// public properties as key-value pairs.
        /// </param>
        /// <param name="innerContents">
        /// One or more <see cref="IHtmlContent"/> objects to be added as inner HTML.
        /// </param>
        /// <returns>
        /// A new <see cref="TagBuilder"/> representing the HTML tag.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required for the extension method.")]
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

        /// <summary>
        /// Creates a new HTML tag that has the specified tag name.
        /// </summary>
        /// <param name="generator">The <see cref="IHtmlGenerator"/> service.</param>
        /// <param name="tagName">An HTML tag name.</param>
        /// <param name="htmlAttributes">
        /// An object of HTML attributes to merge into the generated tag. The
        /// value can be of type <see cref="IDictionary{TKey,TValue}"/> or
        /// <see cref="IReadOnlyDictionary{TKey,TValue}"/> or an object with
        /// public properties as key-value pairs.
        /// </param>
        /// <param name="innerContent">The HTML inner content to be appended.</param>
        /// <returns>
        /// A new <see cref="TagBuilder"/> representing the HTML tag.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required for the extension method.")]
        public static TagBuilder GenerateTag(this IHtmlGenerator generator, string tagName, object htmlAttributes, string innerContent)
        {
            var tagBuilder = new TagBuilder(tagName);
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), replaceExisting: true);
            tagBuilder.InnerHtml.AppendHtml(innerContent);
            return tagBuilder;
        }

        /// <summary>
        /// Creates a new HTML tag that has the specified tag name.
        /// </summary>
        /// <param name="generator">The <see cref="IHtmlGenerator"/> service.</param>
        /// <param name="tagName">An HTML tag name.</param>
        /// <param name="innerContents">
        /// One or more <see cref="IHtmlContent"/> objects to be added as inner HTML.
        /// </param>
        /// <returns>
        /// A new <see cref="TagBuilder"/> representing the HTML tag.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required for the extension method.")]
        public static TagBuilder GenerateTag(this IHtmlGenerator generator, string tagName, params IHtmlContent[] innerContents)
        {
            var tagBuilder = new TagBuilder(tagName);
            foreach (var innerContent in innerContents ?? Enumerable.Empty<IHtmlContent>())
            {
                tagBuilder.InnerHtml.AppendHtml(innerContent);
            }

            return tagBuilder;
        }

        /// <summary>
        /// Adds a CSS class to the list of CSS classes in the tag. If there are
        /// already CSS classes on the tag then a space character and the new
        /// class will be appended to the existing list.
        /// </summary>
        /// <param name="tagBuilder">
        /// The <see cref="TagBuilder"/> instance to update.
        /// </param>
        /// <param name="values">The CSS class names to add.</param>
        /// <returns>
        /// A new <see cref="TagBuilder"/> representing the HTML tag.
        /// </returns>
        public static TagBuilder WithCssClass(this TagBuilder tagBuilder, params string[] values)
        {
            foreach (var value in values ?? Enumerable.Empty<string>())
            {
                tagBuilder.AddCssClass(value);
            }

            return tagBuilder;
        }
    }
}