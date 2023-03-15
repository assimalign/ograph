namespace Assimalign.OGraph.Syntax.Internal;

internal class ConstantParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {
        var token = lexer.Current;

        return new ConstantNode()
        {
            Value = token.Value.ToArray()
        };
    }
}
