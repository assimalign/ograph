using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Elements;

public abstract class GdmNamedElement : GdmElement, IOGraphGdmNamedElement
{
    private GdmName _name;

    internal GdmNamedElement() { }
    internal GdmNamedElement(GdmName name)
    {
        _name = name;
    }

    public virtual GdmName Name => _name;
    public override IEnumerable<TElement> GetElements<TElement>()
    {
        if (this is TElement element)
        {
            yield return element;
        }
    }
    internal virtual void SetName(GdmName name) => _name = name;
}
