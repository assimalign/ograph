using Assimalign.OGraph.Gdm.Internal;
using System;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Elements;

public abstract class GdmScalarType<T> : GdmType<T>, IOGraphGdmScalarType
{
    public GdmScalarType(GdmGraph graph)
    {
        base.Graph = ThrowHelper.ThrowIfNull(graph, nameof(graph));
    }

    public virtual string[] Formats => [];
    public abstract GdmPrimitiveType PrimitiveType { get; }
    public abstract T Parse(string? value);
    public abstract bool TryParse(string? value, out T result);
    public object Parse(object? value)
    {
        if (value is not string)
        {
            throw new ArgumentException("");
        }
        return Parse(value)!;
    }
    public bool TryParse(object? value, out object? result)
    {
        result = default;
        if (value is string str && TryParse(str, out var r))
        {
            result = r;
            return true;
        }
        return false;
    }
    /// <inheritdoc />
    public override GdmTypeKind Kind => GdmTypeKind.Scalar;
}