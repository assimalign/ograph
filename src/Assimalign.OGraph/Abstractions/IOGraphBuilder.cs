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
    /// <typeparam name="TNode"></typeparam>
    /// <returns></returns>
    IOGraphBuilder AddNode<TNode>() where TNode : IOGraphNode, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="edge"></param>
    /// <returns></returns>
    IOGraphBuilder AddEdge(IOGraphEdge edge);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphBuilder AddEdge(Func<IOGraph, IOGraphEdge> configure);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEdge"></typeparam>
    /// <returns></returns>
    IOGraphBuilder AddEdge<TEdge>() where TEdge : IOGraphEdge, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    IOGraphBuilder AddOperation(IOGraphOperation operation);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphBuilder AddOperation(Func<IOGraph, IOGraphOperation> configure);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOperation"></typeparam>
    /// <returns></returns>
    IOGraphBuilder AddOperation<TOperation>() where TOperation : IOGraphOperation, new();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IOGraph Build();



    IOGraphNodeDescriptor AddNode(Label label);


    IOGraphEdgeDescriptor AddEdge(Name name);

    IOGraphOperationDescriptor AddOperation(Name name);

}