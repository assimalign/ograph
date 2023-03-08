using System;
using System.Linq;

namespace Assimalign.OGraph.Syntax.Internal;

internal class RootParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        if (queryNode is not RootQueryNode root)
        {
            // This should never happen, but here for safety.
            throw QueryParserException.UnexpectedQueryNode(
                typeof(RootQueryNode),
                queryNode.GetType());
        }
        while (lexer.HasNext)
        {
            var token = lexer.Next();
            
            switch (token.TokenType)
            {
                case TokenType.Page:
                    root = ParsePage(ref lexer, context, root);
                    break;
                case TokenType.Filter:
                    root = ParseFilter(ref lexer, context, root);
                    break;
                case TokenType.Project:
                    root = ParseProjections(ref lexer, context, root);
                    break;
                case TokenType.Sort:
                    root = ParseSort(ref lexer, context, root);
                    break;
                case TokenType.Dot:
                    continue;
                default:
                    context.AddUnexptedTokenError(ref lexer); // Add Diagnostic information. Unexpected lexerToken
                    break;
            }
        }
        return root;
    }

    private RootQueryNode ParsePage(ref TokenLexer lexer, ParserContext context, RootQueryNode queryNode)
    {
        var nodes = queryNode.Nodes.ToList();
        var pageParser = context.GetParser<PageParser>();
        var pageNode = pageParser.Parse<PageQueryNode>(ref lexer, context);

        nodes.Add(pageNode);

        return new RootQueryNode()
        {
            Nodes = nodes
        };
    }
    private RootQueryNode ParseSort(ref TokenLexer lexer, ParserContext context, RootQueryNode node)
    {
        var nodes = node.Nodes.ToList();
        var sortParser = context.GetParser<SortParser>();
        var sortNode = sortParser.Parse<SortQueryNode>(ref lexer, context);

        nodes.Add(sortNode);

        return new RootQueryNode()
        {
            Nodes = nodes
        };
    }
    private RootQueryNode ParseProjections(ref TokenLexer lexer, ParserContext context, RootQueryNode node)
    {
        var nodes = node.Nodes.ToList();
        var projectionParser = context.GetParser<ProjectionParser>();
        var projectionNode = projectionParser.Parse<ProjectionQueryNode>(ref lexer, context);

        nodes.Add(projectionNode);

        return new RootQueryNode()
        {
            Nodes = nodes
        };
    }
    private RootQueryNode ParseFilter(ref TokenLexer lexer, ParserContext context, RootQueryNode node)
    {
        var nodes = node.Nodes.ToList();
        var filterParser = context.GetParser<FilterParser>();
        var filterNode = filterParser.Parse<FilterQueryNode>(ref lexer, context);

        nodes.Add(filterNode);

        return new RootQueryNode()
        {
            Nodes = nodes
        };
    }
}