namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public class GdmEntityKey : GdmElement, IOGraphGdmEntityKey
{
    public GdmEntityKey(GdmProperty property)
    {
        Property = ThrowHelper.ThrowIfNull(property);
    }

    public GdmProperty Property { get; }
    public override GdmElementKind ElementKind { get; }= GdmElementKind.Key;
    IOGraphGdmProperty IOGraphGdmEntityKey.Property => Property ;
    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;
}
