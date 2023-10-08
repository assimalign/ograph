using System;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphNodeDescriptor
{
    /// <summary>
    /// Sets the label of the node
    /// </summary>
    /// <param name="label"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphNodeDescriptor UseLabel(Name name);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphNodeDescriptor UseMetadata(string key, object value);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns>The current descriptor.</returns>
    /// <remarks>
    /// The type being binded to the node should be a complex type.
    /// </remarks>
    IOGraphNodeDescriptor UseType(IOGraphType type);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <returns></returns>
    IOGraphNodeDescriptor UseType<TType>() where TType : IOGraphType, new();
    /// <summary>
    /// Configures a complex type to be binded to the node.
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphNodeDescriptor UseType(Action<IOGraphComplexTypeDescriptor> configure);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphNodeDescriptor UseType<T>(Action<IOGraphComplexTypeDescriptor<T>> configure);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphEdgeDescriptor AddEdge(Name name);
}


