using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public sealed class OGraphQueryCollection : Dictionary<string, QueryValue>, 
    IOGraphQueryCollection
{

    public OGraphQueryCollection()
    {
        
    }

    public OGraphQueryCollection(Dictionary<string, QueryValue> collection) 
        : base(collection, StringComparer.InvariantCultureIgnoreCase)
    {
        
    }

}
