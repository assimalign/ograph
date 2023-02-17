using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphEdgeCollection : List<IOGraphEdge>,
    IOGraphEdgeCollection
{
    public IOGraphEdge this[Name name]
    {
        get => this.First(x => x.Name == name);
        set => this.Add(value);
    }
}
