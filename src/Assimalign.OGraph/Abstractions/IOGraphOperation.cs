using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// Represents a single HTTP REST operation.
/// </summary>
/// <remarks>
/// An OGraph Operation represent the root 
/// Operation -- resolves --> Root(s) -- resolves --> Edge(s) -- resolves --> Root(s) -- resolves --> Operation(s)
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
    /// 
    /// </summary>
    Method Method { get; }
    /// <summary>
    /// 
    /// </summary>
    bool IsEnabled { get; }
    /// <summary>
    /// Represents the node that is binded to this operation.
    /// </summary>
    IOGraphNode Node { get; }
    /// <summary>
    /// A collection of the types that can be submitted to this operation.
    /// </summary>
    IOGraphTypeCollection RequestTypes { get; }
    /// <summary>
    /// A collection of the types that are returned by this operation.
    /// </summary>
    IOGraphTypeCollection ResponseTypes { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphOperationResolver Resolver { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphOperationMiddlewareQueue Middleware { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphMetadata Metadata { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    OGraphOperationHandler GetResolverChain();
}
