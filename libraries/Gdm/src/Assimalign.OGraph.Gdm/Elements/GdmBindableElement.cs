using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Elements;

public abstract class GdmBindableElement : GdmNamedElement, IOGraphGdmBindableElement
{
    internal GdmBindableElement() { }
    internal GdmBindableElement(GdmName name) : base(name) { }
    public bool IsBound { get; }
    public override IEnumerable<TElement> GetElements<TElement>()
    {
        if (this is TElement element)
        {
            yield return element;
        }
    }
}
