using System;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.OGraph.Internal;

internal readonly struct OGraphCollection : IEnumerable<OGraphCollectionItem>
{

    public OGraphCollection(OGraphCollectionItem[] items)
    {
        Items = items;
    }

    public OGraphCollectionItem[] Items { get; }

    public IEnumerator<OGraphCollectionItem> GetEnumerator() => (IEnumerator<OGraphCollectionItem>)Items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}