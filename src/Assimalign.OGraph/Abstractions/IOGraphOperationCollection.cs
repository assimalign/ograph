using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

public interface IOGraphOperationCollection : IEnumerable<IOGraphOperation>
{
    void Add(IOGraphOperation operation);
}
