using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmEntityTypeDescriptor
{
    IOGraphGdmEntityTypeDescriptor HasLabel(Label label);
    IOGraphGdmEntityTypeDescriptor HasKey(IOGraphGdmEntityKey key);
    IOGraphGdmEntityTypeDescriptor HasKey(Func<IOGraphGdmEntityType, IOGraphGdmEntityKey> key);
    IOGraphGdmEntityTypeDescriptor HasProperty(IOGraphGdmProperty property);
    IOGraphGdmEntityTypeDescriptor HasProperty(Func<IOGraphGdmEntityType, IOGraphGdmProperty> property);
    IOGraphGdmEntityTypeDescriptor HasFunction(IOGraphGdmFunction function);
    IOGraphGdmEntityTypeDescriptor HasFunction(Func<IOGraphGdmEntityType, IOGraphGdmFunction> function);
    IOGraphGdmEntityTypeDescriptor AddMeta(string key, string value);
}