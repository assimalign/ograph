namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public class GdmEntityKey : IOGraphGdmEntityKey
{
    public GdmEntityKey(GdmProperty property)
    {
        Property = ThrowHelper.ThrowIfNull(property, nameof(property));
    }

    public GdmProperty Property { get; }
    public GdmMetadata Meta { get; } = new GdmMetadata();
    public GdmElementKind ElementKind => GdmElementKind.Key;

    IOGraphGdmProperty IOGraphGdmEntityKey.Property => Property ;
    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;
}
