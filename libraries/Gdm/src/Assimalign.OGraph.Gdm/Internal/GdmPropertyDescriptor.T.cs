namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmPropertyDescriptor<T> : IOGraphGdmPropertyDescriptor<T>
{
    private readonly GdmProperty property;

    public GdmPropertyDescriptor(GdmProperty property)
    {
        this.property = property;
    }

    public IOGraphGdmPropertyDescriptor<T> IsComputed()
    {
        property.IsComputed = true;
        return this;
    }

    public IOGraphGdmPropertyDescriptor<T> IsRequired()
    {
        property.IsNullable = false;
        return this;
    }

    public IOGraphGdmPropertyDescriptor<T> UseGetter(GdmPropertyGetter getter)
    {
        if (getter is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(getter));
        }
        property.Getter = getter;
        return this;
    }
    public IOGraphGdmPropertyDescriptor<T> UseSetter(GdmPropertySetter setter)
    {
        if (setter is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(setter));
        }
        property.Setter = setter;
        return this;
    }
    public IOGraphGdmPropertyDescriptor<T> UseMetadata(Label key, object value)
    {
        property.Metadata.Add(key, value);
        return this;
    }
    public IOGraphGdmPropertyDescriptor<T> UsePropertyName(Label label)
    {
        property.Label = label;
        return this;
    }
    public IOGraphGdmPropertyDescriptor<T> UseType<TGdmType>() where TGdmType : IOGraphGdmType, new()
    {
        return UseType(new TGdmType());
    }
    public IOGraphGdmPropertyDescriptor<T> UseType(IOGraphGdmType type)
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