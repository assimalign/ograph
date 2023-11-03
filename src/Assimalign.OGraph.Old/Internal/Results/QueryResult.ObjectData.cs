using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal.Results;

internal class QueryObjectData : Dictionary<Label, IOGraphQueryDataItem>,
    IOGraphQueryObjectData
{
    public IOGraphQueryEdgeData Edges => throw new NotImplementedException();

    public int Count => throw new NotImplementedException();

    public bool IsReadOnly => throw new NotImplementedException();

    public void Add(KeyValuePair<Label, IOGraphQueryDataItem> item)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(KeyValuePair<Label, IOGraphQueryDataItem> item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(KeyValuePair<Label, IOGraphQueryDataItem>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<KeyValuePair<Label, IOGraphQueryDataItem>> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public bool Remove(KeyValuePair<Label, IOGraphQueryDataItem> item)
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
