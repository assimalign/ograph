using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;

public static class OGraphBuilderExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOperation"></typeparam>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IOGraphBuilder AddOperation<TOperation>(this IOGraphBuilder builder) where TOperation : IOGraphOperation, new()
    {
        return builder.AddOperation(new TOperation());
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static IOGraphOperationDescriptor AddOperation(this IOGraphBuilder builder, Name name)
    {
        var operation = new OGraphOperation()
        {
            Name = name
        };

        var descriptor = new OGraphOperationDescriptor(operation, default);


        builder.AddOperation(operation);

        return descriptor;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IOGraphBuilder AddNode<TNode>(this IOGraphBuilder builder) where TNode : IOGraphNode, new()
    {
        return builder.AddNode(new TNode());
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="label"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraphNodeDescriptor AddNode(this IOGraphBuilder builder, Label label)
    {
        var node = new OGraphNode()
        {
            Label = label
        };

        builder.AddNode(node);

        return new OGraphNodeDescriptor(node);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static IOGraphEdgeDescriptor AddEdge(this IOGraphBuilder builder, Name name)
    {
        var edge = new OGraphEdge()
        {
            Name = name
        };
        builder.AddEdge(edge);

        return new OGraphEdgeDescriptor(edge);
    }
}
