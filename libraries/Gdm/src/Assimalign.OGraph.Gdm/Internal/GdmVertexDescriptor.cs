using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmVertexDescriptor : IOGraphGdmVertexDescriptor
{
    private readonly GdmVertex vertex;

    public GdmVertexDescriptor(GdmVertex vertex)
    {
        this.vertex = vertex;
    }

    public IOGraphGdmVertexDescriptor HasLabel(Label label)
    {
        vertex.label = label;
        return this;
    }

    public IOGraphGdmVertexDescriptor HasType(IOGraphGdmEntityType type)
    {
        if (type is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(type));
        }
        vertex.type = new GdmTypeReference()
        {
            Definition = type
        };
        return this;
    }

    public IOGraphGdmVertexDescriptor HasType<TGdmType>() where TGdmType : IOGraphGdmEntityType, new()
    {
        return HasType(new TGdmType());
    }
}
