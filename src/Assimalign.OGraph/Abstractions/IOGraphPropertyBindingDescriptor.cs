using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphPropertyBindingDescriptor<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphPropertyBindingDescriptor<T> UseMiddleware(OGraphPropertyMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphPropertyBindingDescriptor<T> UseMiddleware(IOGraphPropertyBindingMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMiddleware"></typeparam>
    /// <returns></returns>
    IOGraphPropertyBindingDescriptor<T> UseMiddleware<TMiddleware>() where TMiddleware : IOGraphPropertyBindingMiddleware, new();
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResolver"></typeparam>
    /// <returns></returns>
    IOGraphPropertyBindingDescriptor<T> UseResolver<TResolver>() where TResolver : IOGraphPropertyBindingResolver, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphPropertyBindingDescriptor<T> UseResolver(IOGraphPropertyBindingResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphPropertyBindingDescriptor<T> UseResolver(OGraphPropertyResolver resolver);
}
