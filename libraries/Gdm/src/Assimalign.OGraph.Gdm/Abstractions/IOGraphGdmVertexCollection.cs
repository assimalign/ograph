using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmVertexCollection : IEnumerable<IOGraphGdmVertex>
{
    /// <summary>
    /// 
    /// </summary>
    int Count { get; }

    /// <summary>
    /// 
    /// </summary>
    bool IsReadOnly { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmVertex this[GdmLabel label] { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vertex"></param>
    void Add(IOGraphGdmVertex vertex);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vertex"></param>
    void Remove(IOGraphGdmVertex vertex);

    /// <summary>
    /// 
    /// </summary>
    void Clear();
}
