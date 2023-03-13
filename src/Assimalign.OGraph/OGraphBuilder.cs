using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;

public sealed class OGraphBuilder : IOGraphBuilder
{
    // These are our build actions
    private readonly IList<Action<OGraph>> onNodeAdd;
    private readonly IList<Action<OGraph>> onEdgeAdd;
    private readonly IList<Action<OGraph>> onOperationAdd;

    private readonly OGraph graph = new();

    private OGraphBuilder()
    {
        this.onNodeAdd = new List<Action<OGraph>>();
        this.onEdgeAdd = new List<Action<OGraph>>();
        this.onOperationAdd = new List<Action<OGraph>>();
    }

    IOGraphBuilder IOGraphBuilder.AddEdge(IOGraphEdge edge)
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
    IOGraphBuilder IOGraphBuilder.AddNode(IOGraphNode node)
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
    IOGraphBuilder IOGraphBuilder.AddOperation(IOGraphOperation operation)
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