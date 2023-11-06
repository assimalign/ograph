using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm Edge Collection: {Count}")]
internal class GdmEdgeCollection : List<IOGraphGdmEdge>, 
    IOGraphGdmEdgeCollection
{

}