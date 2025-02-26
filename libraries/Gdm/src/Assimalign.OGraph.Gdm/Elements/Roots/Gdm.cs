using System;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Elements;

[DebuggerDisplay("{Label} [Model]")]
public class Gdm : IOGraphGdm
{
    public Gdm(GdmLabel label)
    {
        Label = label;
    }

    public GdmLabel Label { get; }
    public GdmGraphCollection Graphs { get; } = new GdmGraphCollection();
    public GdmMetadata Meta { get; } = new GdmMetadata();
    public GdmElementKind ElementKind { get; } = GdmElementKind.Model;

    IOGraphGdmGraphCollection IOGraphGdm.Graphs => Graphs;
    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;
}
