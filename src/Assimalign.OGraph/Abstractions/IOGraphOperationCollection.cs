using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

public interface IOGraphOperationCollection : ICollection<IOGraphOperation>
{
    
    bool TryGetOperation(Name name, out IOGraphOperation operation);
}
