namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmPropertyDescriptor<T>
{
    IOGraphGdmPropertyDescriptor<T> UsePropertyName(Label label);
    IOGraphGdmPropertyDescriptor<T> UseType<TGdmType>() where TGdmType : IOGraphGdmType, new();
    IOGraphGdmPropertyDescriptor<T> UseType(IOGraphGdmType type);
    //IOGraphGdmPropertyDescriptor<T> UseTypeReference(Label label);
    IOGraphGdmPropertyDescriptor<T> UseMetadata(Label key, object value);
    IOGraphGdmPropertyDescriptor<T> IsComputed();
    IOGraphGdmPropertyDescriptor<T> IsRequired();
    IOGraphGdmPropertyDescriptor<T> UseGetter(GdmPropertyGetter getter);
    IOGraphGdmPropertyDescriptor<T> UseSetter(GdmPropertySetter setter);
}