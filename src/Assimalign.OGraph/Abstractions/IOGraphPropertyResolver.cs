using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphPropertyResolver
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    ValueTask<IOGraphPropertyResult> InvokeAsync(IOGraphPropertyResolverContext context);
}
