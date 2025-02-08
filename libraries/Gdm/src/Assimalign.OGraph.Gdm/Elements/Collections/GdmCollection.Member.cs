using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Elements;

public class GdmMemberCollection : List<GdmMember>, IOGraphGdmMemberCollection
{
    public bool IsReadOnly => throw new NotImplementedException();
    public GdmMember this[Label label] => (this as IEnumerable<GdmMember>).Find(label);

    IOGraphGdmMember IOGraphGdmMemberCollection.this[Label label] => this[label];
    void ICollection<IOGraphGdmMember>.Add(IOGraphGdmMember item)
    {
        base.Add(AssertType(item));
    }
    void ICollection<IOGraphGdmMember>.Clear()
    {
        base.Clear();
    }
    bool ICollection<IOGraphGdmMember>.Remove(IOGraphGdmMember item)
    {
        return base.Remove(AssertType(item));
    }
    bool ICollection<IOGraphGdmMember>.Contains(IOGraphGdmMember item)
    {
        return base.Contains(AssertType(item));
    }
    void ICollection<IOGraphGdmMember>.CopyTo(IOGraphGdmMember[] array, int arrayIndex)
    {
        base.CopyTo(array.Select(p=> AssertType(p)).ToArray(), arrayIndex);
    }
    IEnumerator<IOGraphGdmMember> IEnumerable<IOGraphGdmMember>.GetEnumerator()
    {
        return GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    private GdmMember AssertType(IOGraphGdmMember item)
    {
        if (item is not GdmMember member)
        {
            throw new ArgumentException("Item must be of type GdmMember");
        }
        return member;
    }
}
