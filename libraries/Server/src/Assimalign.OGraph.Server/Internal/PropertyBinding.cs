using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

using Assimalign.OGraph.Gdm;

internal class PropertyBinding : IOGraphPropertyBinding
{
    public PropertyBinding()
    {
        
    }
    public IOGraphPropertyBindingResolver Resolver { get; set; } = default!;
    public IOGraphPropertyBindingMiddlewareQueue Middleware { get; } = default!;
    public async Task ExecuteAsync(IOGraphPropertyBindingContext context, CancellationToken cancellationToken = default)
    {
        if (context is not PropertyBindingContext propertyContext)
        {
            throw new InvalidOperationException();
        }

        var property = propertyContext.Element!;
        var propertyParent = propertyContext.Parent;
        var propertySetter = property.Setter!;
        var propertyResult = await GetHandler().Invoke(propertyContext, cancellationToken);

        if (propertyResult is IOGraphError error)
        {
            propertyContext.Errors.Add(error);
        }
        else if (propertyResult is IOGraphPropertyResult success)
        {
            propertySetter.Invoke
                (propertyParent, 
                success.Value!);
        }
        else
        {
            throw new InvalidOperationException("Expected result is invalid");
        }
    }

    Task IOGraphGdmBinding.ExecuteAsync(IOGraphGdmBindingContext context, CancellationToken cancellationToken = default)
    {
        if (context is not IOGraphPropertyBindingContext)
        {
            ThrowHelper.ThrowInvalidOperationException("");
        }
        return ExecuteAsync((IOGraphPropertyBindingContext)context, cancellationToken);
    }



    private OGraphPropertyHandler GetHandler()
    {
        var index = 0;
        var root = new OGraphPropertyHandler(Resolver.InvokeAsync);

        if (Middleware.Count == 0)
        {
            return root;
        }

        return Chain(root);

        OGraphPropertyHandler Chain(OGraphPropertyHandler root)
        {
            var middleware = Middleware.Reverse().Skip(index).First();
            var next = new OGraphPropertyHandler((context, cancellationToken) =>
            {
                return middleware.InvokeAsync(context, cancellationToken, root);
            });
            if (index < Middleware.Count - 1)
            {
                index++;
                return Chain(next);
            }
            return next;
        }
    }
}
