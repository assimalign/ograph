using System;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphVertexDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphVertexDescriptor UseMetadata(string key, object value);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns>The current descriptor.</returns>
    /// <remarks>
    /// The type being bound to the node should be a complex type.
    /// </remarks>
    IOGraphVertexDescriptor HasType(IOGraphType type);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <returns></returns>
    IOGraphVertexDescriptor HasType<TType>() where TType : IOGraphType, new();
    /// <summary>
    /// Configures a complex type to be bound to the node.
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphVertexDescriptor HasType(Action<IOGraphComplexTypeDescriptor> configure);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphVertexDescriptor HasType<T>(Action<IOGraphComplexTypeDescriptor<T>> configure) where T : class, new();
}