namespace Assimalign.OGraph.Syntax.Internal;

internal abstract class Parser
{
    internal abstract QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode);
    internal static Parser Create() => new VertexParser();
}
internal abstract class Parser<TNode> : Parser
    where TNode : QueryNode
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        if (queryNode is not TNode node)
        {
            throw QueryParserException.UnexpectedQueryNode(
                typeof(TNode),
                queryNode.GetType());
        }

        return Parse(ref lexer, context, node);
    }

    internal abstract TNode Parse(ref TokenLexer lexer, ParserContext context, TNode queryNode);
}