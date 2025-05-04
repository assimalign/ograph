using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmEdgeCollection: IEnumerable<IOGraphGdmEdge>
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
    //IOGraphGdmEdge this[GdmLabel label] { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="edge"></param>
    void Add(IOGraphGdmEdge edge);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="edge"></param>
    void Remove(IOGraphGdmEdge edge);

    /// <summary>
    /// 
    /// </summary>
    void Clear();
}