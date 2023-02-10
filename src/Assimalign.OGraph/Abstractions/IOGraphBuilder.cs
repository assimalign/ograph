using System;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <returns></returns>
    IOGraphBuilder AddNode<TNode>() where TNode : IOGraphNode, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    IOGraphBuilder AddNode(IOGraphNode node);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    /// <returns></returns>
    IOGraphBuilder AddNode(Name name, Action<IOGraphNodeDescriptor> descriptor);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="descriptor"></param>
    /// <returns></returns>
    IOGraphBuilder AddNode<T>(Name name, Action<IOGraphNodeDescriptor<T>> descriptor);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IOGraph Build();
}
