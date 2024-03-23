using System;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm;

public abstract class GdmPrimitiveType<T> : GdmType<T>,
    IOGraphGdmPrimitiveType
{
    public virtual string[]? Formats => [];

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

    #endregion





}