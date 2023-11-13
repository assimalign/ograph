using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Server.Internal;

using Gdm;

internal class PropertyBindingResolver : IOGraphGdmPropertyBindingResolver
{
    private readonly OGraphGdmPropertyBindingResolver resolver;
    public PropertyBindingResolver(OGraphGdmPropertyBindingResolver resolver)
    {
        this.resolver = resolver;
    }
    public Task<IOGraphResult> InvokeAsync(IOGraphGdmPropertyBindingContext context, CancellationToken cancellationToken)
    {
        return resolver.Invoke(context, cancellationToken);
    }
}