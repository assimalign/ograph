using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Internal;

internal class OGraphEdgeDescriptor : IOGraphEdgeDescriptor
{
    private readonly OGraphEdge edge;

    public OGraphEdgeDescriptor(OGraphEdge edge)
    {
        if (edge is null)
        {
            throw new ArgumentNullException(nameof(edge));
        }
        this.edge = edge;
    }

    public IList<Action<OGraph>> OnConfigure { get; init; }

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
        edge.Metadata[key] = value;

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
        edge.Middleware.Enqueue(new OGraphEdgeMiddlwareDefault(middleware));
        return this;
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
    public IOGraphEdgeDescriptor UseSourceNode<TNode>() where TNode : IOGraphNode, new()
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
    public IOGraphEdgeDescriptor UseTargetNode<TNode>() where TNode : IOGraphNode, new()
    {
        edge.Target = new TNode();
        return this;
    }
}
