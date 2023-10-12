using System;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.OGraph.Internal;

internal class OGraphMetadata : Dictionary<string, object>,
    IOGraphMetadata
{
    public bool IsValid(object value)
    {
        throw new NotImplementedException();
    }
}
