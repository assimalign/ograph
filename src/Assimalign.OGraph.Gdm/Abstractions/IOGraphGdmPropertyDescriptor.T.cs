namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmPropertyDescriptor<T>
{
    IOGraphGdmPropertyDescriptor<T> UsePropertyName(Label label);
    IOGraphGdmPropertyDescriptor<T> UseType<TType>() where TType : IOGraphGdmType, new();
    IOGraphGdmPropertyDescriptor<T> UseType(IOGraphGdmType type);
}