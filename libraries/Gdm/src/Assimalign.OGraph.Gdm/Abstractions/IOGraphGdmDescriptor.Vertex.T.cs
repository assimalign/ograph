using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmVertexDescriptor<T> where T : class, new()
{
    IOGraphGdmVertexDescriptor<T> HasLabel(GdmLabel label);
    IOGraphGdmVertexDescriptor<T> HasEntityType(IOGraphGdmEntityType type);
    IOGraphGdmVertexDescriptor<T> HasEntityType(Func<IOGraphGdmGraph, IOGraphGdmEntityType> func);
    IOGraphGdmVertexDescriptor<T> HasOperation(IOGraphGdmOperation operation);
    IOGraphGdmVertexDescriptor<T> HasOperation(Func<IOGraphGdmGraph, IOGraphGdmOperation> func);
    IOGraphGdmVertexDescriptor<T> AddMeta(string key, string value);
}
