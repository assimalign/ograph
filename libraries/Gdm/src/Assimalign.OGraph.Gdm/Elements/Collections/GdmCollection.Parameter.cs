using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Elements;

[DebuggerDisplay("Count = {Count}")]
public class GdmParameterCollection : IOGraphGdmParameterCollection
{
    int IOGraphGdmParameterCollection.Count => throw new NotImplementedException();

    bool IOGraphGdmParameterCollection.IsReadOnly => throw new NotImplementedException();

    void IOGraphGdmParameterCollection.Add(IOGraphGdmParameter item)
    {
        throw new NotImplementedException();
    }

    void IOGraphGdmParameterCollection.Clear()
    {
        throw new NotImplementedException();
    }

    IEnumerator<IOGraphGdmParameter> IEnumerable<IOGraphGdmParameter>.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    void IOGraphGdmParameterCollection.Remove(IOGraphGdmParameter item)
    {
        throw new NotImplementedException();
    }
}
