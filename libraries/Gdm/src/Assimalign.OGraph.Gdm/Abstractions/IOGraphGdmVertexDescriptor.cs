using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmVertexDescriptor
{
    IOGraphGdmVertexDescriptor HasLabel(GdmLabel label);
    IOGraphGdmVertexDescriptor HasEntityType(IOGraphGdmEntityType type);
    IOGraphGdmVertexDescriptor HasEntityType(Func<IOGraphGdmGraph, IOGraphGdmEntityType> type);
    IOGraphGdmVertexDescriptor HasOperation(IOGraphGdmOperation operation);
    IOGraphGdmVertexDescriptor HasOperation(Func<IOGraphGdmGraph, IOGraphGdmOperation> func);
    IOGraphGdmVertexDescriptor AddMeta(string key, string value);
}
