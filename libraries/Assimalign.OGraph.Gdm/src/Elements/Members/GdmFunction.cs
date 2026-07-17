namespace Assimalign.OGraph.Gdm.Elements;

using Internal;
using System;

public class GdmFunction : GdmMember, IOGraphGdmFunction
{
    internal GdmFunction(
        GdmName label,
        GdmType returnType,
        GdmComplexType declaringType)
        : base(label, declaringType)
    {
        ReturnType = ThrowHelper.ThrowIfNull(returnType, nameof(returnType));
    }

    public GdmType ReturnType { get; }
    public GdmParameterCollection Parameters { get; } = new GdmParameterCollection();

    IOGraphGdmType IOGraphGdmFunction.ReturnType => ReturnType;
    IOGraphGdmParameterCollection IOGraphGdmFunction.Parameters => throw new NotImplementedException();

}
