using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax.Internal;

internal class ProjectionParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        if (queryNode is not ProjectionQueryNode projectionNode)
        {
            throw QueryParserException.UnexpectedQueryNode(
                typeof(ProjectionQueryNode),
                queryNode.GetType());
        }
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

        return ParseParenthesisBlock(ref lexer, context, projectionNode);
    }
    private ProjectionQueryNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, ProjectionQueryNode queryNode)
    {
        var next = default(Token);

        if (!lexer.TryPeek(out next))
        {
            context.AddDiagnostic(Diagnostic.UnexpectedEOF(
                lexer.Current.End));

            return queryNode;
        }
        // Check if projection is followed by an edge identifier
        if (next.TokenType == TokenType.Identifier)
        {
            var parser = context.GetParser<EdgeParser>();

            queryNode = new ProjectionQueryNode()
            {
                Edge = parser.Parse<EdgeQueryNode>(ref lexer, context)
            };

            if (!lexer.TryNext(out next))
            {
                context.AddDiagnostic(Diagnostic.UnexpectedEOF(
                    lexer.Current.End));

                return queryNode;
            }
        }
        if (next.TokenType != TokenType.OpenBracket)
        {
            context.AddDiagnostic(Diagnostic.ExpectedOpeningBracket(
                next.Start,
                next.End));

            return queryNode;
        }
        // Parse Parenthesis Block
        while (lexer.HasNext)
        {
            var token = lexer.Next();
            
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
    private ProjectionQueryNode ParseBracketBlock(ref TokenLexer lexer, ParserContext context, ProjectionQueryNode queryNode)
    {
        var properties = new List<PropertyQueryNode>();

        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseBracket)
            {
                return new ProjectionQueryNode()
                {
                    Edge = queryNode.Edge,
                    Properties = properties
                };
            }
            switch (token.TokenType)
            {
                case TokenType.Identifier:
                    {
                        properties.Add(context.GetParser<PropertyParser>().Parse<PropertyQueryNode>(ref lexer, context));
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
