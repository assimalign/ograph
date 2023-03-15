using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax.Internal;

internal class PropertyParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        if (queryNode is not PropertyNode propertyNode)
        {
            throw QueryParserException.UnexpectedQueryNode(
                typeof(PropertyNode),
                queryNode.GetType());
        }
        // Check if 
        if (lexer.Current.TokenType != TokenType.Identifier)
        {
            // TODO: Add diagnostics
            return queryNode;
        }

        return ParseProperty(ref lexer, context, propertyNode);
    }
    private PropertyNode ParseProperty(ref TokenLexer lexer, ParserContext context, PropertyNode queryNode)
    {
        var peek = default(Token);

        queryNode = new PropertyNode()
        {
            Name = lexer.Current.Text
        };
        // Alias Check
        if (lexer.TryPeek(out peek) && peek.TokenType == TokenType.Alias)
        {
            lexer.Skip();
            queryNode = ParsePropertyAlias(ref lexer, context, queryNode);
        }
        // Comma Check
        if (lexer.TryPeek(out peek) && peek.TokenType == TokenType.Comma)
        {
            lexer.Skip(); // Skip comma
        }
        // Nested Projection Check
        if (lexer.TryPeek(out peek) && peek.TokenType == TokenType.OpenBracket)
        {
            lexer.Skip();

            var children = queryNode.Children?.ToList() ?? new List<PropertyNode>();

            while (lexer.HasNext)
            {
                var token = lexer.Next();

                if (token.TokenType == TokenType.CloseBracket)
                {
                    return queryNode = new PropertyNode()
                    {
                        Name = queryNode.Name,
                        Alias = queryNode.Alias,
                        Children = children
                    };
                }
                switch (token.TokenType)
                {
                    case TokenType.Identifier:
                        {
                            children.Add((PropertyNode)Parse(ref lexer, context, new PropertyNode()));
                        }
                        break;
                    default:
                        {
                            context.AddDiagnostic(Diagnostic.InvalidToken(ref token));
                            break;
                        }
                }
            }
        }

        return queryNode;
    }
    private PropertyNode ParsePropertyAlias(ref TokenLexer lexer, ParserContext context, PropertyNode queryNode)
    {
        if (!lexer.HasNext)
        {
            context.AddDiagnostic(Diagnostic.UnexpectedEOF(
                lexer.Current.End));

            return queryNode;
        }

        var token = lexer.Next();

        if (token.TokenType != TokenType.Identifier)
        {
            context.AddDiagnostic(Diagnostic.InvalidToken(ref token));
            return queryNode;
        }

        return queryNode = new PropertyNode()
        {
            Alias = token.Text,
            Name = queryNode.Name,
            Children = queryNode.Children
        };
    }
}
