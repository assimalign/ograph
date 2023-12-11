using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;

/// <summary>
/// Represents an HTTP operation binding for a vertex.
/// </summary>
public interface IOGraphOperationBinding : IOGraphGdmBinding
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
    IOGraphOperationBindingHeaders Headers { get; }
    /// <summary>
    /// A collection of query params within the operation.
    /// </summary>
    IOGraphOperationBindingQueryParams Query { get; }
    /// <summary>
    /// Gets the operation resolver.
    /// </summary>
    IOGraphOperationBindingResolver Resolver { get; }
    /// <summary>
    /// Get the operation middleware.
    /// </summary>
    IOGraphOperationBindingMiddlewareQueue Middleware { get; }
    /// <summary>
    /// 
    /// </summary>
    OGraphQueryOptions QueryOptions { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphQueryProvider QueryProvider { get; }
    /// <summary>
    /// Executes the binding.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExecuteAsync(IOGraphOperationBindingContext context, CancellationToken cancellationToken);
}



public interface IOGraphQueryOperationBinding
{
}
