using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmComplexTypeDescriptor : IOGraphGdmDescriptor<IOGraphGdmComplexType>
{
    IOGraphGdmComplexTypeDescriptor HasName(GdmName name);
    IOGraphGdmComplexTypeDescriptor HasFunction(IOGraphGdmFunction function);
    IOGraphGdmComplexTypeDescriptor HasProperty(IOGraphGdmProperty property);
    IOGraphGdmComplexTypeDescriptor AddMeta(string key, string value);
    //IOGraphGdmComplexTypeDescriptor HasFunction(Func<IOGraphGdmComplexType, IOGraphGdmFunction> func);
    //IOGraphGdmComplexTypeDescriptor HasProperty(Func<IOGraphGdmComplexType, IOGraphGdmProperty> func);
}