using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

public interface IOGraphOperationCollection : ICollection<IOGraphOperation>
{
    
    bool TryGetOperation(Label name, out IOGraphOperation operation);
}
