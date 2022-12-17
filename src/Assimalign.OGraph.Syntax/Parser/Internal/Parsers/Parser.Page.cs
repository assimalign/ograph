using System;
using System.Linq;

namespace Assimalign.OGraph.Syntax.Internal;

using Assimalign.OGraph.Syntax.Lexer;

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

    private QueryNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, QueryNode node)
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
            pageNode.SetSkip(constant);
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
            pageNode.SetTake(constant);
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
            pageNode.SetToken(constant);
        }
        else
        {
            // TODO: Add diagnostic information
        }

        return node;
    }
}
