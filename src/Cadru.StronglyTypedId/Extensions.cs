using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Threading;
using System.Data;
using System.Collections.Immutable;
using System.Xml.Linq;
using Cadru.StronglyTypedId.Diagnostics;

namespace Cadru.StronglyTypedId
{
    internal static class Extensions
    {
        public static string GetHintName(this StronglyTypedIdConverter converter)
        {
            return converter switch
            {
                StronglyTypedIdConverter.DapperTypeHandler => "DapperTypeHandler",
                StronglyTypedIdConverter.EfCoreValueConverter => "ValueConverter",
                StronglyTypedIdConverter.NewtonsoftJson => "NewtonsoftJsonConverter",
                StronglyTypedIdConverter.SystemTextJson => "JsonConverter",
                StronglyTypedIdConverter.TypeConverter => "TypeConverter",
                _ => string.Empty
            };
        }

        public static string GetAttributeName(this StronglyTypedIdConverter converter)
        {
            return converter switch
            {
                StronglyTypedIdConverter.DapperTypeHandler => "DapperTypeHandler",
                StronglyTypedIdConverter.EfCoreValueConverter => "ValueConverter",
                StronglyTypedIdConverter.NewtonsoftJson => "Newtonsoft.Json.JsonConverter",
                StronglyTypedIdConverter.SystemTextJson => "System.Text.Json.Serialization.JsonConverter",
                StronglyTypedIdConverter.TypeConverter => "System.ComponentModel.TypeConverter",
                _ => string.Empty
            };
        }
        //public static object? DecodeArgument(this List<AttributeArgumentSyntax>? attributeArguments, SemanticModel? semanticModel, int index)
        //{
        //    if (attributeArguments != null && semanticModel != null)
        //    {
        //        var arg = attributeArguments.ElementAtOrDefault(index);
        //        if (arg != null)
        //        {
        //            var constantValue = semanticModel.GetConstantValue(arg.Expression);
        //            if (constantValue.HasValue)
        //            {
        //                return constantValue.Value;
        //            }
        //        }
        //    }

        //    return null;
        //}

        //public static bool HasArgument(this List<AttributeArgumentSyntax>? attributeArguments, string name, StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
        //{
        //    return attributeArguments.GetArgument(name, comparisonType) != null;
        //}

        public static AttributeArgumentSyntax? GetArgument(this List<AttributeArgumentSyntax>? attributeArguments, string name, StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
        {
            return attributeArguments?.FirstOrDefault(a => a.HasName(name, comparisonType));
        }

        private static int IndexOfNamedArgument(ImmutableArray<KeyValuePair<string, TypedConstant>> namedArguments, string name)
        {
            for (int num = namedArguments.Length - 1; num >= 0; num--)
            {
                if (string.Equals(namedArguments[num].Key, name, StringComparison.Ordinal))
                {
                    return num;
                }
            }

            return -1;
        }

        //public static BackingType? DecodeBackingType(this List<AttributeArgumentSyntax>? attributeArguments)
        //{
        //    BackingType? backingType = null;
        //    var idTypeValue = attributeArguments.GetArgument("backingType");
        //    if (idTypeValue != null)
        //    {
        //        if (idTypeValue.Expression is MemberAccessExpressionSyntax memberAccessExpressionSyntax)
        //        {
        //            if (Enum.TryParse(memberAccessExpressionSyntax.Name.Identifier.ValueText, out BackingType rawBackingType))
        //            {
        //                backingType = rawBackingType;
        //            }
        //        }
        //    }

        //    return backingType;
        //}

        internal static T? DecodeValue<T>(this TypedConstant typedConstant, SpecialType specialType)
        {
            typedConstant.TryDecodeValue(specialType, out T? value);
            return value;
        }

        internal static bool TryDecodeValue<T>(this TypedConstant typedConstant, SpecialType specialType, out T? value)
        {
            if (typedConstant.Kind == TypedConstantKind.Error)
            {
                value = default;
                return false;
            }

            if (typedConstant.Type!.SpecialType == specialType || (typedConstant.Type!.TypeKind == TypeKind.Enum && specialType == SpecialType.System_Enum))
            {
                value = (T?)typedConstant.Value;
                return true;
            }

            value = default;
            return false;
        }

        //public static T? DecodeAttributeArgument<T>(this AttributeData attributeData, string name)
        //{
        //    T? value = default;
        //    if (!attributeData.ConstructorArguments.IsEmpty)
        //    {
        //        value = (T)attributeData.ConstructorArguments[0].Value!;
        //    }

        //    if (!attributeData.NamedArguments.IsEmpty)
        //    {
        //        var num = IndexOfNamedArgument(attributeData.NamedArguments, name);
        //        if (num >= 0)
        //        {
        //            value = attributeData.NamedArguments[num].Value.DecodeValue<T>(SpecialType.System_Enum);
        //        }
        //    }

        //    return value;
        //}

        public static BackingType? DecodeBackingType(this AttributeData attributeData)
        {
            BackingType? backingType = null;
            if (!attributeData.ConstructorArguments.IsEmpty)
            {
                backingType = (BackingType)attributeData.ConstructorArguments[0].Value!;
            }

            if (!attributeData.NamedArguments.IsEmpty)
            {
                int num = IndexOfNamedArgument(attributeData.NamedArguments, "BackingType");
                if (num >= 0)
                {
                    backingType = attributeData.NamedArguments[num].Value.DecodeValue<BackingType>(SpecialType.System_Enum);
                }
            }

            return backingType; // ?? BackingType.Default;
        }

        public static StronglyTypedIdConverter? DecodeConverters(this AttributeData attributeData)
        {
            StronglyTypedIdConverter? converters = null;
            if (!attributeData.ConstructorArguments.IsEmpty)
            {
                converters = (StronglyTypedIdConverter)attributeData.ConstructorArguments[1].Value!;
            }

            if (!attributeData.NamedArguments.IsEmpty)
            {
                int num = IndexOfNamedArgument(attributeData.NamedArguments, "Converters");
                if (num >= 0)
                {
                    converters = attributeData.NamedArguments[num].Value.DecodeValue<StronglyTypedIdConverter>(SpecialType.System_Enum);
                }
            }

            return converters;// ?? StronglyTypedIdConverter.None;
        }

        //public static object? DecodeNamedArgument(this List<AttributeArgumentSyntax>? attributeArguments, SemanticModel? semanticModel, string name, StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
        //{
        //    if (attributeArguments != null && semanticModel != null)
        //    {
        //        var arg = attributeArguments.GetArgument(name, comparisonType);
        //        if (arg != null)
        //        {
        //            var constantValue = semanticModel.GetConstantValue(arg.Expression);
        //            if (constantValue.HasValue)
        //            {
        //                return constantValue.Value;
        //            }
        //        }
        //    }

        //    return null;
        //}

        public static bool HasName(this AttributeArgumentSyntax attributeArgumentSyntax, string name, StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
        {
            if (attributeArgumentSyntax.NameEquals?.Name is IdentifierNameSyntax identifierNameSyntax)
            {
                return identifierNameSyntax.Identifier.Text.Equals(name, comparisonType);
            }

            return false;
        }

        //public static bool HasNameLike(this AttributeSyntax attributeSyntax, string name, StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
        //{
        //    if (attributeSyntax.Name is IdentifierNameSyntax identifierNameSyntax)
        //    {
        //        return identifierNameSyntax.Identifier.Text.StartsWith(name, comparisonType);
        //    }

        //    return false;
        //}

        public static string? GetTemplate(this EmittedTypeInfo typeInfo, ResourceCollection resourceCollection)
        {
            return typeInfo switch
            {
                _ when typeInfo.IsRecord || typeInfo.IsRecordStruct => resourceCollection.RecordTemplate,
                _ when typeInfo.IsClass => resourceCollection.ClassTemplate,
                _ when typeInfo.IsStruct => resourceCollection.StructTemplate,
                _ => null
            };
        }
    }
}
