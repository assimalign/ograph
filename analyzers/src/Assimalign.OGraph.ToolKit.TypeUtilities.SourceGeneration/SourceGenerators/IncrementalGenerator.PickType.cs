using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;

namespace Assimalign.OGraph.ToolKit.TypeUtilities.SourceGeneration;


[Generator]
public sealed class PickTypeIncrementalGenerator : IIncrementalGenerator
{
    public const string AttributeName = "PickTypeAttribute";
    public const string AttributeFullName = "System.PickTypeAttribute";
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<PickTypContext> provider = context.SyntaxProvider.ForAttributeWithMetadataName(
            AttributeFullName,
            predicate: static (node, _) => node is ClassDeclarationSyntax declaration && declaration.HasParameterlessConstructor(),
            transform: static (context, _) =>
            {
                var symbol = (INamedTypeSymbol)context.TargetSymbol;
                var declaration = (TypeDeclarationSyntax)context.TargetNode;
                var source = new PickTypContext();

                //context.SemanticModel.Compilation.GetTypeByMetadataName
                foreach (var attribute in declaration.GetAttributesByName(AttributeName)) 
                {
                    var membersToInclude = attribute.GetArrayOfLiteralArguments<string>("Properties");
                    
                    var pickType = new PickType()
                    {
                        NameOfRoot = declaration.GetNameOfType(),
                        NamespaceOfRoot = declaration.GetNamespaceOfType(),
                        NameOfType = attribute.GetLiteralArgumentByIndex<string>(0),
                        NamespaceOfType = attribute.GetLiteralArgumentByName<string>("Namespace") ?? declaration.GetNamespaceOfType(),
                        Properties = declaration.Members.OfType<PropertyDeclarationSyntax>()
                            .Where(member =>
                            {
                                // Get all properties that are public, have both get and set accessors, and are not expression-bodied
                                return membersToInclude.Contains(member.Identifier.Text) &&
                                    member.Modifiers.Any(i => i.ValueText == "public") &&
                                    member.AccessorList is not null &&
                                    member.AccessorList.Accessors.Any(SyntaxKind.GetAccessorDeclaration) &&
                                    member.AccessorList.Accessors.Any(SyntaxKind.SetAccessorDeclaration) &&
                                    member.AccessorList.Accessors.All(accessor => accessor.ExpressionBody is null);
                            })
                            .Select(property =>
                            {
                                return new PickTypeProperty()
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


                    if (pickType.NamespaceOfType != pickType.NamespaceOfRoot)
                    {
                        pickType.FileScopedUsings.Add(pickType.NamespaceOfRoot!);
                    }

                    pickType.FileScopedUsings.AddRange(declaration.GetCompilationUnitUsingStatements());
                    pickType.NamespaceScopedUsings.AddRange(declaration.GetFileScopedUsingStatements());

                    source.PickTypes.Add(pickType);
                }

                return source;
            });

        context.RegisterSourceOutput(provider, (context, source) =>
        {
            for (int i = 0; i < source.PickTypes.Count; i++)
            {
                var pickType = source.PickTypes[i];
                var builder = new StringBuilder();

                GeneratePickType(builder, pickType);

                context.AddSource($"{pickType.NameOfType}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
            }
        });
    }

    partial class PickTypContext
    {
        public List<PickType> PickTypes { get; set; } = new List<PickType>();
    }

    partial class PickType
    {
        public string? NameOfRoot { get; set; }
        public string? NamespaceOfRoot { get; set; }
        public string? NameOfType { get; set; }
        public string? NamespaceOfType { get; set; }
        public List<string> FileScopedUsings { get; } = new List<string>(); // Using at file scope
        public List<string> NamespaceScopedUsings { get; } = new List<string>(); // Using under namespace
        public IEnumerable<PickTypeProperty> Properties { get; set; } = [];
    }

    partial class PickTypeProperty
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
    }

    private static void GeneratePickType(StringBuilder builder, PickType context)
    {
        foreach (var fileScopedUsing in context.FileScopedUsings)
        {
            builder.AppendLine($"using {fileScopedUsing};");
        }

        builder.AppendLine();
        builder.AppendLine($"namespace {context.NamespaceOfType}");
        builder.AppendLine("{");

        foreach (var namespaceScopedUsing in context.NamespaceScopedUsings)
        {
            builder.AppendTabbedLine(1, $"using {namespaceScopedUsing};");
        }

        builder.AppendTabbedLine(1, $"partial class {context.NameOfType}");
        builder.AppendTabbedLine(1, "{");

        foreach (var property in context.Properties)
        {
            builder.AppendTabbedLine(2, $"public {property.Type} {property.Name} {{ get; set; }}");
        }

        builder.AppendTabbedLine(2, $$"""
            public static implicit operator {{context.NameOfRoot}}({{context.NameOfType}} pick)
            {
                return new {{context.NameOfRoot}}()
                {
            """);

        foreach (var property in context.Properties)
        {
            builder.AppendTabbedLine(4, $"{property.Name} = pick.{property.Name},");
        }

        builder.AppendTabbedLine(2, """
                };
            }
            """);

        builder.AppendLine("""
                }
            }
            """);
    }
}
