using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Represents an HTTP operation binding for a vertex.
/// </summary>
public interface IOGraphGdmOperationBinding : IOGraphGdmBinding
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
    /// The operation type.
    /// </summary>
    OperationType OperationType { get; }
    /// <summary>
    /// The type definition for the request body of the operation.
    /// </summary>
    IOGraphGdmTypeReference RequestType { get; }
    /// <summary>
    /// The type definition of the response body
    /// </summary>
    IOGraphGdmTypeReference ResponseType { get; }
    /// <summary>
    /// A collection of headers defined within the operation.
    /// </summary>
    IOGraphGdmOperationBindingHeaders Headers { get; }
    /// <summary>
    /// A collection of query params within the operation.
    /// </summary>
    IOGraphGdmOperationBindingQueryParams Query { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmOperationBindingResolver Resolver { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmOperationBindingMiddlewareQueue Middleware { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExecuteAsync(IOGraphGdmOperationBindingContext context, CancellationToken cancellationToken);
}