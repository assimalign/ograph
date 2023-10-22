using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class TypeCollection : HashSet<IOGraphType>,
    IOGraphTypeCollection
{
    public bool IsReadOnly { get; set; }
    bool ICollection<IOGraphType>.IsReadOnly => IsReadOnly;

    public bool TryAdd(IOGraphType type)
    {
       
       
        throw new NotImplementedException();
    }

    public bool TryGet(Name name, out IOGraphType type)
    {
        throw new NotImplementedException();
    }

    private void AssertReadOnly()
    {
        if (IsReadOnly)
        {
            throw new InvalidOperationException("The Collection is ReadOnly.");
        }
    }
}
