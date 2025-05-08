using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmGraphDescriptor 
{
    IOGraphGdmGraphDescriptor AddType(IOGraphGdmType type);
    IOGraphGdmGraphDescriptor AddVertex(IOGraphGdmVertex vertex);
    IOGraphGdmGraphDescriptor AddEdge(IOGraphGdmEdge edge);
    IOGraphGdmGraphDescriptor AddMeta(string key, string value);
}
