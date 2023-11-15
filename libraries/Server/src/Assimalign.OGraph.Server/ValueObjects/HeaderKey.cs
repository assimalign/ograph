using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

public readonly struct HeaderKey : 
    IEquatable<HeaderKey>,
    IEqualityComparer<HeaderKey>,
    IComparable<HeaderKey>
{
    public HeaderKey(string value)
    {
        Value = value;
    }
    /// <summary>
    /// 
    /// </summary>
    public string Value { get; }

    public bool Equals(HeaderKey other)
    {
        throw new NotImplementedException();
    }

    int IComparable<HeaderKey>.CompareTo(HeaderKey other)
    {
        throw new NotImplementedException();
    }

    bool IEqualityComparer<HeaderKey>.Equals(HeaderKey x, HeaderKey y)
    {
        throw new NotImplementedException();
    }

    int IEqualityComparer<HeaderKey>.GetHashCode(HeaderKey obj)
    {
        throw new NotImplementedException();
    }


    public static implicit operator string(HeaderKey key) => key.Value;
    public static implicit operator HeaderKey(string value) => new HeaderKey(value);
}
