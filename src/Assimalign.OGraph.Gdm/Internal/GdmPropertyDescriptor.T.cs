using System;
using System.Collections.Generic;
using System.Diagnostics;
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

    public IOGraphGdmPropertyDescriptor<T> UseType<TType>() where TType : IOGraphGdmType, new()
    {
        return UseType(new TType());
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
