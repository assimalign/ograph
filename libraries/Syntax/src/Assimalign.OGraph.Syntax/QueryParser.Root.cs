using Assimalign.OGraph.Syntax.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed partial class QueryParser
{
    private RootNode? ParseRoot(ref TokenLexer lexer, ParserContext context)
    {
        Token token;

        if (lexer.TryPeek(out token) && token.TokenType == TokenType.Vertex)
        {
            lexer.Skip();

            var vertex = ParseVertex(ref lexer, context);

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

        return new RootNode(new VertexNode(nodes));
    }
}
