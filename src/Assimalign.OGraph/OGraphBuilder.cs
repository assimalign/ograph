using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;

public sealed class OGraphBuilder : IOGraphBuilder
{
    private readonly static ConcurrentDictionary<Name, IOGraph> cache = new();

    private readonly IList<Action<Graph>> onNodeAdd;
    private readonly IList<Action<Graph>> onEdgeAdd;
    private readonly IList<Action<Graph>> onOperationAdd;
    private readonly IList<Action<Graph>> onBuild;

    private OGraphBuilder()
    {
        this.onNodeAdd = new List<Action<Graph>>();
        this.onEdgeAdd = new List<Action<Graph>>();
        this.onOperationAdd = new List<Action<Graph>>();
        this.onBuild = new List<Action<Graph>>();  
    }

    internal Graph Graph { get; init; } = default!;

    #region Node
    IOGraphBuilder IOGraphBuilder.AddVertex<TNode>()
    {
        var node = new TNode();

        return (this as IOGraphBuilder).AddVertex(node);
    }
    IOGraphBuilder IOGraphBuilder.AddVertex(IOGraphVertex node)
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
    IOGraphBuilder IOGraphBuilder.AddVertex<T>(Action<IOGraphVertexDescriptor<T>> descriptor)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Edge
    IOGraphBuilder IOGraphBuilder.AddEdge<TEdge>() 
    {
        return (this as IOGraphBuilder).AddEdge(new TEdge());
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
    IOGraphBuilder IOGraphBuilder.AddEdge(Func<IOGraph, IOGraphEdge> configure)
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
    IOGraphEdgeDescriptor IOGraphBuilder.AddEdge(Name name)
    {
        var edge = new OGraphEdgeDefault() 
        { 
            Label = name 
        };
        var descriptor = new OGraphEdgeDescriptor(edge)
        {
            OnConfigure = onEdgeAdd
        };

        (this as IOGraphBuilder).AddEdge(edge);

        return descriptor;
    }
    #endregion

    #region Operations
    IOGraphBuilder IOGraphBuilder.AddOperation<TOperation>()
    {
        return (this as IOGraphBuilder).AddOperation(new TOperation());
    }
    IOGraphBuilder IOGraphBuilder.AddOperation(IOGraphOperation operation)
    {
        if (operation is null)
        {
            throw new ArgumentNullException(nameof(operation));
        }
        this.onBuild.Add(graph =>
        {
            var node = operation.Node;
            var root = operation.Route.Segments[0].Value;

            if (!node.Label.Equals(root))
            {
                throw new InvalidOperationException($"The operation node label '{node.Label}' does not match the root segment '/{root}' of the route.");
            }

            graph.Operations.Add(operation);
        });

        return this;
    }
    IOGraphBuilder IOGraphBuilder.AddOperation(Func<IOGraph, IOGraphOperation> configure)
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
    IOGraphCommandOperationDescriptor IOGraphBuilder.AddCommand(Name name)
    {
        var operation = new OGraphCommandOperationDefault()
        {
            name = name
        };
        var descriptor = new OGraphCommandOperationDescriptor(operation)
        {
            OnConfigure = onOperationAdd
        };

        (this as IOGraphBuilder).AddOperation(operation);

        return descriptor;
    }
    IOGraphQueryOperationDescriptor IOGraphBuilder.AddQuery(Name name)
    {
        var operation = new OGraphQueryOperationDefault()
        {
            name = name
        };
        var descriptor = new OGraphQueryOperationDescriptor(operation)
        {
            OnConfigure = onOperationAdd
        };

        (this as IOGraphBuilder).AddOperation(operation);

        return descriptor;
        throw new NotImplementedException();
    }
    #endregion

    IOGraph IOGraphBuilder.Build()
    {
        return cache.GetOrAdd(Graph.Label, name =>
        {
            OnBuild(onNodeAdd);
            OnBuild(onEdgeAdd);
            OnBuild(onOperationAdd);
            OnBuild(onBuild);

            return Graph;
        });

        void OnBuild(IList<Action<Graph>> actions)
        {
            foreach (var action in actions)
            {
                action.Invoke(Graph);
            }
        }
    }

    /// <summary>
    /// Creates a <see cref="IOGraph"/> model.
    /// </summary>
    /// <param name="name">The name of the graph model.</param>
    /// <param name="configure"></param>
    /// <returns>OGraph model</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraph Create(Name name, Action<IOGraphBuilder> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        var builder = new OGraphBuilder()
        {
            Graph = new()
            {
                Label = name
            }
        };

        configure.Invoke(builder);

        return ((IOGraphBuilder)builder).Build();
    }

    /// <summary>
    /// Creates an <see cref="IOGraphBuilder"/>.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static IOGraphBuilder Create(Name name)
    {
        return new OGraphBuilder()
        {
            Graph = new()
            {
                Label = name
            }
        };
    }

    
}