namespace Assimalign.OGraph.Gdm.Internal;

using Elements;
using System;

internal class GdmPropertyDescriptor : IOGraphGdmPropertyDescriptor
{
    private readonly GdmProperty property;

    public GdmPropertyDescriptor(GdmProperty property)
    {
        this.property = property;
    }

    public IOGraphGdmPropertyDescriptor IsRequired()
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor UseGetter(GdmPropertyGetter getter)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor UsePropertyName(Label label)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor UseSetter(GdmPropertySetter setter)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor UseType(IOGraphGdmType type)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor UseType(Func<IOGraphGdmGraph, IOGraphGdmType> type)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor AddMeta(string key, string value)
    {
        throw new NotImplementedException();
    }
}