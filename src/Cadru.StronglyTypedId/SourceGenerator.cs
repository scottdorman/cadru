using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Cadru.StronglyTypedId
{
    [Generator]
    internal sealed partial class SourceGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
#if DEBUG_SOURCE_GENERATOR
            if (!Debugger.IsAttached)
            {
               Debugger.Launch();
            }
#endif

            context.RegisterPostInitializationOutput(ctx =>
            {
                foreach(var source in EmbeddedSources.AttributeSources)
                {
                    ctx.AddSource(source.Key, source.Text);
                }
            });
            
            var types = context.SyntaxProvider.CreateSyntaxProvider(
             predicate: static (syntax, cancellationToken) => Parser.IsSyntaxTargetForGeneration(syntax),
             transform: static (ctx, cancellationToken) => Parser.GetSemanticTargetForGeneration(ctx, cancellationToken))
                 .Where(static m => m is not null);

            var typesToProcess = context.CompilationProvider.Combine(types.Collect());
            context.RegisterSourceOutput(typesToProcess, (spc, source) => Execute(spc, source.Left, source.Right!));
        }

        private static void Execute(SourceProductionContext context, Compilation compilation, ImmutableArray<TypeDeclarationSyntax> typeDeclarations)
        {
            var attributeSymbol = compilation.GetTypeByMetadataName("Cadru.StronglyTypedId.StronglyTypedIdAttribute");
            Debug.Assert(attributeSymbol != null);

            AttributeInfo? globalValuesAttributeInfo = null;
            var globalValuesAttributeSymbol = compilation.GetTypeByMetadataName("Cadru.StronglyTypedId.StronglyTypedIdDefaultsAttribute");
            if (globalValuesAttributeSymbol != null)
            {
                var globalValuesAttribute = compilation.Assembly.GetAttributes().FirstOrDefault(x => globalValuesAttributeSymbol.Equals(x.AttributeClass, SymbolEqualityComparer.Default));
                globalValuesAttributeInfo = new(globalValuesAttributeSymbol)
                {
                    BackingType = globalValuesAttribute?.DecodeBackingType() ?? Defaults.BackingType,
                    Converters = globalValuesAttribute?.DecodeConverters() ?? Defaults.Converters,
                };
            }

            foreach (var typeDeclaration in typeDeclarations)
            {
                context.CancellationToken.ThrowIfCancellationRequested();
                var semanticModel = compilation.GetSemanticModel(typeDeclaration.SyntaxTree);
                var typeSymbol = semanticModel?.GetDeclaredSymbol(typeDeclaration);

                if (Parser.IsValidSyntax(typeDeclaration, typeSymbol, attributeSymbol, globalValuesAttributeInfo, context.ReportDiagnostic, context.CancellationToken, out var typeInfo))
                {
                    if (typeInfo != null)
                    {
                        var results = Emitter.Create(typeInfo);
                        foreach (var (hintName, source) in results)
                        {
                            if (source != null)
                            {
                                context.AddSource(hintName, source);
                            }
                        }
                    }
                }
            }
        }
    }
}
