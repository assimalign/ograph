using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmElementCollection : List<IOGraphGdmElement>,
    IOGraphGdmElementCollection
{
    public TGdmElement Get<TGdmElement>(Label label) where TGdmElement : IOGraphGdmLabeledElement
    {
        foreach (var item in this)
        {
            if (item is TGdmElement element && element.Label == label)
            {
                return element;
            }
        }

        throw new InvalidOperationException();
    }
}