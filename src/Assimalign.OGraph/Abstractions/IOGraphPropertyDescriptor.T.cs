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
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor<T> UseMetadata(string key, object value);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor<T> UseType(IOGraphType type);
    /// <summary>
    /// Overrides the OGraph type for the current property.
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <returns></returns>
    IOGraphPropertyDescriptor<T> UseType<TType>() where TType : IOGraphType, new();    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor<T> UseType(Action<IOGraphComplexTypeDescriptor<T>> action);
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
    /// <typeparam name="TResolver"></typeparam>
    /// <returns></returns>
    IOGraphPropertyDescriptor<T> UseResolver<TResolver>() where TResolver : IOGraphPropertyResolver, new();
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
}