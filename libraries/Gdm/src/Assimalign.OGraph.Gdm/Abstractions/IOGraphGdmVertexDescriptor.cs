using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmVertexDescriptor : IOGraphGdmDescriptor<IOGraphGdmVertex>
{
    IOGraphGdmVertexDescriptor HasLabel(GdmLabel label);
    IOGraphGdmVertexDescriptor HasEntityType(IOGraphGdmEntityType type);
    IOGraphGdmVertexDescriptor HasOperation(IOGraphGdmOperation operation);
    IOGraphGdmVertexDescriptor AddMeta(string key, string value);
   // IOGraphGdmVertexDescriptor HasEntityType(Func<IOGraphGdmGraph, IOGraphGdmEntityType> type);
    //IOGraphGdmVertexDescriptor HasOperation(Func<IOGraphGdmGraph, IOGraphGdmOperation> func);
}
