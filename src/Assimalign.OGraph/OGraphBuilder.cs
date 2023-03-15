using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;
using System.Collections.Concurrent;

public sealed class OGraphBuilder : IOGraphBuilder
{

    private readonly static ConcurrentDictionary<Name, IOGraph> cache = new();

    // These are our build actions
    internal readonly IList<Action<OGraph>> onNodeAdd;
    internal readonly IList<Action<OGraph>> onEdgeAdd;
    internal readonly IList<Action<OGraph>> onOperationAdd;

    private readonly OGraph graph = new();

    private OGraphBuilder()
    {
        this.onNodeAdd = new List<Action<OGraph>>();
        this.onEdgeAdd = new List<Action<OGraph>>();
        this.onOperationAdd = new List<Action<OGraph>>();
    }


    /// <inheritdoc />
    public IOGraphBuilder AddNode<TNode>() where TNode : IOGraphNode, new()
    {
        return AddNode(new TNode());
    }

    /// <inheritdoc />
    public IOGraphBuilder AddNode(IOGraphNode node)
    {
        if (node is null)
        {
            throw new ArgumentNullException(nameof(node));
        }
        this.onNodeAdd.Add(graph =>
        {
            graph.Nodes.Add(node);
        });
        return this;
    }

    /// <inheritdoc />
    public IOGraphNodeDescriptor AddNode(Label label)
    {
        var node = new OGraphNode()
        {
            Label = label
        };

        var descriptor = new OGraphNodeDescriptor(node)
        {
            OnConfigure = onNodeAdd
        };

        AddNode(node);

        return descriptor;
    }

    /// <inheritdoc />
    public IOGraphBuilder AddEdge<TEdge>() where TEdge : IOGraphEdge, new()
    {
        return AddEdge(new TEdge());
    }

    /// <inheritdoc />
    public IOGraphBuilder AddEdge(IOGraphEdge edge)
    {
        if (edge is null)
        {
            throw new ArgumentNullException(nameof(edge));
        }
        this.onEdgeAdd.Add(graph =>
        {
            graph.Edges.Add(edge);
        });
        return this;
    }

    /// <inheritdoc />
    public IOGraphBuilder AddEdge(Func<IOGraph, IOGraphEdge> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        this.onEdgeAdd.Add(graph =>
        {
            var edge = configure.Invoke(graph);
            
            graph.Edges.Add(edge);
        });

        return this;
    }

    /// <inheritdoc />
    public IOGraphEdgeDescriptor AddEdge(Name name)
    {
        var edge = new OGraphEdge() 
        { 
            Name = name 
        };
        var descriptor = new OGraphEdgeDescriptor(edge)
        {
            OnConfigure = onEdgeAdd
        };

        AddEdge(edge);

        return descriptor;
    }

    /// <inheritdoc />
    public IOGraphBuilder AddOperation<TOperation>() where TOperation : IOGraphOperation, new()
    {
        return AddOperation(new TOperation());
    }

    /// <inheritdoc />
    public IOGraphBuilder AddOperation(IOGraphOperation operation)
    {
        if (operation is null)
        {
            throw new ArgumentNullException(nameof(operation));
        }
        this.onOperationAdd.Add(graph =>
        {
            graph.Operations.Add(operation);
        });

        return this;
    }

    /// <inheritdoc />
    public IOGraphBuilder AddOperation(Func<IOGraph, IOGraphOperation> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        this.onOperationAdd.Add(graph =>
        {
            var operation = configure.Invoke(graph);

            graph.Operations.Add(operation);
        });

        return this;
    }

    /// <inheritdoc />
    public IOGraphOperationDescriptor AddOperation(Name name)
    {
        var operation = new OGraphOperation()
        {
            Name = name
        };
        var descriptor = new OGraphOperationDescriptor(operation)
        {
            OnConfigure = onOperationAdd
        };

        AddOperation(operation);

        return descriptor;
    }
    IOGraph IOGraphBuilder.Build()
    {
        Build(onNodeAdd);
        Build(onEdgeAdd);
        Build(onOperationAdd);

        return graph;
    }

    private void Build(IList<Action<OGraph>> actions)
    {
        foreach (var action in actions)
        {
            action.Invoke(graph);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">The name of the graph model.</param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraph Create(Name name, Action<IOGraphBuilder> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        var builder = new OGraphBuilder();

        builder.onNodeAdd.Add(graph =>
        {
            graph.Name = name;
        });

        configure.Invoke(builder);

        return ((IOGraphBuilder)builder).Build();
    }
}