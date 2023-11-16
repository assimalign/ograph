namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;

public interface IOGraphPropertyBindingDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResolver"></typeparam>
    /// <returns></returns>
    IOGraphPropertyBindingDescriptor UseResolver<TResolver>() 
        where TResolver : IOGraphPropertyBindingResolver, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphPropertyBindingDescriptor UseResolver(OGraphPropertyBindingResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphPropertyBindingDescriptor UseResolver(IOGraphPropertyBindingResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMiddleware"></typeparam>
    /// <returns></returns>
    IOGraphPropertyBindingDescriptor UseMiddleware<TMiddleware>()
        where TMiddleware : IOGraphPropertyBindingMiddleware, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphPropertyBindingDescriptor UseMiddleware(OGraphPropertyBindingMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphPropertyBindingDescriptor UseMiddleware(IOGraphPropertyBindingMiddleware middleware);
}
