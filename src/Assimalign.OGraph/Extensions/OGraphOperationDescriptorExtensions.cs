using System;
using System.Linq;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;

public static class OGraphOperationDescriptorExtensions
{
    public static IOGraphOperationDescriptor UseResolver<TQueryable>(
        this IOGraphOperationDescriptor descriptor,
        Func<IOGraphOperationContext, TQueryable> resolver) where TQueryable : IQueryable
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }
        return descriptor.UseResolver(context =>
        {
            return Task.FromResult<IOGraphOperationResult>(new QueryableOperationResult()
            {
            
            });
        });
    }
}
