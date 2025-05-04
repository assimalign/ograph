using System;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Elements;

[DebuggerDisplay("{Name} [Model]")]
public class Gdm : GdmElement, IOGraphGdm
{
    public Gdm(GdmName name)
    {
        Name = name;
    }

    public GdmName Name { get; }
    public GdmGraphCollection Graphs { get; } = new GdmGraphCollection();
    public override GdmElementKind ElementKind { get; } = GdmElementKind.Model;

    IOGraphGdmGraphCollection IOGraphGdm.Graphs => Graphs;
    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;
}
