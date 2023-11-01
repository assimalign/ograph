using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax.Internal;

internal class ProjectionParser : Parser<ProjectionNode>
{
    internal override ProjectionNode Parse(ref TokenLexer lexer, ParserContext context, ProjectionNode queryNode)
    {
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

        return ParseParenthesisBlock(ref lexer, context, queryNode);
    }
    private ProjectionNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, ProjectionNode queryNode)
    {
        Token token;

        if (!lexer.TryPeek(out token))
        {
            context.AddDiagnostic(Diagnostic.UnexpectedEOF(
                lexer.Current.End));

            return queryNode;
        }
        if (token.TokenType != TokenType.OpenBracket)
        {
            context.AddDiagnostic(Diagnostic.ExpectedOpeningBracket(
                token.Start,
                token.End));

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
    private ProjectionNode ParseBracketBlock(ref TokenLexer lexer, ParserContext context, ProjectionNode queryNode)
    {
        var properties = new List<PropertyNode>();

        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseBracket)
            {
                return new ProjectionNode()
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
