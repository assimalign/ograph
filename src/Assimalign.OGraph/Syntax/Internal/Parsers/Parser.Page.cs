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

    private PageQueryNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, PageQueryNode queryNode)
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
            var edgeNode = edgeParser.Parse<EdgeQueryNode>(ref lexer, context);

            queryNode = new PageQueryNode()
            {
                Edge = edgeNode
            };

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
    private PageQueryNode ParseBracketBlock(ref TokenLexer lexer, ParserContext context, PageQueryNode queryNode)
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
                    queryNode = ParseSkip(ref lexer, context, queryNode);
                    break;
                case TokenType.Take:
                    queryNode = ParseTake(ref lexer, context, queryNode);
                    break;
                case TokenType.Token:
                    queryNode = ParseToken(ref lexer, context, queryNode);
                    break;
                default:
                    {
                        // TODO: Add Diagnostic information. Unexpected token
                        break;
                    }
            }
        }

        return queryNode;
    }
    private PageQueryNode ParseSkip(ref TokenLexer lexer, ParserContext context, PageQueryNode queryNode)
    {
        if (queryNode is not PageQueryNode pageNode)
        {
            // TODO: 
            return queryNode;
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

        return queryNode;
    }
    private PageQueryNode ParseTake(ref TokenLexer lexer, ParserContext context, PageQueryNode queryNode)
    {
        if (queryNode is not PageQueryNode pageNode)
        {
            // TODO: 
            return queryNode;
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

        return queryNode;
    }
    private PageQueryNode ParseToken(ref TokenLexer lexer, ParserContext context, PageQueryNode queryNode)
    {
        if (queryNode is not PageQueryNode pageNode)
        {
            // TODO: 
            return queryNode;
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

        return queryNode;
    }
}
