using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Assimalign.OGraph.Syntax.Internal;

internal class ProjectionParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        if (queryNode is not ProjectionQueryNode projectionNode)
        {
            // This is internal error. Some dumbass messed with the code.
            return queryNode;
        }
        if (!lexer.HasNext)
        {
            context.AddUnexpectedEOFDiagnosticError(lexer.Current.End);
            return queryNode;
        }

        var token = lexer.Next();

        if (token.TokenType != TokenType.OpenParenthesis)
        {
            // TODO: Add diagnostic error. Expected starting parenthesis block
            return queryNode;
        }

        return ParseParenthesisBlock(ref lexer, context, projectionNode);
    }

    private ProjectionQueryNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, ProjectionQueryNode queryNode)
    {
        var next = default(Token);

        if (!lexer.TryPeek(out next))
        {
            // TODO: Add Diagnostic error. Unexpected EOF
            return queryNode;
        }
        // Check if projection is followed by an edge identifier
        if (next.TokenType == TokenType.Identifier)
        {
            var edgeParser = context.GetParser<EdgeParser>();

            if (edgeParser.Parse(ref lexer, context, new EdgeQueryNode()) is not EdgeQueryNode edge)
            {
                // TODO: 
            }
            else
            {
                queryNode = new ProjectionQueryNode() 
                { 
                    Edge = edge, 
                };
            }
            if (!lexer.TryPeek(out next))
            {
                // TODO: Add Diagnostic error. Unexpected EOF
                return queryNode;
            }
        }
        if (next.TokenType != TokenType.OpenBracket)
        {
            // TODO: Add diagnostic error. Expected starting bracket block
            return queryNode;
        }
        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseParenthesis)
            {
                if (lexer.TryPeek(out var peek) && peek.TokenType != TokenType.Dot)
                {
                    // TODO: Diagnostics error dot notation is required
                }

                return queryNode;
            }

            queryNode = ParseBracketBlock(ref lexer, context, queryNode);
        }

        // TODO: Add diagnostics error. Missing Closing Parentheisis


        return queryNode;
    }
    private ProjectionQueryNode ParseBracketBlock(ref TokenLexer lexer, ParserContext context, ProjectionQueryNode queryNode)
    {
        var attributes = new List<AttributeQueryNode>();

        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseBracket)
            {
                break;
            }
            switch (token.TokenType)
            {
                case TokenType.Identifier:
                    {
                        attributes.Add(ParseIdentifierNode(ref lexer, context, new AttributeQueryNode()));
                    }
                    break;
                case TokenType.String:
                case TokenType.Integer:
                case TokenType.FloatingPoint:
                case TokenType.Boolean:
                case TokenType.Null:
                    {
                        attributes.Add(ParseConstantAsIdentifier(ref lexer, context, new AttributeQueryNode()));
                        break;
                    }
                default:
                    {
                        // TODO: Add Diagnostic information. Unexpected token
                        break;
                    }
            }
        }

        queryNode = new ProjectionQueryNode()
        {
            Edge = queryNode.Edge,
            Attributes = attributes
        };

        return queryNode;
    }
    private AttributeQueryNode ParseIdentifierNode(ref TokenLexer lexer, ParserContext context, AttributeQueryNode queryNode)
    {
        var next = default(Token);
        var identifierParser = context.GetParser<IdentifierParser>();
        var identifierNode = identifierParser.Parse(ref lexer, context, queryNode);

        queryNode = new AttributeQueryNode()
        {
            Value = identifierNode
        };

        // Comma Check
        if (lexer.TryPeek(out next) && next.TokenType == TokenType.Comma)
        {
            lexer.Skip();
        }
        // Alias Check
        if (lexer.TryPeek(out next) && next.TokenType == TokenType.Alias)
        {
            lexer.Skip();
            queryNode = ParseIdentifierAlias(ref lexer, context, queryNode);
        }
        // Nested Project Check
        if (lexer.TryPeek(out next) && next.TokenType == TokenType.OpenBracket)
        {
            queryNode = ParseNestedAttributeNode(ref lexer, context, queryNode);
        }

        
        return queryNode;
    }
    private AttributeQueryNode ParseNestedAttributeNode(ref TokenLexer lexer, ParserContext context, AttributeQueryNode queryNode)
    {
        var children = queryNode.Children?.ToList() ?? new List<AttributeQueryNode>();

        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseBracket)
            {
                break;
            }
            switch (token.TokenType)
            {
                case TokenType.Identifier:
                    {
                        children.Add(ParseIdentifierNode(ref lexer, context, new AttributeQueryNode()));
                    }
                    break;
                case TokenType.String:
                case TokenType.Integer:
                case TokenType.FloatingPoint:
                    {

                        break;
                    }
                default:
                    {
                        // TODO: Add Diagnostic information. Unexpected token
                        break;
                    }
            }
        }

        queryNode = new AttributeQueryNode()
        {
            Alias = queryNode.Alias,
            Value = queryNode.Value,
            Children = children
        };

        return queryNode;
    }
    private AttributeQueryNode ParseIdentifierAlias(ref TokenLexer lexer, ParserContext context, AttributeQueryNode node)
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

        return new AttributeQueryNode()
        {
            Alias = token.Text,
            Children = node.Children,
            Value = node.Value
        };
    }
    private AttributeQueryNode ParseConstantAsIdentifier(ref TokenLexer lexer, ParserContext context, AttributeQueryNode queryNode)
    {
        var next = default(Token);
        var constantNode = context.GetParser<ConstantParser>()
            .Parse(ref lexer, context, new ConstantQueryNode());

        // Comma Check
        if (lexer.TryPeek(out next) && next.TokenType == TokenType.Comma)
        {
            lexer.Skip();
        }
        // Alias Check
        if (lexer.TryPeek(out next) && next.TokenType == TokenType.Alias)
        {
            lexer.Skip();
            queryNode = ParseIdentifierAlias(ref lexer, context, queryNode);
        }
        else
        {
            var random = new Random();
            queryNode = new AttributeQueryNode()
            {
                Alias = $"fd{random.Next(999)}",
                Children = queryNode.Children,
                Value = queryNode.Value
            };
        }

        return queryNode;
    }
    private AttributeQueryNode ParseBinaryAsIdentifierNode(ref TokenLexer lexer, ParserContext context, AttributeQueryNode queryNode)
    {

        return queryNode;
    }

}
