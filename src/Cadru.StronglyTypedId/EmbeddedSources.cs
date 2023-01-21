using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using Scriban;

namespace Cadru.StronglyTypedId
{
    record EmbeddedSource(string Key, SourceText Text);

    internal static class EmbeddedSources
    {
        private static readonly Assembly ThisAssembly = typeof(EmbeddedSources).Assembly;

        internal static readonly EmbeddedSource[] AttributeSources =
        {
               new EmbeddedSource("BackingType.g.cs", SourceText.From(LoadEmbeddedResource($"{ Constants.TemplatesKey }.Attributes.BackingType.cs"), Encoding.UTF8)),
               new EmbeddedSource("StronglyTypedIdConverter.g.cs", SourceText.From(LoadEmbeddedResource($"{ Constants.TemplatesKey }.Attributes.StronglyTypedIdConverter.cs"), Encoding.UTF8)),
               new EmbeddedSource("StronglyTypedIdAttribute.g.cs", SourceText.From(LoadEmbeddedResource($"{ Constants.TemplatesKey }.Attributes.StronglyTypedIdAttribute.cs"), Encoding.UTF8)),
               new EmbeddedSource("StronglyTypedIdDefaultsAttribute.g.cs", SourceText.From(LoadEmbeddedResource($"{ Constants.TemplatesKey }.Attributes.StronglyTypedIdDefaultsAttribute.cs"), Encoding.UTF8)),
        };

        internal static readonly ResourceCollection ByteResources = new()
        {
            RecordTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.Byte.Record.csx"),
            StructTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.Byte.Struct.csx"),
            ClassTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.Byte.Class.csx"),
            Converters =
            {
                { StronglyTypedIdConverter.SystemTextJson, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Byte.JsonConverter.csx") },
                { StronglyTypedIdConverter.TypeConverter, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Byte.TypeConverter.csx") },
                { StronglyTypedIdConverter.NewtonsoftJson, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Byte.NewtonsoftJsonConverter.csx") },
                { StronglyTypedIdConverter.EfCoreValueConverter, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Byte.ValueConverter.csx") },
                { StronglyTypedIdConverter.DapperTypeHandler, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Byte.DapperTypeHandler.csx") },
            }
        };

        internal static readonly ResourceCollection CharResources = new()
        {
            RecordTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.Char.Record.csx"),
            StructTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.Char.Struct.csx"),
            ClassTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.Char.Class.csx"),
            Converters =
            {
                { StronglyTypedIdConverter.SystemTextJson, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Char.JsonConverter.csx") },
                { StronglyTypedIdConverter.TypeConverter, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Char.TypeConverter.csx") },
                { StronglyTypedIdConverter.NewtonsoftJson, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Char.NewtonsoftJsonConverter.csx") },
                { StronglyTypedIdConverter.EfCoreValueConverter, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Char.ValueConverter.csx") },
                { StronglyTypedIdConverter.DapperTypeHandler, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Char.DapperTypeHandler.csx") },
            }
        };

        internal static readonly ResourceCollection GuidResources = new()
        {
            RecordTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.Guid.Record.csx"),
            StructTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.Guid.Struct.csx"),
            ClassTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.Guid.Class.csx"),
            Converters = 
            {
                { StronglyTypedIdConverter.SystemTextJson, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Guid.JsonConverter.csx") },
                { StronglyTypedIdConverter.TypeConverter, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Guid.TypeConverter.csx") },
                { StronglyTypedIdConverter.NewtonsoftJson, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Guid.NewtonsoftJsonConverter.csx") },
                { StronglyTypedIdConverter.EfCoreValueConverter, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Guid.ValueConverter.csx") },
                { StronglyTypedIdConverter.DapperTypeHandler, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Guid.DapperTypeHandler.csx") },
            }
        };
        
        internal static readonly ResourceCollection IntResources = new()
        {
            RecordTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.Int.Record.csx"),
            StructTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.Int.Struct.csx"),
            ClassTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.Int.Class.csx"),
            Converters =
            {
                { StronglyTypedIdConverter.SystemTextJson, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Int.JsonConverter.csx") },
                { StronglyTypedIdConverter.TypeConverter, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Int.TypeConverter.csx") },
                { StronglyTypedIdConverter.NewtonsoftJson, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Int.NewtonsoftJsonConverter.csx") },
                { StronglyTypedIdConverter.EfCoreValueConverter, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Int.ValueConverter.csx") },
                { StronglyTypedIdConverter.DapperTypeHandler, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Int.DapperTypeHandler.csx") },
            }
        };
        
        internal static readonly ResourceCollection LongResources = new()
        {
            RecordTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.Long.Record.csx"),
            StructTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.Long.Struct.csx"),
            ClassTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.Long.Class.csx"),
            Converters =
            {
                { StronglyTypedIdConverter.SystemTextJson, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Long.JsonConverter.csx") },
                { StronglyTypedIdConverter.TypeConverter, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Long.TypeConverter.csx") },
                { StronglyTypedIdConverter.NewtonsoftJson, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Long.NewtonsoftJsonConverter.csx") },
                { StronglyTypedIdConverter.EfCoreValueConverter, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Long.ValueConverter.csx") },
                { StronglyTypedIdConverter.DapperTypeHandler, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Long.DapperTypeHandler.csx") },
            }
        };
        
        internal static readonly ResourceCollection NullableStringResources = new()
        {
            RecordTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.NullableString.Record.csx"),
            StructTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.NullableString.Struct.csx"),
            ClassTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.NullableString.Class.csx"),
            Converters =
            {
                { StronglyTypedIdConverter.SystemTextJson, LoadEmbeddedResource($"{ Constants.TemplatesKey }.NullableString.JsonConverter.csx") },
                { StronglyTypedIdConverter.TypeConverter, LoadEmbeddedResource($"{ Constants.TemplatesKey }.NullableString.TypeConverter.csx") },
                { StronglyTypedIdConverter.NewtonsoftJson, LoadEmbeddedResource($"{ Constants.TemplatesKey }.NullableString.NewtonsoftJsonConverter.csx") },
                { StronglyTypedIdConverter.EfCoreValueConverter, LoadEmbeddedResource($"{ Constants.TemplatesKey }.NullableString.ValueConverter.csx") },
                { StronglyTypedIdConverter.DapperTypeHandler, LoadEmbeddedResource($"{ Constants.TemplatesKey }.NullableString.DapperTypeHandler.csx") },
            }
        };
        
        internal static readonly ResourceCollection ShortResources = new()
        {
            RecordTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.Short.Record.csx"),
            StructTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.Short.Struct.csx"),
            ClassTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.Short.Class.csx"),
            Converters =
            {
                { StronglyTypedIdConverter.SystemTextJson, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Short.JsonConverter.csx") },
                { StronglyTypedIdConverter.TypeConverter, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Short.TypeConverter.csx") },
                { StronglyTypedIdConverter.NewtonsoftJson, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Short.NewtonsoftJsonConverter.csx") },
                { StronglyTypedIdConverter.EfCoreValueConverter, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Short.ValueConverter.csx") },
                { StronglyTypedIdConverter.DapperTypeHandler, LoadEmbeddedResource($"{ Constants.TemplatesKey }.Short.DapperTypeHandler.csx") },
            }
        };
        
        internal static readonly ResourceCollection StringResources = new()
        {
            RecordTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.String.Record.csx"),
            StructTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.String.Struct.csx"),
            ClassTemplate = LoadEmbeddedResource($"{ Constants.TemplatesKey }.String.Class.csx"),
            Converters =
            {
                { StronglyTypedIdConverter.SystemTextJson, LoadEmbeddedResource($"{ Constants.TemplatesKey }.String.JsonConverter.csx") },
                { StronglyTypedIdConverter.TypeConverter, LoadEmbeddedResource($"{ Constants.TemplatesKey }.String.TypeConverter.csx") },
                { StronglyTypedIdConverter.NewtonsoftJson, LoadEmbeddedResource($"{ Constants.TemplatesKey }.String.NewtonsoftJsonConverter.csx") },
                { StronglyTypedIdConverter.EfCoreValueConverter, LoadEmbeddedResource($"{ Constants.TemplatesKey }.String.ValueConverter.csx") },
                { StronglyTypedIdConverter.DapperTypeHandler, LoadEmbeddedResource($"{ Constants.TemplatesKey }.String.DapperTypeHandler.csx") },
            }
        };

        private static string LoadEmbeddedResource(string resourceName)
        {
            var resourceStream = ThisAssembly.GetManifestResourceStream(resourceName);
            if (resourceStream is null)
            {
                var existingResources = ThisAssembly.GetManifestResourceNames();
                throw new ArgumentException($"Could not find embedded resource {resourceName}. Available names: {string.Join(", ", existingResources)}");
            }

            using var reader = new StreamReader(resourceStream, Encoding.UTF8);
            return reader.ReadToEnd();
        }
    }
}
