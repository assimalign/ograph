using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphOperationBindingMiddleware
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    Task<IOGraphResult> InvokeAsync(
        IOGraphOperationBindingContext context,
        CancellationToken cancellationToken,
        OGraphOperationBindingMiddlewareHandler next);
}
