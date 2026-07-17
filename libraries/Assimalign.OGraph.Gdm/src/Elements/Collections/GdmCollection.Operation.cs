using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Elements;

public class GdmOperationCollection : IOGraphGdmOperationCollection
{
    int IOGraphGdmOperationCollection.Count => throw new NotImplementedException();

    bool IOGraphGdmOperationCollection.IsReadOnly => throw new NotImplementedException();

    void IOGraphGdmOperationCollection.Add(IOGraphGdmOperation item)
    {
        throw new NotImplementedException();
    }

    void IOGraphGdmOperationCollection.Clear()
    {
        throw new NotImplementedException();
    }

    IEnumerator<IOGraphGdmOperation> IEnumerable<IOGraphGdmOperation>.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    void IOGraphGdmOperationCollection.Remove(IOGraphGdmOperation item)
    {
        throw new NotImplementedException();
    }
}
