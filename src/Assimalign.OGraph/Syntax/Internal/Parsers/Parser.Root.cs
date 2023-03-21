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
        var pageNode = context.GetParser<PageParser>()
            .Parse(ref lexer, context, new PageNode());

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
    private RootNode ParseProjections(ref TokenLexer lexer, ParserContext context, RootNode queryNode)
    {
        var nodes = queryNode.Nodes.ToList();
        var projectionNode = (ProjectionNode)context.GetParser<ProjectionParser>()
            .Parse(ref lexer, context, new ProjectionNode());

        // Get Root Projection
        if (queryNode.TryGetProjection(out var projections))
        {
            nodes.Remove(projections);

            if (projectionNode.Identifier is null)
            {
                // TODO: 
            }
            else
            {
                projectionNode = WalkTree(projections, projectionNode);
            }
        }
        if (projectionNode.Identifier is not null)
        {
            // TODO: Duplicate projection
        }

        nodes.Add(projectionNode);

        return new RootNode()
        {
            Nodes = nodes
        };

        ProjectionNode WalkTree(ProjectionNode root, ProjectionNode node, int index = 0)
        {
            var edgeNode = (EdgeNode)node.Identifier;
            var segments = edgeNode.GetSegments();
            var next = root.Edges.FirstOrDefault(x => x.Identifier.Name.Equals(segments[index], StringComparison.InvariantCultureIgnoreCase));

            if (segments.Length == (index + 1))
            {
                var edges = next?.Edges ?? new ProjectionNode[0];

                root = new ProjectionNode()
                {
                    Identifier = root.Identifier,
                    Properties = root.Properties,
                    Edges = root.Edges.Concat(new[] { node })
                };
            }
            else
            {
                var i = index + 1;
                root = new ProjectionNode()
                {
                    Identifier = root.Identifier,
                    Properties = root.Properties,
                    Edges = root.Edges.Where(x => !x.Identifier.Name.Equals(next.Identifier.Name)).Concat(new[] 
                    { 
                        WalkTree(next, node, i) 
                    })
                }; 
            }

            return root;
        }
    }
    private RootNode ParseFilter(ref TokenLexer lexer, ParserContext context, RootNode node)
    {
        var nodes = node.Nodes.ToList();
        var filterNode = context.GetParser<FilterParser>()
            .Parse(ref lexer, context, new FilterNode());

        nodes.Add(filterNode);

        return new RootNode()
        {
            Nodes = nodes
        };
    }
}