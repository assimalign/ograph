using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public sealed partial class QueryParser
{
    private SortNode? ParseSort(ref TokenLexer lexer, ParserContext context)
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
                if (lexer.TryPeek(out var peek) && peek.TokenType != TokenType.Dot)
                {
                    lexer.Next();
                    AddExpectedDotSeparatorDiagnostic(ref lexer, context);
                }
            }
        }

        throw new NotImplementedException();
    }
}
