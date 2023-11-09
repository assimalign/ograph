using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;

public interface IOGraphOperationBinding : IOGraphGdmBinding
{
    /// <summary>
    /// 
    /// </summary>
    Label Label { get; }
    /// <summary>
    /// 
    /// </summary>
    Route Route { get; }
    /// <summary>
    /// 
    /// </summary>
    Method Method { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphOperationBindingResolver Resolver { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphOperationBindingMiddlewareQueue Middleware { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExecuteAsync(IOGraphOperationBindingContext context, CancellationToken cancellationToken = default);
}