using System;

namespace Assimalign.OGraph;

public interface IOGraphApplicationQueryDescriptor<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="route"></param>
    /// <returns></returns>
    IOGraphApplicationQueryDescriptor<T> UseRoute(Route route);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMiddleware"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphApplicationQueryDescriptor<T> UseMiddleware<TMiddleware>()
        where TMiddleware : IOGraphOperationMiddleware, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphApplicationQueryDescriptor<T> UseMiddleware(IOGraphOperationMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphApplicationQueryDescriptor<T> UseMiddleware(OGraphOperationMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResolver"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphApplicationQueryDescriptor<T> UseResolver<TResolver>()
        where TResolver : IOGraphOperationResolver, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphApplicationQueryDescriptor<T> UseResolver(IOGraphOperationResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphApplicationQueryDescriptor<T> UseResolver(OGraphOperationResolver resolver);
    /// <summary>
    /// Overrides the default query provider.
    /// </summary>
    /// <typeparam name="TQueryProvider"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphApplicationQueryDescriptor<T> UseQueryProvider<TQueryProvider>()
        where TQueryProvider : IOGraphQueryProvider, new();
    /// <summary>
    /// Overrides the default query provider.
    /// </summary>
    /// <param name="queryProvider"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphApplicationQueryDescriptor<T> UseQueryProvider(IOGraphQueryProvider queryProvider);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphApplicationQueryDescriptor<T> UseQueryOptions(OGraphQueryOptions options);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TQueryOptions"></typeparam>
    /// <param name="configure"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphApplicationQueryDescriptor<T> UseQueryOptions<TQueryOptions>(Action<TQueryOptions> configure)
        where TQueryOptions : OGraphQueryOptions, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphApplicationQueryDescriptor<T> UseQueryOptions(Action<OGraphQueryOptions> configure);

}