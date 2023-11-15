using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmOperationBindingMiddleware
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    Task InvokeAsync(
        IOGraphGdmOperationBindingContext context, 
        CancellationToken cancellationToken, 
        IOGraphGdmOperationBindingResolver next);
}
