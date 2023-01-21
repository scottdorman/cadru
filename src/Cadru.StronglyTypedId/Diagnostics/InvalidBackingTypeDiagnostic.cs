using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.CodeAnalysis;

namespace Cadru.StronglyTypedId.Diagnostics
{
    internal static class InvalidBackingTypeDiagnostic
    {
        public static Diagnostic Create(SyntaxNode currentNode) =>
            Diagnostic.Create(
                new DiagnosticDescriptor(
                    Resources.InvalidBackingTypeDiagnostic_Id,
                    Resources.InvalidBackingTypeDiagnostic_Title,
                    Resources.InvalidBackingTypeDiagnostic_Message, 
                    category: Constants.Usage, 
                    defaultSeverity: DiagnosticSeverity.Warning, 
                    isEnabledByDefault: true),
                currentNode.GetLocation());
    }
}
