using System;
using System.Security.Claims;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;
using Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public interface IOGraphOperationBindingContext //: IOGraphGdmBindingContext
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphExecutorRequest Request { get; }
    /// <summary>
    /// The HTTP response to send back to the client.
    /// </summary>
    IOGraphExecutorResponse Response { get; }
    /// <summary>
    /// 
    /// </summary>
    IServiceProvider ServiceProvider { get; }
    /// <summary>
    /// 
    /// </summary>
    new IOGraphGdmNode Element { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T GetService<T>();
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="paramName"></param>
    /// <returns></returns>
    T GetRouteValue<T>(string paramName);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ClaimsPrincipal GetClaimsPrincipal();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    QueryDocument? GetQueryDocument();


    OGraphQueryOptions GetQueryOptions();
    IOGraphQueryProvider GetQueryProvider();
}