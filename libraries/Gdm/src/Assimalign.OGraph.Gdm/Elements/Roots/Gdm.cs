using System;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Elements;

[DebuggerDisplay("{Label} [Model]")]
public class Gdm : IOGraphGdm
{
    public Gdm(Label label)
    {
        Label = label;
    }

    public Label Label { get; }
    public GdmElementCollection Elements { get; } = new GdmElementCollection();
    public GdmMetadata Meta { get; } = new GdmMetadata();
    public GdmElementKind ElementKind { get; } = GdmElementKind.Model;

    IOGraphGdmElementCollection IOGraphGdm.Elements => Elements;
    IOGraphGdmMetadata IOGraphGdmElement.Meta => Meta;
}
