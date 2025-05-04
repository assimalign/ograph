using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmTypeCollection : IEnumerable<IOGraphGdmType>
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
    IOGraphGdmType this[GdmName name] { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    void Add(IOGraphGdmType type);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    void Remove(IOGraphGdmType type);
}