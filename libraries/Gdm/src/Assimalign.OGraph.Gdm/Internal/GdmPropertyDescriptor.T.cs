namespace Assimalign.OGraph.Gdm.Internal;

using Elements;
using System;

internal class GdmPropertyDescriptor<T> : IOGraphGdmPropertyDescriptor<T>
{
    private readonly GdmProperty property;
    private readonly GdmGraph Graph;

    public GdmPropertyDescriptor(GdmProperty property, GdmGraph graph)
    {
        this.property = property;
        this.Graph = graph;
    }

    public IOGraphGdmPropertyDescriptor<T> AddMeta(string key, string value)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor<T> IsRequired()
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor<T> UseGetter(GdmPropertyGetter getter)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor<T> UsePropertyName(GdmLabel label)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor<T> UseSetter(GdmPropertySetter setter)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor<T> UseType(IOGraphGdmType type)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor<T> UseType(Func<IOGraphGdmGraph, IOGraphGdmType> type)
    {
        throw new NotImplementedException();
    }
}