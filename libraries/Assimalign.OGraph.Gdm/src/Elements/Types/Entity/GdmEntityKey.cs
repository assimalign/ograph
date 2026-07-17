namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public sealed class GdmEntityKey : GdmElement, IOGraphGdmEntityKey
{
    internal GdmEntityKey(GdmProperty property)
    {
        Property = ThrowHelper.ThrowIfNull(property);
    }

    public GdmProperty Property { get; }
    public sealed override GdmElementKind ElementKind { get; } = GdmElementKind.Key;
    IOGraphGdmProperty IOGraphGdmEntityKey.Property => Property;
}
