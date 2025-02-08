using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmPropertyDescriptor<T>
{
    IOGraphGdmPropertyDescriptor<T> UsePropertyName(Label label);
    IOGraphGdmPropertyDescriptor<T> UseType(IOGraphGdmType type);
    IOGraphGdmPropertyDescriptor<T> UseType(Func<IOGraphGdmGraph, IOGraphGdmType> type);
    IOGraphGdmPropertyDescriptor<T> UseGetter(GdmPropertyGetter getter);
    IOGraphGdmPropertyDescriptor<T> UseSetter(GdmPropertySetter setter);
    IOGraphGdmPropertyDescriptor<T> AddMeta(string key, string value);
    IOGraphGdmPropertyDescriptor<T> IsRequired();
}