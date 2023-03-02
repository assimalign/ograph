using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Assimalign.OGraph.Internal;

internal class OGraphNodeCollection : List<IOGraphNode>, 
    IOGraphNodeCollection
{ 
    public IOGraphNode this[Label label]
    {
        get => this.First(x=>x.Label== label);
        set => this.Add(value);
    }

}
