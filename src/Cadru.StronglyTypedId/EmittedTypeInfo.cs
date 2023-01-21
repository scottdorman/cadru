using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Cadru.StronglyTypedId.Diagnostics;
using System.Diagnostics;
using System.Collections;

namespace Cadru.StronglyTypedId
{
    internal sealed class EmittedTypeInfo
    {
        private TypeDeclarationSyntax _typeDeclaration;
        private INamedTypeSymbol _type;

        public EmittedTypeInfo(AttributeInfo attributeInfo, AttributeInfo? globalValues, bool nullableEnabled = false)
        {
            if (attributeInfo.TypeSymbol == null)
            {
                throw new InvalidOperationException();
            }

            if (attributeInfo.TypeDeclaration == null)
            {
                throw new InvalidOperationException();
            }

            _type = attributeInfo.TypeSymbol!;
            _typeDeclaration = attributeInfo.TypeDeclaration;

            IsPartial = _typeDeclaration.Modifiers.Any(SyntaxKind.PartialKeyword);
            IsSealed = _typeDeclaration.Modifiers.Any(SyntaxKind.SealedKeyword);

            BackingType = (attributeInfo.BackingType, globalValues?.BackingType) switch
            {
                (StronglyTypedId.BackingType.Default, null) => Defaults.BackingType,
                (StronglyTypedId.BackingType.Default, StronglyTypedId.BackingType.Default) => Defaults.BackingType,
                (StronglyTypedId.BackingType.Default, var globalDefault) => globalDefault,
                (null, StronglyTypedId.BackingType.Default) => Defaults.BackingType,
                (null, var globalDefault) => globalDefault,
                (var specificValue, _) => specificValue,
            };

            Converters = (attributeInfo.Converters, globalValues?.Converters) switch
            {
                (StronglyTypedIdConverter.Default, null) => Defaults.Converters,
                (StronglyTypedIdConverter.Default, StronglyTypedIdConverter.Default) => Defaults.Converters,
                (StronglyTypedIdConverter.Default, var globalDefault) => globalDefault,
                (null, StronglyTypedIdConverter.Default) => Defaults.Converters,
                (null, var globalDefault) => globalDefault,
                (var specificValue, _) => specificValue,
            };

            if (Converters.HasValue && Converters.Value != StronglyTypedIdConverter.None)
            {
                ConverterAttributes = new BitArray(new bool[]
                {
                    Converters.Value.HasFlag(StronglyTypedIdConverter.TypeConverter),
                    Converters.Value.HasFlag(StronglyTypedIdConverter.NewtonsoftJson),
                    Converters.Value.HasFlag(StronglyTypedIdConverter.SystemTextJson),
                    Converters.Value.HasFlag(StronglyTypedIdConverter.EfCoreValueConverter),
                    Converters.Value.HasFlag(StronglyTypedIdConverter.DapperTypeHandler),
                });
            }
            else
            {
                ConverterAttributes = new BitArray(0);
            }

            NewValue = attributeInfo.NewValue;
            EmptyValue = attributeInfo.EmptyValue;
            NullableEnabled = nullableEnabled;
        }

        public bool IsClass => _typeDeclaration.IsKind(SyntaxKind.ClassDeclaration);
        public bool IsRecord => _typeDeclaration.IsKind(SyntaxKind.RecordDeclaration);
        public bool IsStruct => _typeDeclaration.IsKind(SyntaxKind.StructDeclaration);
        public bool IsRecordStruct => _typeDeclaration.IsKind(SyntaxKind.RecordStructDeclaration);
        public bool IsReferenceType => IsClass || IsRecord;
        public bool IsPartial { get; } = false;
        public bool IsSealed { get; } = false;
        public string EmittedTypeName => $"{_type.Name }.g.cs";
        public string GetEmittedConverterName(StronglyTypedIdConverter converter) => $"{ _type.Name }.{ converter.GetHintName() }.g.cs";
        public bool NullableEnabled { get; }
        public TypeKind Kind => _type.TypeKind;
        public string ContainingNamespace => _type.ContainingNamespace.ToString();
        public string TypeName => _type.Name;
        public string? NewValue { get; }
        public string? EmptyValue { get; }
        public BackingType? BackingType { get; }
        public StronglyTypedIdConverter? Converters { get; } = StronglyTypedIdConverter.None;
        public string AssemblyVersion => ThisAssembly.AssemblyVersion;
        public List<string> UsingDirectives { get; init; } = new List<string>();
        public BitArray ConverterAttributes { get; init; }
    }
}
