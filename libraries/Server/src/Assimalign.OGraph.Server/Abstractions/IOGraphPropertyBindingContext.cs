using System;
using System.Security.Claims;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;
using Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public interface IOGraphPropertyBindingContext //: IOGraphGdmBindingContext
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphExecutorRequest Request { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphExecutorResponse Response { get; }
    /// <summary>
    /// 
    /// </summary>
    IServiceProvider ServiceProvider { get; }
    /// <summary>
    /// The GDM Property Element
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
    ClaimsPrincipal? GetClaimsPrincipal();
}
