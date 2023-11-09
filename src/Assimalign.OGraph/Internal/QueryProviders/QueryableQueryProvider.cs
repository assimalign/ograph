
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal.QueryProviders;


using Assimalign.OGraph.Gdm;
using Assimalign.OGraph.Syntax;


internal class QueryableQueryProvider : IOGraphQueryProvider
{
    private readonly IQueryable queryable;

    public QueryableQueryProvider(IQueryable queryable)
    {
        this.queryable = queryable;
    }


    public Type ElementType => throw new NotImplementedException();

    public async Task ExecuteAsync(IOGraphQueryContext context, OGraphQueryOptions options, CancellationToken cancellationToken = default)
    {
        var query = context.Query;
        var vertex = context.Vertex;

        if (query.Root is not VertexNode vertexNode)
        {
            throw new Exception();
        }

        var projectionNode = vertexNode.Nodes.OfType<ProjectionNode>().First();

        var projectionType = ApplyProjections(projectionNode, vertex);
        var writer = new Utf8JsonWriter(context.Stream, new()
        {
            SkipValidation = true,
        });



     
        throw new NotImplementedException();
    }



    private IOGraphGdmCollectionType ApplyProjections(ProjectionNode node, IOGraphGdmVertex vertex)
    {
        var collectionType = new GdmCollectionType<GdmComplexType>();
        var complexType = new GdmComplexType();

        foreach (var propertyNode in node.Properties)
        {
            if (vertex.TryGetProperty(propertyNode.Name!, out var property))
            {
                var binding = property!.GetBindings().OfType<IOGraphPropertyBinding>()
                    .FirstOrDefault();

                
                var tasks = new Task<>


                if (propertyNode.HasAlias)
                {
                    property.Gett
                }
            }
        }

        return collectionType;
    }
}