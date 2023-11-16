using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            GdmThrowHelper.ThrowArgumentNullException(nameof(getter));
        }
        property.Getter = getter;
        return this;
    }
    public IOGraphGdmPropertyDescriptor UseSetter(GdmPropertySetter setter)
    {
        if (setter is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(setter));
        }
        property.Setter = setter;
        return this;
    }

    public IOGraphGdmPropertyDescriptor UseMetadata(Label key, object value)
    {
        property.Metadata.Add(key, value);
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
            GdmThrowHelper.ThrowArgumentNullException(nameof(type));
        }
        property.Type = new GdmTypeReference()
        {
            Definition = type
        };
        return this;
    }
}
