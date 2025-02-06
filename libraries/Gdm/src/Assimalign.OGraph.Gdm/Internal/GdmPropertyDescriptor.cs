namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmPropertyDescriptor : IOGraphGdmPropertyDescriptor
{
    private readonly GdmProperty property;

    public GdmPropertyDescriptor(GdmProperty property)
    {
        this.property = property;
    }

    public IOGraphGdmPropertyDescriptor IsComputed()
    {
        property.IsComputed = true;
        return this;
    }
    public IOGraphGdmPropertyDescriptor IsRequired()
    {
        property.IsNullable = false;
        return this;
    }
    public IOGraphGdmPropertyDescriptor UseGetter(GdmPropertyGetter getter)
    {
        if (getter is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(getter));
        }
        property.Getter = getter;
        return this;
    }
    public IOGraphGdmPropertyDescriptor UseSetter(GdmPropertySetter setter)
    {
        if (setter is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(setter));
        }
        property.Setter = setter;
        return this;
    }
    public IOGraphGdmPropertyDescriptor UseMeta(string key, string value)
    {
        property.Metadata.Add(key, value);
        return this;
    }
    public IOGraphGdmPropertyDescriptor UsePropertyName(Label label)
    {
        property.Label = label;
        return this;
    }
    public IOGraphGdmPropertyDescriptor UseType<TType>() where TType : IOGraphGdmType, new()
    {
        return UseType(new TType());
    }
    public IOGraphGdmPropertyDescriptor UseType(IOGraphGdmType type)
    {
        if (type is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(type));
        }
        property.Type = new GdmTypeReference()
        {
            Definition = type
        };
        return this;
    }
}