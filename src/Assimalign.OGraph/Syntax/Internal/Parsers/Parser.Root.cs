using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal class RootParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryToken)
    {
        if (queryToken is not RootQueryNode root)
        {
            // TODO: Add diagnostic information 
            return queryToken;
        }
        while (lexer.HasNext)
        {
            var lexerToken = lexer.Next();

            switch (lexerToken.TokenType)
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
                    {
                        // Add Diagnostic information. Unexpected lexerToken
                        context.AddUnexptedTokenDiagnosticError(ref lexerToken);
                        break;
                    }
            }
        }
        return root;
    }
    
    private RootQueryNode ParsePage(ref TokenLexer lexer, ParserContext context, RootQueryNode node)
    {
        var nodes = node.Nodes.ToList();
        var pageParser = context.GetParser<PageParser>();
        var pageNode = pageParser.Parse(ref lexer, context, new PageQueryNode());

        if (pageNode is not PageQueryNode)
        {
            // TODO: Add diagnositic information
        }

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
        var sortNode = sortParser.Parse(ref lexer, context, new FilterQueryNode());

        if (sortNode is not ProjectionQueryNode)
        {
            // TODO: Add diagnositic information
        }

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
        var projectionNode = projectionParser.Parse(ref lexer, context, new ProjectionQueryNode());

        if (projectionNode is not ProjectionQueryNode)
        {
            // TODO: Add diagnositic information
        }

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
        var filterNode = filterParser.Parse(ref lexer, context, new FilterQueryNode());

        if (filterNode is not ProjectionQueryNode)
        {
            // TODO: Add diagnositic information
        }

        nodes.Add(filterNode);

        return new RootQueryNode()
        {
            Nodes = nodes
        };
    }
}
