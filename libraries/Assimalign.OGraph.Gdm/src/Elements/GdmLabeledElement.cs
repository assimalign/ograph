using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Elements;

public abstract class GdmLabeledElement : GdmElement, IOGraphGdmLabeledElement
{
    private GdmLabel _label;

    internal GdmLabeledElement() { }
    internal GdmLabeledElement(GdmLabel label)
    {
        _label = label;
    }
    public virtual GdmLabel Label => _label;
    public override IEnumerable<TElement> GetElements<TElement>()
    {
        if (this is TElement element)
        {
            yield return element;
        }
    }
    internal void SetLabel(GdmLabel label) => _label = label;
}
