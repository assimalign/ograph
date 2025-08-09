using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assimalign.OGraph.ToolKit.Analyzers;

internal static class DiagnosticAnalyzerRules
{
    public static readonly DiagnosticDescriptor ConflictAttributes = new DiagnosticDescriptor(
        id: "OG001",
        title: "Conflicting Attributes Detected",
        messageFormat: "'{0}' and '{1}' cannot be used together on the same class",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor RequiresParameterlessConstructor = new DiagnosticDescriptor(
        id: "OG002",
        title: "Attributes Requires Parameterless Constructor",
        messageFormat: "Class {0} requires a parameterless constructor in order to use attributes '{1}'",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);
}
