using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmPropertyDescriptor
{
    IOGraphGdmPropertyDescriptor UsePropertyName(GdmName name);
    IOGraphGdmPropertyDescriptor UseType(IOGraphGdmType type);
    IOGraphGdmPropertyDescriptor UseDirective(IOGraphGdmDirective directive);
    IOGraphGdmPropertyDescriptor UseGetter(GdmPropertyGetter getter);
    IOGraphGdmPropertyDescriptor UseSetter(GdmPropertySetter setter);
    IOGraphGdmPropertyDescriptor IsRequired();
}
