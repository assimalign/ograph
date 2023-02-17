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
    /// <typeparam label="TNode"></typeparam>
    /// <returns></returns>
    IOGraphBuilder AddNode<TNode>() where TNode : IOGraphNode, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param label="node"></param>
    /// <returns></returns>
    IOGraphBuilder AddNode(IOGraphNode node);
    /// <summary>
    /// This is a description
    /// </summary>
    /// <param label="descriptor"></param>
    /// <returns></returns>
    IOGraphBuilder AddNode(Label label, Action<IOGraphNodeDescriptor> descriptor);
    /// <summary>
    /// This is a description
    /// </summary>
    /// <typeparam label="T"></typeparam>
    /// <param label="descriptor"></param>
    /// <returns></returns>
    IOGraphBuilder AddNode<T>(Label label, Action<IOGraphNodeDescriptor<T>> descriptor);
    /// <summary>
    /// Define HTTP operations and link them to nodes.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="descriptor"></param>
    /// <returns></returns>
    IOGraphBuilder AddOperation(Name name, Action<IOGraphOperationDescriptor> descriptor);



    IOGraphBuilder AddSubscriber();


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IOGraph Build();
}
