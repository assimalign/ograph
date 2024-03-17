using System;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public sealed partial class QueryParser
{
    private PageNode? ParsePage(ref TokenLexer lexer, ParserContext context)
    {
        Token token = lexer.Current;
        
        // Capture the dot notation if the previous node was chained.
        if (lexer.Previous.TokenType == TokenType.Dot)
        {
            token = lexer.Previous;
        }

        Int32 start = token.Start;
        Int32 startLine = token.Line;
        Int32 end;
        Int32 endLine;

        ConstantNode? skip = null;
        ConstantNode? take = null;

        // Ensure next token is an Open Parenthesis Block
        if (!lexer.TryNext(out token))
        {
            AddEofDiagnostic(ref lexer, context);
            return null;
        }
        if (token.TokenType != TokenType.OpenParenthesis || lexer.Previous.TokenType != TokenType.Page)
        {
            AddExpectedOpenParenDiagnostic(ref lexer, context);
            return null;
        }

        // Ensure next token is bracket block
        if (lexer.TryPeek(out token) && token.TokenType != TokenType.OpenBracket)
        {
            AddExpectedOpenBracketDiagnostic(ref lexer, context);
            return null;
        }

        // Parse Parenthesis Block
        while (lexer.TryNext(out token))
        {
            if (token.TokenType == TokenType.CloseParenthesis)
            {
                // Capture ending position and line
                end = token.End;
                endLine = token.Line;

                // If there is more token after the closing parenthesis and no dot separator, then error
                if (lexer.TryNext(out token) && token.TokenType != TokenType.Dot)
                {
                    AddExpectedDotSeparatorDiagnostic(ref lexer, context);
                }

                var text = lexer.GetText(start, end);
                var location = Location.Create(startLine, endLine, start, end);

                return new PageNode(
                    skip!, 
                    take!,
                    text,
                    location);
            }

            while (lexer.TryNext(out token))
            {
                if (token.TokenType == TokenType.CloseBracket)
                {
                    break;
                }
                if (token.TokenType == TokenType.Skip)
                {
                    if (lexer.TryNext(out token) && token.TokenType != TokenType.Integer)
                    {
                        AddExpectedIntegerDiagnostic(ref lexer, context);
                        return null;
                    }
                    skip = ParseConstant(ref lexer, context);
                }
                else if (token.TokenType == TokenType.Take)
                {
                    if (lexer.TryNext(out token) && token.TokenType != TokenType.Integer)
                    {
                        AddExpectedIntegerDiagnostic(ref lexer, context);
                        return null;
                    }
                    take = ParseConstant(ref lexer, context);
                }
                else
                {
                    SkipToClosingBracket(ref lexer);
                    break;
                }
                
            }
        }

        AddExpectedClosingParenDiagnostic(ref lexer, context);

        return null;
    }
}
