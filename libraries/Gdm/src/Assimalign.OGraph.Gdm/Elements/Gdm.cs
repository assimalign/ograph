using System;

namespace Assimalign.OGraph.Gdm.Elements;

public class Gdm : IOGraphGdm
{
    public Gdm(Label label)
    {
        Label = label;
    }

    public Label Label { get; }
    public IOGraphGdmElementCollection Elements { get; } = new GdmElementCollection();
    public IOGraphGdmMetadata Meta { get; } = new GdmMetadata();
    public GdmElementKind ElementKind { get; } = GdmElementKind.Model;
}
