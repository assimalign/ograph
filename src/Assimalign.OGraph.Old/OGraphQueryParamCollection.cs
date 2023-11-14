using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

public sealed class OGraphQueryParamCollection : Dictionary<string, QueryValue>, 
    IOGraphQueryParamCollection
{
    public OGraphQueryParamCollection() { }

    public OGraphQueryParamCollection(Dictionary<string, QueryValue> collection) 
        : base(collection, StringComparer.InvariantCultureIgnoreCase) { }
}
