using System;
using System.Security.Claims;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public interface IOGraphOperationContext
{
    /// <summary>
    /// Get's the OGraph Model.
    /// </summary>
    /// <returns><see cref="IOGraph"/></returns>
    IOGraph GetGraph();
    /// <summary>
    /// Get's the binded node for the given operation being executed.
    /// </summary>
    /// <returns></returns>
    IOGraphOperation GetOperation();
    /// <summary>
    /// Get's the HTTP request query.
    /// </summary>
    /// <returns></returns>
    QueryDocument GetQuery();
    /// <summary>
    /// Get's the OGraph query options
    /// </summary>
    /// <returns></returns>
    OGraphQueryOptions GetQueryOptions();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IOGraphQueryProvider GetQueryProvider();
    /// <summary>
    /// Gets a service from the <see cref="IServiceProvider"/>.
    /// </summary>
    /// <typeparam name="T">The service to return.</typeparam>
    /// <returns></returns>
    T? GetService<T>();
    /// <summary>
    /// Gets the current authenticatted user if available.
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