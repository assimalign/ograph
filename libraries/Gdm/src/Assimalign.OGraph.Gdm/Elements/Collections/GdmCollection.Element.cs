using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Elements;

public class GdmElementCollection : List<IOGraphGdmElement>, IOGraphGdmElementCollection
{    
    public TElement Find<TElement>(Label label) where TElement : IOGraphGdmLabeledElement
    {
        return this.OfType<TElement>().First(p => p.Label.Equals(label));
    }
}