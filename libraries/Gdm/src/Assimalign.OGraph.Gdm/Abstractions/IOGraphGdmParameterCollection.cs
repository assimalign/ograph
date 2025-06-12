using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmParameterCollection : IEnumerable<IOGraphGdmParameter>
{
    int Count { get; }
    bool IsReadOnly { get; }
    void Add(IOGraphGdmParameter item);
    void Remove(IOGraphGdmParameter item);
    void Clear();
}