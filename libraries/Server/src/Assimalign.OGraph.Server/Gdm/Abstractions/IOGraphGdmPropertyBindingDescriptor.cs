namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmPropertyBindingDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResolver"></typeparam>
    /// <returns></returns>
    IOGraphGdmPropertyBindingDescriptor UseResolver<TResolver>() 
        where TResolver : IOGraphGdmPropertyBindingResolver, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphGdmPropertyBindingDescriptor UseResolver(OGraphGdmPropertyBindingResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphGdmPropertyBindingDescriptor UseResolver(IOGraphGdmPropertyBindingResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMiddleware"></typeparam>
    /// <returns></returns>
    IOGraphGdmPropertyBindingDescriptor UseMiddleware<TMiddleware>()
        where TMiddleware : IOGraphGdmPropertyBindingMiddleware, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphGdmPropertyBindingDescriptor UseMiddleware(OGraphGdmPropertyBindingMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphGdmPropertyBindingDescriptor UseMiddleware(IOGraphGdmPropertyBindingMiddleware middleware);
}
