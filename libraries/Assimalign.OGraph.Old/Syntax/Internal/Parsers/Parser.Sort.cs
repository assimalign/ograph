using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal class SortParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        if (queryNode is not SortNode sortNode)
        {
            throw QueryParserException.UnexpectedQueryNode(
                typeof(SortNode),
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

        return ParseParenthesisBlock(ref lexer, context, sortNode);
    }

    private SortNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, SortNode queryNode)
    {
        var token = default(Token);

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
    private SortNode ParseBracketBlock(ref TokenLexer lexer, ParserContext context, SortNode queryNode)
    {
        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseBracket)
            {
                return queryNode;
            }
            if (token.TokenType != TokenType.Identifier)
            {
                context.AddDiagnostic(Diagnostic.InvalidToken(ref token));
            }
            else
            {
                queryNode = ParseSortIdentifier(ref lexer, context, queryNode);
            }            
        }

        context.AddDiagnostic(Diagnostic.ExpectedClosingBracket(
            lexer.Current.Start,
            lexer.Current.End));

        return queryNode;
    }

    private SortNode ParseSortIdentifier(ref TokenLexer lexer, ParserContext context, SortNode queryNode)
    {
        var token = lexer.Current;
        var sortNode = default(QueryNode);

        switch (token.TokenType)
        {
            case TokenType.Identifier when token.Value.IsFunction():
                {
                    queryNode = new SortNode()
                    {
                        ThenBy = queryNode.ThenBy,
                        Direction = queryNode.Direction,
                        Identifier = (FunctionCallNode)context.GetParser<FunctionParser>()
                            .Parse(ref lexer, context, new FunctionCallNode())
                    };
                    break;
                }
            case TokenType.Identifier:
                {
                    queryNode = new SortNode()
                    {
                        ThenBy = queryNode.ThenBy, 
                        Direction = queryNode.Direction,
                        Identifier = (PropertyNode)context.GetParser<PropertyParser>()
                            .Parse(ref lexer, context, new PropertyNode())
                    };
                    break;
                }
            default:
                {
                    context.AddDiagnostic(Diagnostic.InvalidToken(ref token));
                    return queryNode;
                }
        }
        if (lexer.TryPeek(out token))
        {
            if (token.TokenType == TokenType.Ascending || token.TokenType == TokenType.Descending)
            {
                token = lexer.Next();

                queryNode = new SortNode()
                {
                    Direction = (SortDirection)token.TokenType,
                    Identifier = queryNode.Identifier,
                    ThenBy = queryNode.ThenBy,
                };

                if (!lexer.TryPeek(out token))
                {
                    context.AddDiagnostic(Diagnostic.UnexpectedEOF(
                        lexer.Current.End));
                }
            }
            if (token.TokenType == TokenType.Identifier)
            {
                token = lexer.Next();

                queryNode = new SortNode()
                {
                    Direction = queryNode.Direction,
                    Identifier = queryNode.Identifier,
                    ThenBy = ParseSortIdentifier(ref lexer, context, new SortNode()),
                };
            }
            else
            {
                queryNode = new SortNode()
                {
                    ThenBy = queryNode.ThenBy, 
                    Direction = queryNode.Direction,
                    Identifier = queryNode.Identifier
                };
            }
        }

        return queryNode;
    }
}
