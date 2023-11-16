using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;

/// <summary>
/// 
/// </summary>
[DebuggerDisplay("{Value}")]
public readonly struct QueryKey :
    IEquatable<QueryKey>,
    IEqualityComparer<QueryKey>,
    IComparable<QueryKey>
{
    public QueryKey(string value)
    {
        if (string.IsNullOrEmpty(value)) 
        {
            ThrowHelper.ThrowArgumentNullException(nameof(value));
        }
        Value = value;
    }
    /// <summary>
    /// 
    /// </summary>
    public string Value { get; }

    /// <inheritdoc />
    public override bool Equals(object? instance)
    {
        if (instance is QueryKey queryKey)
        {
            return Equals(queryKey);
        }
        return false;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(QueryKey), Value.ToLower());
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Value;
    }

    /// <inheritdoc />
    public bool Equals(QueryKey other)
    {
        return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    int IComparable<QueryKey>.CompareTo(QueryKey other)
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
    bool IEqualityComparer<QueryKey>.Equals(QueryKey left, QueryKey right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc />
    int IEqualityComparer<QueryKey>.GetHashCode(QueryKey instance)
    {
        return instance.GetHashCode();
    }


    public static implicit operator string(QueryKey key) => key.Value;
    public static implicit operator QueryKey(string value) => new QueryKey(value);
}