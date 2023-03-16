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
    IOGraphQueryProvider QueryProvider { get; }
    /// <summary>
    /// 
    /// </summary>
    OGraphQueryOptions QueryOptions { get; }
    /// <summary>
    /// Builds an execution chain for the given edge.
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Utilize Chain-Of-Responsibility pattern to combine the middleware and resolver to be executed in order. 
    /// </remarks>
    OGraphEdgeHandler GetResolverChain();
}
