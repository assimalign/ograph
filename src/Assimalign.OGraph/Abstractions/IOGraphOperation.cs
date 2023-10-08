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
    /// Represents the node that is binded to this operation.
    /// </summary>
    IOGraphNode Node { get; }
    /// <summary>
    /// The resolver for the operation.
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
    /// Gets the Query provider.
    /// </summary>
    IOGraphQueryProvider QueryProvider { get; }
    /// <summary>
    /// Gets the query options to be used for the query provider.
    /// </summary>
    OGraphQueryOptions QueryOptions { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    OGraphOperationHandler GetResolverChain();
}
