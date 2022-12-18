using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Assimalign.OGraph.Syntax.Internal;

internal class ProjectionParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {
        if (node is not ProjectionQueryNode projectionNode)
        {
            throw QueryParserException.InvalidPage();
        }

        if (!lexer.HasNext)
        {
            // TODO: Add diagnostics unexpected EOF
            return node;
        }

        var token = lexer.Next();

        if (token.TokenType != TokenType.OpenParenthesis)
        {
            // TODO: Add diagnostic error. Expected starting parenthesis block
            return node;
        }

        return ParseParenthesisBlock(ref lexer, context, projectionNode);
    }


    private ProjectionQueryNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, ProjectionQueryNode node)
    {
        if (!lexer.TryPeek(out var next))
        {
            // TODO: Add Diagnostic error. Unexpected EOF
            return node;
        }
        if (next.TokenType != TokenType.OpenBracket)
        {
            // TODO: Add diagnostic error. Expected starting bracket block
            return node;
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

                break;
            }

            node = ParseBracketBlock(ref lexer, context, node);
        }

        return node;
    }
    private ProjectionQueryNode ParseBracketBlock(ref TokenLexer lexer, ParserContext context, ProjectionQueryNode node)
    {
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
                        var nodes = node.Fields.ToList();
                        nodes.Add(ParseIdentifierNode(ref lexer, context, new FieldQueryNode()));
                        node = new ProjectionQueryNode()
                        {
                            Fields = nodes
                        };
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

        return node;
    }

    private FieldQueryNode ParseIdentifierNode(ref TokenLexer lexer, ParserContext context, FieldQueryNode node)
    {
        var identifierParser = context.GetParser<IdentifierParser>();
        var identifierNode = identifierParser.Parse(ref lexer, context, node);

        node = new FieldQueryNode()
        {
            Value = identifierNode
        };

        // Comma Check
        if (lexer.TryPeek(out var next1) && next1.TokenType == TokenType.Comma)
        {
            lexer.Skip();
        }
        // Alias Check
        if (lexer.TryPeek(out var next2) && next2.TokenType == TokenType.Alias)
        {
            lexer.Skip();
            node = ParseIdentifierAlias(ref lexer, context, node);
        }
        // Nested Project Check
        if (lexer.TryPeek(out var next3) && next3.TokenType == TokenType.OpenBracket)
        {
            node = ParseNestedFieldNode(ref lexer, context, node);
        }


        return node;
    }

    public FieldQueryNode ParseNestedFieldNode(ref TokenLexer lexer, ParserContext context, FieldQueryNode node)
    {
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
                        var nodes = node.Children?.ToList() ?? new List<FieldQueryNode>();

                        nodes.Add(ParseIdentifierNode(ref lexer, context, new FieldQueryNode()));

                        node = new FieldQueryNode()
                        {
                            Alias = node.Alias,
                            Children = nodes,
                            Value = node.Value
                        };
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

        return node;
    }
    private FieldQueryNode ParseIdentifierAlias(ref TokenLexer lexer, ParserContext context, FieldQueryNode node)
    {
        if (!lexer.HasNext)
        {
            // TODO: Add diagnostic error. Unexpected EOF
            return node;
        }

        var token  = lexer.Next();

        if (token.TokenType != TokenType.Identifier)
        {
            // TODO: Add diagnostic error. Expected identifier to follow 'as' operator
            return node;
        }

        return new FieldQueryNode()
        {
            Alias = token.ValueAsText,
            Children = node.Children,
            Value = node.Value
        };
    }

    private QueryNode ParseBinaryAsIdentifier(ref TokenLexer lexer, ParserContext context, FieldQueryNode node)
    {


        return default;
    }

}
