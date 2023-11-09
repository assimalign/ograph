using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphOperationBindingDescriptor
{

    /// <summary>
    /// Sets the name of the operation
    /// </summary>
    /// <param name="name">A string name.</param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationBindingDescriptor UseName(Label name);
    /// <summary>
    /// Sets the route to use for the operation.
    /// </summary>
    /// <param name="route">The route value.</param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationBindingDescriptor UseRoute(Route route);
    /// <summary>
    /// Sets and exposes a generic query parameter.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationBindingDescriptor UseQueryParam(string paramKey);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMiddleware"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationBindingDescriptor UseMiddleware<TMiddleware>()
        where TMiddleware : IOGraphOperationBindingMiddleware, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationBindingDescriptor UseMiddleware(IOGraphOperationBindingMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationBindingDescriptor UseMiddleware(OGraphOperationMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResolver"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationBindingDescriptor UseResolver<TResolver>()
        where TResolver : IOGraphOperationBindingResolver, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationBindingDescriptor UseResolver(IOGraphOperationBindingResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationBindingDescriptor UseResolver(OGraphOperationResolver resolver);
    /// <summary>
    /// Overrides the default query provider.
    /// </summary>
    /// <typeparam name="TQueryProvider"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationBindingDescriptor UseQueryProvider<TQueryProvider>()
        where TQueryProvider : IOGraphQueryProvider, new();
    /// <summary>
    /// Overrides the default query provider.
    /// </summary>
    /// <param name="queryProvider"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationBindingDescriptor UseQueryProvider(IOGraphQueryProvider queryProvider);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationBindingDescriptor UseQueryOptions(OGraphQueryOptions options);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TQueryOptions"></typeparam>
    /// <param name="configure"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationBindingDescriptor UseQueryOptions<TQueryOptions>(Action<TQueryOptions> configure)
        where TQueryOptions : OGraphQueryOptions, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationBindingDescriptor UseQueryOptions(Action<OGraphQueryOptions> configure);
}
