using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmVertexOperationBindingMiddleware
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    Task InvokeAsync(
        IOGraphGdmVertexOperationBindingContext context, 
        CancellationToken cancellationToken, 
        IOGraphGdmVertexOperationBindingResolver next);
}
