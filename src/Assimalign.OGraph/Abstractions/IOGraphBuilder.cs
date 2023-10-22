using System;

namespace Assimalign.OGraph;

/// <summary>
/// A fluent builder for creating a <see cref="IOGraph"/> model.
/// </summary>
public interface IOGraphBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="vertex"></param>
    /// <returns></returns>
    IOGraphBuilder AddVertex(IOGraphVertex vertex);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <returns></returns>
    IOGraphBuilder AddVertex<TVertex>() where TVertex : IOGraphVertex, new();
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="descriptor"></param>
    /// <returns></returns>
    IOGraphBuilder AddVertex<T>(Action<IOGraphVertexDescriptor<T>> descriptor) where T : class, new();
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="edge"></param>
    ///// <returns></returns>
    //IOGraphBuilder AddEdge(IOGraphEdge edge);
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="configure"></param>
    ///// <returns></returns>
    //IOGraphBuilder AddEdge(Func<IOGraph, IOGraphEdge> configure);
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <typeparam name="TEdge"></typeparam>
    ///// <returns></returns>
    //IOGraphBuilder AddEdge<TEdge>() where TEdge : IOGraphEdge, new();
    /// <summary>
    /// Add a raw operation to the graph model.
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    IOGraphBuilder AddOperation(IOGraphOperation operation);
    /// <summary>
    /// Add a raw operation to the graph model.
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphBuilder AddOperation(Func<IOGraph, IOGraphOperation> configure);
    /// <summary>
    /// Add a raw operation to the graph model.
    /// </summary>
    /// <typeparam name="TOperation"></typeparam>
    /// <returns></returns>
    IOGraphBuilder AddOperation<TOperation>() where TOperation : IOGraphOperation, new();
    /// <summary>
    /// Builds the graph model.
    /// </summary>
    /// <returns>OGraph Model</returns>
    //IOGraph Build();
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="name"></param>
    ///// <returns></returns>
    //IOGraphEdgeDescriptor AddEdge(Name name);
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="label"></param>
    ///// <returns></returns>
    //IOGraphVertexDescriptor AddNode(Name label);
    /// <summary>
    /// Adds a query operation.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphQueryOperationDescriptor AddQuery(Name name);
    /// <summary>
    /// Adds a command operation
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphCommandOperationDescriptor AddCommand(Name name);
    
}