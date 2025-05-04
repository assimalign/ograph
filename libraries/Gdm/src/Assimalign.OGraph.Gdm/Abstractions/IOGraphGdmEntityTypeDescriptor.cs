using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmEntityTypeDescriptor : IOGraphGdmDescriptor<IOGraphGdmEntityType>
{
    IOGraphGdmEntityTypeDescriptor HasName(GdmName name);
    IOGraphGdmEntityTypeDescriptor HasKey(IOGraphGdmEntityKey key);
    IOGraphGdmEntityTypeDescriptor HasProperty(IOGraphGdmProperty property);
    IOGraphGdmEntityTypeDescriptor HasFunction(IOGraphGdmFunction function);
    IOGraphGdmEntityTypeDescriptor AddMeta(string key, string value);
    //IOGraphGdmEntityTypeDescriptor HasKey(Func<IOGraphGdmEntityType, IOGraphGdmEntityKey> key);
    //IOGraphGdmEntityTypeDescriptor HasProperty(Func<IOGraphGdmEntityType, IOGraphGdmProperty> property);
    //IOGraphGdmEntityTypeDescriptor HasFunction(Func<IOGraphGdmEntityType, IOGraphGdmFunction> function);
}