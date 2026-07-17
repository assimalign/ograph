namespace Assimalign.OGraph.Gdm.Elements;

public abstract class GdmCollectionType : GdmType, IOGraphGdmCollectionType
{
    internal GdmCollectionType(GdmName name, GdmGraph graph) : base(name, graph) { }
    public abstract GdmType ItemType { get; }
    public sealed override GdmTypeKind Kind { get; } = GdmTypeKind.Collection;
    IOGraphGdmType IOGraphGdmCollectionType.ItemType => ItemType;
}
