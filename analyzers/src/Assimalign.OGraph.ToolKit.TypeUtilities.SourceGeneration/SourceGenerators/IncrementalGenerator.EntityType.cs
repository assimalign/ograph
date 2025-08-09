using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assimalign.OGraph.ToolKit.TypeUtilities.SourceGeneration;

//[Generator]
public sealed class EntityTypeAttributeIncrementalGenerator : IIncrementalGenerator
{
    public const string AttributeName = "EntityTypeAttribute";
    public const string AttributeFullName = "System.EntityTypeAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<EntityTypeContext> provider = context.SyntaxProvider.ForAttributeWithMetadataName(
            AttributeFullName,
            predicate: (node, _) => node is StructDeclarationSyntax or ClassDeclarationSyntax,
            transform: static (context, cancellationToken) =>
            {
                var symbol = (INamedTypeSymbol)context.TargetSymbol;
                var source = new EntityTypeContext()
                {
                    Name = symbol.Name, // The name of the class or struct
                    Namespace = symbol.ContainingNamespace.Name
                };


                //foreach (var attribute in symbol.GetAttributes())
                //{
                //    if (attribute.AttributeClass is not null && attribute.AttributeClass.Name == AttributeName)
                //    {
                //        TypedConstant argument = attribute.ConstructorArguments[0];

                //        source.Type = (ScalarUnderlyingType)argument.Value!;

                //        foreach (var keyValuePair in attribute.NamedArguments)
                //        {
                //            argument = keyValuePair.Value;

                //            switch (keyValuePair.Key)
                //            {
                //                case "IncludeImplicitOperators":
                //                case "IncludeIsValidMethod":
                //                    source.IncludeImplicitOperators = (bool)argument.Value!;
                //                    break;
                //            }
                //        }

                //        break;
                //    }
                //}

                return source;
            });

        // Generate runtime type
        context.RegisterSourceOutput(provider, (context, source) =>
        {
            var builder = new StringBuilder();

            GenerateEntityType(builder, source);

            context.AddSource(
                $"{source.Name}.g.cs",
                SourceText.From(builder.ToString(), Encoding.UTF8));
        });
    }

    partial class EntityTypeContext
    {
        public string? Name { get; set; }
        public string? Namespace { get; set; }
    }


    private static void GenerateEntityType(StringBuilder builder, EntityTypeContext context)
    {

    }
}
