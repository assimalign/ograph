using System;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmComplexTypeDescriptor
{
    /// <summary>
    /// Adds a ty
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmComplexTypeDescriptor HasLabel(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    IOGraphGdmComplexTypeDescriptor HasRuntimeType(Type type);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IOGraphGdmComplexTypeDescriptor HasRuntimeType<T>() where T : class, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphGdmPropertyDescriptor HasProperty(Label name);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">The property name to ignore.</param>
    /// <returns></returns>
    IOGraphGdmComplexTypeDescriptor Ignore(Label name);
}