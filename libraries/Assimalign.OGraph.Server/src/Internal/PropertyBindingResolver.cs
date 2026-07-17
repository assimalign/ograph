using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Server.Internal;

using Gdm;

internal class PropertyBindingResolver : IOGraphPropertyBindingResolver
{
    private readonly OGraphPropertyBindingResolver resolver;
    public PropertyBindingResolver(OGraphPropertyBindingResolver resolver)
    {
        this.resolver = resolver;
    }
    public Task<IOGraphResult> InvokeAsync(IOGraphPropertyBindingContext context, CancellationToken cancellationToken)
    {
        return resolver.Invoke(context, cancellationToken);
    }
}