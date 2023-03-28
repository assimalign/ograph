using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

public sealed class OGraphQueryCollection : Dictionary<string, QueryValue>, 
    IOGraphQueryCollection
{

    public OGraphQueryCollection() { }

    public OGraphQueryCollection(Dictionary<string, QueryValue> collection) 
        : base(collection, StringComparer.InvariantCultureIgnoreCase)
    {  
    }
}
