using System;
using System.Linq;
using System.Reflection.Metadata;

namespace Assimalign.OGraph.Syntax.Internal;

internal sealed class PageParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        if (queryNode is not PageQueryNode pageNode)
        {
            throw QueryParserException.UnexpectedQueryNode(
                typeof(PageQueryNode),
                queryNode.GetType());
        }
        if (!lexer.HasNext)
        {
            context.AddDiagnostic(Diagnostic.UnexpectedEOF(
                lexer.Current.End));

            return queryNode;
        }

        var token = lexer.Next();

        if (token.TokenType != TokenType.OpenParenthesis)
        {
            context.AddDiagnostic(Diagnostic.ExpectedOpeningParenthesis(
                token.Start,
                token.End));

            return queryNode;
        }

        return ParseParenthesisBlock(ref lexer, context, pageNode);
    }
    private PageQueryNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, PageQueryNode queryNode)
    {
        var next = default(Token);

        if (!lexer.TryPeek(out next))
        {
            context.AddDiagnostic(Diagnostic.UnexpectedEOF(
                lexer.Current.End));

            return queryNode;
        }
        // Check if projection is followed by an edge identifier
        if (next.TokenType == TokenType.Identifier)
        {
            var parser = context.GetParser<EdgeParser>();

            queryNode = new PageQueryNode()
            {
                Edge = parser.Parse<EdgeQueryNode>(ref lexer, context)
            };

            if (!lexer.TryNext(out next))
            {
                context.AddDiagnostic(Diagnostic.UnexpectedEOF(
                    lexer.Current.End));

                return queryNode;
            }
        }
        if (next.TokenType != TokenType.OpenBracket)
        {
            context.AddDiagnostic(Diagnostic.ExpectedOpeningBracket(
                next.Start,
                next.End));

            return queryNode;
        }
        // Parse Parenthesis Block
        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseParenthesis)
            {
                // If there is more token after the closing parenthesis and no dot separator, then error
                if (lexer.TryPeek(out var peek) && peek.TokenType != TokenType.Dot)
                {
                    context.AddDiagnostic(Diagnostic.ExpectedDotSeparator(
                        peek.Start,
                        peek.End));
                }

                return queryNode;
            }

            queryNode = ParseBracketBlock(ref lexer, context, queryNode);
        }

        context.AddDiagnostic(Diagnostic.ExpectedClosingParenthesis(
            lexer.Current.Start,
            lexer.Current.End));

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
