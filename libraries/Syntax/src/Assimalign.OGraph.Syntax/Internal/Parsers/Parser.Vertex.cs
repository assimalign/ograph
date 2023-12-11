using System;
using System.Linq;

namespace Assimalign.OGraph.Syntax.Internal;

internal class VertexParser : Parser<VertexNode>
{
    internal override VertexNode Parse(ref TokenLexer lexer, ParserContext context, VertexNode queryNode)
    {
        while (lexer.HasNext)
        {
            var token = lexer.Next();

            switch (token.TokenType)
            {
                case TokenType.Page:
                    queryNode = ParsePage(ref lexer, context, queryNode);
                    break;
                //case TokenType.Filter:
                //    queryNode = ParseFilter(ref lexer, context, queryNode);
                //    break;
                case TokenType.Project:
                    queryNode = ParseProjections(ref lexer, context, queryNode);
                    break;
                //case TokenType.Sort:
                //    queryNode = ParseSort(ref lexer, context, queryNode);
                //    break;
                case TokenType.Edge:
                    queryNode = ParseEdge(ref lexer, context, queryNode);
                    break;
                case TokenType.Dot:
                    continue;
                default:
                    context.AddUnexptedTokenError(ref lexer); // Add Diagnostic information. Unexpected lexerToken
                    break;
            }
        }
        return queryNode;
    }
    private VertexNode ParseEdge(ref TokenLexer lexer, ParserContext context, VertexNode queryNode)
    {
        var nodes = queryNode.Nodes.ToList();
        var edgeParser = context.GetParser<EdgeParser>();
        var edgeNode = edgeParser.Parse(ref lexer, context, new EdgeNode()
        {
            Source = queryNode
        });
        
        nodes.Add(edgeNode);

        return new VertexNode()
        {
            Label = queryNode.Label,
            Nodes = nodes
        };
    }

    private VertexNode ParsePage(ref TokenLexer lexer, ParserContext context, VertexNode queryNode)
    {
        var nodes = queryNode.Nodes.ToList();
        // Check for duplicate nodes
        if (nodes.Any(p => p is PageNode))
        {
            // TODO: Add diagnostics
            return queryNode;
        }

        var pageParser = context.GetParser<PageParser>();
        var pageNode = pageParser.Parse(ref lexer, context, new PageNode());

        nodes.Add(pageNode);

        return new VertexNode()
        {
            Label = queryNode.Label,
            Nodes = nodes
        };
    }
    //private VertexNode ParseSort(ref TokenLexer lexer, ParserContext context, VertexNode queryNode)
    //{
    //    var nodes = queryNode.Nodes.ToList();
    //    var sortNode = (SortNode)context.GetParser<SortParser>()
    //        .Parse(ref lexer, context, new SortNode());

    //    if (queryNode.TryGetSort(out var sortRoot))
    //    {
    //        // Let's remove from root collection to be reformatted
    //        nodes.Remove(sortRoot);

    //        if (sortNode.Identifier is null)
    //        {
    //            // TODO: 
    //        }
    //        else
    //        {
    //            sortNode = FormatEdgeTree(sortRoot, sortNode);
    //        }
    //    }
    //    if (sortNode.Identifier is not null)
    //    {
    //        // TODO: Duplicate or missing Root Projection
    //    }

    //    nodes.Add(sortNode);

    //    return new VertexNode()
    //    {
    //        Nodes = nodes
    //    };

    //    SortNode FormatEdgeTree(SortNode root, SortNode node, int index = 0)
    //    {
    //        var edgeNode = (EdgeNode)node.Identifier;
    //        var segments = edgeNode.GetSegments();

    //        // Check if we've reached the of the tree
    //        if (segments.Length == (index + 1))
    //        {
    //            root = new SortNode()
    //            {
    //                Identifier = root.Identifier,
    //                Direction = root.Direction,
    //                ThenBy = root.ThenBy,
    //                Edges = root.Edges.Concat(new[] { node })
    //            };
    //        }
    //        else
    //        {
    //            var next = root.Edges.FirstOrDefault(x =>
    //                x.Identifier.Name.Equals(segments[index], StringComparison.InvariantCultureIgnoreCase));

    //            index = index + 1;
    //            root = new SortNode()
    //            {
    //                Identifier = root.Identifier,
    //                Direction = root.Direction,
    //                ThenBy = root.ThenBy,
    //                Edges = root.Edges
    //                    .Where(x => !x.Identifier.Name.Equals(next.Identifier.Name))
    //                    .Concat(new[]
    //                    {
    //                        FormatEdgeTree(next, node, index)
    //                    })
    //            };
    //        }

    //        return root;
    //    }
    //}
    private VertexNode ParseProjections(ref TokenLexer lexer, ParserContext context, VertexNode queryNode)
    {
        var nodes = queryNode.Nodes.ToList();
        var projectionsParser = context.GetParser<ProjectionParser>();
        var projectionsNode = projectionsParser.Parse(ref lexer, context, new ProjectionNode());

        nodes.Add(projectionsNode);

        return new VertexNode()
        {
            Label = queryNode.Label,
            Nodes = nodes
        };
    }
    //private VertexNode ParseFilter(ref TokenLexer lexer, ParserContext context, VertexNode queryNode)
    //{
    //    var nodes = queryNode.Nodes.ToList();
    //    var filterNode = (FilterNode)context.GetParser<FilterParser>()
    //        .Parse(ref lexer, context, new FilterNode());

    //    // Get Root Projection
    //    if (queryNode.TryGetFilter(out var filterRoot))
    //    {
    //        // Let's remove from root collection to be reformatted
    //        nodes.Remove(filterRoot);

    //        if (filterNode.Identifier is null)
    //        {
    //            // TODO: 
    //        }
    //        else
    //        {
    //            filterNode = FormatEdgeTree(filterRoot, filterNode);
    //        }
    //    }
    //    if (filterNode.Identifier is not null)
    //    {
    //        // TODO: Duplicate or missing Root Projection
    //    }

    //    nodes.Add(filterNode);

    //    return new VertexNode()
    //    {
    //        Nodes = nodes
    //    };

    //    FilterNode FormatEdgeTree(FilterNode root, FilterNode node, int index = 0)
    //    {
    //        var edgeNode = (EdgeNode)node.Identifier;
    //        var segments = edgeNode.GetSegments();

    //        // Check if we've reached the of the tree
    //        if (segments.Length == (index + 1))
    //        {
    //            root = new FilterNode()
    //            {
    //                Identifier = root.Identifier,
    //                Predicate = root.Predicate,
    //                Edges = root.Edges.Concat(new[] { node })
    //            };
    //        }
    //        else
    //        {
    //            var next = root.Edges.FirstOrDefault(x =>
    //                x.Identifier.Name.Equals(segments[index], StringComparison.InvariantCultureIgnoreCase));

    //            index = index + 1;
    //            root = new FilterNode()
    //            {
    //                Identifier = root.Identifier,
    //                Predicate = root.Predicate,
    //                Edges = root.Edges
    //                    .Where(x => !x.Identifier.Name.Equals(next.Identifier.Name))
    //                    .Concat(new[]
    //                    {
    //                        FormatEdgeTree(next, node, index)
    //                    })
    //            };
    //        }

    //        return root;
    //    }        
    //}
}