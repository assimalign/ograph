using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmComplexTypeDescriptor 
{
    IOGraphGdmComplexTypeDescriptor HasName(GdmName name);
    IOGraphGdmComplexTypeDescriptor HasFunction(IOGraphGdmFunction function);
    IOGraphGdmComplexTypeDescriptor HasProperty(IOGraphGdmProperty property);
    IOGraphGdmComplexTypeDescriptor AddMeta(string key, string value);
}