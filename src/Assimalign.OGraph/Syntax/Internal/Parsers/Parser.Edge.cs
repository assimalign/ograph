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
            throw QueryParserException.UnexpectedQueryNode(
                typeof(EdgeQueryNode),
                queryNode.GetType());
        }

        var edgePath = string.Empty;

        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.Comma)
            {
                break;
            }
            if (token.TokenType != TokenType.Identifier)
            {
                // TODO: Expected Identifier
            }
            else
            {
                edgePath = string.IsNullOrEmpty(edgePath) ?
                    token.Text : 
                    string.Join('/', edgePath, token.Text);
            }
            if (!lexer.TryPeek(out var next))
            {
                context.AddUnexpectedEOFError(ref lexer);
            }
            if (next.TokenType != TokenType.Slash && next.TokenType != TokenType.Comma)
            {
                context.AddExpectedCommaSeparatorDiagnosticError(ref next);   
            }
        }

        return edgeNode = new EdgeQueryNode()
        {
            Path = edgePath,
        };
    }
}
