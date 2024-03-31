using System;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm;

public abstract class GdmPrimitiveType<T> : GdmType<T>,
    IOGraphGdmPrimitiveType
{
    public virtual string[]? Formats => [];

    public virtual T Parse(string? value)
    {
        throw new NotImplementedException();
    }

    public virtual bool TryParse(string? value, out T result)
    {
        throw new NotImplementedException();
    }

    #region Overloads

    /// <inheritdoc />
    public override GdmTypeKind Kind => GdmTypeKind.Primitive;


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

    object IOGraphGdmPrimitiveType.Parse(string? value)
    {
        return Parse(value)!;
    }

    bool IOGraphGdmPrimitiveType.TryParse(string? value, out object? result)
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