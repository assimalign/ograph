using System;

namespace Assimalign.OGraph.Syntax.Internal;

using Assimalign.OGraph.Syntax.Lexer;

internal class ConstantParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {
        var token = lexer.Current;

        return new ConstantQueryNode()
        {
            Value = token.Value,
        };
    }
}
