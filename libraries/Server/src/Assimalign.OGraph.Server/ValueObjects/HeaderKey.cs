using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;

/// <summary>
/// 
/// </summary>
[DebuggerDisplay("{Value}")]
public readonly struct HeaderKey : 
    IEquatable<HeaderKey>,
    IEqualityComparer<HeaderKey>,
    IComparable<HeaderKey>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public HeaderKey(string value)
    {
        if (string.IsNullOrEmpty(value)) 
        {
            ThrowHelper.ThrowArgumentNullException(nameof(value));
        }
        Value = value;
    }
    /// <summary>
    /// The raw header key.
    /// </summary>
    public string Value { get; }

    /// <inheritdoc />
    public override bool Equals(object? instance)
    {
        if (instance is HeaderKey headerKey) 
        {
            return Equals(headerKey);
        }
        return false;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(HeaderKey), Value.ToLower());
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Value;
    }

    /// <inheritdoc />
    public bool Equals(HeaderKey other)
    {
        return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    int IComparable<HeaderKey>.CompareTo(HeaderKey other)
    {
        var left = Value ?? string.Empty;
        var right = other.Value ?? string.Empty;
        var length = Math.Min(left.Length, right.Length); // Only need to compare up to the min length of either value

        for (int i = 0; i < length; i++)
        {
            var a = char.ToLower(left[i]);
            var b = char.ToLower(right[i]);
            var c = a - b;

            if (c != 0)
            {
                return c;
            }
        }
        return 0;
    }
    
    /// <inheritdoc />
    bool IEqualityComparer<HeaderKey>.Equals(HeaderKey left, HeaderKey right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc />
    int IEqualityComparer<HeaderKey>.GetHashCode(HeaderKey instance)
    {
        return instance.GetHashCode();
    }


    public static implicit operator string(HeaderKey key) => key.Value;
    public static implicit operator HeaderKey(string value) => new HeaderKey(value);
}
