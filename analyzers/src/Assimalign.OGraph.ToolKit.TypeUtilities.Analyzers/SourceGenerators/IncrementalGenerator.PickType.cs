using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace Assimalign.OGraph.SourceGeneration;

[Generator]
public sealed class PickTypeIncrementalGenerator : IIncrementalGenerator
{
    public const string AttributeName = "PickTypeAttribute";
    public const string AttributeFullName = "System.PickTypeAttribute";
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<PickObjectTypeContext> provider = context.SyntaxProvider.ForAttributeWithMetadataName(
            AttributeFullName,
            predicate: (node, _) => node is ClassDeclarationSyntax declaration && declaration.HasParameterlessConstructor(),
            transform: static (context, _) =>
            {
                var symbol = (INamedTypeSymbol)context.TargetSymbol;
                var declaration = (TypeDeclarationSyntax)context.TargetNode;
                var membersToInclude = declaration.GetArrayAttributeArgumentOfType<string>(AttributeFullName, "Properties");
                var source = new PickObjectTypeContext()
                {
                    NameOfRoot = declaration.GetNameOfType(),
                    NamespaceOfRoot = declaration.GetNamespaceOfType(),
                    NameOfType = declaration.GetLiteralAttributeArgumentOfTypeByIndex<string>(AttributeFullName, 0),
                    NamespaceOfType = declaration.GetLiteralAttributeArgumentOfTypeByName<string>(AttributeFullName, "Namespace") ?? declaration.GetNamespaceOfType(),
                    Properties = declaration.Members.OfType<PropertyDeclarationSyntax>()
                        .Where(member =>
                        {
                            return membersToInclude.Contains(member.Identifier.Text) &&
                                member.Modifiers.Any(i => i.ValueText == "public") &&
                                member.AccessorList is not null &&
                                member.AccessorList.Accessors.Any(SyntaxKind.GetAccessorDeclaration) &&
                                member.AccessorList.Accessors.Any(SyntaxKind.SetAccessorDeclaration) &&
                                member.AccessorList.Accessors.All(accessor => accessor.ExpressionBody is null);
                        })
                        .Select(property =>
                        {
                            return new PickObjectPropertyContext()
                            {
                                Name = property.Identifier.ValueText,
                                Type = property.Type switch
                                {
                                    IdentifierNameSyntax identifier => identifier.Identifier.ValueText,
                                    PredefinedTypeSyntax predefined => predefined.Keyword.ValueText,
                                    _ => property.Type.ToFullString().Trim()
                                }
                            };
                        })
                        .ToList()
                };

                return source;
            });

        context.RegisterSourceOutput(provider, (context, source) =>
        {
            var builder = new StringBuilder();

            GeneratePickType(builder, source);

            context.AddSource($"{source.NameOfType}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        });
    }

    partial class PickObjectTypeContext
    {
        public string? NameOfRoot { get; set; }
        public string? NamespaceOfRoot { get; set; }
        public string? NameOfType { get; set; }
        public string? NamespaceOfType { get; set; }
        public IEnumerable<PickObjectPropertyContext> Properties { get; set; } = [];
    }

    partial class PickObjectPropertyContext
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
    }

    private static void GeneratePickType(StringBuilder builder, PickObjectTypeContext context)
    {
        builder.AppendLine($"namespace {context.NamespaceOfType}");
        builder.AppendLine("{");
        builder.AppendTabbedLine(1, $"partial class {context.NameOfType}");
        builder.AppendTabbedLine(1, "{");

        foreach (var property in context.Properties)
        {
            builder.AppendTabbedLine(2, $"public {property.Type} {property.Name} {{ get; set; }}");
        }



        builder.AppendTabbedLine(2, $"public static implicit operator {context.NameOfRoot}({context.NameOfType} pick)");
        builder.AppendTabbedLine(2, "{");

        builder.AppendTabbedLine(3, $"return new {context.NameOfType}()");
        builder.AppendTabbedLine(3, "{");
        foreach (var property in context.Properties)
        {
            builder.AppendTabbedLine(4,$"{property.Name} = pick.{property.Name},");
        }
        builder.AppendTabbedLine(3, "};");


        builder.AppendTabbedLine(2, "}");
        builder.AppendTabbedLine(1, "}");
        builder.AppendLine("}");
        //        builder.Append($$"""
        //namespace {{context.Namespace}}
        //{
        //    public class {{context.PickTypeName}}
        //    {

        //""");

        //        foreach (var property in context.Properties)
        //        {
        //            builder.Append("        ");
        //            builder.AppendLine(property);
        //        }

        //        builder.Append($$"""
        //        public static implicit operator {{context.RootTypeName}}({{context.PickTypeName}} pick)
        //        {
        //            return new {{context.RootTypeName}}()
        //            {

        //""");


        //foreach (var name in context.Names)
        //{
        //    builder.Append("                ");
        //    builder.AppendLine($"{name} = pick.{name},");
        //        //}
        //    };
        //}

        //builder.Append($$"""

        //    }
        //}
        //""");
    }
}
