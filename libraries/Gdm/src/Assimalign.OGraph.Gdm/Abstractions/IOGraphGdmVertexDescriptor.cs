using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmVertexDescriptor
{
    IOGraphGdmVertexDescriptor HasLabel(GdmLabel label);
    IOGraphGdmVertexDescriptor HasEntityType(IOGraphGdmEntityType type);
    IOGraphGdmVertexDescriptor HasOperation(IOGraphGdmOperation operation);
    IOGraphGdmVertexDescriptor AddMeta(string key, string value);
}
