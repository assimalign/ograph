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
    /// <param label="node"></param>
    /// <returns></returns>
    IOGraphBuilder AddNode(IOGraphNode node);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    IOGraphBuilder AddOperation(IOGraphOperation operation);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IOGraph Build();
}


//IOGraphBuilder AddNode<TNode>() where TNode : IOGraphNode, new();
//IOGraphBuilder AddNode(Label label, Action<IOGraphNodeDescriptor> descriptor);
//IOGraphBuilder AddNode<T>(Label label, Action<IOGraphNodeDescriptor<T>> descriptor);
//IOGraphBuilder AddOperation(Name name, Action<IOGraphOperationDescriptor> descriptor);

//IOGraphOperationDescriptor AddOperation(Name name);
//IOGraphBuilder AddSubscriber();
