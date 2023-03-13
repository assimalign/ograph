namespace Assimalign.OGraph;

/// <summary>
/// An edge links to edges together.
/// </summary>
/// <remarks>
/// An Edge is also referred to as a Link.
/// </remarks>
public interface IOGraphEdge
{
    /// <summary>
    /// The name of the Edge.
    /// </summary>
    Name Name { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphNode Source { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphNode Target { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphMetadata Metadata { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphEdgeResolver Resolver { get; }
    /// <summary>
    /// A collection of middleware that will be executed before the edge is resolved.
    /// </summary>
    IOGraphEdgeMiddlewareQueue Middleware { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    OGraphEdgeHandler GetResolverChain();
}
