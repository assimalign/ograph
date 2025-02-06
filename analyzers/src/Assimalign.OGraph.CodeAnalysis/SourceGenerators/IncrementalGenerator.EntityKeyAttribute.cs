using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.CompilerServices;

namespace Assimalign.OGraph.CodeAnalysis;

[Generator]
public class EntityKeyAttributeIncrementalGenerator : IIncrementalGenerator
{
    private const string fqmd = "Assimalign.OGraph.EntityKeyAttribute";

    #region Partials

    partial record class StructContext
    {
        public string? Name { get; set; }
        public string? Namespace { get; set; }
        public string? RuntimeType { get; set; }
        public bool IncludeImplicitOperators { get; set; }
        public bool IncludeIsValidMethod { get; set; }
    }
    enum StructType
    {
        Short,
        Int,
        Long,
        String,
        Guid
    }

    #endregion

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<StructContext> provider = context.SyntaxProvider.ForAttributeWithMetadataName(
            fqmd,
            predicate: (node, _) => node is StructDeclarationSyntax,
            transform: static (context, _) =>
            {
                var @struct = new StructContext()
                {
                    Namespace = GetStructNamespace(context),
                    Name = GetStructName(context)
                };

                var node = (StructDeclarationSyntax)context.TargetNode;
                var attribute = node.AttributeLists
                    .SelectMany(list => list.Attributes)
                    .FirstOrDefault(attribute =>
                    {
                        var name = attribute.Name.ToString();

                        name = name.EndsWith("Attribute") ? name : name + "Attribute";

                        return (name == fqmd || name == fqmd.Split('.').Last());
                    });

                if (attribute is not null && attribute.ArgumentList is not null)
                {
                    // Extract the key value from the attribute
                    var arguments = attribute.ArgumentList.Arguments;

                    foreach (var argument in arguments)
                    {
                        if (argument.Expression is MemberAccessExpressionSyntax member &&
                            member.Expression is IdentifierNameSyntax identifier &&
                            identifier.ToString() == "EntityKeyRuntimeType")
                        {
                            @struct.RuntimeType = member.Name.ToString() switch
                            {
                                "Int" => "int",
                                "Short" => "short",
                                "Long" => "long",
                                "UInt" => "uint",
                                "UShort" => "ushort",
                                "ULong" => "ulong",
                                "String" => "string",
                                "Guid" => "Guid"
                            };
                        }
                        if (argument.NameEquals is NameEqualsSyntax equals && equals.Name is IdentifierNameSyntax identifier1 && argument.Expression is LiteralExpressionSyntax literal)
                        {
                            var value = literal.ToString();

                            switch (identifier1.ToString())
                            {
                                case "IncludeImplicitOperators" when bool.TryParse(value, out var bln):
                                    @struct.IncludeImplicitOperators = bln;
                                    break;

                                case "IncludeIsValidMethod" when bool.TryParse(value, out var bln):
                                    @struct.IncludeIsValidMethod = bln;
                                    break;
                            }
                        }
                    }
                }

                return @struct;
            });

        context.RegisterSourceOutput(provider, (context, source) =>
        {
            var code = GenerateCode(source);
            var text = SourceText.From(code, Encoding.UTF8);

            context.AddSource($"{source.Name}.g.cs", text);
        });
    }

    #region Filters

    private static string GetStructName(GeneratorAttributeSyntaxContext context)
    {
        if (context.TargetNode is StructDeclarationSyntax node)
        {
            return node.Identifier.Text;
        }
        return string.Empty;
    }
    private static string GetStructNamespace(GeneratorAttributeSyntaxContext context)
    {
        var node = (StructDeclarationSyntax)context.TargetNode;

        // determine the namespace the struct is declared in, if any
        SyntaxNode? ns = node.Parent;

        while (ns != null && ns is not NamespaceDeclarationSyntax && ns is not FileScopedNamespaceDeclarationSyntax)
        {
            ns = ns.Parent;
        }
        if (ns is BaseNamespaceDeclarationSyntax parent)
        {
            string name = parent.Name.ToString();

            while (true)
            {
                if (parent.Parent is not NamespaceDeclarationSyntax nsp)
                {
                    break;
                }

                parent = nsp;
                name = $"{parent.Name}.{name}";
            }

            return name;
        }
        return string.Empty;
    }


    #endregion

    #region Code Generation

    private static string GenerateCode(StructContext info)
    {
        var builder = new StringBuilder();

        builder.Append("namespace ");
        builder.AppendLine(info.Namespace);
        builder.AppendLine("{");
        builder.Append("    partial struct ");
        builder.Append(info.Name);
        builder.AppendLine(" : ");

        WriteInterfaces(builder, info);

        builder.AppendLine("    {");

        WriteConsturctor(builder, info);
        WriteProperties(builder, info);
        WriteMethods(builder, info);
        WriteOverloads(builder, info);
        WriteOperators(builder, info);

        builder.AppendLine("    }");
        builder.AppendLine("}");

        return builder.ToString();
    }
    private static void WriteInterfaces(StringBuilder builder, StructContext info)
    {
        builder.AppendLine("	#if NET7_0_OR_GREATER");

        // IEqualityOperators<,,>
        builder.Append("		global::System.Numerics.IEqualityOperators<");
        builder.Append(info.Name);
        builder.Append(", ");
        builder.Append(info.Name);
        builder.AppendLine(", bool>,");

        // IComparisonOperators<,,>
        builder.Append("		global::System.Numerics.IComparisonOperators<");
        builder.Append(info.Name);
        builder.Append(", ");
        builder.Append(info.Name);
        builder.AppendLine(", bool>,");

        builder.AppendLine("	#endif");

        // IComparable<>
        builder.Append("		global::System.IComparable<");
        builder.Append(info.Name);
        builder.AppendLine(">,");

        // IEquatable<>
        builder.Append("		global::System.IEquatable<");
        builder.Append(info.Name);
        builder.AppendLine(">,");

        // IFormattable
        builder.AppendLine("		global::System.IFormattable");
    }
    private static void WriteConsturctor(StringBuilder builder, StructContext info)
    {
        builder.Append("        public ");
        builder.Append(info.Name);
        builder.Append("(");
        builder.Append(info.RuntimeType);
        builder.Append(" ");
        builder.AppendLine("value)");
        builder.AppendLine("        {");

        if (info.IncludeIsValidMethod)
        {
            builder.AppendLine("			if (!IsValid(value, out string message))");
            builder.AppendLine("			{");
            builder.AppendLine("				throw new ArgumentException(message);");
            builder.AppendLine("			}");
        }

        builder.AppendLine("			Value = value;");
        builder.AppendLine("        }");
        builder.AppendLine();
    }
    private static void WriteProperties(StringBuilder builder, StructContext info)
    {

        builder.Append("		public ");
        builder.Append(info.RuntimeType);
        builder.AppendLine(" Value { get; }");
        builder.AppendLine();
    }
    private static void WriteOperators(StringBuilder builder, StructContext info)
    {
        var operators = new string[] { "==", "!=", ">", "<", ">=", "<=" };

        foreach (var op in operators)
        {
            builder.Append("		public static bool operator ");
            builder.Append(op);
            builder.Append("(");
            builder.Append(info.Name);
            builder.Append(" a, ");
            builder.Append(info.Name);
            builder.Append(" b) => ");
            builder.AppendLine(op switch
            {
                "==" => "a.Equals(b);",
                "!=" => "!a.Equals(b);",
                ">" => "a.CompareTo(b) > 0;",
                "<" => "a.CompareTo(b) < 0;",
                ">=" => "a.CompareTo(b) >= 0;",
                "<=" => "a.CompareTo(b) <= 0;",
            });
        }

        if (info.IncludeImplicitOperators)
        {
            builder.Append("		public static implicit operator ");
            builder.Append(info.RuntimeType);
            builder.Append("(");
            builder.Append(info.Name);
            builder.AppendLine(" item) => item.Value;");

            builder.Append("		public static implicit operator ");
            builder.Append(info.Name);
            builder.Append("(");
            builder.Append(info.RuntimeType);
            builder.Append(" item) => new ");
            builder.Append(info.Name);
            builder.AppendLine("(item);");
        }
    }
    private static void WriteOverloads(StringBuilder builder, StructContext info)
    {
        builder.AppendLine("		public override int GetHashCode() => Value.GetHashCode();");
        builder.AppendLine(info.RuntimeType switch
        {
            "int" => "		public override string ToString() => Value.ToString(global::System.Globalization.CultureInfo.InvariantCulture);",
            "long" => "		public override string ToString() => Value.ToString(global::System.Globalization.CultureInfo.InvariantCulture);",
            "short" => "		public override string ToString() => Value.ToString(global::System.Globalization.CultureInfo.InvariantCulture);",
            "uint" => "		public override string ToString() => Value.ToString(global::System.Globalization.CultureInfo.InvariantCulture);",
            "ulong" => "		public override string ToString() => Value.ToString(global::System.Globalization.CultureInfo.InvariantCulture);",
            "ushort" => "		public override string ToString() => Value.ToString(global::System.Globalization.CultureInfo.InvariantCulture);",
            "string" => "		public override string ToString() => Value;",
            "Guid" => "		public override string ToString() => Value.ToString();"
        });
        builder.AppendLine("		public override bool Equals(object? obj)");
        builder.AppendLine("		{");
        builder.Append("			if (ReferenceEquals(null, obj) || obj is not ");
        builder.Append(info.Name);
        builder.AppendLine(" instance)");
        builder.AppendLine("			{");
        builder.AppendLine("				return false;");
        builder.AppendLine("			}");
        builder.AppendLine("			return Equals(instance);");
        builder.AppendLine("		}");
        builder.AppendLine();
    }
    private static void WriteMethods(StringBuilder builder, StructContext info)
    {
        if (info.IncludeIsValidMethod)
        {
            builder.Append("		public partial bool IsValid(");
            builder.Append(info.RuntimeType);
            builder.AppendLine(" value, out string message);");
        }

        // Write CompareTo
        builder.Append("		public int CompareTo(");
        builder.Append(info.Name);
        builder.AppendLine(" other) => Value.CompareTo(other.Value);");

        // Write IEquality
        builder.Append("		public bool Equals(");
        builder.Append(info.Name);
        builder.AppendLine(" other) => Value.Equals(other.Value);");

        // Write IFormattable
        builder.AppendLine("		public string ToString(");
        builder.AppendLine("			#if NET7_0_OR_GREATER");
        builder.AppendLine("			[global::System.Diagnostics.CodeAnalysis.StringSyntax(global::System.Diagnostics.CodeAnalysis.StringSyntaxAttribute.NumericFormat)]");
        builder.AppendLine("			#endif");
        builder.AppendLine("			string? format,");
        builder.AppendLine("			global::System.IFormatProvider? formatProvider)");

        if (info.RuntimeType.Equals("string"))
        {
            builder.AppendLine("			=> Value.ToString(formatProvider);");
        }
        else
        {
            builder.AppendLine("			=> Value.ToString(format, formatProvider);");
        }

        builder.AppendLine();
    }

    #endregion
}
