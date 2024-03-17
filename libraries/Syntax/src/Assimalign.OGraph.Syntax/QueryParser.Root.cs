using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public sealed partial class QueryParser
{
    private RootNode? ParseRoot(ref TokenLexer lexer, ParserContext context)
    {
        Token token;
        Int32 start = lexer.Current.Start;
        Int32 startLine = lexer.Current.Line;
        Int32 end;
        Int32 endLine;
        Location location;

        String text;

        if (lexer.TryPeek(out token) && token.TokenType == TokenType.Vertex)
        {
            lexer.Skip();

            var vertex = ParseVertex(ref lexer, context);

            if (vertex is null)
            {
                return null;
            }

            end = lexer.Position;
            endLine = lexer.Line;
            text = lexer.GetText(start, end);
            location = Location.Create(startLine, endLine, start, end);

            return new RootNode(
                vertex,
                text,
                location);
        }

        var nodes = new List<QueryNode>();

        while (lexer.TryNext(out token))
        {
            QueryNode? current = token.TokenType switch
            {
                TokenType.Project => ParseProject(ref lexer, context),
                TokenType.Sort => ParseSort(ref lexer, context),
                TokenType.Filter => ParseFilter(ref lexer, context),
                TokenType.Page => ParsePage(ref lexer, context),
                _ => null
            };
            if (current is null)
            {
                // TODO: Add unexpected token diagnostic
                return null;
            }
            else
            {
                nodes.Add(current);
            }
        }

        // Capture ending position and line
        end = lexer.Position;
        endLine = lexer.Line;
        text = lexer.GetText(start, end);
        location = Location.Create(start, endLine, start, end);

        return new RootNode(
            new VertexNode(nodes),
            text,
            location);
    }
}
