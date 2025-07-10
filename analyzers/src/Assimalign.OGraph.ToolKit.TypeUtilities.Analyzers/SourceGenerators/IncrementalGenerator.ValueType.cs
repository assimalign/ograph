using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Assimalign.OGraph.SourceGeneration;

[Generator]
public sealed class ValueTypeIncrementalGenerator : IIncrementalGenerator
{
    public const string AttributeName = "ValueTypeAttribute";
    public const string AttributeFullName = "System.ValueTypeAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<ValueTypeContext> provider = context.SyntaxProvider.ForAttributeWithMetadataName(
            AttributeFullName,
            predicate: (node, _) => node is StructDeclarationSyntax or ClassDeclarationSyntax,
            transform: static (context, cancellationToken) =>
            {
                var symbol = (INamedTypeSymbol)context.TargetSymbol;
                var declaration = (TypeDeclarationSyntax)context.TargetNode;
                var source = new ValueTypeContext()
                {
                    Name = declaration.GetNameOfType(), // The name of the class or struct
                    Namespace = declaration.GetNamespaceOfType(),
                    //GdmTypeName = $"Gdm{symbol.Name}Type",
                    //GdmTypeNamespace = $"{symbol.ContainingNamespace.Name}.Gdm",
                    IsClass = symbol.IsReferenceType,
                    IsStruct = symbol.IsValueType
                };


                foreach (var attribute in symbol.GetAttributes())
                {
                    if (attribute.AttributeClass is not null && attribute.AttributeClass.Name == AttributeName)
                    {
                        TypedConstant argument = attribute.ConstructorArguments[0];

                        source.Type = (ScalarUnderlyingType)argument.Value!;

                        foreach (var keyValuePair in attribute.NamedArguments)
                        {
                            argument = keyValuePair.Value;

                            switch (keyValuePair.Key)
                            {
                                case "IncludeImplicitOperators":
                                case "IncludeIsValidMethod":
                                    source.IncludeImplicitOperators = (bool)argument.Value!;
                                    break;
                            }
                        }

                        break;
                    }
                }

                return source;
            });

        // Generate runtime type
        context.RegisterSourceOutput(provider, (context, source) =>
        {
            var builder = new StringBuilder();

            GenerateValueType(builder, source);

            context.AddSource(
                $"{source.Name}.g.cs",
                SourceText.From(builder.ToString(), Encoding.UTF8));
        });
    }

    partial class ValueTypeContext
    {
        public string? Name { get; set; }
        public string? Namespace { get; set; }
        public bool IsClass { get; set; }
        public bool IsStruct { get; set; }
        public bool IncludeIsValidMethod { get; set; }
        public bool IncludeImplicitOperators { get; set; }
        public ScalarUnderlyingType? Type { get; set; }
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
    private static void GenerateValueType(StringBuilder builder, ValueTypeContext context)
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

        GenerateValueTypeInterfaces(builder, context);

        builder.AppendLine("    {");

        GenerateValueTypeConstructor(builder, context);
        GenerateValueTypeProperties(builder, context);
        GenerateValueTypeMethods(builder, context);
        GenerateValueTypeOverloads(builder, context);
        GenerateValueTypeOperators(builder, context);

        builder.AppendLine("    }");
        builder.AppendLine("}");
    }
    private static void GenerateValueTypeInterfaces(StringBuilder builder, ValueTypeContext context)
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
        builder.AppendLine("		global::System.IFormattable,");


        // ISpanParsable
        if (context.Type != ScalarUnderlyingType.String)
        {
            builder.AppendLine($"		global::System.ISpanParsable<{context.Name}>");
        }
    }
    private static void GenerateValueTypeConstructor(StringBuilder builder, ValueTypeContext info)
    {
        builder.Append("        public ");
        builder.Append(info.Name);
        builder.Append("(");
        builder.Append(GetNormalizedName(info.Type));
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
    private static void GenerateValueTypeProperties(StringBuilder builder, ValueTypeContext context)
    {

        builder.Append("		public ");
        builder.Append(GetNormalizedName(context.Type));
        builder.AppendLine(" Value { get; }");
        builder.AppendLine();
    }
    private static void GenerateValueTypeOperators(StringBuilder builder, ValueTypeContext context)
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
            builder.Append(GetNormalizedName(context.Type));
            builder.Append("(");
            builder.Append(context.Name);
            builder.AppendLine(" item) => item.Value;");

            builder.Append("		public static implicit operator ");
            builder.Append(context.Name);
            builder.Append("(");
            builder.Append(GetNormalizedName(context.Type));
            builder.Append(" item) => new ");
            builder.Append(context.Name);
            builder.AppendLine("(item);");
        }
    }
    private static void GenerateValueTypeOverloads(StringBuilder builder, ValueTypeContext context)
    {
        builder.AppendLine("		public override int GetHashCode() => Value.GetHashCode();");
        builder.AppendLine(GetNormalizedName(context.Type) switch
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
    private static void GenerateValueTypeMethods(StringBuilder builder, ValueTypeContext context)
    {
        if (context.IncludeIsValidMethod)
        {
            builder.Append("		public static partial bool IsValid(");
            builder.Append(GetNormalizedName(context.Type));
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




    //public static test Parse(string s, IFormatProvider? provider)
    //{
    //    throw new NotImplementedException();
    //}

    //public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out test result)
    //{
    //    throw new NotImplementedException();
    //}

    //public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out test result)
    //{
    //    throw new NotImplementedException();
    //}
        switch (context.Type)
        {
            case ScalarUnderlyingType.String:
                builder.AppendLine("			=> Value.ToString(formatProvider);");
                break;

            case ScalarUnderlyingType.Guid:
                builder.AppendLine("			=> Value.ToString(format, formatProvider);");
                builder.AppendLine($"\t\tpublic static {context.Name} New{context.Name}() => Guid.NewGuid();");
                builder.AppendLine($"        public static {context.Name} Parse(string value) => Guid.Parse(value);");
                builder.AppendLine($"        public static {context.Name} Parse(ReadOnlySpan<char> value) => Guid.Parse(value);");
                break;

            case ScalarUnderlyingType.Short:
                builder.AppendLine("			=> Value.ToString(format, formatProvider);");
                builder.AppendLine($"        public static {context.Name} Parse(string value) => short.Parse(value);");
                builder.AppendLine($"        public static {context.Name} Parse(ReadOnlySpan<char> span) => Parse(span, null);");
                builder.AppendLine($"        public static {context.Name} Parse(ReadOnlySpan<char> span, IFormatProvider? provider) => new {context.Name}(short.Parse(span, provider));");
                builder.AppendLine($"        public static bool TryParse(ReadOnlySpan<char> span, IFormatProvider? provider, [MaybeNullWhen(false)] out {context.Name} result)");
                builder.AppendLine("        {");
                builder.AppendLine($"           if (short.TryParse(span, provider, out short value))");
                builder.AppendLine("            {");

                break;

            case ScalarUnderlyingType.Int:
                builder.AppendLine("			=> Value.ToString(format, formatProvider);");
                builder.AppendLine($"        public static {context.Name} Parse(string value) => int.Parse(value);");
                builder.AppendLine($"        public static {context.Name} Parse(ReadOnlySpan<char> value) => int.Parse(value);");
                break;

            case ScalarUnderlyingType.Long:
                builder.AppendLine("			=> Value.ToString(format, formatProvider);");
                builder.AppendLine($"        public static {context.Name} Parse(string value) => long.Parse(value);");
                builder.AppendLine($"        public static {context.Name} Parse(ReadOnlySpan<char> value) => long.Parse(value);");
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
}
