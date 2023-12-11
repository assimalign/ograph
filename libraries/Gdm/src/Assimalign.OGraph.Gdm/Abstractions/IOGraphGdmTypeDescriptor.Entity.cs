using System;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmEntityTypeDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmEntityTypeDescriptor HasLabel(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    IOGraphGdmEntityTypeDescriptor HasRuntimeType(Type type);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IOGraphGdmEntityTypeDescriptor HasRuntimeType<T>() where T : class, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmEntityTypeDescriptor HasKey(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmEntityTypeDescriptor Ignore(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmPropertyDescriptor HasProperty(Label label);
}
