using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            GdmThrowHelper.ThrowArgumentNullException(nameof(getter));
        }
        property.Getter = getter;
        return this;
    }
    public IOGraphGdmPropertyDescriptor<T> UseSetter(GdmPropertySetter setter)
    {
        if (setter is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(setter));
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
            throw new ArgumentNullException(nameof(type));
        }
        property.Type = new GdmTypeReference()
        {
            Definition = type
        };
        return this;
    }
}
