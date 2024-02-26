using System;
using System.Collections.Generic;
using System.Linq;

namespace Assimalign.OGraph.Syntax.Internal;

internal class RootParser : Parser
{ 
    internal override RootNode? Parse(ref TokenLexer lexer, ParserContext context)
    {
        Token token;

        if (lexer.TryPeek(out token) && token.TokenType == TokenType.Vertex)
        {
            lexer.Skip();

            var vertex = context.GetParser<VertexParser>()
                .Parse(ref lexer, context);

            if (vertex is null)
            {
                return null;
            }

            return new RootNode(vertex);
        }

        var label = new LabelNode(string.Empty);
        var nodes = new List<QueryNode>();

        while (lexer.TryNext(out token))
        {
            var current = token.TokenType switch
            {
                //TokenType.Edge => context.GetParser<EdgeParser>()
                //    .Parse(ref lexer, context),
                //TokenType.Project => context.GetParser<ProjectParser>()
                //    .Parse(ref lexer, context),
                TokenType.Page => context.GetParser<PageParser>()
                    .Parse(ref lexer, context),
                //TokenType.Sort => context.GetParser<SortParser>()
                //    .Parse(ref lexer, context),
                //TokenType.Filter => context.GetParser<FilterParser>()
                //    .Parse(ref lexer, context),
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

        return new RootNode(new VertexNode(label, nodes));
    }
}
