namespace Assimalign.OGraph.Syntax.Internal;

internal abstract class Parser
{
    internal abstract QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode node);
}
