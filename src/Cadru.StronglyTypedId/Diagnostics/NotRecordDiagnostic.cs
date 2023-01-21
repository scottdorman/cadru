using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.CodeAnalysis;

namespace Cadru.StronglyTypedId.Diagnostics
{
    internal static class NotRecordDiagnostic
    {
        public static Diagnostic Create(SyntaxNode currentNode) =>
            Diagnostic.Create(
                new DiagnosticDescriptor(
                    Resources.NotRecordDiagnostic_Id,
                    Resources.NotRecordDiagnostic_Title,
                    Resources.NotRecordDiagnostic_Message,
                    category: Constants.Usage,
                    defaultSeverity: DiagnosticSeverity.Warning,
                    isEnabledByDefault: true),
                currentNode.GetLocation());
    }
}
