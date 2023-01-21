using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

using Scriban;
using Scriban.Runtime;

namespace Cadru.StronglyTypedId
{
    internal sealed partial class SourceGenerator
    {
        internal static class Emitter
        {
            public static IEnumerable<(string hintName, SourceText? source)> Create(EmittedTypeInfo typeInfo)
            {
                var resourceCollection = typeInfo.BackingType switch
                {
                    BackingType.Byte => EmbeddedSources.ByteResources,
                    BackingType.Char => EmbeddedSources.CharResources,
                    BackingType.Guid => EmbeddedSources.GuidResources,
                    BackingType.Int => EmbeddedSources.IntResources,
                    BackingType.Long => EmbeddedSources.LongResources,
                    BackingType.NullableString => EmbeddedSources.NullableStringResources,
                    BackingType.Short => EmbeddedSources.ShortResources,
                    BackingType.String => EmbeddedSources.StringResources,
                    _ => null
                };

                if (resourceCollection != null)
                {
                    //var customEnumFunctions = new EnumFunctions();
                    //((ScriptObject)customEnumFunctions).Import(typeInfo, renamer: member => member.Name);

                    //var templateContext = new TemplateContext();
                    //templateContext.PushGlobal(customEnumFunctions);
                    ////templateContext.MemberRenamer = member => member.Name;

                    var baseTemplate = typeInfo.GetTemplate(resourceCollection);
                    var template = Template.Parse(baseTemplate);
                    System.Diagnostics.Debug.Assert(template.HasErrors == false, "baseTemplate");

                    StringBuilder builder = new();
                    //builder.Append(template.Render(templateContext));

                    builder.Append(template.Render(typeInfo, memberRenamer: member => member.Name));
                    yield return (typeInfo.EmittedTypeName, SourceText.From(builder.ToString(), Encoding.UTF8));

                    if (resourceCollection.Converters.Any())
                    {
                        var converters = typeInfo.Converters ?? Defaults.Converters;
                        foreach (var converter in resourceCollection.Converters)
                        {
                            if (converters.HasFlag(converter.Key))
                            {
                                template = Template.Parse(converter.Value);
                                System.Diagnostics.Debug.Assert(template.HasErrors == false, $"{converter.Key} for {typeInfo.BackingType}");

                                builder = new();
                                builder.Append(template.Render(typeInfo, memberRenamer: member => member.Name));

                                var converterName = typeInfo.GetEmittedConverterName(converter.Key);
                                yield return (converterName, SourceText.From(builder.ToString(), Encoding.UTF8));
                            }
                        }
                    }
                }
            }
        }
    }
}
