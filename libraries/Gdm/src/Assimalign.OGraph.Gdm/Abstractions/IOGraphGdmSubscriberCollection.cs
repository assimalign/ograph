using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmSubscriberCollection : IEnumerable<IOGraphGdmSubscriber>
{
    int Count { get; }
    bool IsReadOnly { get; }
    void Add(IOGraphGdmSubscriber item);
    void Remove(IOGraphGdmSubscriber item);
    void Clear();
}
