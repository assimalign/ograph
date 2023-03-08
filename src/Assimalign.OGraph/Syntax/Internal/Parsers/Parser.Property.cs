using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax.Internal;

internal class PropertyParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        if (queryNode is not PropertyQueryNode propertyNode)
        {
            // TODO: Add diagnostic 
            return queryNode;
        }
        // Check if 
        if (lexer.Current.TokenType != TokenType.Identifier)
        {
            // TODO: Add diagnostics
            return queryNode;
        }

        return ParseIdentifier(ref lexer, context, propertyNode);
    }
    private PropertyQueryNode ParseIdentifier(ref TokenLexer lexer, ParserContext context, PropertyQueryNode queryNode)
    {
        var token = default(Token);

        queryNode = new PropertyQueryNode()
        {
            Name = lexer.Current.Text
        };

        // Comma Check
        if (lexer.TryPeek(out token) && token.TokenType == TokenType.Comma)
        {
            lexer.Skip();
        }
        // Alias Check
        if (lexer.TryPeek(out token) && token.TokenType == TokenType.Alias)
        {
            lexer.Skip();
            queryNode = ParsePropertyAlias(ref lexer, context, queryNode);
        }
        // Nested Projection Check
        if (lexer.TryPeek(out token) && token.TokenType == TokenType.OpenBracket)
        {
            var children = queryNode.Children?.ToList() ?? new List<PropertyQueryNode>();

            while (lexer.HasNext)
            {
                if (!lexer.TryNext(out token))
                {
                    context.AddUnexpectedEOFError(ref lexer);
                    return queryNode;
                }

                if (token.TokenType == TokenType.CloseBracket)
                {
                    break;
                }
                switch (token.TokenType)
                {
                    case TokenType.Identifier:
                        {
                            if (Parse(ref lexer, context, new PropertyQueryNode()) is not PropertyQueryNode propertyNode)
                            {
                                // TODO: Add Diagnostic Error
                                continue;
                            }
                            children.Add(propertyNode);
                        }
                        break;
                    default:
                        {
                            // TODO: Add Diagnostic information. Unexpected token
                            break;
                        }
                }
            }

            queryNode = new PropertyQueryNode()
            {
                Alias = queryNode.Alias,
                Children = children
            };
        }

        return queryNode;
    }
    private PropertyQueryNode ParsePropertyAlias(ref TokenLexer lexer, ParserContext context, PropertyQueryNode node)
    {
        if (!lexer.HasNext)
        {
            // TODO: Add diagnostic error. Unexpected EOF
            return node;
        }

        var token = lexer.Next();

        if (token.TokenType != TokenType.Identifier)
        {
            // TODO: Add diagnostic error. Expected identifier to follow 'as' operator
            return node;
        }

        return new PropertyQueryNode()
        {
            Name = node.Name,
            Alias = token.Text,
            Children = node.Children
        };
    }
}
