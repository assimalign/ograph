using System;
using System.Security.Claims;

namespace Assimalign.OGraph;

/// <summary>
/// The context for the resolver being executed for the property.
/// </summary>
public interface IOGraphPropertyContext
{
    /// <summary>
    /// Gets the property's declaring type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T GetParent<T>();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IOGraphType GetPropertyType();
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