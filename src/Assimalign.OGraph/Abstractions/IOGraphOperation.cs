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
    Name Name { get; }
    /// <summary>
    /// The route associated with this operation.
    /// </summary>
    Route Route { get; }
    /// <summary>
    /// The HTTP method.
    /// </summary>
    Method Method { get; }
    /// <summary>
    /// Specifies whether the operation is enabled.
    /// </summary>
    bool IsEnabled { get; }
    /// <summary>
    /// Specifies whether the operation is a command or query.
    /// </summary>
    OperationType OperationType { get; }
    /// <summary>
    /// Represents the node that is binded to this operation.
    /// </summary>
    IOGraphNode Node { get; }
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
    /// Builds a handler that create invocation chain to execute middleware and resolver.
    /// </summary>
    /// <returns></returns>
    OGraphOperationHandler BuildHandlerChain();
}