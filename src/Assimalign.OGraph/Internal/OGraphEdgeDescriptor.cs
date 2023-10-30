using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Internal;

internal class OGraphEdgeDescriptor : IOGraphEdgeDescriptor
{
    private readonly OGraphEdgeDefault edge;

    public OGraphEdgeDescriptor(OGraphEdgeDefault edge)
    {
        if (edge is null)
        {
            throw new ArgumentNullException(nameof(edge));
        }
        this.edge = edge;
    }

    public IList<Action<Graph>> OnConfigure { get; init; }

    public IOGraphEdgeDescriptor UseMetadata(string key, object value)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
       // edge.Metadata[key] = value;

        return this;
    }
    public IOGraphEdgeDescriptor UseMiddleware<TMiddleware>() where TMiddleware : IOGraphEdgeMiddleware, new()
    {
        edge.Middleware.Enqueue(new TMiddleware());
        return this;
    }
    public IOGraphEdgeDescriptor UseMiddleware(IOGraphEdgeMiddleware middleware)
    {
        if (middleware is null)
        {
            throw new ArgumentNullException(nameof(middleware));
        }
        edge.Middleware.Enqueue(middleware);
        return this;
    }
    public IOGraphEdgeDescriptor UseMiddleware(OGraphEdgeMiddleware middleware)
    {
        if (middleware is null)
        {
            throw new AccessViolationException(nameof(middleware));
        }
        edge.Middleware.Enqueue(new OGraphEdgeMiddlewareDefault(middleware));
        return this;
    }

    public IOGraphEdgeDescriptor UseQueryOptions(OGraphQueryOptions options)
    {
        throw new NotImplementedException();
    }

    public IOGraphEdgeDescriptor UseQueryOptions<TQueryOptions>(Action<TQueryOptions> configure) where TQueryOptions : OGraphQueryOptions, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphEdgeDescriptor UseQueryOptions(Action<OGraphQueryOptions> configure)
    {
        throw new NotImplementedException();
    }

    public IOGraphEdgeDescriptor UseQueryProvider<TQueryProvider>() where TQueryProvider : IOGraphQueryProvider, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphEdgeDescriptor UseQueryProvider(IOGraphQueryProvider queryProvider)
    {
        throw new NotImplementedException();
    }

    public IOGraphEdgeDescriptor UseResolver<TResovler>() where TResovler : IOGraphEdgeResolver, new()
    {
        edge.Resolver = new TResovler();
        return this;
    }
    public IOGraphEdgeDescriptor UseResolver(IOGraphEdgeResolver resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }
        edge.Resolver = resolver;
        return this;
    }
    public IOGraphEdgeDescriptor UseResolver(OGraphEdgeResolver resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }
        edge.Resolver = new OGraphEdgeResolverDefault(resolver);
        return this;
    }
    public IOGraphEdgeDescriptor UseSourceNode(Label label)
    {
        OnConfigure.Add(graph =>
        {
            if (!graph.Nodes.TryGet(label, out var node))
            {
                throw new Exception();
            }
            edge.Source = node;
        });
        return this;
    }
    public IOGraphEdgeDescriptor UseSourceNode<TNode>() where TNode : IOGraphVertex, new()
    {
        edge.Source = new TNode();
        return this;
    }
    public IOGraphEdgeDescriptor UseTargetNode(Label label)
    {
        OnConfigure.Add(graph =>
        {
            if (!graph.Nodes.TryGet(label, out var node))
            {
                throw new Exception();
            }
            edge.Target = node;
        });
        return this;
    }
    public IOGraphEdgeDescriptor UseTarget<TNode>() where TNode : IOGraphVertex, new()
    {
        edge.Target = new TNode();
        return this;
    }
}
