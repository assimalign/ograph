using System;

namespace Assimalign.OGraph;

/// <summary>
/// A raw descriptor for defining an edge.
/// </summary>
public interface IOGraphEdgeDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseTarget<TVertex>() 
        where TVertex : IOGraphVertex, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseMetadata(string key, object value);
}