using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal class EdgeParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        if (queryNode is not EdgeQueryNode edgeNode)
        {
            return queryNode;
        }
        var edgePath = string.Empty;

        while (lexer.HasNext)
        {
            var lexerToken = lexer.Next();

            if (lexerToken.TokenType == TokenType.Comma)
            {
                break;
            }
            if (lexerToken.TokenType != TokenType.Identifier)
            {
                // TODO: Expected Identifier
            }
            else
            {
                edgePath = string.IsNullOrEmpty(edgePath) ? lexerToken.Text : string.Join('/', edgePath, lexerToken.Text);
            }
            if (!lexer.TryPeek(out var next))
            {
                context.AddUnexptedTokenDiagnosticError(ref lexerToken);
            }
            if (next.TokenType != TokenType.Slash)
            {
                // TODO: Expected Slash or comma
            }
        }

        return new EdgeQueryNode()
        {
            Path = edgePath,
        };
    }
}
