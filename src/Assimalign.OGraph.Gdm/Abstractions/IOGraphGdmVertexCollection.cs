using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmVertexCollection : ICollection<IOGraphGdmVertex>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmVertex this[Label label] { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="vertex"></param>
    /// <returns></returns>
    bool TryAddVertex(IOGraphGdmVertex vertex);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Label"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    bool TryGetVertex(Label label, out IOGraphGdmVertex? node);
}
