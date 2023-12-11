using System;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm Type Element = {Label} Primitive")]
public abstract class GdmPrimitiveType<T> : GdmType<T>,
    IOGraphGdmPrimitiveType
{
    public string[]? Formats => throw new NotImplementedException();


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