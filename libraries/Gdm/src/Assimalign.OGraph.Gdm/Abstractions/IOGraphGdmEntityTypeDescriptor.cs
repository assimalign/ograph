using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmEntityTypeDescriptor 
{
    IOGraphGdmEntityTypeDescriptor HasName(GdmName name);
    IOGraphGdmEntityTypeDescriptor HasKey(IOGraphGdmEntityKey key);
    IOGraphGdmEntityTypeDescriptor HasProperty(IOGraphGdmProperty property);
    IOGraphGdmEntityTypeDescriptor HasFunction(IOGraphGdmFunction function);
}