using System.Collections.Generic;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphVertexCollection : ICollection<IOGraphVertex>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphVertex this[Label label] { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="vertex"></param>
    /// <returns></returns>
    bool TryAddVertex(IOGraphVertex vertex);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Label"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    bool TryGetVertex(Label label, out IOGraphVertex? node);
}
