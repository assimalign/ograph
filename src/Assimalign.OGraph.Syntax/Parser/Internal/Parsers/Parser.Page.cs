using System;
using System.Linq;

namespace Assimalign.OGraph.Syntax.Internal;

using Assimalign.OGraph.Syntax.Lexer;
internal class PageBlockParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {
        if (node is not RootQueryNode root)
        {
            throw QueryParserException.UnexpectedNode();
        }

        var token = lexer.Next();

        if (context.Parse(ref lexer, new PageQueryNode()) is not PageQueryNode pageNode)
        {
            throw QueryParserException.InvalidPage();
        }
        if (root.Nodes.Any(node => node is PageQueryNode))
        {
            // TODO: Add duplicate node diagnostics error 
        }

        root.AddNode(pageNode);

        return root;
    }
}
