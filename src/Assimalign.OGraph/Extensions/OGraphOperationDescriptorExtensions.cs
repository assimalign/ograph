using System;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public static class OGraphOperationDescriptorExtensions
{
    public static IOGraphOperationDescriptor UseResolver<T>(
        this IOGraphOperationDescriptor descriptor,
        Func<IOGraphOperationResolverContext, T> resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }
        return descriptor.UseResolver(context =>
        {
            return Task.FromResult<IOGraphOperationResult>(new OGraphOperationResult<T>()
            {
                StatusCode = 200,
                Data = resolver.Invoke(context)
            });
        });
    }
}
