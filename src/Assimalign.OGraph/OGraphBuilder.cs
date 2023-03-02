using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;

public sealed class OGraphBuilder : IOGraphBuilder
{

    // These are our build actions
    private readonly IList<Action<OGraph>> actions;


    private OGraphBuilder()
    {
        this.actions = new List<Action<OGraph>>();
    }


    IOGraphBuilder IOGraphBuilder.AddNode(IOGraphNode node)
    {
        this.actions.Add(graph => graph.Nodes.Add(node));
        return this;
    }
    IOGraphBuilder IOGraphBuilder.AddNode(Label name, Action<IOGraphNodeDescriptor> descriptor)
    {
        throw new NotImplementedException();
    }
    IOGraphBuilder IOGraphBuilder.AddNode<T>(Label name, Action<IOGraphNodeDescriptor<T>> configure)
    {
        this.actions.Add(graph =>
        {
            var node = new OGraphNodeDefault<T>()
            {
                
            };

            node.Configure(configure);

            graph.Nodes.Add(node);

        });
        return this;
    }

    IOGraph IOGraphBuilder.Build()
    {
        var graph = new OGraph();

        foreach (var action in actions)
        {
            action.Invoke(graph);
        }

        return graph;
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

        builder.actions.Add(graph =>
        {
            graph.Name = name;
        });

        configure.Invoke(builder);

        return ((IOGraphBuilder)builder).Build();
    }

    IOGraphBuilder IOGraphBuilder.AddNode<TNode>()
    {
        throw new NotImplementedException();
    }

    public IOGraphBuilder AddOperation(Name name, Action<IOGraphOperationDescriptor> descriptor)
    {
        throw new NotImplementedException();
    }

    IOGraphBuilder IOGraphBuilder.AddOperation(Name name, Action<IOGraphOperationDescriptor> descriptor)
    {
        throw new NotImplementedException();
    }

    IOGraphBuilder IOGraphBuilder.AddSubscriber()
    {
        throw new NotImplementedException();
    }
}
