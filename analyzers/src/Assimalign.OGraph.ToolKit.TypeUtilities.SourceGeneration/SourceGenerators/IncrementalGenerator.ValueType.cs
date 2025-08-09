using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using System.Linq;

namespace Assimalign.OGraph.ToolKit.TypeUtilities.SourceGeneration;

[Generator]
public sealed class ValueTypeIncrementalGenerator : IIncrementalGenerator
{
    public const string AttributeName = "ValueTypeAttribute";
    public const string AttributeFullName = "System.ValueTypeAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<ValueTypeContext> provider = context.SyntaxProvider.ForAttributeWithMetadataName(
            AttributeFullName,
            predicate: static (node, _) => node is StructDeclarationSyntax or ClassDeclarationSyntax,
            transform: static (context, cancellationToken) =>
            {
                var symbol = (INamedTypeSymbol)context.TargetSymbol;
                var declaration = (TypeDeclarationSyntax)context.TargetNode;
                var attribute = declaration.GetAttributeByName(AttributeFullName);
                var source = new ValueTypeContext()
                {
                    Name = declaration.GetNameOfType(), // The name of the class or struct
                    Namespace = declaration.GetNamespaceOfType(),
                    IsClass = symbol.IsReferenceType,
                    IsStruct = symbol.IsValueType,
                    Type = attribute.GetMemberAccessArgumentByIndex<UnderlyingType>(0), // The underlying type
                    IncludeImplicitOperators = attribute!.GetLiteralArgumentByName<bool>("IncludeImplicitOperators"),
                    IncludeIsValidMethod = attribute!.GetLiteralArgumentByName<bool>("IncludeIsValidMethod"),
                };

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
        public UnderlyingType? Type { get; set; }
    }
    enum UnderlyingType
    {
        Int,
        Short,
        Long,
        UInt,
        UShort,
        ULong,
        Double,
        Decimal,
        String,
        Guid
    }

    private static void GenerateValueType(StringBuilder builder, ValueTypeContext context)
    {
        builder.AppendLine($$"""
		namespace {{context.Namespace}}
		{
			partial {{GetIdentifier()}} {{context.Name}} :
		""");

        GenerateValueTypeInterfaces(builder, context);

        builder.AppendLine("    {");

        GenerateValueTypeConstructor(builder, context);
        GenerateValueTypeProperties(builder, context);
        GenerateValueTypeMethods(builder, context);
        GenerateValueTypeOverloads(builder, context);
        GenerateValueTypeOperators(builder, context);

        builder.AppendLine("    }");
        builder.AppendLine("}");

        string GetIdentifier()
        {
            if (context.IsClass) return "class";
            if (context.IsStruct) return "struct";
            return string.Empty;
        }
    }
    private static void GenerateValueTypeInterfaces(StringBuilder builder, ValueTypeContext context)
    {
        builder.AppendTabbedLine(2, $$"""
            global::System.Numerics.IEqualityOperators<{{context.Name}}, {{context.Name}}, bool>
            ,global::System.Numerics.IComparisonOperators<{{context.Name}}, {{context.Name}}, bool>
            ,global::System.IComparable<{{context.Name}}>
            ,global::System.IEquatable<{{context.Name}}>
            """);

        // ISpanParsable
        if (context.Type != UnderlyingType.String)
        {
            builder.AppendTabbedLine(2, $$"""
                ,global::System.IFormattable
                """);
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
        builder.AppendTabbedLine(2, $$"""
            public {{GetNormalizedName(context.Type)}} Value { get; }
            """);
    }
    private static void GenerateValueTypeOperators(StringBuilder builder, ValueTypeContext context)
    {
        var operators = new string[] { "==", "!=", ">", "<", ">=", "<=" };

        foreach (var op in operators)
        {
            var body = op switch
            {
                "==" => "a.Equals(b);",
                "!=" => "!a.Equals(b);",
                ">" => "a.CompareTo(b) > 0;",
                "<" => "a.CompareTo(b) < 0;",
                ">=" => "a.CompareTo(b) >= 0;",
                "<=" => "a.CompareTo(b) <= 0;",
            };

            builder.AppendTabbedLine(2, $$"""
                public static bool operator {{op}}({{context.Name}} a, {{context.Name}} b) => {{body}}
                """);
        }

        if (context.IncludeImplicitOperators)
        {
            builder.AppendTabbedLine(2, $$"""
                public static implicit operator {{GetNormalizedName(context.Type)}}({{context.Name}} item) => item.Value;
                public static implicit operator {{context.Name}}({{GetNormalizedName(context.Type)}} item) => new {{context.Name}}(item);
                """);
        }
    }
    private static void GenerateValueTypeOverloads(StringBuilder builder, ValueTypeContext context)
    {
        builder.AppendTabbedLine(2, $$"""
		public override int GetHashCode() => Value.GetHashCode();
		public override string ToString() => {{GetToString(context.Type)}}
		public override bool Equals(object? obj) => ReferenceEquals(null, obj) || obj is not {{context.Name}} instance ? false : Equals(instance);
		""");

        string GetToString(UnderlyingType? type) => type switch
        {
            UnderlyingType.Guid => "Value.ToString();",
            UnderlyingType.String => "Value;",
            _ => "Value.ToString(global::System.Globalization.CultureInfo.InvariantCulture);"
        };

    }
    private static void GenerateValueTypeMethods(StringBuilder builder, ValueTypeContext context)
    {
        if (context.IncludeIsValidMethod)
        {
            builder.Append("		public partial bool IsValid(");
            builder.Append(GetNormalizedName(context.Type));
            builder.AppendLine(" value, out string message);");
        }

        // Write CompareTo
        builder.Append("		public int CompareTo(");
        builder.Append(context.Name);
        builder.AppendLine(" other) => Value.CompareTo(other.Value);");

        // Write IEquality
        builder.AppendTabbedLine(2, $"public bool Equals({context.Name} other) => Value.Equals(other.Value);");

        // Write IFormattable
        var toStringBody = context.Type switch
        {
            UnderlyingType.String => "Value.ToString(formatProvider);",
            _ => " Value.ToString(format, formatProvider);"
        };

        builder.AppendTabbedLine(2, $$"""
            public string ToString(
                [global::System.Diagnostics.CodeAnalysis.StringSyntax(global::System.Diagnostics.CodeAnalysis.StringSyntaxAttribute.NumericFormat)] string? format,
                global::System.IFormatProvider? formatProvider) => {{toStringBody}}
            """);

        // IParsable<>
        if (context.Type != UnderlyingType.String)
        {
            builder.AppendTabbedLine(2, $$"""
			public static {{context.Name}} Parse(string value) => {{GetNormalizedName(context.Type)}}.Parse(value);
			public static {{context.Name}} Parse(string value, IFormatProvider? provider) => Parse(value.AsSpan());
			public static bool TryParse([global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] string? value, IFormatProvider? provider, [global::System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out {{context.Name}} result) => TryParse(value.AsSpan(), provider, out result);
			public static {{context.Name}} Parse(ReadOnlySpan<char> span) => Parse(span, null);
			public static {{context.Name}} Parse(ReadOnlySpan<char> span, IFormatProvider? provider) => new {{context.Name}}({{GetNormalizedName(context.Type)}}.Parse(span, provider));
			public static bool TryParse(ReadOnlySpan<char> span, IFormatProvider? provider, [global::System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out {{context.Name}} result)
			{
				result = default;
				
				if ({{GetNormalizedName(context.Type)}}.TryParse(span, provider, out {{GetNormalizedName(context.Type)}} value))
				{
					result = new {{context.Name}}(value);
					return true;
				}
				
				return false;
			}
			""");
        }

        builder.AppendLine();
    }

    private static string GetNormalizedName(UnderlyingType? underlyingType)
    {
        return underlyingType!.Value switch
        {
            UnderlyingType.Guid => "Guid",
            _ => underlyingType.Value.ToString().ToLower()
        };
    }


}
