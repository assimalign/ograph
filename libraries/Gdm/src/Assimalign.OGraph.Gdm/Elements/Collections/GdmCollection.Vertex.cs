using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Elements;

public class GdmVertexCollection : IOGraphGdmVertexCollection
{

    public void Add(GdmVertex vertex)
    {

    }
    IOGraphGdmVertex IOGraphGdmVertexCollection.this[GdmLabel label] => throw new NotImplementedException();

    int IOGraphGdmVertexCollection.Count => throw new NotImplementedException();

    bool IOGraphGdmVertexCollection.IsReadOnly => throw new NotImplementedException();

    void IOGraphGdmVertexCollection.Add(IOGraphGdmVertex vertex)
    {
        throw new NotImplementedException();
    }

    void IOGraphGdmVertexCollection.Clear()
    {
        throw new NotImplementedException();
    }

    IEnumerator<IOGraphGdmVertex> IEnumerable<IOGraphGdmVertex>.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    void IOGraphGdmVertexCollection.Remove(IOGraphGdmVertex vertex)
    {
        throw new NotImplementedException();
    }
}
