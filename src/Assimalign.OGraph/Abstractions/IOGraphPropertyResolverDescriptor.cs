using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphPropertyResolverDescriptor<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphPropertyResolverDescriptor<T> UseMiddleware(OGraphPropertyMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphPropertyResolverDescriptor<T> UseMiddleware(IOGraphPropertyMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMiddleware"></typeparam>
    /// <returns></returns>
    IOGraphPropertyResolverDescriptor<T> UseMiddleware<TMiddleware>() where TMiddleware : IOGraphPropertyMiddleware, new();
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResolver"></typeparam>
    /// <returns></returns>
    IOGraphPropertyResolverDescriptor<T> UseResolver<TResolver>() where TResolver : IOGraphPropertyResolver, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphPropertyResolverDescriptor<T> UseResolver(IOGraphPropertyResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphPropertyResolverDescriptor<T> UseResolver(OGraphPropertyResolver resolver);
}
