using System;

namespace Assimalign.OGraph;

public interface IOGraphQueryOperationDescriptor
{
    /// <summary>
    /// Sets the name of the operation
    /// </summary>
    /// <param name="name">A string name.</param>
    /// <returns>The current descriptor.</returns>
    IOGraphQueryOperationDescriptor UseName(Name name);
    /// <summary>
    /// Sets the route to use for the operation.
    /// </summary>
    /// <param name="route">The route value.</param>
    /// <returns>The current descriptor.</returns>
    IOGraphQueryOperationDescriptor UseRoute(Route route);
    /// <summary>
    /// Sets and exposes a generic query parameter.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphQueryOperationDescriptor UseQueryParam(string paramKey);
    /// <summary>
    /// Binds a node to the operation.
    /// </summary>
    /// <remarks></remarks>
    /// <param name="label"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphQueryOperationDescriptor UseNode(Name name);
    /// <summary>
    /// Binds a node to the operation. 
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphQueryOperationDescriptor UseNode<TNode>() where TNode : IOGraphVertex, new();
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMiddleware"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphQueryOperationDescriptor UseMiddleware<TMiddleware>() where TMiddleware : IOGraphOperationMiddleware, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphQueryOperationDescriptor UseMiddleware(IOGraphOperationMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphQueryOperationDescriptor UseMiddleware(OGraphOperationMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResolver"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphQueryOperationDescriptor UseResolver<TResolver>() where TResolver : IOGraphOperationResolver, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphQueryOperationDescriptor UseResolver(IOGraphOperationResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphQueryOperationDescriptor UseResolver(OGraphOperationResolver resolver);
    /// <summary>
    /// Overrides the default query provider.
    /// </summary>
    /// <typeparam name="TQueryProvider"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphQueryOperationDescriptor UseQueryProvider<TQueryProvider>() where TQueryProvider : IOGraphQueryProvider, new();
    /// <summary>
    /// Overrides the default query provider.
    /// </summary>
    /// <param name="queryProvider"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphQueryOperationDescriptor UseQueryProvider(IOGraphQueryProvider queryProvider);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphQueryOperationDescriptor UseQueryOptions(OGraphQueryOptions options);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TQueryOptions"></typeparam>
    /// <param name="configure"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphQueryOperationDescriptor UseQueryOptions<TQueryOptions>(Action<TQueryOptions> configure) where TQueryOptions : OGraphQueryOptions, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphQueryOperationDescriptor UseQueryOptions(Action<OGraphQueryOptions> configure);
}
