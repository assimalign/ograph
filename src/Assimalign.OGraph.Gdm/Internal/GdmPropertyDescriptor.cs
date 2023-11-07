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
            throw new ArgumentNullException(nameof(type));
        }
        property.Type = new GdmTypeReference()
        {
            Definition = type
        };
        return this;
    }
}
