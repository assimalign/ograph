
using System;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assimalign.OGraph.Internal;

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

    public async Task ExecuteAsync(IOGraphQueryProviderContext context, OGraphQueryOptions options, CancellationToken cancellationToken = default)
    {
        //var query = context.Query;
        //var vertex = context.Vertex;

        //if (query.Root is not VertexNode vertexNode)
        //{
        //    throw new Exception();
        //}

        //foreach (var edgeNode in vertexNode.Nodes.OfType<EdgeNode>())
        //{
        //    var label = edgeNode.Label.Name!;
        //    var edge = vertex.Edges
        //        .Where(p => p.Definition is IOGraphGdmEdge e && e.Label == label)
        //        .Select(p => p.Definition)
        //        .First();

        //    var edgeBinding = edge.Bindings.OfType<IOGraphOperationBinding>().First();
        //}

        //var projectionNode = vertexNode.Nodes.OfType<ProjectionNode>().First();

        //var projectionType = ApplyProjections(projectionNode, vertex);
        //var writer = new Utf8JsonWriter(context.Stream, new()
        //{
        //    SkipValidation = true,
        //});

        //var items = new List<object>();
        //var tasks = new List<Task>();

        //writer.WriteStartObject();


        //writer.WritePropertyName("data");
        //writer.WriteStartArray();

        //int index = 0;

        //var propertyContext = new PropertyBindingContext()
        //{

        //};

        //foreach (var item in queryable)
        //{
        //    propertyContext.Parent = item;

        //    await foreach (var property in GetPropertiesAsync(propertyContext, options, out IOGraphError? error))
        //    {
        //        if (error is not null)
        //        {

        //        }
        //    }
        //}

        //writer.WriteEndArray();
    }



    private IAsyncEnumerable<IOGraphGdmProperty> GetPropertiesAsync(
        IOGraphPropertyBindingContext context, 
        OGraphQueryOptions options,
        out IOGraphError? error)
    {
        error = null;
        //var query = context.Query;
        //var vertex = context.Vertex;

        //if (query.Root is not VertexNode vertexNode)
        //{
        //    throw new Exception();
        //}



        throw new NotImplementedException();
    }



    private IEnumerable<IOGraphGdmProperty> GetProperties(IOGraphGdmNode vertex, VertexNode node)
    {
        foreach (var propertyNode in node.Nodes.OfType<ProjectNode>().First().Properties)
        {
            var propertyName = propertyNode.Name!;

            if (!vertex.TryGetProperty(propertyName, out var property))
            {
                throw new Exception();
            }
            if (propertyNode.HasChildren)
            {

            }
            else
            {

            }
        }

        // Let's asynchronously complete tasks as they finish
        //while (tasks.Any())
        //{
        //    var task = await Task.WhenAny(tasks);

        //    tasks.Remove(task);


        //}
        throw new NotImplementedException();
    }


    private IEnumerable<IOGraphGdmEdge> GetEdgeBindings(IOGraphGdmNode vertex, VertexNode node)
    {
        foreach (var edgeNode in node.Nodes.OfType<EdgeNode>())
        {
            var label = edgeNode.Label.Name!;
            yield return vertex.Edges
                .Where(p => p.Definition is IOGraphGdmEdge e && e.Label == label)
                .Select(p => p.Definition)
                .First();
        }
    }

    private IOGraphPropertyBinding GetBinding(IOGraphGdmProperty property)
    {
        throw new NotImplementedException();
    }





    protected void ApplyFiltering(OGraphQueryOptions options)
    {
        if (!options.CanFilter)
        {
            throw new Exception();
        }

    }

    protected void ApplySorting(OGraphQueryOptions options)
    {
        if (!options.CanSort)
        {
            throw new Exception();
        }
    }

    protected void ApplyPaging(OGraphQueryOptions options)
    {
        if (!options.CanPage)
        {
            throw new Exception();
        }
    }





    private IOGraphGdmCollectionType ApplyProjections(ProjectNode node, IOGraphGdmNode vertex)
    {
        var collectionType = new GdmListType<GdmComplexType>();
        var complexType = new GdmComplexType();

        foreach (var propertyNode in node.Properties)
        {
            if (vertex.TryGetProperty(propertyNode.Name!, out var property))
            {
               // var bindings = property!.GetBindings().OfType<IOGraphPropertyBinding>();
                //var binding = bindings.First();


            }
        }

        return collectionType;
    }

    Task<IOGraphQueryResult> IOGraphQueryProvider.ExecuteAsync(IOGraphQueryProviderContext context, OGraphQueryOptions options, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}