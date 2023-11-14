using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmVertexOperationBindingResolver
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IOGraphResult> InvokeAsync(IOGraphGdmPropertyBindingContext context, CancellationToken cancellationToken);
}
