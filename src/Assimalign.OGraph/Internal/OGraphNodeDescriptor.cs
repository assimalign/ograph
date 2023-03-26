using System;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.OGraph.Internal;

internal class OGraphNodeDescriptor : IOGraphNodeDescriptor
{
    private readonly OGraphNode node;

    public OGraphNodeDescriptor(OGraphNode node)
    {
        if (node is null)
        {
            throw new ArgumentNullException(nameof(node));
        }            
        this.node = node;
    }


    public IList<Action<OGraph>> OnConfigure { get; init; }

    public IOGraphNodeDescriptor UseLabel(Label label)
    {
        node.label = label;
        return this;
    }
    public IOGraphNodeDescriptor UseMetadata(string key, object value)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        node.metadata[key] = value;

        return this;
    }
    public IOGraphNodeDescriptor UseType(IOGraphType type)
    {
        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }
        node.type = type;
        return this;
    }
    public IOGraphNodeDescriptor UseType<TType>() where TType : IOGraphType, new()
    {
        node.type = new TType();
        return this;
    }
    public IOGraphNodeDescriptor HasType(Action<OGraphComplexTypeDescriptor> configure)
    {
        throw new NotImplementedException();
    }
    public IOGraphNodeDescriptor UseType(Action<IOGraphComplexTypeDescriptor> configure)
    {
        throw new NotImplementedException();
    }
    public IOGraphNodeDescriptor UseType<T>(Action<IOGraphComplexTypeDescriptor<T>> configure)
    {
        OnConfigure.Add(graph =>
        {
            if (configure is null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var complexType = new ComplexType<T>();

            var descriptor = new OGraphComplexTypeDescriptor<T>(complexType);

            configure.Invoke(descriptor);

            node.type = complexType;
        });

        return this;
    }

    public IOGraphEdgeDescriptor AddEdge(Name name)
    {
        return new OGraphEdgeDescriptor(new OGraphEdge()
        {
            Name = name,
        });
    }
}