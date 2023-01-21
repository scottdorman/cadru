using System.Threading;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System;
using Cadru.StronglyTypedId.Diagnostics;
using System.Diagnostics;

namespace Cadru.StronglyTypedId
{
    internal sealed partial class SourceGenerator
    {
        internal static class Parser
        {
            static bool IsPossibleAllowedKind(SyntaxKind kind) =>
                kind == SyntaxKind.RecordStructDeclaration ||
                kind == SyntaxKind.RecordDeclaration ||
                kind == SyntaxKind.ClassDeclaration ||
                kind == SyntaxKind.StructDeclaration;

            static bool IsAllowedKind(SyntaxKind kind) =>
                kind == SyntaxKind.RecordStructDeclaration ||
                kind == SyntaxKind.RecordDeclaration;

            public static bool IsSyntaxTargetForGeneration(SyntaxNode syntax)
            {
                return IsPossibleAllowedKind(syntax.Kind()) 
                    && ((TypeDeclarationSyntax)syntax).AttributeLists.Count > 0;
            }

            public static TypeDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context, CancellationToken cancellationToken)
            {
                var semanticModel = context.SemanticModel;
                var compilation = semanticModel.Compilation;
                var typeDeclaration = (TypeDeclarationSyntax)context.Node;

                var attributeSymbol = compilation.GetTypeByMetadataName(Constants.AttributeFullName);
                if (attributeSymbol == null)
                {
                    return null;
                }

                var symbol = semanticModel.GetDeclaredSymbol(typeDeclaration, cancellationToken);
                if (symbol == null)
                {
                    return null;
                }

                foreach (var attribute in symbol.GetAttributes())
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (attributeSymbol.Equals(attribute.AttributeClass, SymbolEqualityComparer.Default))
                    {
                        return typeDeclaration;
                    }
                }

                return null;
            }

            public static bool IsValidSyntax(TypeDeclarationSyntax typeDeclaration, INamedTypeSymbol? typeSymbol, INamedTypeSymbol? attributeSymbol, AttributeInfo? defaults, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken, out EmittedTypeInfo? emittedTypeInfo)
            {
                var isValid = true;
                emittedTypeInfo = null;
                
                if (typeSymbol != null)
                {
                    var defaultBackingType = defaults?.BackingType;
                    var idTypeAttribute = typeSymbol.GetAttributes().FirstOrDefault(a => attributeSymbol!.Equals(a.AttributeClass, SymbolEqualityComparer.Default));
                    Debug.Assert(idTypeAttribute != null);

                    AttributeInfo attributeInfo = new(typeDeclaration, typeSymbol)
                    {
                        BackingType = idTypeAttribute?.DecodeBackingType(),
                        Converters = idTypeAttribute?.DecodeConverters(),
                    };

                    emittedTypeInfo = new EmittedTypeInfo(attributeInfo, defaults, nullableEnabled: true);

                    if (!emittedTypeInfo.IsPartial)
                    {
                        reportDiagnostic(NotPartialDiagnostic.Create(typeDeclaration));
                        isValid &= false;
                    }

                    if (!IsPossibleAllowedKind(typeDeclaration.Kind()))
                    {
                        reportDiagnostic(NotRecordDiagnostic.Create(typeDeclaration));
                        isValid &= false;
                    }

                    if (!emittedTypeInfo.BackingType.HasValue)
                    {
                        reportDiagnostic(InvalidBackingTypeDiagnostic.Create(typeDeclaration));
                        isValid &= false;
                    }

                    if (!emittedTypeInfo.Converters.HasValue)
                    {
                        reportDiagnostic(InvalidConverterDiagnostic.Create(typeDeclaration));
                        isValid &= false;
                    }
                }

                return isValid;
            }
        }
    }
}
