using System;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphComplexTypeDescriptor
{ 
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    IOGraphComplexTypeDescriptor HasUnderlyingType(Type type);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IOGraphComplexTypeDescriptor HasUnderlyingType<T>() where T : class, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor HasProperty(Name name);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">The property name to ignore.</param>
    /// <returns></returns>
    IOGraphComplexTypeDescriptor Ignore(Name name);
}
