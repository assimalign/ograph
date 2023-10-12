using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// Computed properties are non filterable or sortable properties that are executed 
/// after entities are returned from the query provider;
/// </summary>
//bool IsComputed { get; } // TODO: May need to come up with a different convention for managing filterable and sortable properties.

/// <summary>
/// 
/// </summary>
public interface IOGraphProperty
{
    /// <summary>
    /// The name of the property.
    /// </summary>
    Name Name { get; }
    /// <summary>
    /// The OGraph Property Type.
    /// </summary>
    IOGraphType Type { get; }
    /// <summary>
    /// Metadata of the property.
    /// </summary>
    IOGraphMetadata Metadata { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphPropertyResolver Resolver { get; }
    /// <summary>
    /// The collection of middleware to execute before the resolver.
    /// </summary>
    IOGraphPropertyMiddlewareQueue Middleware { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IOGraphResult> ExecuteAsync(IOGraphPropertyContext context, CancellationToken cancellationToken = default);
}
