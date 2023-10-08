using System;
using System.Collections.Generic;
using System.Linq;

namespace Assimalign.OGraph.Internal;

internal class OGraphOperationDescriptor : IOGraphOperationDescriptor
{
    private readonly OGraphOperation operation;

    public OGraphOperationDescriptor(OGraphOperation operation)
    {
        if (operation is null)
        {
            throw new ArgumentNullException(nameof(operation));
        }
        this.operation = operation;
    }

    public IList<Action<OGraph>> OnConfigure { get; init; }

    public IOGraphOperationDescriptor UseName(Name name)
    {
        OnConfigure.Add(graph =>
        {
            operation.name = name;
        });
        return this;
    }
    public IOGraphOperationDescriptor UseQueryParam(string query)
    {
        OnConfigure.Add(graph =>
        {
            
        });
        return this;
    }
    public IOGraphOperationDescriptor UseRoute(Route route)
    {
        OnConfigure.Add(graph =>
        {
            operation.route = route;
        });
        return this;
    }
    public IOGraphOperationDescriptor UseMethod(Method method)
    {
        OnConfigure.Add(graph =>
        {
            operation.method = method;
        });
        return this;
    }
    public IOGraphOperationDescriptor UseNode(Name label)
    {
        OnConfigure.Add(graph =>
        {
            if (!graph.Nodes.TryGet(label, out var node) || node is null)
            {
                throw new Exception();
            }
            operation.node = node;
        });
        return this;
    }
    public IOGraphOperationDescriptor UseNode<TNode>() where TNode : IOGraphNode, new()
    {
        OnConfigure.Add(graph =>
        {
            var node = graph.Nodes.FirstOrDefault(x => x is TNode);

            if (node is null)
            {
                node = new TNode();
                graph.Nodes.Add(node);
            }

            operation.node = node;
        });
        return this;
    }
    public IOGraphOperationDescriptor UseMiddleware<TMiddleware>()
       where TMiddleware : IOGraphOperationMiddleware, new()
    {
        OnConfigure.Add(graph =>
        {
            operation.Middleware.Enqueue(new TMiddleware());
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
    public IOGraphOperationDescriptor UseResolver<TResolver>()
        where TResolver : IOGraphOperationResolver, new()
    {
        OnConfigure.Add(graph =>
        {
            operation.resolver = new TResolver();
        });
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

            operation.resolver = resolver;
        });
        return this;
    }
    public IOGraphOperationDescriptor UseResolver(OGraphOperationResolver resolver)
    {
        OnConfigure.Add(graph =>
        {
            if (resolver is null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }

            operation.resolver = new OGraphOperationResolverDefault(resolver);
        });
        return this;
    }
    public IOGraphOperationDescriptor UseQueryProvider<TQueryProvider>()
        where TQueryProvider : IOGraphQueryProvider, new()
    {
        OnConfigure.Add(graph =>
        {
            operation.queryProvider = new TQueryProvider();
        });
        return this;
    }
    public IOGraphOperationDescriptor UseQueryProvider(IOGraphQueryProvider queryProvider)
    {
        OnConfigure.Add(graph =>
        {
            if (queryProvider is null)
            {
                throw new ArgumentNullException(nameof(queryProvider));
            }
            operation.queryProvider = queryProvider;
        });
        return this;
    }
    public IOGraphOperationDescriptor UseQueryOptions(OGraphQueryOptions options)
    {
        OnConfigure.Add(graph =>
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            operation.queryOptions = options;
        });
        return this;
    }
    public IOGraphOperationDescriptor UseQueryOptions(Action<OGraphQueryOptions> configure)
    {
        OnConfigure.Add(graph =>
        {
            if (configure is null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var options = new OGraphQueryOptionsDefault();

            configure.Invoke(options);

            operation.queryOptions = options;

        });
        return this;
    }
    public IOGraphOperationDescriptor UseQueryOptions<TQueryOptions>(Action<TQueryOptions> configure)
        where TQueryOptions : OGraphQueryOptions, new()
    {
        OnConfigure.Add(graph =>
        {
            if (configure is null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var options = new TQueryOptions();
            configure.Invoke(options);
            operation.queryOptions = options;
        });
        return this;
    }
}