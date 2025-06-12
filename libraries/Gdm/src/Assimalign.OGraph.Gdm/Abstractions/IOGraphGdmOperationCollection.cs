using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmOperationCollection : IEnumerable<IOGraphGdmOperation>
{
    int Count { get; }
    bool IsReadOnly { get; }
    void Add(IOGraphGdmOperation item);
    void Remove(IOGraphGdmOperation item);
    void Clear();
}