namespace Assimalign.OGraph.Syntax.Internal;

using Assimalign.OGraph.Syntax.Lexer;

internal abstract class Parser
{
    internal abstract QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode node);

}
