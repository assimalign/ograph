using System;
using System.Linq;

namespace Assimalign.OGraph.Syntax.Internal;

internal class VertexParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        if (queryNode is not VertexNode root)
        {
            // This should never happen, but here for safety.
            throw QueryParserException.UnexpectedQueryNode(
                typeof(VertexNode),
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
                case TokenType.Edge:

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

    private VertexNode ParsePage(ref TokenLexer lexer, ParserContext context, VertexNode queryNode)
    {
        var nodes = queryNode.Nodes.ToList();
        var pageNode = (PageNode)context.GetParser<PageParser>()
            .Parse(ref lexer, context, new PageNode());

        nodes.Add(pageNode);

        return new VertexNode()
        {
            Nodes = nodes
        };
    }
    private VertexNode ParseSort(ref TokenLexer lexer, ParserContext context, VertexNode queryNode)
    {
        var nodes = queryNode.Nodes.ToList();
        var sortNode = (SortNode)context.GetParser<SortParser>()
            .Parse(ref lexer, context, new SortNode());

        if (queryNode.TryGetSort(out var sortRoot))
        {
            // Let's remove from root collection to be reformatted
            nodes.Remove(sortRoot);

            if (sortNode.Identifier is null)
            {
                // TODO: 
            }
            else
            {
                sortNode = FormatEdgeTree(sortRoot, sortNode);
            }
        }
        if (sortNode.Identifier is not null)
        {
            // TODO: Duplicate or missing Root Projection
        }

        nodes.Add(sortNode);

        return new VertexNode()
        {
            Nodes = nodes
        };

        SortNode FormatEdgeTree(SortNode root, SortNode node, int index = 0)
        {
            var edgeNode = (EdgeNode)node.Identifier;
            var segments = edgeNode.GetSegments();

            // Check if we've reached the of the tree
            if (segments.Length == (index + 1))
            {
                root = new SortNode()
                {
                    Identifier = root.Identifier,
                    Direction = root.Direction,
                    ThenBy = root.ThenBy,
                    Edges = root.Edges.Concat(new[] { node })
                };
            }
            else
            {
                var next = root.Edges.FirstOrDefault(x =>
                    x.Identifier.Name.Equals(segments[index], StringComparison.InvariantCultureIgnoreCase));

                index = index + 1;
                root = new SortNode()
                {
                    Identifier = root.Identifier,
                    Direction = root.Direction,
                    ThenBy = root.ThenBy,
                    Edges = root.Edges
                        .Where(x => !x.Identifier.Name.Equals(next.Identifier.Name))
                        .Concat(new[]
                        {
                            FormatEdgeTree(next, node, index)
                        })
                };
            }

            return root;
        }
    }
    private VertexNode ParseProjections(ref TokenLexer lexer, ParserContext context, VertexNode queryNode)
    {
        var nodes = queryNode.Nodes.ToList();
        var projectionNode = (ProjectionNode)context.GetParser<ProjectionParser>()
            .Parse(ref lexer, context, new ProjectionNode());

        // Get Root Projection
        if (queryNode.TryGetProjection(out var projections))
        {
            // Let's remove from root collection to be reformatted
            nodes.Remove(projections);

            if (projectionNode.Identifier is null)
            {
                // TODO: 
            }
            else
            {
                projectionNode = FormatEdgeTree(projections, projectionNode);
            }
        }
        if (projectionNode.Identifier is not null)
        {
            // TODO: Duplicate or missing Root Projection
        }

        nodes.Add(projectionNode);

        return new VertexNode()
        {
            Nodes = nodes
        };

        ProjectionNode FormatEdgeTree(ProjectionNode root, ProjectionNode node, int index = 0)
        {
            var edgeNode = (EdgeNode)node.Identifier;
            var segments = edgeNode.GetSegments();
            
            // Check if we've reached the of the tree
            if (segments.Length == (index + 1))
            {
                root = new ProjectionNode()
                {
                    Identifier = root.Identifier,
                    Properties = root.Properties,
                    Edges = root.Edges.Concat(new[] { node })
                };
            }
            else
            {
                var next = root.Edges.FirstOrDefault(x => 
                    x.Identifier.Name.Equals(segments[index], StringComparison.InvariantCultureIgnoreCase));

                index = index + 1;
                root = new ProjectionNode()
                {
                    Identifier = root.Identifier,
                    Properties = root.Properties,
                    Edges = root.Edges
                        .Where(x => !x.Identifier.Name.Equals(next.Identifier.Name))
                        .Concat(new[] 
                        { 
                            FormatEdgeTree(next, node, index) 
                        })
                }; 
            }

            return root;
        }
    }
    private VertexNode ParseFilter(ref TokenLexer lexer, ParserContext context, VertexNode queryNode)
    {
        var nodes = queryNode.Nodes.ToList();
        var filterNode = (FilterNode)context.GetParser<FilterParser>()
            .Parse(ref lexer, context, new FilterNode());

        // Get Root Projection
        if (queryNode.TryGetFilter(out var filterRoot))
        {
            // Let's remove from root collection to be reformatted
            nodes.Remove(filterRoot);

            if (filterNode.Identifier is null)
            {
                // TODO: 
            }
            else
            {
                filterNode = FormatEdgeTree(filterRoot, filterNode);
            }
        }
        if (filterNode.Identifier is not null)
        {
            // TODO: Duplicate or missing Root Projection
        }

        nodes.Add(filterNode);

        return new VertexNode()
        {
            Nodes = nodes
        };

        FilterNode FormatEdgeTree(FilterNode root, FilterNode node, int index = 0)
        {
            var edgeNode = (EdgeNode)node.Identifier;
            var segments = edgeNode.GetSegments();

            // Check if we've reached the of the tree
            if (segments.Length == (index + 1))
            {
                root = new FilterNode()
                {
                    Identifier = root.Identifier,
                    Predicate = root.Predicate,
                    Edges = root.Edges.Concat(new[] { node })
                };
            }
            else
            {
                var next = root.Edges.FirstOrDefault(x =>
                    x.Identifier.Name.Equals(segments[index], StringComparison.InvariantCultureIgnoreCase));

                index = index + 1;
                root = new FilterNode()
                {
                    Identifier = root.Identifier,
                    Predicate = root.Predicate,
                    Edges = root.Edges
                        .Where(x => !x.Identifier.Name.Equals(next.Identifier.Name))
                        .Concat(new[]
                        {
                            FormatEdgeTree(next, node, index)
                        })
                };
            }

            return root;
        }        
    }
}