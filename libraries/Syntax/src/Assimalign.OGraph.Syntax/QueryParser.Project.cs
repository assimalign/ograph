using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public sealed partial class QueryParser
{
    private ProjectNode? ParseProject(ref TokenLexer lexer, ParserContext context)
    {
        Token token;

        // Ensure next token is an Open Parenthesis Block
        if (!lexer.TryNext(out token))
        {
            AddEofDiagnostic(ref lexer, context);
            return null;
        }
        if (token.TokenType != TokenType.OpenParenthesis || lexer.Previous.TokenType != TokenType.Project)
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

        var properties = new List<PropertyNode>();

        while (lexer.TryNext(out token))
        {
            if (token.TokenType == TokenType.CloseParenthesis)
            {
                // If there is more token after the closing parenthesis and no dot separator, then error
                if (lexer.TryNext(out token) && token.TokenType != TokenType.Dot)
                {
                    AddExpectedDotSeparatorDiagnostic(ref lexer, context);
                }
                return new ProjectNode(properties);
            }

            while (lexer.TryNext(out token))
            {
                if (token.TokenType == TokenType.Identifier)
                {
                    var property = ParseProperty(ref lexer, context);

                    properties.Add(property);
                }
                else if (token.TokenType == TokenType.CloseBracket)
                {
                    break;
                }
            }
        }

        AddExpectedClosingParenDiagnostic(ref lexer, context);

        return null;
    }
}
