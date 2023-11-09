using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphPropertyBindingResolver
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    ValueTask<IOGraphResult> InvokeAsync(IOGraphPropertyBindingResolverContext context, CancellationToken cancellationToken = default);
}