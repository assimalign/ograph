using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmPropertyDescriptor
{
    IOGraphGdmPropertyDescriptor UsePropertyName(GdmName name);
    IOGraphGdmPropertyDescriptor UseType(IOGraphGdmType type);
    IOGraphGdmPropertyDescriptor UseType(Func<IOGraphGdmGraph, IOGraphGdmType> type);
    IOGraphGdmPropertyDescriptor UseGetter(GdmPropertyGetter getter);
    IOGraphGdmPropertyDescriptor UseSetter(GdmPropertySetter setter);
    IOGraphGdmPropertyDescriptor AddMeta(string key, string value);
    IOGraphGdmPropertyDescriptor IsRequired();
}
