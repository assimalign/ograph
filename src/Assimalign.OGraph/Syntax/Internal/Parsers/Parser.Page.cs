using System;
using System.Linq;
using System.Reflection.Metadata;

namespace Assimalign.OGraph.Syntax.Internal;

internal sealed class PageParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {
        if (node is not PageQueryNode pageNode)
        {
            // TODO: Add diagnostics information. Expected RootQueryNode to follow
            return node;
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

        return ParseParenthesisBlock(ref lexer, context, pageNode);
    }

    private QueryNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
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

                break;
            }

            queryNode = ParseBracketBlock(ref lexer, context, queryNode);
        }

        return queryNode;
    }
    private QueryNode ParseBracketBlock(ref TokenLexer lexer, ParserContext context, QueryNode node)
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
                case TokenType.Skip:
                    node = ParseSkip(ref lexer, context, node);
                    break;
                case TokenType.Take:
                    node = ParseTake(ref lexer, context, node);
                    break;
                case TokenType.Token:
                    node = ParseToken(ref lexer, context, node);
                    break;
                default:
                    {
                        // TODO: Add Diagnostic information. Unexpected token
                        break;
                    }
            }
        }

        return node;
    }
    private QueryNode ParseSkip(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {
        if (node is not PageQueryNode pageNode)
        {
            // TODO: 
            return node;
        }

        var token = lexer.Next();
        var parser = context.GetParser<ConstantParser>();

        if (parser.Parse(ref lexer, context, pageNode) is ConstantQueryNode constant)
        {
            return new PageQueryNode()
            {
                Skip = constant,
                Take = pageNode.Take,
                Token = pageNode.Token
            };
        }
        else
        {
            // TODO: Add diagnostic information
        }

        return node;
    }
    private QueryNode ParseTake(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {
        if (node is not PageQueryNode pageNode)
        {
            // TODO: 
            return node;
        }

        var token = lexer.Next();
        var parser = context.GetParser<ConstantParser>();

        if (parser.Parse(ref lexer, context, pageNode) is ConstantQueryNode constant)
        {
            return new PageQueryNode()
            {
                Take = constant,
                Skip = pageNode.Skip,
                Token = pageNode.Token
            };
        }
        else
        {
            // TODO: Add diagnostic information
        }

        return node;
    }
    private QueryNode ParseToken(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {
        if (node is not PageQueryNode pageNode)
        {
            // TODO: 
            return node;
        }

        var token = lexer.Next();
        var parser = context.GetParser<ConstantParser>();

        if (parser.Parse(ref lexer, context, pageNode) is ConstantQueryNode constant)
        {
            return new PageQueryNode()
            {
                Skip = pageNode.Skip,
                Take = pageNode.Take,
                Token = constant
            };
        }
        else
        {
            // TODO: Add diagnostic information
        }

        return node;
    }
}
