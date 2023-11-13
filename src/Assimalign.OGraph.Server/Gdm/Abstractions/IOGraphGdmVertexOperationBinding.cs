using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Represents an HTTP operation binding for a vertex.
/// </summary>
public interface IOGraphGdmVertexOperationBinding : IOGraphGdmBinding
{
    /// <summary>
    /// A unique label as the operation name.
    /// </summary>
    Label Label { get; }
    /// <summary>
    /// The operation route.
    /// </summary>
    Route Route { get; }
    /// <summary>
    /// The operation method.
    /// </summary>
    Method Method { get; }
    /// <summary>
    /// 
    /// </summary>
    OperationType OperationType { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmTypeReference RequestType { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmTypeReference ResponseType { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmVertexOperationBindingResolver Resolver { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmVertexOperationBindingMiddlewareQueue Middleware { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExecuteAsync(IOGraphGdmVertexOperationBindingContext context, CancellationToken cancellationToken);
}