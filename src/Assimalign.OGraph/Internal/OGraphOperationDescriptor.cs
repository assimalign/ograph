using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphOperationDescriptor : IOGraphOperationDescriptor
{
    private readonly OGraphOperation operation;

    public OGraphOperationDescriptor(OGraphOperation operation)
    {
        this.operation = operation;
    }

    public IList<Action<OGraph>> OnConfigure { get; init; }


    public IOGraphOperationDescriptor UseMethod(Method method)
    {
        OnConfigure.Add(graph =>
        {
            operation.Method = method;
        });
        return this;
    }

    public IOGraphOperationDescriptor UseMiddleware(IOGraphOperationMiddleware middleware)
    {
        OnConfigure.Add(graph =>
        {
            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }
            operation.Middleware.Enqueue(middleware);
        });        
        return this;
    }

    public IOGraphOperationDescriptor UseMiddleware(OGraphOperationMiddleware middleware)
    {
        OnConfigure.Add(graph =>
        {
            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }
            operation.Middleware.Enqueue(new OGraphOperationMiddlewareDefault(middleware));
        });      

        return this;
    }

    public IOGraphOperationDescriptor UseNode(Label label)
    {
        OnConfigure.Add(graph =>
        {
            if (!graph.Nodes.TryGet(label, out var node))
            {
                throw new Exception();
            }
            operation.Node = node;
        });
       
        return this;
    }

    public IOGraphOperationDescriptor UseQuery(QueryValue query)
    {
        return this;
    }

    public IOGraphOperationDescriptor UseResolver(IOGraphOperationResolver resolver)
    {
        OnConfigure.Add(graph =>
        {
            if (resolver is null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }

            operation.Resolver = resolver;
        });
        

        return this;
    }

    public IOGraphOperationDescriptor UseResolver(OGraphOperationResolver resolver)
    {
        operation.Resolver = new OGraphOperationResolverDefault(resolver);
        return this;
    }


    public IOGraphOperationDescriptor UseRoute(Route route)
    {
        OnConfigure.Add(graph =>
        {
            operation.Route = route;
        });
       
        return this;
    }


    public IOGraphOperationDescriptor UseRequestType(IOGraphType type)
    {
        throw new NotImplementedException();
    }

    public IOGraphOperationDescriptor UseResponseType<TType>() where TType : IOGraphType, new()
    {
        operation.ResponseType = new TType();
        return this;
    }

    public IOGraphOperationDescriptor UseResponseType(IOGraphType type)
    {
        throw new NotImplementedException();
    }

    public IOGraphOperationDescriptor UseNode<TNode>() where TNode : IOGraphNode, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphOperationDescriptor UseRequestType(IOGraphComplexType type)
    {
        throw new NotImplementedException();
    }

    public IOGraphOperationDescriptor UseMiddleware<TMiddleware>() where TMiddleware : IOGraphOperationMiddleware, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphOperationDescriptor UseResolver<TResolver>() where TResolver : IOGraphOperationResolver, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphOperationDescriptor UseRequestType<TType>() where TType : IOGraphComplexType, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphOperationDescriptor UseQueryProvider<TQueryProvider>() where TQueryProvider : IOGraphQueryProvider, new()
    {
        operation.QueryProvider = new TQueryProvider();
        return this;
    }

    public IOGraphOperationDescriptor UseQueryProvider(IOGraphQueryProvider queryProvider)
    {
        if (queryProvider is null)
        {
            throw new ArgumentNullException(nameof(queryProvider));
        }

        operation.QueryProvider = queryProvider;
        return this;
    }
}
