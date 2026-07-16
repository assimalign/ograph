using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmNodeDescriptor
{
    IOGraphGdmNodeDescriptor HasLabel(GdmLabel label);
    IOGraphGdmNodeDescriptor HasEntityType(IOGraphGdmEntityType type);
    IOGraphGdmNodeDescriptor HasOperation(IOGraphGdmOperation operation);
    IOGraphGdmNodeDescriptor AddMeta(string key, string value);
}
