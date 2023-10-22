using System;
using System.Collections.Generic;
using System.Linq;

namespace Assimalign.OGraph.Internal;

internal class VertexDescriptor : IOGraphVertexDescriptor
{
    private readonly OGraphVertex node;

    public VertexDescriptor(OGraphVertex node)
    {
        if (node is null)
        {
            throw new ArgumentNullException(nameof(node));
        }            
        this.node = node;
    }

    public IList<Action<Graph>> OnConfigure { get; init; } = new List<Action<Graph>>();

    public IOGraphVertexDescriptor UseLabel(Name label)
    {
        if (!node.labels.Contains(label))
        {
            var index = node.labels.Length + 1;
            Array.Resize(ref node.labels, index);
            node.labels[index] = label;
        }
        return this;
    }
    public IOGraphVertexDescriptor UseMetadata(string key, object value)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        //node.metadata[key] = value;

        return this;
    }
    public IOGraphVertexDescriptor HasType(IOGraphType type)
    {
        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }
        OnConfigure.Add(graph =>
        {
            graph.Types.TryAdd(type);
        });
        node.type = type;
        return this;
    }
    public IOGraphVertexDescriptor HasType<TType>() where TType : IOGraphType, new()
    {
        var type = new TType();
        node.type = type;

        OnConfigure.Add(graph =>
        {
            graph.Types.TryAdd(type);
        });
        return this;
    }
    public IOGraphVertexDescriptor HasType(Action<OGraphComplexTypeDescriptor> configure)
    {
        throw new NotImplementedException();
    }
    public IOGraphVertexDescriptor HasType(Action<IOGraphComplexTypeDescriptor> configure)
    {
        throw new NotImplementedException();
    }
    public IOGraphVertexDescriptor HasType<T>(Action<IOGraphComplexTypeDescriptor<T>> configure) where T : class, new()
    {
        OnConfigure.Add(graph =>
        {
            if (configure is null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var complexType = new ComplexType<T>();

            graph.Types.TryAdd(complexType);

            var descriptor = new OGraphComplexTypeDescriptor<T>(complexType);

            configure.Invoke(descriptor);

            node.type = complexType;
        });

        return this;
    }

    public IOGraphEdgeDescriptor AddEdge(Name label)
    {
        var edge = new OGraphEdgeDefault()
        {
            Label = label
        };

        if (!node.edges.TryAddEdge(edge))
        {
            
        }

        OnConfigure.Add(graph =>
        {

        });
        return new OGraphEdgeDescriptor(edge);
    }

    public IOGraphVertexDescriptor AddEdge<TEdge>() where TEdge : IOGraphEdge, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphVertexDescriptor AddEdge(IOGraphEdge edge)
    {
        throw new NotImplementedException();
    }

    public IOGraphQueryOperationDescriptor AddQuery(Name operationName)
    {
        var operation = new OGraphQueryOperationDefault()
        {
            name = operationName
        };
        var descriptor = new OGraphQueryOperationDescriptor(operation)
        {
            OnConfigure = this.OnConfigure
        };

        node.operations.Add(operation);

        OnConfigure.Add(graph =>
        {
            graph.Operations.Add(operation);
        });

        return descriptor;
    }

    public IOGraphCommandOperationDescriptor AddCommand(Name operationName)
    {
        var operation = new OGraphCommandOperationDefault()
        {
            name = operationName
        };
        var descriptor = new OGraphCommandOperationDescriptor(operation)
        {
            OnConfigure = this.OnConfigure
        };

        OnConfigure.Add(graph =>
        {
            graph.Operations.Add(operation);
        });

        return descriptor;
    }
}