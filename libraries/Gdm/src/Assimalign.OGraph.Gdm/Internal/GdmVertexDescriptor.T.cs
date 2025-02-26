using System;

namespace Assimalign.OGraph.Gdm.Internal;

using Assimalign.OGraph.Gdm.Elements;

internal class GdmVertexDescriptor<T> : IOGraphGdmVertexDescriptor<T>
    where T : class, new()
{
    private readonly GdmVertex<T> vertex;

    public GdmVertexDescriptor(GdmVertex<T> vertex)
    {
        this.vertex = vertex;
    }

    public IOGraphGdmVertexDescriptor<T> AddMeta(string key, string value)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor<T> HasOperation(IOGraphGdmOperation operation)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor<T> HasOperation(Func<IOGraphGdmGraph, IOGraphGdmOperation> func)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor<T> HasEntityType(IOGraphGdmEntityType type)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor<T> HasEntityType(Func<IOGraphGdmGraph, IOGraphGdmEntityType> func)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor<T> HasLabel(GdmLabel label)
    {
        throw new NotImplementedException();
    }
}
