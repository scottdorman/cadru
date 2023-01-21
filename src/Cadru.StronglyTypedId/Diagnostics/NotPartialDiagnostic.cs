﻿using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.CodeAnalysis;

namespace Cadru.StronglyTypedId.Diagnostics
{
    internal static class NotPartialDiagnostic
    {
        public static Diagnostic Create(SyntaxNode currentNode) =>
            Diagnostic.Create(
                new DiagnosticDescriptor(
                    Resources.NotPartialDiagnostic_Id,
                    Resources.NotPartialDiagnostic_Title,
                    Resources.NotPartialDiagnostic_Message,
                    category: Constants.Usage,
                    defaultSeverity: DiagnosticSeverity.Warning,
                    isEnabledByDefault: true),
                currentNode.GetLocation());
    }
}
