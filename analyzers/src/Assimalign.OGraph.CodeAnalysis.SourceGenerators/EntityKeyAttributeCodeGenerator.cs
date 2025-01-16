using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Assimalign.OGraph.CodeAnalysis;


[Generator]
public class EntityKeyAttributeCodeGenerator : IIncrementalGenerator
{
    partial record class PartialStructToGen
    {
        public string? Namespace { get; set; }
        public string? Name { get; set; }
    }

    private static string GetNameSpace(StructDeclarationSyntax structSymbol)
    {
        // determine the namespace the struct is declared in, if any
        SyntaxNode? potentialNamespaceParent = structSymbol.Parent;
        while (potentialNamespaceParent != null &&
               potentialNamespaceParent is not NamespaceDeclarationSyntax
               && potentialNamespaceParent is not FileScopedNamespaceDeclarationSyntax)
        {
            potentialNamespaceParent = potentialNamespaceParent.Parent;
        }

        if (potentialNamespaceParent is BaseNamespaceDeclarationSyntax namespaceParent)
        {
            string nameSpace = namespaceParent.Name.ToString();
            while (true)
            {
                if (namespaceParent.Parent is not NamespaceDeclarationSyntax namespaceParentParent)
                {
                    break;
                }

                namespaceParent = namespaceParentParent;
                nameSpace = $"{namespaceParent.Name}.{nameSpace}";
            }

            return nameSpace;
        }
        return string.Empty;
    }
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<PartialStructToGen> partialStructToGen = context.SyntaxProvider.ForAttributeWithMetadataName(
            "Assimalign.OGraph.EntityKeyAttribute",
            predicate: (node, _) =>
            {
                return node is StructDeclarationSyntax structNode;
            },
            transform: static (context, _) =>
            {
                var node = (StructDeclarationSyntax)context.TargetNode;

                return new PartialStructToGen()
                {
                    Namespace = GetNameSpace(node),
                    Name = node.Identifier.Text
                };
            });

        context.RegisterSourceOutput(partialStructToGen, (context, source) =>
        {
            var code = GenerateCode(source);

            context.AddSource($"{source.Name}.g.cs", SourceText.From(code, Encoding.UTF8));
        });



        //// Register a syntax provider that will collect structs with the EntityKeyAttribute
        //var structsWithEntityKeyAttribute = context.SyntaxProvider
        //    .CreateSyntaxProvider(
        //        predicate: (syntaxNode, _) => IsStructWithEntityKeyAttribute(syntaxNode),
        //        transform: (context, _) => GetStructWithEntityKey(context))
        //    .Where(static m => m is not null);

        //// Generate the code based on the found structs
        //context.RegisterSourceOutput(structsWithEntityKeyAttribute, (context, structInfo) =>
        //{
        //    var generatedCode = GenerateCode((StructDeclarationSyntax)structInfo!.Value.Item1, structInfo.Value.Item2!);
        //    context.AddSource($"{structInfo.Value.Item2}.g.cs", SourceText.From(generatedCode, Encoding.UTF8));
        //});
    }

    //private static bool IsStructWithEntityKeyAttribute(SyntaxNode node)
    //{
    //    // Check if the node is a struct declaration with the EntityKeyAttribute
    //    return node is StructDeclarationSyntax structDeclaration &&
    //           structDeclaration.AttributeLists.Count > 0;
    //}

    //private static (StructDeclarationSyntax, string)? GetStructWithEntityKey(GeneratorSyntaxContext context)
    //{
    //    var structDeclaration = (StructDeclarationSyntax)context.Node;

    //    // Find the attribute and extract its key value
    //    var attributeSyntax = structDeclaration.AttributeLists
    //        .SelectMany(list => list.Attributes)
    //        .FirstOrDefault(attr => context.SemanticModel.GetSymbolInfo(attr).Symbol is IMethodSymbol methodSymbol &&
    //                                 methodSymbol.ContainingType.ToDisplayString() == "EntityKeyAttribute");

    //    if (attributeSyntax != null)
    //    {
    //        // Extract the key value from the attribute
    //        var keyArgument = attributeSyntax.ArgumentList?.Arguments.FirstOrDefault();
    //        if (keyArgument != null)
    //        {
    //            var key = context.SemanticModel.GetConstantValue(keyArgument.Expression).ToString();
    //            return (structDeclaration, key);
    //        }
    //    }

    //    return null;
    //}

    private static string GenerateCode(PartialStructToGen gen)
    {
        // Generate additional code for the struct
        var sourceBuilder = new StringBuilder($@"
namespace {gen.Namespace}
{{
    public partial struct {gen.Name}
    {{
        public string GetEntityKey() => ""test"";
    }}
}}");

        return sourceBuilder.ToString();
    }
}
