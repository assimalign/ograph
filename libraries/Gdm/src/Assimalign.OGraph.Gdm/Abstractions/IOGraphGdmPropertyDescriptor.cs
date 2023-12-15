namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmPropertyDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmPropertyDescriptor UsePropertyName(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TGdmType"></typeparam>
    /// <returns></returns>
    IOGraphGdmPropertyDescriptor UseType<TGdmType>() where TGdmType : IOGraphGdmType, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    IOGraphGdmPropertyDescriptor UseType(IOGraphGdmType type);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label">The type name to bind to property.</param>
    /// <returns></returns>
    //IOGraphGdmPropertyDescriptor UseTypeReference(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    IOGraphGdmPropertyDescriptor UseMetadata(Label key, object value);
    IOGraphGdmPropertyDescriptor IsComputed();
    IOGraphGdmPropertyDescriptor IsRequired();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="getter"></param>
    /// <returns></returns>
    IOGraphGdmPropertyDescriptor UseGetter(GdmPropertyGetter getter);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="setter"></param>
    /// <returns></returns>
    IOGraphGdmPropertyDescriptor UseSetter(GdmPropertySetter setter);
}
