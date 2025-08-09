using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;

namespace Assimalign.OGraph.ToolKit.TypeUtilities.SourceGeneration;

internal static class SyntaxTreeExtensions
{

    extension(TypeDeclarationSyntax typeDeclaration)
    {
        public bool HasParameterlessConstructor()
        {
            // Look for constructor declarations in the type's members
            var constructors = typeDeclaration.Members.OfType<ConstructorDeclarationSyntax>();

            // Check if any constructor has no parameters
            return !constructors.Any() || constructors.Any(constructor => !constructor.ParameterList.Parameters.Any());
        }

        public string? GetNamespaceOfType()
        {

            // determine the namespace the struct is declared in, if any
            SyntaxNode? ns = typeDeclaration.Parent;

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

        public string GetNameOfType()
        {
            return typeDeclaration.Identifier.Text;
        }

        public AttributeSyntax GetAttributeByName(string fullQualifiedNamespace)
        {
            return typeDeclaration.AttributeLists
                .SelectMany(p => p.Attributes)
                .FirstOrDefault(attribute =>
                {
                    var name = attribute.Name.ToString();

                    name = name.EndsWith("Attribute") ? name : name + "Attribute";

                    return (name == fullQualifiedNamespace || name == fullQualifiedNamespace.Split('.').Last());
                });
        }

        public IEnumerable<AttributeSyntax> GetAttributesByName(string fullQualifiedNamespace)
        {
            return typeDeclaration.AttributeLists
                .SelectMany(p => p.Attributes)
                .Where(attribute =>
                {
                    var name = attribute.Name.ToString();

                    name = name.EndsWith("Attribute") ? name : name + "Attribute";

                    return (name == fullQualifiedNamespace || name == fullQualifiedNamespace.Split('.').Last());
                });
        }

        public IEnumerable<string> GetCompilationUnitUsingStatements()
        {
            var fileScope = typeDeclaration.Parent as FileScopedNamespaceDeclarationSyntax;
            var compilationUnit = typeDeclaration?.Parent?.Parent as CompilationUnitSyntax;

            if (compilationUnit is null)
            {
                return [];
            }

            var items = compilationUnit.Usings.Select(p =>
            {
                var name = p.Name?.ToString();

                if (p.Alias is not null)
                {
                    name = p.Alias.Name.ToString() + " = " + name;
                }

                if (p.StaticKeyword.Value is not null)
                {
                    name = "static " + name;
                }

                return name;

            }).ToArray();

            return items!;
        }

        public IEnumerable<string> GetFileScopedUsingStatements()
        {
            var fileScope = typeDeclaration.Parent as FileScopedNamespaceDeclarationSyntax;

            if (fileScope is null)
            {
                return [];
            }

            var items = fileScope.Usings.Select(p =>
            {
                var name = p.Name?.ToString();

                if (p.Alias is not null)
                {
                    name = p.Alias.Name.ToString() + " = " + name;
                }

                if (p.StaticKeyword.Value is not null)
                {
                    name = "static " + name;
                }

                return name;

            }).ToArray();

            return items!;
        }
    }

    extension(AttributeSyntax? attribute)
    {
        // 
        public T? GetLiteralArgumentByName<T>(string name)
        {
            if (attribute is null)
            {
                return default;
            }

            bool isFound = false;
            string value = string.Empty;

            if (attribute is not null && attribute.ArgumentList is not null)
            {
                // Extract the key value from the attribute
                var arguments = attribute.ArgumentList.Arguments;

                foreach (var argument in arguments)
                {
                    // Check for literal values: 'true or value', '123..', 'a string'
                    if (argument.NameEquals is not null && argument.Expression is LiteralExpressionSyntax literal)
                    {
                        if (argument.NameEquals.Name.ToString() == name)
                        {
                            value = literal.ToString().Trim('"');
                            isFound = true;
                            break;
                        }
                    }

                    // if (argument.Expression is MemberAccessExpressionSyntax member && member.Expression is IdentifierNameSyntax identifier &&
                    //identifier.ToString() == type.Name &&)
                    // {
                    //     return value;
                    // }
                }
            }

            if (!isFound || value == "null")
            {
                return default;
            }

            return Cast<T>(value as object);
        }

        public T? GetMemberAccessArgumentByIndex<T>(int index)
        {
            var type = typeof(T);

            if (attribute is null || attribute.ArgumentList is null)
            {
                return default;
            }

            var argument = attribute.ArgumentList.Arguments[index];

            if (argument.Expression is MemberAccessExpressionSyntax memberAccess)
            {
                // Extract the member access value
                if (memberAccess.Expression is IdentifierNameSyntax identifier && memberAccess.Name is IdentifierNameSyntax name)
                {
                    var value = memberAccess.Name.ToString();

                    return Cast<T>(value);
                }
            }

            return default;

        }

        public T? GetLiteralArgumentByIndex<T>(int index)
        {
            if (attribute is null)
            {
                return default;
            }

            bool isFound = false;
            string value = string.Empty;

            // Extract the key value from the attribute
            if (attribute.ArgumentList is null)
            {
                return default!;
            }

            var argument = attribute.ArgumentList.Arguments[index];

            // Check for literal values: 'true or value', '123..', 'a string'
            if (argument.NameEquals is not null && argument.Expression is LiteralExpressionSyntax literal)
            {
                value = literal.ToString().Trim('"');
                isFound = true;
            }

            if (argument.Expression is LiteralExpressionSyntax literal1)
            {
                value = literal1.ToString().Trim('"');
                isFound = true;
            }

            if (!isFound || value == "null")
            {
                return default;
            }

            return Cast<T>(value as object);
        }

        public T[] GetArrayOfLiteralArguments<T>(string name)
        {
            if (attribute is null || attribute.ArgumentList is null)
            {
                return [];
            }

            // Extract the key value from the attribute
            var arguments = attribute.ArgumentList.Arguments;

            foreach (var argument in arguments)
            {
                if (argument.NameEquals is not null && argument.Expression is CollectionExpressionSyntax collection)
                {
                    if (argument.NameEquals.Name.ToString() == name)
                    {
                        var items = new List<T>();

                        foreach (var element in collection.Elements.OfType<ExpressionElementSyntax>())
                        {
                            if (element.Expression is LiteralExpressionSyntax literal)
                            {
                                object value = literal.ToString().Trim('"');

                                items.Add(Cast<T>(value)!);
                            }

                            if (element.Expression is InvocationExpressionSyntax invocation && 
                                invocation.Expression is IdentifierNameSyntax identifierName && 
                                identifierName.Identifier.ValueText == "nameof" &&
                                invocation.ArgumentList.Arguments[0].Expression is IdentifierNameSyntax argumentName &&
                                argumentName.Identifier.Value is T item)
                            {
                                items.Add(item);
                            }
                        }

                        return items.ToArray();
                    }
                }
            }

            return [];
        }
    }

    private static T? Cast<T>(object value)
    {
        var type = typeof(T);

        if (type.IsEnum)
        {
            if (Enum.Parse(type, value.ToString()!, true) is T cast)
            {
                return cast;
            }
            return default;
        }

        return type.Name switch
        {
            nameof(String) when value is T cast => cast,
            nameof(Boolean) when bool.Parse(value.ToString()) is T cast => cast,
            nameof(Int16) when short.Parse(value.ToString()) is T cast => cast,
            nameof(Int32) when int.Parse(value.ToString()) is T cast => cast,
            nameof(Int64) when long.Parse(value.ToString()) is T cast => cast,
            nameof(UInt16) when ushort.Parse(value.ToString()) is T cast => cast,
            nameof(UInt32) when uint.Parse(value.ToString()) is T cast => cast,
            nameof(UInt64) when ulong.Parse(value.ToString()) is T cast => cast,
            _ => default!
        };
    }
}
