using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmGraphDescriptor : IOGraphGdmDescriptor<IOGraphGdmGraph>
{
    IOGraphGdmGraphDescriptor AddType(IOGraphGdmType type);
    IOGraphGdmGraphDescriptor AddVertex(IOGraphGdmVertex vertex);
    IOGraphGdmGraphDescriptor AddEdge(IOGraphGdmEdge edge);
    IOGraphGdmGraphDescriptor AddMeta(string key, string value);
    //IOGraphGdmGraphDescriptor AddType(Func<IOGraphGdmGraph, IOGraphGdmType> configure);
    //IOGraphGdmGraphDescriptor AddVertex(Func<IOGraphGdmGraph, IOGraphGdmVertex> configure);
   // IOGraphGdmGraphDescriptor AddEdge(Func<IOGraphGdmGraph, IOGraphGdmEdge> configure);
}
