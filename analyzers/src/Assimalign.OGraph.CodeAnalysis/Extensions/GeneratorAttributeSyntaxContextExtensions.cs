using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Assimalign.OGraph.CodeAnalysis;

internal static class GeneratorAttributeSyntaxContextExtensions
{
    public static string? GetNamespaceOfType(this TypeDeclarationSyntax typeDeclarationNode)
    {

        // determine the namespace the struct is declared in, if any
        SyntaxNode? ns = typeDeclarationNode.Parent;

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

    public static string GetNameOfType(this TypeDeclarationSyntax typeDeclarationNode)
    {
        return typeDeclarationNode.Identifier.Text;
    }

    public static T[] GetArrayAttributeArgumentOfType<T>(
        this TypeDeclarationSyntax typeDeclarationSyntax,
        string fullQualifiedNamespace,
        string argumentName)
    {
        var attribute = typeDeclarationSyntax.GetFirstOrDefaultAttribute(fullQualifiedNamespace);

        if (attribute is not null && attribute.ArgumentList is not null)
        {
            // Extract the key value from the attribute
            var arguments = attribute.ArgumentList.Arguments;

            foreach (var argument in arguments)
            {
                if (argument.NameEquals is not null && argument.Expression is CollectionExpressionSyntax collection)
                {
                    if (argument.NameEquals.Name.ToString() == argumentName)
                    {
                        var items = new List<T>();

                        foreach (var element in collection.Elements.OfType<ExpressionElementSyntax>())
                        {
                            if (element.Expression is LiteralExpressionSyntax literal)
                            {
                                object value = literal.ToString().Trim('"');

                                if (value is T item)
                                {
                                    items.Add(item);
                                }
                            }
                        }

                        return items.ToArray();
                    }
                }
            }
        }

        return [];
    }

    public static T? GetEnumAttributeArgumentOfType<T>(
        this TypeDeclarationSyntax typeDeclarationSyntax,
        string fullQualifiedNamespace,
        string argumentName) where T : struct, Enum
    {
        var type = typeof(T);
        var attribute = typeDeclarationSyntax.GetFirstOrDefaultAttribute(fullQualifiedNamespace);

        if (attribute is not null && attribute.ArgumentList is not null)
        {
            // Extract the key value from the attribute
            var arguments = attribute.ArgumentList.Arguments;

            foreach (var argument in arguments)
            {
                // Check for const or enum access
                if (argument.Expression is MemberAccessExpressionSyntax member &&
                    member.Expression is IdentifierNameSyntax identifier &&
                    identifier.ToString() == type.Name &&
                    Enum.TryParse<T>(member.Name.ToString(), out var value))
                {
                    return value;
                }
            }
        }

        return default;
    }

    public static T GetLiteralAttributeArgumentOfType<T>(
        this TypeDeclarationSyntax typeDeclarationSyntax,
        string fullQualifiedNamespace,
        string argumentName)
    {
        bool isFound = false;
        string value = string.Empty;

        var attribute = typeDeclarationSyntax.GetFirstOrDefaultAttribute(fullQualifiedNamespace);

        if (attribute is not null && attribute.ArgumentList is not null)
        {
            // Extract the key value from the attribute
            var arguments = attribute.ArgumentList.Arguments;

            foreach (var argument in arguments)
            {
                // Check for literal values: 'true or value', '123..', 'a string'
                if (argument.NameEquals is not null && argument.Expression is LiteralExpressionSyntax literal)
                {
                    if (argument.NameEquals.Name.ToString() == argumentName)
                    { 
                        value = literal.ToString().Trim('"');
                        isFound = true;
                        break;
                    }
                }
            }
        }


        if (!isFound || value == "null")
        {
            return default;
        }

        object parsed = value;

        switch (typeof(T).Name)
        {
            case nameof(String) when parsed is T v:
                return v;

            case nameof(Boolean) when (parsed = bool.Parse(value)) is T v:
                return v;

            case nameof(Int16) when (parsed = short.Parse(value)) is T v:
                return v;

            case nameof(Int32) when (parsed = int.Parse(value)) is T v:
                return v;

            case nameof(Int64) when (parsed = long.Parse(value)) is T v:
                return v;

            case nameof(UInt16) when (parsed = ushort.Parse(value)) is T v:
                return v;

            case nameof(UInt32) when (parsed = uint.Parse(value)) is T v:
                return v;

            case nameof(UInt64) when (parsed = ulong.Parse(value)) is T v:
                return v;

            default: return default;
        }
    }

    private static AttributeSyntax? GetFirstOrDefaultAttribute(
        this TypeDeclarationSyntax typeDeclarationSyntax,
        string fullQualifiedNamespace)
    {
        return typeDeclarationSyntax.AttributeLists
            .SelectMany(list => list.Attributes)
            .FirstOrDefault(attribute =>
            {
                var name = attribute.Name.ToString();

                name = name.EndsWith("Attribute") ? name : name + "Attribute";

                return (name == fullQualifiedNamespace || name == fullQualifiedNamespace.Split('.').Last());
            });
    }


    public static bool HasParameterlessConstructor(this TypeDeclarationSyntax typeDeclaration)
    {
        // Look for constructor declarations in the type's members
        var constructors = typeDeclaration.Members.OfType<ConstructorDeclarationSyntax>();

        // Check if any constructor has no parameters
        return constructors.Any(constructor => !constructor.ParameterList.Parameters.Any());
    }
}
