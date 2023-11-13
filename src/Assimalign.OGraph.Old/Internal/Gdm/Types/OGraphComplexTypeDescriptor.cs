using System;

namespace Assimalign.OGraph.Internal;

internal class OGraphComplexTypeDescriptor : IOGraphComplexTypeDescriptor
{
    private readonly ComplexType complexType;

    public OGraphComplexTypeDescriptor(ComplexType complexTyp)
    {
        this.complexType = complexTyp;
    }

    public IOGraphPropertyDescriptor HasProperty(Label name)
    {
        var property = new Property()
        {
            Name = name,
        };
        var descriptor = new OGraphPropertyDescriptor(property);

        complexType.Properties.Add(property);

        return descriptor;
    }

    public IOGraphComplexTypeDescriptor HasUnderlyingType(Type type)
    {
        throw new NotImplementedException();
    }

    public IOGraphComplexTypeDescriptor HasUnderlyingType<T>() where T : class, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphComplexTypeDescriptor Ignore(Label name)
    {
        throw new NotImplementedException();
    }
}
