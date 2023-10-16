using System;

namespace Assimalign.OGraph;

/// <summary>
/// A raw descriptor for defining an edge.
/// </summary>
public interface IOGraphEdgeDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label">The name of the node within the OGraph Model.</param>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseTargetNode(Name name);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseTargetNode<TNode>() where TNode : IOGraphNode, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseSourceNode(Name name);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseSourceNode<TNode>() where TNode : IOGraphNode, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseMetadata(string key, object value);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TQueryProvider"></typeparam>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseQueryProvider<TQueryProvider>() where TQueryProvider : IOGraphQueryProvider, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryProvider"></param>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseQueryProvider(IOGraphQueryProvider queryProvider);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphEdgeDescriptor UseQueryOptions(OGraphQueryOptions options);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TQueryOptions"></typeparam>
    /// <param name="configure"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphEdgeDescriptor UseQueryOptions<TQueryOptions>(Action<TQueryOptions> configure) where TQueryOptions : OGraphQueryOptions, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphEdgeDescriptor UseQueryOptions(Action<OGraphQueryOptions> configure);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMiddleware"></typeparam>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseMiddleware<TMiddleware>() where TMiddleware : IOGraphEdgeMiddleware, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseMiddleware(IOGraphEdgeMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseMiddleware(OGraphEdgeMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResolver"></typeparam>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseResolver<TResolver>() where TResolver : IOGraphEdgeResolver, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseResolver(IOGraphEdgeResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseResolver(OGraphEdgeResolver resolver);
}