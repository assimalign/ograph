using System;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphOperationDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseName(Name name);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="route"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseRoute(Route route);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseMethod(Method method);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseQuery(QueryValue query);
    /// <summary>
    /// Binds the operation to a specific node.
    /// </summary>
    /// <remarks></remarks>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseNode(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <returns></returns>
    IOGraphOperationDescriptor UseNode<TNode>() where TNode : IOGraphNode, new();
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TQueryProvider"></typeparam>
    /// <returns></returns>
    IOGraphOperationDescriptor UseQueryProvider<TQueryProvider>() where TQueryProvider : IOGraphQueryProvider, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryProvider"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseQueryProvider(IOGraphQueryProvider queryProvider);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseQueryOptions(OGraphQueryOptions options);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TQueryOptions"></typeparam>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseQueryOptions<TQueryOptions>(Action<TQueryOptions> configure) where TQueryOptions : OGraphQueryOptions, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseQueryOptions(Action<OGraphQueryOptions> configure);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMiddleware"></typeparam>
    /// <returns></returns>
    IOGraphOperationDescriptor UseMiddleware<TMiddleware>() where TMiddleware : IOGraphOperationMiddleware, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseMiddleware(IOGraphOperationMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseMiddleware(OGraphOperationMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResolver"></typeparam>
    /// <returns></returns>
    IOGraphOperationDescriptor UseResolver<TResolver>() where TResolver : IOGraphOperationResolver, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseResolver(IOGraphOperationResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseResolver(OGraphOperationResolver resolver);


}

