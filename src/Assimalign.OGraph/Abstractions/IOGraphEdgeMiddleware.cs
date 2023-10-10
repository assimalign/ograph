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
    Task<IOGraphResult> InvokeAsync(IOGraphEdgeContext context, OGraphEdgeHandler next);
}
