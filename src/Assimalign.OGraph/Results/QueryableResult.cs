
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Syntax;

// TODO: I need to bridge operation results and graph types

internal class QueryableResult : IOGraphOperationResult, IOGraphEdgeResult
{
    public IQueryable Queryable { get; init; }


    public Task ExecuteAsync(IOGraphEdgeContext context, CancellationToken cancellationToken = default)
    {

  
        throw new NotImplementedException();
    }


    public async Task ExecuteAsync(IOGraphOperationContext context, CancellationToken cancellationToken = default)
    {
        var graphNode           = context.GetOGraphNode();
        var graphProjections    = context.GetOGraphProjections();
        var graphFiltering      = context.GetOGraphFiltering();

        var graphType = graphNode.Type;

        if (graphType is not IOGraphComplexType complexType)
        {
            throw new Exception();
        }

        var propertyContext = default(IOGraphPropertyContext);

        foreach (var projectionProperty in graphProjections.Properties)
        {
            if (!complexType.Properties.TryGet(projectionProperty.Name, out var property))
            {
                throw new Exception();
            }

            var result = await property.GetResolverChain().Invoke(propertyContext);

            if (property.Is)
        }

        if (graphProjections.HasEdges)
        {
            foreach (var edgeProjection in graphProjections.Edges)
            {
                var edgeName = edgeProjection.GetEdgeName();

                if (!graphNode.Edges.TryGet(edgeName.Value, out var edge))
                {
                    throw new Exception();
                }

                var edgeResult = await edge.Resolver.InvokeAsync(default);

                edgeResult.
            }
        }

        throw new NotImplementedException();
    }

    private void ApplyFiltering(FilterNode filter)
    {

    }
    private void ApplySorting(SortNode sort)
    {

    }
    private void ApplyPaging(PageNode page)
    {

    }
}
