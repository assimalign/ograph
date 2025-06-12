using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmNodeDescriptor
{
    IOGraphGdmNodeDescriptor HasLabel(GdmLabel label);
    IOGraphGdmNodeDescriptor HasEntityType(IOGraphGdmEntityType type);
    IOGraphGdmNodeDescriptor AddMeta(string key, string value);
}
