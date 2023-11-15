using System;
using System.Security.Claims;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphExecutorContext
{
    /// <summary>
    /// The authenticated user or application.
    /// </summary>
    ClaimsPrincipal? ClaimsPrincipal { get; }
    /// <summary>
    /// The incoming HTTP request
    /// </summary>
    public IOGraphExecutorRequest Request { get; }
    /// <summary>
    /// The outgoing HTTP response.
    /// </summary>
    public IOGraphExecutorResponse Response { get; }
}
