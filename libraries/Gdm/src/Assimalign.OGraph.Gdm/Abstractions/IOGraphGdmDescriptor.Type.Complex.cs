using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmComplexTypeDescriptor
{
    IOGraphGdmComplexTypeDescriptor HasLabel(Label label);
    IOGraphGdmComplexTypeDescriptor HasFunction(IOGraphGdmFunction function);
    IOGraphGdmComplexTypeDescriptor HasFunction(Func<IOGraphGdmComplexType, IOGraphGdmFunction> func);
    IOGraphGdmComplexTypeDescriptor HasProperty(IOGraphGdmProperty property);
    IOGraphGdmComplexTypeDescriptor HasProperty(Func<IOGraphGdmComplexType, IOGraphGdmProperty> func);
    IOGraphGdmComplexTypeDescriptor AddMeta(string key, string value);
}