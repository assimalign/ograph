using System;

namespace Assimalign.OGraph.Internal;

internal class OGraphEdgeDescriptor : IOGraphEdgeDescriptor
{
    private readonly OGraphEdge edge;

    public OGraphEdgeDescriptor(OGraphEdge edge)
    {
        this.edge = edge;
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
        throw new NotImplementedException();
    }

    public IOGraphEdgeDescriptor UseSourceNode(Label label)
    {
        throw new NotImplementedException();
    }

    public IOGraphEdgeDescriptor UseSourceNode<TNode>() where TNode : IOGraphNode, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphEdgeDescriptor UseTargetNode(Label label)
    {
        throw new NotImplementedException();
    }

    public IOGraphEdgeDescriptor UseTargetNode<TNode>() where TNode : IOGraphNode, new()
    {
        throw new NotImplementedException();
    }
}
