using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace Assimalign.OGraph.CodeAnalysis;

[Generator]
public class PickComplexTypeAttributeIncrementalGenerator : IIncrementalGenerator
{
    public const string AttributeName = "Assimalign.OGraph.PickComplexTypeAttribute";
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<PickObjectTypeContext> provider = context.SyntaxProvider.ForAttributeWithMetadataName(
            AttributeName,
            predicate: (node, _) => node is ClassDeclarationSyntax declaration && declaration.HasParameterlessConstructor(),
            transform: static (context, _) =>
            {
                var declaration = (TypeDeclarationSyntax)context.TargetNode;

                var source = new PickObjectTypeContext()
                {
                    Namespace = declaration.GetNamespaceOfType(),
                    RootTypeName = declaration.GetNameOfType(),
                    PickTypeName = declaration.GetLiteralAttributeArgumentOfType<string>(AttributeName, "Name"),
                    ExcludeGdmType = declaration.GetLiteralAttributeArgumentOfType<bool>(AttributeName, "ExcludeGdmType"),
                    GdmTypeNamespace = declaration.GetLiteralAttributeArgumentOfType<string>(AttributeName, "GdmTypeNamespace"),
                    Names = declaration.GetArrayAttributeArgumentOfType<string>(AttributeName, "Properties"),
                };

                var props = new List<string>();

                foreach (var name in source.Names)
                {
                    foreach (var member in declaration.Members.OfType<PropertyDeclarationSyntax>())
                    {
                        if (member.Identifier.ToString() == name)
                        {
                            props.Add(member.ToString().Trim('"'));
                        }
                    }
                }

                source.Properties = props;

                return source;
            });

        context.RegisterSourceOutput(provider, (context, source) =>
        {
            var builder = new StringBuilder();

            GeneratePickType(builder, source);

            context.AddSource($"{source.PickTypeName}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        });
    }

    #region Partials

    partial class PickObjectTypeContext
    {
        public string? RootTypeName { get; set; }
        public string? PickTypeName { get; set; }
        public string? Namespace { get; set; }
        public bool ExcludeGdmType { get; set; }
        public string? GdmTypeNamespace { get; set; }
        public IEnumerable<string> Properties { get; set; } = [];
        public IEnumerable<string> Names { get; set; } = [];
    }

    #endregion

    private static void GeneratePickType(StringBuilder builder, PickObjectTypeContext context)
    {


        builder.Append($$"""
namespace {{context.Namespace}}
{
    public class {{context.PickTypeName}}
    {

""");

        foreach (var property in context.Properties)
        {
            builder.Append("        ");
            builder.AppendLine(property);
        }

        builder.Append($$"""
        public static implicit operator {{context.RootTypeName}}({{context.PickTypeName}} pick)
        {
            return new {{context.RootTypeName}}()
            {

""");


        foreach (var name in context.Names)
        {
            builder.Append("                ");
            builder.AppendLine($"{name} = pick.{name},");
        }

        builder.Append($$"""
            };
        }
    }
}
""");
    }
}
