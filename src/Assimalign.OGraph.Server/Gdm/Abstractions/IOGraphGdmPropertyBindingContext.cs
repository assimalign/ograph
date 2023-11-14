using System;
using System.Security.Claims;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmPropertyBindingContext : IOGraphGdmBindingContext
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
    new IOGraphGdmProperty Element { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T GetService<T>();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ClaimsPrincipal GetClaimsPrincipal();
}
