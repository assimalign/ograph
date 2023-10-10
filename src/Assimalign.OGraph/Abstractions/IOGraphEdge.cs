namespace Assimalign.OGraph;

/// <summary>
/// An edge links two nodes together.
/// </summary>
/// <remarks>
/// An edge is also referred to as a Link.
/// </remarks>
public interface IOGraphEdge
{
    /// <summary>
    /// The name of the edge.
    /// </summary>
    Name Name { get; }
    /// <summary>
    /// The source node.
    /// </summary>
    IOGraphNode Source { get; }
    /// <summary>
    /// The target is the node in which is linked to the source.
    /// </summary>
    IOGraphNode Target { get; }
    /// <summary>
    /// Metadata for the edge.
    /// </summary>
    IOGraphMetadata Metadata { get; }
    /// <summary>
    /// The edge resolver.
    /// </summary>
    IOGraphEdgeResolver Resolver { get; }
    /// <summary>
    /// A collection of middleware that will be executed before the edge is resolved.
    /// </summary>
    IOGraphEdgeMiddlewareQueue Middleware { get; }
    /// <summary>
    /// Gets the OGraph query provider.
    /// </summary>
    IOGraphQueryProvider QueryProvider { get; }
    /// <summary>
    /// Gets the OGraph query options.
    /// </summary>
    OGraphQueryOptions QueryOptions { get; }
    /// <summary>
    /// Builds an execution chain for the given edge.
    /// </summary>
    /// <returns></returns>
    OGraphEdgeHandler BuildHandlerChain();
}