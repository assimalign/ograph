using System;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphOperationDescriptor
{
    /// <summary>
    /// Sets the name of the operation
    /// </summary>
    /// <param name="name">A string name.</param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationDescriptor UseName(Name name);
    /// <summary>
    /// Sets the route to use for the operation.
    /// </summary>
    /// <param name="route">The route value.</param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationDescriptor UseRoute(Route route);
    /// <summary>
    /// Sets the HTTP method for the operaiton.
    /// </summary>
    /// <param name="method">The HTTP Method</param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationDescriptor UseMethod(Method method);
    /// <summary>
    /// Sets and exposes a generic query parameter.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationDescriptor UseQueryParam(string paramKey);
    /// <summary>
    /// Binds a node to the operation.
    /// </summary>
    /// <remarks></remarks>
    /// <param name="label"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationDescriptor UseNode(Name name);
    /// <summary>
    /// Binds a node to the operation. 
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationDescriptor UseNode<TNode>() where TNode : IOGraphNode, new();
    /// <summary>
    /// Overrides the default query provider.
    /// </summary>
    /// <typeparam name="TQueryProvider"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationDescriptor UseQueryProvider<TQueryProvider>() where TQueryProvider : IOGraphQueryProvider, new();
    /// <summary>
    /// Overrides the default query provider.
    /// </summary>
    /// <param name="queryProvider"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationDescriptor UseQueryProvider(IOGraphQueryProvider queryProvider);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationDescriptor UseQueryOptions(OGraphQueryOptions options);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TQueryOptions"></typeparam>
    /// <param name="configure"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationDescriptor UseQueryOptions<TQueryOptions>(Action<TQueryOptions> configure) where TQueryOptions : OGraphQueryOptions, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationDescriptor UseQueryOptions(Action<OGraphQueryOptions> configure);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMiddleware"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationDescriptor UseMiddleware<TMiddleware>() where TMiddleware : IOGraphOperationMiddleware, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationDescriptor UseMiddleware(IOGraphOperationMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationDescriptor UseMiddleware(OGraphOperationMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResolver"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationDescriptor UseResolver<TResolver>() where TResolver : IOGraphOperationResolver, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationDescriptor UseResolver(IOGraphOperationResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphOperationDescriptor UseResolver(OGraphOperationResolver resolver);
}