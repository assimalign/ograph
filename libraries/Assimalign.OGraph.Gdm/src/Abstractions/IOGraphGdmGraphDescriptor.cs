using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmGraphDescriptor 
{
    IOGraphGdmGraphDescriptor AddType(IOGraphGdmType type);
    IOGraphGdmGraphDescriptor AddNode(IOGraphGdmNode node);
    IOGraphGdmGraphDescriptor AddEdge(IOGraphGdmEdge edge);
    IOGraphGdmGraphDescriptor AddQuery(IOGraphGdmOperation operation);
    IOGraphGdmGraphDescriptor AddCommand(IOGraphGdmOperation operation);
    IOGraphGdmGraphDescriptor AddEvent(IOGraphGdmOperation operation);
    IOGraphGdmGraphDescriptor AddSubscriber(IOGraphGdmSubscriber subscriber);
    IOGraphGdmGraphDescriptor AddMeta(string key, string value);
}
