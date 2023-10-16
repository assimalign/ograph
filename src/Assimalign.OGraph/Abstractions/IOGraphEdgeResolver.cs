using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphEdgeResolver
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task<IOGraphResult> InvokeAsync(IOGraphEdgeContext context, CancellationToken cancellationToken = default);
}
