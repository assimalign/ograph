using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphEdgeMiddleware
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    Task<IOGraphEdgeResult> InvokeAsync(IOGraphEdgeResolverContext context, OGraphEdgeHandler next);
}
