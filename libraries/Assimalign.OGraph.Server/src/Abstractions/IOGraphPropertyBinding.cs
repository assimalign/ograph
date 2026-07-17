using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphPropertyBinding : IOGraphGdmBinding
{
    /// <summary>
    /// Gets the property resolver.
    /// </summary>
    IOGraphPropertyBindingResolver Resolver { get; }
    /// <summary>
    /// Middleware to be invoked before property resolution.
    /// </summary>
    IOGraphPropertyBindingMiddlewareQueue Middleware { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExecuteAsync(IOGraphPropertyBindingContext context, CancellationToken cancellationToken);
}