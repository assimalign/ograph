using System;
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
    /// 
    /// </summary>
    IOGraphPropertyBindingResolver Resolver { get; }
    /// <summary>
    /// The collection of middleware to execute before the resolver.
    /// </summary>
    IOGraphPropertyBindingMiddlewareQueue Middleware { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExecuteAsync(IOGraphPropertyBindingContext context, CancellationToken cancellationToken = default);
}
