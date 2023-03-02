using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
