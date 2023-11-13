using System;
using System.Security.Claims;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphExecutorContext
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphRequest Request { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphResponse Response { get; }
    /// <summary>
    /// 
    /// </summary>
    IServiceProvider ServiceProvider { get; }
    /// <summary>
    /// 
    /// </summary>
    ClaimsPrincipal ClaimsPrincipal { get; }
}