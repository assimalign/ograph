using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph;

[DebuggerDisplay("Path Segment: {Value}")]
public readonly struct PathSegment :
    IEquatable<PathSegment>,
    IEqualityComparer<PathSegment>
{
    internal PathSegment(string value, int ordinal)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        this.Value = value;
        this.Ordinal = ordinal;
    }
    /// <summary>
    /// The raw segment value.
    /// </summary>
    public string Value { get; }
    /// <summary>
    /// 
    /// </summary>
    public int Ordinal { get; }

    /// <inheritdoc />
    public override string ToString()
    {
        return Value;
    }

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? instance)
    {
        if (instance is PathSegment segment)
        {
            return Equals(segment);
        }
        return false;
    }

    /// <inheritdoc />
    public bool Equals(PathSegment other)
    {
        return Ordinal == other.Ordinal && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public bool Equals(PathSegment left, PathSegment right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(PathSegment), Ordinal, string.Create(Value.Length, Value, (chars, name) =>
        {
            name.CopyTo(chars);
            for (int i = 0; i < chars.Length; i++)
            {
                var c = chars[i];
                if (char.IsUpper(c))
                {
                    chars[i] = char.ToLower(c);
                }
            }
        }));
    }

    public int GetHashCode([DisallowNull] PathSegment instance)
    {
        return instance.GetHashCode();
    }
}