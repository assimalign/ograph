using System;
using System.Linq;

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


        if (!lexer.HasNext)
        {
            // TODO : Add diagnostic information
            return node;
        }

        if (lexer.TryPeek(out var next) && next.TokenType == TokenType.Alias)
        {
            return ParseIdentifierAlias(ref lexer, context, node);
        }


        return node;
    }
    private FieldQueryNode ParseIdentifierAlias(ref TokenLexer lexer, ParserContext context, FieldQueryNode node)
    {
        var token = default(Token);

        if (!lexer.HasNext && (token = lexer.Next()).TokenType != TokenType.Identifier)
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

}
