using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmGraphCollection : IEnumerable<IOGraphGdmGraph>
{
    int Count { get; }
    bool IsReadOnly { get; }
    void Add(IOGraphGdmGraph item);
    void Remove(IOGraphGdmGraph item);
    void Clear();
}