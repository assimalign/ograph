using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Internal;


[DebuggerDisplay("Count = {Count}")]
internal class GdmEdgeReferenceCollection : List<IOGraphGdmEdgeReference>,
    IOGraphGdmEdgeReferenceCollection
{
    
}
