namespace Assimalign.OGraph.Gdm.Elements;

public abstract class GdmScalarType : GdmType, IOGraphGdmScalarType
{
    internal GdmScalarType() { }
    internal GdmScalarType(GdmGraph graph) : base(graph) { }
    public virtual string?[]? Formats { get; } = [];
    public abstract GdmPrimitiveType PrimitiveType { get; }
    public sealed override GdmTypeKind Kind { get; } = GdmTypeKind.Scalar;
    public abstract object Parse(object? value);
    public abstract bool TryParse(object? value, out object? result);
}
