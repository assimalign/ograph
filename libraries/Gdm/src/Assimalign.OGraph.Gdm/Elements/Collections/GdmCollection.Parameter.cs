using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Internal;

public class GdmParameterCollection : List<IOGraphGdmParameter>, IOGraphGdmParameterCollection
{
    public IOGraphGdmParameter this[Label label] => this.Find(label);
}
