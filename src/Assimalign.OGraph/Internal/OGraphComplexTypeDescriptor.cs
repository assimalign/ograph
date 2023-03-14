namespace Assimalign.OGraph.Internal;

internal class OGraphComplexTypeDescriptor : IOGraphComplexTypeDescriptor
{
    private readonly IOGraphComplexType complexType;

    public OGraphComplexTypeDescriptor(IOGraphComplexType complexTyp)
    {
        this.complexType = complexTyp;
    }

    public IOGraphPropertyDescriptor HasProperty(Name name)
    {
        var property = new OGraphProperty()
        {
            Name = name,
        };
        var descriptor = new OGraphPropertyDescriptor(property);

        complexType.Properties.Add(property);

        return descriptor;
    }
}
