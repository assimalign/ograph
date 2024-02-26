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
        Token token;

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
