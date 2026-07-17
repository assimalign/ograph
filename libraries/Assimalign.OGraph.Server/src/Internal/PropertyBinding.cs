using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

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
        var propertyType = property.Type.Definition;
        var propertyParent = propertyContext.Parent;
        var propertySetter = property.Setter!;
        var propertyResult = await GetChain().Invoke(propertyContext, cancellationToken);

        if (propertyResult is IOGraphError error)
        {
            propertyContext.Errors.Add(error);
        }
        else if (propertyResult is IOGraphPropertyResult success)
        {
            propertySetter.Invoke(
                propertyParent, 
                success.Value!);

            if (propertyType is IOGraphGdmComplexType complexType)
            {
                foreach (var prop in complexType.Properties)
                {

                }
            }
            else if (propertyType is IOGraphGdmCollectionType collectionType)
            {

            }


            if (property.HasBinding<IOGraphPropertyBinding>(out var binding))
            {

            }
        }
        else
        {
            throw new InvalidOperationException("Expected result is invalid");
        }
    }

    Task IOGraphGdmBinding.ExecuteAsync(IOGraphGdmBindingContext context, CancellationToken cancellationToken)
    {
        if (context is not IOGraphPropertyBindingContext)
        {
            ThrowHelper.ThrowInvalidOperationException("");
        }
        return ExecuteAsync((IOGraphPropertyBindingContext)context, cancellationToken);
    }

    private OGraphPropertyBindingMiddlewareHandler GetChain()
    {
        var index = 0;
        var root = new OGraphPropertyBindingMiddlewareHandler(Resolver.InvokeAsync);

        if (Middleware.Count == 0)
        {
            return root;
        }

        return Chain(root);

        OGraphPropertyBindingMiddlewareHandler Chain(OGraphPropertyBindingMiddlewareHandler root)
        {
            var middleware = Middleware.Reverse().Skip(index).First();
            var next = new OGraphPropertyBindingMiddlewareHandler((context, cancellationToken) =>
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
