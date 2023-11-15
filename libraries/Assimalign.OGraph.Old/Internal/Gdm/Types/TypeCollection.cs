using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class TypeCollection : HashSet<IOGraphType>,
    IOGraphTypeCollection
{
    public IOGraphType this[Label name]
    {
        get
        {
            if (TryGetType(name, out var type))
            {
                return type;
            }

            throw new Exception();
        }
    }

    public bool IsReadOnly { get; set; }
    bool ICollection<IOGraphType>.IsReadOnly => IsReadOnly;

    public bool TryAddType(IOGraphType type)
    {
       
        throw new NotImplementedException();
    }

    public bool TryGetType(Label name, out IOGraphType? type)
    {
        type = this.FirstOrDefault(p => p.Label == name);

        return type is not null;
    }

    private void AssertReadOnly()
    {
        if (IsReadOnly)
        {
            throw new InvalidOperationException("The Collection is ReadOnly.");
        }
    }
}
