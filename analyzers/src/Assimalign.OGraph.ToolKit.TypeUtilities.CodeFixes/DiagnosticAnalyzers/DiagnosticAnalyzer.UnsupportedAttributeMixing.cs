using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Xml.Linq;

namespace Assimalign.OGraph.ToolKit.Analyzers;


[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class UnsupportedAttributeMixingDiagnosticAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(DiagnosticAnalyzerRules.ConflictAttributes);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(static context =>
        {
            var declaration = (ClassDeclarationSyntax)context.Node;

            // Get all attributes applied to the class
            var attributes = declaration.AttributeLists.SelectMany(list => list.Attributes);

            if (HasAttribute("ValueType") && HasAttribute("PickType"))
            {
                var diagnostic = Diagnostic.Create(
                    DiagnosticAnalyzerRules.ConflictAttributes,
                    attributes.First().GetLocation(),
                    attributes.Skip(1).Select(p=>p.GetLocation()),
                    "ValueTypeAttribute",
                    "PickTypeAttribute");

                context.ReportDiagnostic(diagnostic);
            }

            bool HasAttribute(string name)
            {
                return attributes.Any(p =>
                {
                    if (p.Name is IdentifierNameSyntax id && id.Identifier.ValueText.StartsWith(name))
                    {
                        return true;
                    }
                    return false;
                });
            }

        }, SyntaxKind.ClassDeclaration);
    }
}
