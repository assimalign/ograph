namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmPropertyDescriptor<T>
{
    IOGraphGdmPropertyDescriptor<T> UsePropertyName(Label label);
    IOGraphGdmPropertyDescriptor<T> UseType<TGdmType>() where TGdmType : IOGraphGdmType, new();
    IOGraphGdmPropertyDescriptor<T> UseType(IOGraphGdmType type);
    IOGraphGdmPropertyDescriptor<T> UseMeta(string key, string value);
    IOGraphGdmPropertyDescriptor<T> UseGetter(GdmPropertyGetter getter);
    IOGraphGdmPropertyDescriptor<T> UseSetter(GdmPropertySetter setter);
    IOGraphGdmPropertyDescriptor<T> IsRequired();
}