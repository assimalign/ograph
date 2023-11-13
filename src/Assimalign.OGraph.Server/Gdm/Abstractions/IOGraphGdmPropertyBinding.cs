using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmPropertyBinding : IOGraphGdmBinding
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmPropertyBindingResolver Resolver { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmPropertyBindingMiddlewareQueue Middleware { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExecuteAsync(IOGraphGdmPropertyBindingContext context, CancellationToken cancellationToken);
}