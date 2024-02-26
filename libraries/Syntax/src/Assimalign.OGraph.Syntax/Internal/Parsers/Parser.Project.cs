using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax.Internal;

internal class ProjectParser : Parser<ProjectNode>
{
    internal override ProjectNode Parse(ref TokenLexer lexer, ParserContext context, ProjectNode queryNode)
    {
        Token token;

        // Ensure not EOF (End of File)
        if (!lexer.HasNext)
        {
            AddEofDiagnostic(ref lexer, context);
            return queryNode;
        }

        token = lexer.Next();

        // Ensure next token is an Open Parenthesis Block
        if (token.TokenType != TokenType.OpenParenthesis)
        {
            AddExpectedOpenParenDiagnostic(ref lexer, context);
            return queryNode;
        }

        return ParseParenthesisBlock(ref lexer, context, queryNode);
    }
    private ProjectNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, ProjectNode queryNode)
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
        if (token.TokenType == TokenType.OpenBracket)
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
    private ProjectNode ParseBracketBlock(ref TokenLexer lexer, ParserContext context, ProjectNode queryNode)
    {
        var properties = new List<PropertyNode>();

        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseBracket)
            {
                return new ProjectNode()
                {
                    Properties = properties
                };
            }
            switch (token.TokenType)
            {
                case TokenType.Identifier:
                    {
                        properties.Add((PropertyNode)context.GetParser<PropertyParser>()
                            .Parse(ref lexer, context, new PropertyNode()));
                    }
                    break;
                default:
                    {
                        context.AddDiagnostic(Diagnostic.InvalidToken(ref token));
                        break;
                    }
            }
        }

        context.AddDiagnostic(Diagnostic.ExpectedClosingBracket(
            lexer.Current.Start,
            lexer.Current.End));

        return queryNode;
    }
}
