using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmNodeCollection : IEnumerable<IOGraphGdmNode>
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
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphGdmNode this[GdmName name] { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vertex"></param>
    void Add(IOGraphGdmNode vertex);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vertex"></param>
    void Remove(IOGraphGdmNode vertex);

    /// <summary>
    /// 
    /// </summary>
    void Clear();
}
