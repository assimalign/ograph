using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Assimalign.OGraph.CodeAnalysis;

[Generator]
public class ValueObjectSourceGenerator : IIncrementalGenerator
{

    private class Model(string Namespace, string ClassName)
    {
        public string Namespace { get; set; } = Namespace;
        public string ClassName { get; set; } = ClassName;
    }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
       
        context.RegisterPostInitializationOutput(static callback =>
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("using System;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("namespace Assimalign.OGraph");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("	[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]");
            stringBuilder.AppendLine("	public sealed class EntityKeyAttribute : Attribute");
            stringBuilder.AppendLine("	{");
            stringBuilder.AppendLine("		public EntityKeyAttribute(EntityKeyType keyType)");
            stringBuilder.AppendLine("		{");
            stringBuilder.AppendLine("			KeyType = keyType;");
            stringBuilder.AppendLine("		}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("		public EntityKeyType KeyType { get; }");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("	}");
            stringBuilder.AppendLine("}");

            
            callback.AddSource("EntityKeyAttribute.g.cs", SourceText.From(stringBuilder.ToString(), Encoding.UTF8));

            stringBuilder.Clear();

            stringBuilder.Append("""
                namespace Assimalign.OGraph
                {
                    public enum EntityKeyType
                    {
                        String,
                        Int,
                        Long,
                        Guid
                    }
                }
                """);


            callback.AddSource("EntityKeyType.g.cs", SourceText.From(stringBuilder.ToString(), Encoding.UTF8));
        });


        var pipeline = context.SyntaxProvider.ForAttributeWithMetadataName(
           fullyQualifiedMetadataName: "Assimalign.OGraph.EntityKeyAttribute",
           predicate: static (syntaxNode, cancellationToken) => syntaxNode is BaseMethodDeclarationSyntax,
           transform: static (context, cancellationToken) =>
           {
               var containingClass = context.TargetSymbol.ContainingType;
               return new Model(
                   // Note: this is a simplified example. You will also need to handle the case where the type is in a global namespace, nested, etc.
                   Namespace: containingClass.ContainingNamespace?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.Omitted)),
                   ClassName: containingClass.Name);
           }
       );



        context.RegisterSourceOutput(pipeline, static (context, model) =>
        {
            var sourceText = SourceText.From($$"""
                namespace {{model.Namespace}};
                partial class {{model.ClassName}}
                {
                    
                }
                """, Encoding.UTF8);

            context.AddSource($"{model.ClassName}.g.cs", sourceText);
        });

        
    }
}
