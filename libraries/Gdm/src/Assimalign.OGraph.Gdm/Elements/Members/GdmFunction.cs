namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public class GdmFunction : GdmMember, IOGraphGdmFunction
{
    public GdmFunction(
        GdmLabel label,
        GdmType returnType,
        GdmType declaringType)
        : base(label, declaringType)
    {
        ReturnType = ThrowHelper.ThrowIfNull(returnType, nameof(returnType));
    }

    public GdmType ReturnType { get; }
    public GdmParameterCollection Parameters { get; } = new GdmParameterCollection();
    public override GdmElementKind ElementKind => GdmElementKind.Function;

    IOGraphGdmType IOGraphGdmFunction.ReturnType => ReturnType;
    IOGraphGdmParameterCollection IOGraphGdmFunction.Parameters => Parameters;

}
