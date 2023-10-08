using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph;

[DebuggerDisplay("Segment: {Value}")]
public readonly struct RouteSegment :
    IEquatable<RouteSegment>,
    IEqualityComparer<RouteSegment>
{
    //private readonly Func<string, >
    internal RouteSegment(string value)
    {
        if (value[0] == '{' && value[value.Length - 1] == '}')
        {
            var item = value.Substring(1, value.Length - 2);

            this.Value = value;
            this.SegmentType = RouteSegmentType.Parameter;
        }
        else
        {
            this.Value = value;
            this.SegmentType = RouteSegmentType.Literal;
        }
    }

    /// <summary>
    /// The raw segment value.
    /// </summary>
    public string Value { get; }
    /// <summary>
    /// A 
    /// </summary>
    public RouteSegmentType SegmentType { get; }
    /// <summary>
    /// The index of the route
    /// </summary>
    public int Index { get; }
    /// <summary>
    /// 
    /// </summary>
    public bool Equals(RouteSegment other)
    {
        return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
    }
    /// <summary>
    /// 
    /// </summary>
    public bool Equals(RouteSegment right, RouteSegment left)
    {
        return right.Equals(left);
    }
    /// <summary>
    /// 
    /// </summary>
    public int GetHashCode(RouteSegment instance)
    {
        return instance.GetHashCode();
    }
    
    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? instance)
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
        return HashCode.Combine(this, Value);
    }

    /// <inheritdoc />
    public override string ToString() => Value;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="paramName"></param>
    /// <returns></returns>
    //public bool HasName(string paramName)
    //{

    //}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="segment"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public bool IsLiteralMatch(PathSegment segment)
    {
        if (SegmentType != RouteSegmentType.Literal)
        {
            throw new InvalidOperationException("");
        }

        return Value.Equals(segment.Value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="segment"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public bool IsParameterMatch(PathSegment segment)
    {
        if (SegmentType != RouteSegmentType.Parameter)
        {
            throw new InvalidOperationException("");
        }

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="segment"></param>
    /// <returns></returns>
    public bool IsValid(PathSegment segment)
    {
        //segment.Value.Se


        return true;
    }
}
