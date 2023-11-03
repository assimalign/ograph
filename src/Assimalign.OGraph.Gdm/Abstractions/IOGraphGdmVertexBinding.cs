using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmVertexBinding
{
    Task InvokeAsync(IOGraphGdmVertexBindingContext context, CancellationToken cancellationToken = default);
}
