using System;
using System.Linq;
using System.Reflection.Metadata;

namespace Assimalign.OGraph.Syntax.Internal;

internal sealed class PageParser : Parser
{
    internal override QueryNode? Parse(ref TokenLexer lexer, ParserContext context)
    {
        Token token;

        // Ensure next token is an Open Parenthesis Block
        if (lexer.TryNext(out token) && token.TokenType != TokenType.OpenParenthesis)
        {
            AddExpectedOpenParenDiagnostic(ref lexer, context);
            return null;
        }

        return ParseParenthesisBlock(ref lexer, context, default);
    }
  
    private PageNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, PageNode queryNode)
    {
        Token token;

        // Ensure not EOF (End of File)
        if (!lexer.HasNext)
        {
            AddEofDiagnostic(ref lexer, context);
            return queryNode;
        }

        token = lexer.Next();

        // Ensure next token is bracket block
        if (token.TokenType != TokenType.OpenBracket)
        {
            AddExpectedOpenBracketDiagnostic(ref lexer, context);
            return queryNode;
        }

        // Parse Parenthesis Block
        while (lexer.HasNext)
        {
            token = lexer.Next();

            if (token.TokenType == TokenType.CloseParenthesis)
            {
                // If there is more token after the closing parenthesis and no dot separator, then error
                if (lexer.TryPeek(out var peek) && peek.TokenType != TokenType.Dot)
                {
                    lexer.Next();
                    AddExpectedDotSeparatorDiagnostic(ref lexer, context);
                }

                return queryNode;
            }

            queryNode = ParseBracketBlock(ref lexer, context, queryNode);
        }

        AddExpectedClosingParenDiagnostic(ref lexer, context);

        return queryNode;
    }
    private PageNode ParseBracketBlock(ref TokenLexer lexer, ParserContext context, PageNode queryNode)
    {
        Token token = lexer.Current;

        while (lexer.HasNext)
        {
            queryNode = token.TokenType switch
            {
                TokenType.Take => ParseTake(ref lexer, context, queryNode),
                TokenType.Skip => ParseSkip(ref lexer, context, queryNode)
            };

            token = lexer.Next();

            if (token.TokenType == TokenType.CloseBracket)
            {
                break;
            }
        }

        return queryNode;
    }
    private PageNode ParseSkip(ref TokenLexer lexer, ParserContext context, PageNode queryNode)
    {
        Token token;

        // Ensure not EOF (End of File)
        if (!lexer.HasNext)
        {
            AddEofDiagnostic(ref lexer, context);
            return queryNode;
        }

        token = lexer.Next();

        if (token.TokenType != TokenType.Integer)
        {
            AddExpectedIntegerDiagnostic(ref lexer, context);
            return queryNode;
        }

        var constant = (ConstantNode)context.GetParser<ConstantParser>()
            .Parse(ref lexer, context);

        queryNode.SetSkip(constant);

        return queryNode;
    }
    private PageNode ParseTake(ref TokenLexer lexer, ParserContext context, PageNode queryNode)
    {
        Token token;

        // Ensure not EOF (End of File)
        if (!lexer.HasNext)
        {
            AddEofDiagnostic(ref lexer, context);
            return queryNode;
        }

        token = lexer.Next();

        if (token.TokenType != TokenType.Integer)
        {
            AddExpectedIntegerDiagnostic(ref lexer, context);
            return queryNode;
        }

        var constant = (ConstantNode)context.GetParser<ConstantParser>()
            .Parse(ref lexer, context);

        queryNode.SetTake(constant);

        return queryNode;
    }
}
