using System;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmComplexTypeDescriptor : IOGraphGdmComplexTypeDescriptor
{
    private readonly GdmComplexType complexType;

    public GdmComplexTypeDescriptor(GdmComplexType complexType)
    {
        this.complexType = complexType;
    }

    public IOGraphGdmComplexTypeDescriptor HasLabel(GdmLabel label)
    {
        complexType.label = label;
        return this;
    }
    public IOGraphGdmPropertyDescriptor HasProperty(GdmLabel label)
    {
        var propertyInfo = complexType.runtimeType!.GetProperty(label);
        if (propertyInfo is null)
        {
            throw new InvalidOperationException($"The property '{label}' does not exist on type {complexType.runtimeType!.Name}");
        }
        var property = complexType.GetProperty(propertyInfo);
        property.Getter ??= propertyInfo.GetValue;
        property.Setter ??= propertyInfo.SetValue;
        property.DeclaringType = new GdmTypeReference()
        {
            Definition = complexType
        };
        return new GdmPropertyDescriptor(property);
    }
}