using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Elements;

public abstract class GdmBinding : IOGraphGdmBinding
{
    internal GdmBinding(GdmBindableElement element)
    {
        Element = element;
    }

    GdmName IOGraphGdmBinding.Name => throw new NotImplementedException();
    public virtual GdmBindableElement Element { get; }
    IOGraphGdmBindableElement IOGraphGdmBinding.Element => Element;
}

public abstract class GdmPropertyBinding : GdmBinding
{
    public GdmPropertyBinding(GdmProperty property) : base(property)
    {
        Element = property;
    }

    public new GdmProperty Element { get; }
}
