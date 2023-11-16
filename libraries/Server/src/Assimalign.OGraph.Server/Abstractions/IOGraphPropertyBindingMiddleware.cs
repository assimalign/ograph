using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphPropertyBindingMiddleware
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    Task<IOGraphResult> InvokeAsync(
        IOGraphPropertyBindingContext context,
        CancellationToken cancellationToken,
        OGraphPropertyBindingMiddlewareHandler next);
}
