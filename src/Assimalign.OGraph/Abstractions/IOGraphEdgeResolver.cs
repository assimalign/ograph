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
    Task<IOGraphEdgeResult> InvokeAsync(IOGraphEdgeResolverContext context);
}
