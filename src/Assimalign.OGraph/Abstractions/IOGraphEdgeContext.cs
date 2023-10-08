using System;
using System.Security.Claims;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public interface IOGraphEdgeContext
{
    /// <summary>
    /// Get's the OGraph Model.
    /// </summary>
    /// <returns></returns>
    IOGraph GetGraph();
    /// <summary>
    /// Gets edge currently being executed.
    /// </summary>
    /// <returns></returns>
    IOGraphEdge GetEdge();
    /// <summary>
    /// Gets the edge's target type.
    /// </summary>
    /// <returns><see cref="IOGraphEdge.Target"/></returns>
    IOGraphType GetEdgeTarget();
    /// <summary>
    /// Gets the edge's source type.
    /// </summary>
    /// <returns><see cref="IOGraphEdge.Source"/></returns>
    IOGraphType GetEdgeSource();
    /// <summary>
    /// Gets the parsed HTTP request query.
    /// </summary>
    /// <returns></returns>
    QueryDocument GetQuery();
    /// <summary>
    /// Gets the OGraph query options
    /// </summary>
    /// <returns></returns>
    OGraphQueryOptions GetQueryOptions();
    /// <summary>
    /// Gets the query provider for the Edge being executed.
    /// </summary>
    /// <returns></returns>
    IOGraphQueryProvider GetQueryProvider();
    /// <summary>
    /// Gets the Parent object in which the edge is being executed for. This represents the relationship
    /// between <typeparamref name="T"/> and the edge type being returned.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T GetParent<T>();
    /// <summary>
    /// Gets a service from the <see cref="IServiceProvider"/>.
    /// </summary>
    /// <typeparam name="T">The service to return.</typeparam>
    /// <returns></returns>
    T? GetService<T>();
    /// <summary>
    /// Gets the current authenticated user or application if aailable.
    /// </summary>
    /// <returns></returns>
    ClaimsPrincipal GetClaimsPrincipal();
    /// <summary>
    /// Returns the service provider if available.
    /// </summary>
    IServiceProvider? ServiceProvider { get; }
    /// <summary>
    /// The incoming HTTP request.
    /// </summary>
    public IOGraphExecutorRequest Request { get; }
    /// <summary>
    /// The outgoing HTTP response.
    /// </summary>
    public IOGraphExecutorResponse Response { get; }
}
