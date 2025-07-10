using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;
using System.Linq;

namespace Assimalign.OGraph.CodeAnalysis;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class RequiresParameterlessConstructorDiagnosticAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => 
        ImmutableArray.Create(DiagnosticAnalyzerRules.RequiresParameterlessConstructor);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeClassDeclaration, SyntaxKind.ClassDeclaration);
    }

    private static void AnalyzeClassDeclaration(SyntaxNodeAnalysisContext context)
    {
        var classDeclaration = (ClassDeclarationSyntax)context.Node;

        // Get all attributes applied to the class
        var attributes = classDeclaration.AttributeLists.SelectMany(list => list.Attributes);

        bool hasParameterlessConstructor = classDeclaration.HasParameterlessConstructor();
        bool hasOmitComplexType = HasAttribute(context, attributes, "OmitComplexTypeAttribute");
        bool hasPickComplexType = HasAttribute(context, attributes, "PickComplexTypeAttribute");

        // Report a diagnostic if both attributes are present
        if (hasParameterlessConstructor && hasOmitComplexType)
        {
            
            var diagnostic = Diagnostic.Create(
                DiagnosticAnalyzerRules.RequiresParameterlessConstructor,
                classDeclaration.Identifier.GetLocation(),
                classDeclaration.Identifier.ToString(),
                "OmitComplexTypeAttribute");

            context.ReportDiagnostic(diagnostic);
        }
        if (hasParameterlessConstructor && hasPickComplexType)
        {

            var diagnostic = Diagnostic.Create(
                DiagnosticAnalyzerRules.RequiresParameterlessConstructor,
                classDeclaration.Identifier.GetLocation(),
                classDeclaration.Identifier.ToString(),
                "PickComplexTypeAttribute");

            context.ReportDiagnostic(diagnostic);
        }
    }

    private static bool HasAttribute(SyntaxNodeAnalysisContext context, IEnumerable<AttributeSyntax> attributes, string name)
    {
        return attributes.Any(attr =>
            context.SemanticModel.GetSymbolInfo(attr).Symbol is IMethodSymbol methodSymbol &&
            methodSymbol.ContainingType.Name == name);
    }
}
