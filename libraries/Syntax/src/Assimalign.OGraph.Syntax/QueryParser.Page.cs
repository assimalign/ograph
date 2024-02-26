using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public sealed partial class QueryParser
{
    private PageNode? ParsePage(ref TokenLexer lexer, ParserContext context)
    {
        Token token;

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
                // If there is more token after the closing parenthesis and no dot separator, then error
                if (lexer.TryNext(out token) && token.TokenType != TokenType.Dot)
                {
                    AddExpectedDotSeparatorDiagnostic(ref lexer, context);
                }
                return new PageNode(skip!, take!);
            }

            while (lexer.TryNext(out token))
            {
                if (token.TokenType == TokenType.Skip)
                {
                    if (lexer.TryNext(out token) && token.TokenType != TokenType.Integer)
                    {
                        AddExpectedIntegerDiagnostic(ref lexer, context);
                        return null;
                    }
                    skip = ParseConstant(ref lexer, context);
                    continue;
                }
                if (token.TokenType == TokenType.Take)
                {
                    if (lexer.TryNext(out token) && token.TokenType != TokenType.Integer)
                    {
                        AddExpectedIntegerDiagnostic(ref lexer, context);
                        return null;
                    }
                    take = ParseConstant(ref lexer, context);
                    continue;
                }
                if (token.TokenType == TokenType.CloseBracket)
                {
                    break;
                }
            }
        }

        AddExpectedClosingParenDiagnostic(ref lexer, context);

        return null;
    }
}
