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

namespace Assimalign.OGraph.CodeAnalysis;


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

            bool HasScalarType = HasAttribute("ScalarTypeAttribute");

            // Exit analysis if no scalar type attribute
            if (!HasScalarType)
            {
                return;
            }

            foreach (var attr in attributes)
            {
                if (context.SemanticModel.GetSymbolInfo(attr).Symbol is not IMethodSymbol symbol)
                {
                    continue;
                }

                if (symbol.ContainingType.Name == "OmitComplexTypeAttribute")
                {
                    var diagnostic = Diagnostic.Create(
                        DiagnosticAnalyzerRules.ConflictAttributes,
                        attr.GetLocation(),
                        "ScalarTypeAttribute",
                        "OmitComplexTypeAttribute");

                    context.ReportDiagnostic(diagnostic);
                }

                else if (symbol.ContainingType.Name == "PickComplexTypeAttribute")
                {
                    var diagnostic = Diagnostic.Create(
                        DiagnosticAnalyzerRules.ConflictAttributes,
                        attr.GetLocation(),
                        "ScalarTypeAttribute",
                        "PickComplexTypeAttribute");

                    context.ReportDiagnostic(diagnostic);
                }
            }

            bool HasAttribute(string name)
            {
                return attributes.Any(attr =>
                    context.SemanticModel.GetSymbolInfo(attr).Symbol is IMethodSymbol methodSymbol &&
                    methodSymbol.ContainingType.Name == name);
            }

        }, SyntaxKind.ClassDeclaration);
    }
}
