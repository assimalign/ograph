using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmEdgeBinding
{
    Task InvokeAsync(IOGraphGdmEdgeBindingContext context, CancellationToken cancellationToken = default);
}
