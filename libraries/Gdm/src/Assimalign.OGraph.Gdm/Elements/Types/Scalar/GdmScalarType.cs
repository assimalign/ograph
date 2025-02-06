using System;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Elements;

public abstract class GdmScalarType<T> : GdmType<T>,
    IOGraphGdmScalarType
{
    public virtual string[]? Formats => [];

    public virtual T Parse(object? value)
    {
        throw new NotImplementedException();
    }

    public virtual bool TryParse(object? value, out T result)
    {
        throw new NotImplementedException();
    }

    #region Overloads

    /// <inheritdoc />
    public override GdmTypeKind Kind => GdmTypeKind.Scalar;

    /// <inheritdoc />
    public override string ToString()
    {
        return Label;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is not null)
        {
            return GetHashCode() == obj.GetHashCode();
        }
        return false;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Label.GetHashCode();
    }

    object IOGraphGdmScalarType.Parse(object? value)
    {
        return Parse(value)!;
    }

    bool IOGraphGdmScalarType.TryParse(object? value, out object? result)
    {
        result = default;
        if (TryParse(value, out var r))
        {
            result = r;
            return true;
        }
        return false;
    }

    #endregion
}