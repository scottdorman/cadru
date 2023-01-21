using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.CodeAnalysis;

namespace Cadru.StronglyTypedId.Diagnostics
{
    internal static class InvalidConfigurationDiagnostic
    {
        public static Diagnostic Create(SyntaxNode currentNode) =>
            Diagnostic.Create(
                new DiagnosticDescriptor(
                    Resources.InvalidConfigurationDiagnostic_Id,
                    Resources.InvalidConfigurationDiagnostic_Title,
                    Resources.InvalidConfigurationDiagnostic_Message, 
                    category: Constants.Usage, 
                    defaultSeverity: DiagnosticSeverity.Warning, 
                    isEnabledByDefault: true),
                currentNode.GetLocation());
    }
}
