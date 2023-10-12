using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Internal;

internal class OGraphOperationCollection : List<IOGraphOperation>,
    IOGraphOperationCollection
{
    public bool TryGetOperation(Name name, out IOGraphOperation operation)
    {
        throw new NotImplementedException();
    }
}
