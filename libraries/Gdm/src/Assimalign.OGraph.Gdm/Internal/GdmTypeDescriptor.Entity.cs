using System;

namespace Assimalign.OGraph.Gdm.Internal;

using Assimalign.OGraph.Gdm.Elements;

internal class GdmEntityTypeDescriptor : IOGraphGdmEntityTypeDescriptor
{
    private readonly GdmEntityType entityType;

    public GdmEntityTypeDescriptor(GdmEntityType entityType)
    {
        this.entityType = entityType;
    }
    public IOGraphGdmEntityTypeDescriptor HasLabel(Label label)
    {
        entityType.label = label;
        return this;
    }
    public IOGraphGdmEntityTypeDescriptor HasKey(Label label)
    {
        var runtimeType = entityType.runtimeType;
        var propertyInfo = runtimeType!.GetProperty(label);
        if (propertyInfo is null)
        {
            throw new InvalidOperationException($"The property '{label}' does not exist on type {entityType.runtimeType!.Name}");
        }
        var property = entityType.GetProperty(propertyInfo);
        property.Getter ??= propertyInfo.GetValue;
        property.Setter ??= propertyInfo.SetValue;
        return this;
    }
    public IOGraphGdmPropertyDescriptor HasProperty(Label label)
    {
        var propertyInfo = entityType.runtimeType!.GetProperty(label);
        if (propertyInfo is null)
        {
            throw new InvalidOperationException($"The property '{label}' does not exist on type {entityType.runtimeType!.Name}");
        }
        var property = entityType.GetProperty(propertyInfo);
        property.Getter ??= propertyInfo.GetValue;
        property.Setter ??= propertyInfo.SetValue;
        property.DeclaringType = new GdmTypeReference()
        {
            Definition = entityType,
        };
        return new GdmPropertyDescriptor(property);
    }
}
