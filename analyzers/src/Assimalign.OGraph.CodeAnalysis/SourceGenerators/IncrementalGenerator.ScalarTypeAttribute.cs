using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Assimalign.OGraph.CodeAnalysis;

[Generator]
public sealed class ScalarTypeAttributeIncrementalGenerator : IIncrementalGenerator
{
    public const string AttributeName = "Assimalign.OGraph.ScalarTypeAttribute";
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<ScalarTypeContext> provider = context.SyntaxProvider.ForAttributeWithMetadataName(
            AttributeName,
            predicate: (node, _) => node is StructDeclarationSyntax or ClassDeclarationSyntax,
            transform: static (context, _) =>
            {
                var declaration = (TypeDeclarationSyntax)context.TargetNode;

                var typeInfo = context.SemanticModel.GetTypeInfo(declaration);


                var source = new ScalarTypeContext()
                {
                    Name = declaration.GetNameOfType(),
                    Namespace = declaration.GetNamespaceOfType(),
                    IncludeImplicitOperators = declaration.GetLiteralAttributeArgumentOfType<bool>(AttributeName, "IncludeImplicitOperators"),
                    IncludeIsValidMethod = declaration.GetLiteralAttributeArgumentOfType<bool>(AttributeName, "IncludeIsValidMethod"),
                    ExcludeGdmType = declaration.GetLiteralAttributeArgumentOfType<bool>(AttributeName, "ExcludeGdmType"),
                    GdmTypeNamespace = declaration.GetLiteralAttributeArgumentOfType<string>(AttributeName, "GdmTypeNamespace"),
                    UnderlyingType = declaration.GetEnumAttributeArgumentOfType<ScalarUnderlyingType>(AttributeName, "UnderlyingType"),
                    IsClass = context.TargetNode is ClassDeclarationSyntax,
                    IsStruct = context.TargetNode is StructDeclarationSyntax
                };

                return source;
            });

        context.RegisterSourceOutput(provider, (context, source) =>
        {
            var builder = new StringBuilder();

            GenerateScalarType(builder, source);

            context.AddSource($"{source.Name}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        });

        context.RegisterSourceOutput(provider, (context, source) =>
        {
            if (source.ExcludeGdmType) return;

            var builder = new StringBuilder();

            GenerateGdmType(builder, source);

            context.AddSource($"Gdm{source.Name}Type.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        });
    }



    #region Partials

    partial class ScalarTypeContext
    {
        public string? Name { get; set; }
        public string? Namespace { get; set; }
        public bool IsClass { get; set; }
        public bool IsStruct { get; set; }
        public bool IncludeIsValidMethod { get; set; }
        public bool IncludeImplicitOperators { get; set; }
        public bool ExcludeGdmType { get; set; }
        public string? GdmTypeNamespace { get; set; }
        public ScalarUnderlyingType? UnderlyingType { get; set; }
    }

    enum ScalarUnderlyingType
    {
        Int,
        Short,
        Long,
        UInt,
        UShort,
        ULong,
        String,
        Guid
    }

    #endregion

    #region Sclar Type Generation

    private static void GenerateScalarType(StringBuilder builder, ScalarTypeContext context)
    {
        builder.Append("namespace ");
        builder.AppendLine(context.Namespace);
        builder.AppendLine("{");
        builder.Append("    partial ");

        if (context.IsStruct)
        {
            builder.Append("struct ");
        }
        if (context.IsClass)
        {
            builder.Append("class ");
        }
        builder.Append(context.Name);
        builder.AppendLine(" : ");

        GenerateScalarTypeInterfaces(builder, context);

        builder.AppendLine("    {");

        GenerateScalarTypeConstructor(builder, context);
        GenerateScalarTypeProperties(builder, context);
        GenerateScalarTypeMethods(builder, context);
        GenerateScalarTypeOverloads(builder, context);
        GenerateScalarTypeOperators(builder, context);

        builder.AppendLine("    }");
        builder.AppendLine("}");
    }
    private static void GenerateScalarTypeInterfaces(StringBuilder builder, ScalarTypeContext context)
    {
        builder.AppendLine("	#if NET7_0_OR_GREATER");

        // IEqualityOperators<,,>
        builder.Append("		global::System.Numerics.IEqualityOperators<");
        builder.Append(context.Name);
        builder.Append(", ");
        builder.Append(context.Name);
        builder.AppendLine(", bool>,");

        // IComparisonOperators<,,>
        builder.Append("		global::System.Numerics.IComparisonOperators<");
        builder.Append(context.Name);
        builder.Append(", ");
        builder.Append(context.Name);
        builder.AppendLine(", bool>,");

        builder.AppendLine("	#endif");

        // IComparable<>
        builder.Append("		global::System.IComparable<");
        builder.Append(context.Name);
        builder.AppendLine(">,");

        // IEquatable<>
        builder.Append("		global::System.IEquatable<");
        builder.Append(context.Name);
        builder.AppendLine(">,");

        // IFormattable
        builder.AppendLine("		global::System.IFormattable");
    }
    private static void GenerateScalarTypeConstructor(StringBuilder builder, ScalarTypeContext info)
    {
        builder.Append("        public ");
        builder.Append(info.Name);
        builder.Append("(");
        builder.Append(GetNormalizedName(info.UnderlyingType));
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
    private static void GenerateScalarTypeProperties(StringBuilder builder, ScalarTypeContext context)
    {

        builder.Append("		public ");
        builder.Append(GetNormalizedName(context.UnderlyingType));
        builder.AppendLine(" Value { get; }");
        builder.AppendLine();
    }
    private static void GenerateScalarTypeOperators(StringBuilder builder, ScalarTypeContext context)
    {
        var operators = new string[] { "==", "!=", ">", "<", ">=", "<=" };

        foreach (var op in operators)
        {
            builder.Append("		public static bool operator ");
            builder.Append(op);
            builder.Append("(");
            builder.Append(context.Name);
            builder.Append(" a, ");
            builder.Append(context.Name);
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

        if (context.IncludeImplicitOperators)
        {
            builder.Append("		public static implicit operator ");
            builder.Append(GetNormalizedName(context.UnderlyingType));
            builder.Append("(");
            builder.Append(context.Name);
            builder.AppendLine(" item) => item.Value;");

            builder.Append("		public static implicit operator ");
            builder.Append(context.Name);
            builder.Append("(");
            builder.Append(GetNormalizedName(context.UnderlyingType));
            builder.Append(" item) => new ");
            builder.Append(context.Name);
            builder.AppendLine("(item);");
        }
    }
    private static void GenerateScalarTypeOverloads(StringBuilder builder, ScalarTypeContext context)
    {
        builder.AppendLine("		public override int GetHashCode() => Value.GetHashCode();");
        builder.AppendLine(GetNormalizedName(context.UnderlyingType) switch
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
        builder.Append(context.Name);
        builder.AppendLine(" instance)");
        builder.AppendLine("			{");
        builder.AppendLine("				return false;");
        builder.AppendLine("			}");
        builder.AppendLine("			return Equals(instance);");
        builder.AppendLine("		}");
        builder.AppendLine();
    }
    private static void GenerateScalarTypeMethods(StringBuilder builder, ScalarTypeContext context)
    {
        if (context.IncludeIsValidMethod)
        {
            builder.Append("		public static partial bool IsValid(");
            builder.Append(GetNormalizedName(context.UnderlyingType));
            builder.AppendLine(" value, out string message);");
        }

        // Write CompareTo
        builder.Append("		public int CompareTo(");
        builder.Append(context.Name);
        builder.AppendLine(" other) => Value.CompareTo(other.Value);");

        // Write IEquality
        builder.Append("		public bool Equals(");
        builder.Append(context.Name);
        builder.AppendLine(" other) => Value.Equals(other.Value);");

        // Write IFormattable
        builder.AppendLine("		public string ToString(");
        builder.AppendLine("			#if NET7_0_OR_GREATER");
        builder.AppendLine("			[global::System.Diagnostics.CodeAnalysis.StringSyntax(global::System.Diagnostics.CodeAnalysis.StringSyntaxAttribute.NumericFormat)]");
        builder.AppendLine("			#endif");
        builder.AppendLine("			string? format,");
        builder.AppendLine("			global::System.IFormatProvider? formatProvider)");

        switch (context.UnderlyingType)
        {
            case ScalarUnderlyingType.String:
                builder.AppendLine("			=> Value.ToString(formatProvider);");
                break;

            case ScalarUnderlyingType.Guid:
                builder.AppendLine("			=> Value.ToString(format, formatProvider);");
                builder.AppendLine($"       public static {context.Name} New{context.Name}() => Guid.NewGuid();");
                break;
            default:
                builder.AppendLine("			=> Value.ToString(format, formatProvider);");
                break;
        }


        builder.AppendLine();
    }

    private static string GetNormalizedName(ScalarUnderlyingType? underlyingType)
    {
        return underlyingType!.Value switch
        {
            ScalarUnderlyingType.Guid => "Guid",
            _ => underlyingType.Value.ToString().ToLower()
        };
    }
    #endregion

    #region Gdm Type Generation

    private static void GenerateGdmType(StringBuilder builder, ScalarTypeContext context)
    {
        builder.Append("namespace ");

        if (string.IsNullOrEmpty(context.GdmTypeNamespace))
        {
            builder.AppendLine(context.Namespace);
        }
        else
        {
            builder.AppendLine(context.GdmTypeNamespace);
        }

        builder.AppendLine("{");
        builder.Append("    public class Gdm");
        builder.Append(context.Name);
        builder.Append("Type : global::Assimalign.OGraph.Gdm.Elements.GdmScalarType<global::");
        builder.Append(context.Namespace);
        builder.Append(".");
        builder.Append(context.Name);
        builder.AppendLine(">");
        builder.AppendLine("    {");
        builder.AppendLine("    }");
        builder.AppendLine("}");
    }

    #endregion
}
