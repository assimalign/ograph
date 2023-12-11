using System;
using System.Buffers;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Assimalign.OGraph;

[DebuggerDisplay("{Value}")]
public readonly partial struct RouteSegment :
    IEquatable<RouteSegment>,
    IEqualityComparer<RouteSegment>
{
    internal RouteSegment(string value, int ordinal)
    {
        Value = value;
        Ordinal = ordinal;
    }

    /// <summary>
    /// The raw segment value.
    /// </summary>
    public string Value { get; }
    /// <summary>
    /// The index of the route segment.
    /// </summary>
    public int Ordinal { get; }

    /// <inheritdoc />
    public bool Equals(RouteSegment other)
    {
        return Ordinal == other.Ordinal && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public bool Equals(RouteSegment right, RouteSegment left)
    {
        return right.Equals(left);
    }

    /// <inheritdoc />
    public int GetHashCode(RouteSegment instance)
    {
        return instance.GetHashCode();
    }
    
    /// <inheritdoc />
    public override bool Equals(object? instance)
    {
        if (instance is RouteSegment segment)
        {
            return Equals(segment);
        }
        return false;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(RouteSegment), Ordinal, string.Create(Value.Length, Value, (chars, name) =>
        {
            // A string value's hash-code takes casing into account. Let's lowercase the characters since routes are insensitive
            name.CopyTo(chars);
            for (int i = 0; i < chars.Length;i++)
            {
                var c = chars[i];
                if (char.IsUpper(c))
                {
                    chars[i] = char.ToLower(c);
                }
            }
        }));
    }

    /// <inheritdoc />
    public override string ToString() => Value;

    /// <summary>
    /// Checks whether the segment is a route parameter.
    /// </summary>
    /// <returns></returns>
    public bool IsParameter()
    {
        return Value[0] == '{' && Value[Value.Length - 1] == '}';
    }

    /// <summary>
    /// Get's parameter name if segment is parameter.
    /// </summary>
    /// <returns></returns>
    public string? GetParamName()
    {
        return IsParameter() ?
            Value.Substring(1, Value.Length - 2).Split(':').First() : 
            null;
    }
}