using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Gdm;

internal class OperationBindingDescriptor : IOGraphOperationBindingDescriptor
{
    private readonly OperationBinding binding;

    public OperationBindingDescriptor(OperationBinding binding)
    {
        this.binding = binding;
    }

    public OGraphExecutorOptions Options { get; init; }

    public IOGraphOperationBindingDescriptor UseMiddleware<TMiddleware>()
        where TMiddleware : IOGraphOperationBindingMiddleware, new()
    {
        return UseMiddleware(new TMiddleware());
    }
    public IOGraphOperationBindingDescriptor UseMiddleware(OGraphOperationBindingMiddleware middleware)
    {
        if (middleware is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(middleware));
        }
        return UseMiddleware(new OperationBindingMiddlewareWrapper(middleware));
    }

    public IOGraphOperationBindingDescriptor UseMiddleware(IOGraphOperationBindingMiddleware middleware)
    {
        if (middleware is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(middleware));
        }
        binding.Middleware.Enqueue(middleware);
        return this;
    }

    public IOGraphOperationBindingDescriptor UseRequestType<TGdmType>() where TGdmType : IOGraphGdmType
    {
        throw new NotImplementedException();
    }

    public IOGraphOperationBindingDescriptor UseResolver<TResolver>()
        where TResolver : IOGraphOperationBindingResolver, new()
    {
        return UseResolver(new TResolver());
    }

    public IOGraphOperationBindingDescriptor UseResolver(OGraphOperationBindingResolver resolver)
    {
        if (resolver is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(resolver));
        }
        return UseResolver(new OperationBindingResolverWrapper(resolver));
    }

    public IOGraphOperationBindingDescriptor UseResolver(IOGraphOperationBindingResolver resolver)
    {
        if (resolver is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(resolver));
        }
        binding.Resolver = resolver;
        return this;
    }

    public IOGraphOperationBindingDescriptor UseRoute(Route route)
    {
        // Check if a route prefix was set in the options
        if (!string.IsNullOrEmpty(Options.RoutePrefix))
        {
            // Check if the prefix has already been provided
            if (route.Segments[0].Value.Equals(Options.RoutePrefix, StringComparison.OrdinalIgnoreCase))
            {
                binding.Route = route;
            }
            else
            {
                binding.Route = Route.Combine(Options.RoutePrefix, route);
            }
        }
        else
        {
            binding.Route = route;
        }
        return this;
    }
}
