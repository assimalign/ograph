namespace Assimalign.OGraph.Syntax.Internal;

internal abstract class Parser<TNode> : Parser
    where TNode : QueryNode
{
   // internal abstract TNode? Parse(ref TokenLexer lexer, ParserContext context);

    //internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context)
    //{
    //    return Parse(ref lexer, context);
    //}
    //internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context)
    //{
    //    if (queryNode is not TNode node)
    //    {
    //        throw QueryParserException.UnexpectedQueryNode(
    //            typeof(TNode),
    //            queryNode.GetType());
    //    }

    //    return Parse(ref lexer, context, node);
    //}
    //internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    //{
    //    if (queryNode is not TNode node)
    //    {
    //        throw QueryParserException.UnexpectedQueryNode(
    //            typeof(TNode),
    //            queryNode.GetType());
    //    }

    //    return Parse(ref lexer, context, node);
    //}

    //internal abstract TNode Parse(ref TokenLexer lexer, ParserContext context, TNode queryNode);
}
