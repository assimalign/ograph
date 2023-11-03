using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphPropertyResolver : IOGraphGdmPropertyBinding
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    ValueTask<IOGraphResult> InvokeAsync(IOGraphPropertyResolverContext context, CancellationToken cancellationToken = default);
}