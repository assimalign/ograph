namespace Assimalign.OGraph.Gdm.Elements;

public abstract class GdmElement : IOGraphGdmElement
{
    public abstract GdmElementKind ElementKind { get; }

    public IOGraphGdmMetadata Meta { get; } = new GdmMetadata();
}
