using System;
using System.Linq;

namespace Assimalign.OGraph.Syntax.Internal;

internal class RootParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        if (queryNode is not RootNode root)
        {
            // This should never happen, but here for safety.
            throw QueryParserException.UnexpectedQueryNode(
                typeof(RootNode),
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

    private RootNode ParsePage(ref TokenLexer lexer, ParserContext context, RootNode queryNode)
    {
        var nodes = queryNode.Nodes.ToList();
        var pageParser = context.GetParser<PageParser>();
        var pageNode = pageParser.Parse<PageNode>(ref lexer, context);

        nodes.Add(pageNode);

        return new RootNode()
        {
            Nodes = nodes
        };
    }
    private RootNode ParseSort(ref TokenLexer lexer, ParserContext context, RootNode node)
    {
        var nodes = node.Nodes.ToList();
        var sortParser = context.GetParser<SortParser>();
        var sortNode = sortParser.Parse<SortNode>(ref lexer, context);

        nodes.Add(sortNode);

        return new RootNode()
        {
            Nodes = nodes
        };
    }
    private RootNode ParseProjections(ref TokenLexer lexer, ParserContext context, RootNode node)
    {
        var nodes = node.Nodes.ToList();
        var projectionParser = context.GetParser<ProjectionParser>();
        var projectionNode = projectionParser.Parse<ProjectionNode>(ref lexer, context);

        nodes.Add(projectionNode);

        return new RootNode()
        {
            Nodes = nodes
        };
    }
    private RootNode ParseFilter(ref TokenLexer lexer, ParserContext context, RootNode node)
    {
        var nodes = node.Nodes.ToList();
        var filterParser = context.GetParser<FilterParser>();
        var filterNode = filterParser.Parse<FilterNode>(ref lexer, context);

        nodes.Add(filterNode);

        return new RootNode()
        {
            Nodes = nodes
        };
    }
}