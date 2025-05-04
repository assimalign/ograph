using System.Collections.Generic;
using System.Collections;
using System.Runtime.InteropServices;

namespace Assimalign.OGraph.Gdm.Elements;

public class GdmMetadata : Dictionary<GdmMetaKey, IOGraphGdmMetaValue>,
    IOGraphGdmMetaCollection
{



    //public T GetOrAdd<T>(GdmMetaKey key)
    //{
    //    ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(this, key, out var existing);

    //    if (existing)
    //    {

    //    }
    //}

    T IOGraphGdmMetaCollection.GetValue<T>(GdmMetaKey key)
    {
        throw new System.NotImplementedException();
    }
}
