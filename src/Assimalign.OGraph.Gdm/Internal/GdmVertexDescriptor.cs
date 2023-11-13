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
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor HasType(Type type)
    {
        throw new NotImplementedException();
    }



    public IOGraphGdmVertexDescriptor HasType<T>(Action<IOGraphGdmEntityTypeDescriptor<T>> configure) where T : class, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor HasType(IOGraphGdmEntityType type)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor HasType<TGdmType>() where TGdmType : IOGraphGdmEntityType, new()
    {
        throw new NotImplementedException();
    }
}
