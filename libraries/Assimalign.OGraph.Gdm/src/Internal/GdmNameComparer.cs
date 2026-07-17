using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmNameComparer : IEqualityComparer<GdmName>, IAlternateEqualityComparer<GdmName, ReadOnlySpan<char>>
{
    public bool Equals(GdmName x, GdmName y)
    {
        return x.Equals(y);
    }

    public int GetHashCode([DisallowNull] GdmName obj)
    {
        throw new NotImplementedException();
    }

    ReadOnlySpan<char> IAlternateEqualityComparer<GdmName, ReadOnlySpan<char>>.Create(GdmName alternate)
    {
        return alternate.AsSpan();
    }

    bool IAlternateEqualityComparer<GdmName, ReadOnlySpan<char>>.Equals(GdmName alternate, ReadOnlySpan<char> other)
    {
        return alternate.AsSpan().SequenceEqual(other);
        throw new NotImplementedException();
    }

    int IAlternateEqualityComparer<GdmName, ReadOnlySpan<char>>.GetHashCode(GdmName alternate)
    {
        throw new NotImplementedException();
    }
}
