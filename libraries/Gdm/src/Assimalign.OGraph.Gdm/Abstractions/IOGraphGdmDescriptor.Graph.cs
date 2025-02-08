using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmGraphDescriptor
{
    IOGraphGdmGraphDescriptor AddType(IOGraphGdmType type);
    IOGraphGdmGraphDescriptor AddType(Func<IOGraphGdmGraph, IOGraphGdmType> configure);
    IOGraphGdmGraphDescriptor AddVertex(IOGraphGdmVertex vertex);
    IOGraphGdmGraphDescriptor AddVertex(Func<IOGraphGdmGraph, IOGraphGdmVertex> configure);
    IOGraphGdmGraphDescriptor AddEdge(IOGraphGdmEdge edge);
    IOGraphGdmGraphDescriptor AddEdge(Func<IOGraphGdmGraph, IOGraphGdmEdge> configure);
    IOGraphGdmGraphDescriptor AddMeta(string key, string value);
}
