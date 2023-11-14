using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal static class ParserExtensions
{

    internal static TQueryNode Parse<TQueryNode>(this Parser parser, ref TokenLexer lexer, ParserContext context)
        where TQueryNode : QueryNode, new()
    {
        var result = parser.Parse(ref lexer, context, new TQueryNode());

        if (result is not TQueryNode queryNode)
        {
            throw QueryParserException.UnexpectedQueryNode(typeof(TQueryNode), result.GetType());
        }

        return queryNode;
    }
}
