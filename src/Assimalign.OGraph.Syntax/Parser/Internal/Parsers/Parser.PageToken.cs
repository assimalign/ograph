using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

using Assimalign.OGraph.Syntax.Lexer;

internal class PageTokenParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {
        if (node is not PageQueryNode page)
        {
            throw QueryParserException.UnexpectedNode();
        }

        var token = lexer.Next();

        if (context.Parse(ref lexer, default) is not ConstantQueryNode constant)
        {
            throw QueryParserException.InvalidPage();
        }

        page.SetToken(constant);

        return page;
    }
}
