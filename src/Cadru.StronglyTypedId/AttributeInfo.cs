using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;

namespace Cadru.StronglyTypedId
{
    internal sealed class AttributeInfo
    {
        public AttributeInfo(INamedTypeSymbol? typeSymbol)
        {
            TypeSymbol = typeSymbol;
        }

        public AttributeInfo(TypeDeclarationSyntax? typeDeclaration, INamedTypeSymbol? typeSymbol)
            : this(typeSymbol)
        {
            TypeDeclaration = typeDeclaration;
        }

        public BackingType? BackingType { get; set; }

        public StronglyTypedIdConverter? Converters { get; set; }

        public INamedTypeSymbol? TypeSymbol { get;}
        public TypeDeclarationSyntax? TypeDeclaration { get; }
        public string? NewValue { get; set; }
        public string? EmptyValue { get; set; }
    }
}
