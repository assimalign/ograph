using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// Represents a single HTTP REST operation.
/// </summary>
/// <remarks>
/// An OGraph Operation represent the root 
/// Operation -- resolves --> Root(s) -- resolves --> Identifier(s) -- resolves --> Root(s) -- resolves --> Operation(s)
/// </remarks>
public interface IOGraphOperation
{
    /// <summary>
    /// The name of the command.
    /// </summary>
    Name Label { get; }
    /// <summary>
    /// The route associated with this operation.
    /// </summary>
    Route Route { get; }
    /// <summary>
    /// The HTTP method.
    /// </summary>
    Method Method { get; }
    /// <summary>
    /// Specifies whether the operation is a command or query.
    /// </summary>
    OperationType OperationType { get; }
    /// <summary>
    /// Represents the node that is bound to this operation.
    /// </summary>
    IOGraphVertex Node { get; }
    /// <summary>
    /// The resolver for the operation.
    /// </summary>
    IOGraphOperationResolver Resolver { get; }
    /// <summary>
    /// A first-in-first-out queue of middleware that will execute 
    /// </summary>
    IOGraphOperationMiddlewareQueue Middleware { get; }
    /// <summary>
    /// The metadata for the operation.
    /// </summary>
    IOGraphMetadata Metadata { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IOGraphResult> ExecuteAsync(IOGraphOperationContext context, CancellationToken cancellationToken = default);
}