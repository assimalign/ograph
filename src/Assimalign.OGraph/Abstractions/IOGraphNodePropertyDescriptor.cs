using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphNodePropertyDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <returns></returns>
    IOGraphNodePropertyDescriptor UseType<TType>() where TType : IOGraphType, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphNodePropertyDescriptor UseMiddleware(IOGraphNodePropertyMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    IOGraphNodePropertyDescriptor UseMetadata(string key, object value);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphNodePropertyDescriptor UseResolver(IOGraphTypeResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphNodePropertyDescriptor UseResolver(OGraphTypeResolver resolver);
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IOGraphNodePropertyDescriptor<T>
{
    /// <summary>
    /// Overrides the name of the current property.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphNodePropertyDescriptor<T> UseName(Name name);
    /// <summary>
    /// Overrides the OGraph type for the current property.
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <returns></returns>
    IOGraphNodePropertyDescriptor<T> UseType<TType>() where TType : IOGraphType, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphNodePropertyDescriptor<T> UseMiddleware(IOGraphNodePropertyMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    IOGraphNodePropertyDescriptor<T> UseMetadata(string key, object value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphNodePropertyDescriptor<T> UseResolver(IOGraphTypeResolver resolver);

    /// <summary>
    /// Overrides or provides a Type Resolver for the current property.
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphNodePropertyDescriptor<T> UseResolver(OGraphTypeResolver<T> resolver);
}
