using System.Linq;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cadru.AspNetCore.Mvc.TagHelpers
{
    public static class TagBuilderExtensions
    {
        public static TagBuilder WithCssClass(this TagBuilder tagBuilder, string value)
        {
            tagBuilder.AddCssClass(value);
            return tagBuilder;
        }

        public static TagBuilder WithCssClasses(this TagBuilder tagBuilder, params string[] values)
        {
            foreach (var value in values ?? Enumerable.Empty<string>())
            {
                tagBuilder.AddCssClass(value);
            }

            return tagBuilder;
        }
    }
}