using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public sealed partial class QueryParser
{
    private ProjectNode? ParseProject(ref TokenLexer lexer, ParserContext context)
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
        Location location;
        String text;

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
                // Capture ending position and line
                end = lexer.Position;
                endLine = lexer.Line;
                text = lexer.GetText(start, end);
                location = Location.Create(startLine, endLine, start, end);

                // If there is more token after the closing parenthesis and no dot separator, then error
                if (lexer.TryNext(out token) && token.TokenType != TokenType.Dot)
                {
                    AddExpectedDotSeparatorDiagnostic(ref lexer, context);
                }

                return new ProjectNode(
                    properties,
                    text,
                    location);
            }
            while (lexer.TryNext(out token))
            {
                if (token.TokenType == TokenType.CloseBracket)
                {
                    break;
                }
                if (token.TokenType == TokenType.Identifier)
                {
                    var property = ParseProperty(ref lexer, context);

                    if(property is null)
                    {
                        continue;
                    }

                    properties.Add(property);
                }
            }
        }

        AddExpectedClosingParenDiagnostic(ref lexer, context);

        return null;
    }
}
