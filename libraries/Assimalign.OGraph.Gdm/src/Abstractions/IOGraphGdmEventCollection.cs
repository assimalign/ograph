using System;
using System.Collections.Generic;
using System.Text;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmEventCollection : IEnumerable<IOGraphGdmEvent>
{
    int Count { get; }
    bool IsReadOnly { get; }
    void Add(IOGraphGdmEvent item);
    void Remove(IOGraphGdmEvent item);
    void Clear();
}
