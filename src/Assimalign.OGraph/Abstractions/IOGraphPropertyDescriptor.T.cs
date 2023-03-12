using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IOGraphPropertyDescriptor<T>
{
    /// <summary>
    /// Overrides the name of the current property.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor<T> UseName(Name name);
    /// <summary>
    /// Overrides the OGraph type for the current property.
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <returns></returns>
    IOGraphPropertyDescriptor<T> UseType<TType>() where TType : IOGraphType, new();    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor<T> UseMiddleware(OGraphPropertyMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor<T> UseMiddleware(IOGraphPropertyMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMiddleware"></typeparam>
    /// <returns></returns>
    IOGraphPropertyDescriptor<T> UseMiddleware<TMiddleware>() where TMiddleware : IOGraphPropertyMiddleware, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor<T> UseMetadata(string key, object value);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor<T> UseResolver(IOGraphPropertyResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor<T> UseResolver(OGraphPropertyResolver resolver);
    /// <summary>
    /// Overrides or provides a Type Resolver for the current property.
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    //IOGraphPropertyDescriptor<T> UseResolver(OGraphPropertyResolver<T> resolver);
}