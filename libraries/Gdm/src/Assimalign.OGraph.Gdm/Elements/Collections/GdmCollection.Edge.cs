using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Elements;


[DebuggerDisplay("Count = {Count}")]
public class GdmEdgeCollection : List<IOGraphGdmEdge>,
    IOGraphGdmEdgeCollection
{
    public IOGraphGdmEdge this[GdmLabel label] => this.Find(label);
}
