using System;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.OGraph.Internal;

internal class OGraphMetadata : IOGraphMetadata
{
    private readonly Dictionary<string, object> metadata = new();

    public OGraphMetadata()
    {
        
    }

    public object this[string key] 
    { 
        get => this.metadata[key];
        set => this.metadata[key] = value; 
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => this.metadata.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
