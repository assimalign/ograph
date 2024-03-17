using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public sealed partial class QueryParser
{
    private FilterNode? ParseFilter(ref TokenLexer lexer, ParserContext context)
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
        if (token.TokenType != TokenType.OpenParenthesis || lexer.Previous.TokenType != TokenType.Filter)
        {
            AddExpectedOpenParenDiagnostic(ref lexer, context);
          
            return null;
        }
        throw new NotImplementedException();
    }
}
