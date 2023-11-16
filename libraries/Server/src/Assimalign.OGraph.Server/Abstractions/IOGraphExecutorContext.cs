using System;
using System.Security.Claims;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphExecutorContext
{
    /// <summary>
    /// The incoming HTTP request.
    /// </summary>
    IOGraphRequest Request { get; }
    /// <summary>
    /// The outgoing HTTP response.
    /// </summary>
    IOGraphResponse Response { get; }
    /// <summary>
    /// The context service provider.
    /// </summary>
    IServiceProvider? ServiceProvider { get; }
    /// <summary>
    /// The authenticated user or application.
    /// </summary>
    ClaimsPrincipal ClaimsPrincipal { get; }
}