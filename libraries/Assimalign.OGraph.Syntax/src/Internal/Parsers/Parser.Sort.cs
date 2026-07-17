using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal class SortParser : Parser<SortNode>
{
    internal override SortNode ParseSort(ref TokenLexer lexer, ParserContext context, SortNode queryNode)
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

    private SortNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, SortNode queryNode)
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

        //switch (token.TokenType)
        //{
        //    case TokenType.Identifier when token.Value.IsFunction():
        //        {
        //            queryNode.identifier = (FunctionCallNode)context.GetParser<FunctionParser>()
        //                .Parse(ref lexer, context, new FunctionCallNode());
        //            break;
        //        }
        //    case TokenType.Identifier:
        //        {
        //            queryNode.identifier = (PropertyNode)context.GetParser<PropertyParser>()
        //                    .Parse(ref lexer, context, new PropertyNode());
        //            break;
        //        }
        //    default:
        //        {
        //            context.AddDiagnostic(Diagnostic.InvalidToken(ref token));
        //            return queryNode;
        //        }
        //}
        //if (lexer.TryPeek(out token))
        //{
        //    if (token.TokenType == TokenType.Ascending || token.TokenType == TokenType.Descending)
        //    {
        //        token = lexer.Next();

        //        queryNode.direction = (SortDirection)token.TokenType;

        //        if (!lexer.TryPeek(out token))
        //        {
        //            context.AddDiagnostic(Diagnostic.UnexpectedEOF(
        //                lexer.Current.End));
        //        }
        //    }
        //    if (token.TokenType == TokenType.Identifier)
        //    {
        //        token = lexer.Next();

        //        queryNode.thenBy = ParseSortIdentifier(ref lexer, context, SortNode.Create());
        //    }
        //    //else
        //    //{
        //    //    queryNode = new SortNode()
        //    //    {
        //    //        ThenBy = queryNode.ThenBy,
        //    //        Direction = queryNode.Direction,
        //    //        Identifier = queryNode.Identifier
        //    //    };
        //    //}
        //}

        return queryNode;
    }
}
