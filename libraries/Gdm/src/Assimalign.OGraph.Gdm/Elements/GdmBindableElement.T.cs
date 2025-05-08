using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Elements;

public abstract class GdmBindableElement<TBinding> : GdmBindableElement
    where TBinding : IOGraphGdmBinding
{
    internal GdmBindableElement() { }
    internal GdmBindableElement(GdmName name) : base(name) { }
}
