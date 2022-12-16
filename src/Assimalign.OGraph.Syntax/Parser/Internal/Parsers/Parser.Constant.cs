using System;

namespace Assimalign.OGraph.Syntax.Internal;

using Assimalign.OGraph.Syntax.Lexer;

internal class ConstantParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {
        var token = lexer.Current;
        var constantNode = new ConstantQueryNode();

        if (token.TokenType == TokenType.String)
        {
            if (DateOnly.TryParse(token.GetString(), out var dateTime))
            {

            }
        }

        if (token.TokenType == TokenType.Integer)
        {
            var value = token.GetLong();

            constantNode.SetValue(value);
        }


        return constantNode;
    }
}
