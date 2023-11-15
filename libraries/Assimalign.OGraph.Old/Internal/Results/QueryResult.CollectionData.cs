using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal.Results;

internal class QueryCollectionData : IOGraphQueryCollectionData
{
    public IOGraphQueryEdgeData Edges => throw new NotImplementedException();

    public int Count => throw new NotImplementedException();

    public bool IsReadOnly => throw new NotImplementedException();

    public void Add(IOGraphQueryObjectData item)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(IOGraphQueryObjectData item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(IOGraphQueryObjectData[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<IOGraphQueryObjectData> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public bool Remove(IOGraphQueryObjectData item)
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
