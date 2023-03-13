using System;

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

    public IOGraphNodeDescriptor HasLabel(Label label)
    {
        node.Label = label;
        return this;
    }
    public IOGraphNodeDescriptor HasMetadata(string key, object value)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        node.Metadata[key] = value;

        return this;
    }
    public IOGraphNodeDescriptor HasType(IOGraphType type)
    {
        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }
        node.Type = type;
        return this;
    }
    public IOGraphNodeDescriptor HasType<TType>() where TType : IOGraphType, new()
    {
        node.Type = new TType();
        return this;
    }
    public IOGraphNodeDescriptor HasType(Action<OGraphComplexTypeDescriptor> configure)
    {
        throw new NotImplementedException();
    }
    public IOGraphNodeDescriptor HasType(Action<IOGraphComplexTypeDescriptor> configure)
    {
        throw new NotImplementedException();
    }
    public IOGraphNodeDescriptor HasType<T>(Action<IOGraphComplexTypeDescriptor<T>> configure)
    {
        throw new NotImplementedException();
    }   
}