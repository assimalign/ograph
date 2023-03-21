using System;

namespace Assimalign.OGraph.Syntax.Internal;

internal class EdgeParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        if (queryNode is not EdgeNode edgeNode)
        {
            throw QueryParserException.UnexpectedQueryNode(
                typeof(EdgeNode),
                queryNode.GetType());
        }

        var edgePath = string.Empty;

        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.Comma)
            {
                return queryNode;
            }
            if (token.TokenType == TokenType.Slash)
            {
                continue;
            }
            if (token.TokenType == TokenType.Identifier)
            {
                queryNode = new EdgeNode()
                {
                    Name = edgePath = string.IsNullOrEmpty(edgePath) ? token.Text : string.Join('/', edgePath, token.Text)
                };
                if (!lexer.TryPeek(out var peek))
                {
                    context.AddDiagnostic(Diagnostic.UnexpectedEOF(
                        lexer.Current.End));

                    return queryNode;
                }
                if (peek.TokenType != TokenType.Slash && peek.TokenType != TokenType.Comma)
                {
                    context.AddDiagnostic(Diagnostic.ExpectedCommaSeparator(
                        peek.Start,
                        peek.End));
                }
            }
            else
            {
                context.AddDiagnostic(Diagnostic.InvalidToken(ref token));
            }
        }

        return edgeNode;
    }
}
