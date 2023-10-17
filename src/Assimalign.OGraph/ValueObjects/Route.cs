using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Assimalign.OGraph;

[DebuggerDisplay("Route: {Value}")]
public readonly struct Route :
    IEquatable<Route>,
    IEqualityComparer<Route>
{
    private readonly string route;
    private readonly RouteSegment[] segments;

    private static ReadOnlySpan<string> reserved => new string[] 
    { 
        "$root"
    };

    public Route(string route)
    {
        if (string.IsNullOrEmpty(route))
        {
            throw new ArgumentNullException(nameof(route));
        }
        this.route = route;
        this.segments = GetSegments(route);
    }

    private RouteSegment[] GetSegments(ReadOnlySpan<char> route)
    {
        var index = 0;
        var segments = new RouteSegment[10];
        var segment = string.Empty;

        for (int i = 0; i < route.Length; i++)
        {
            var character = route[i];

            // Check if we reached the end of the current segment
            if (character == '/' || character == '\\')
            {
                // Let's skip leading slashes
                if (i == 0) continue;

                segments[index] = new RouteSegment(segment, index);
                index++;
                segment = string.Empty;

                // Lets see if we reached the buffer in the array. Resize if reached.
                if (index == segments.Length)
                {
                    Array.Resize(ref segments, 5);
                }
            }
            else if ((i + 1) >= route.Length)
            {
                segment = segment + character;
                segments[index] = new RouteSegment(segment, index);
                index++;
                segment = string.Empty;
            }
            else
            {
                segment = segment + character;
            }
        }

        // Resizes segments to actual length
        Array.Resize(ref segments, index);

        return segments;
    }

    /// <summary>
    /// Gets the raw route value.
    /// </summary>
    public string Value => route;
    /// <summary>
    /// Gets a copy of the route segment.
    /// </summary>
    public RouteSegment[] Segments
    {
        get
        {
            // Let's only return copy
            var copy = new RouteSegment[segments.Length];
            segments.CopyTo(copy, 0);
            return copy;
        }
    }
    /// <summary>
    /// Returns a formatted route value.
    /// </summary>
    public override string ToString()
    {
        return string.Join('/', Segments.Select(x => x.Value));
    }
    /// <summary>
    /// 
    /// </summary>
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        foreach (var segment in Segments)
        {
            hashCode.Add(segment);
        }
        return hashCode.ToHashCode();
    }
    /// <summary>
    /// 
    /// </summary>
    public override bool Equals([NotNullWhen(true)] object? instance)
    {
        if (instance is Route route)
        {
            return Equals(route);
        }
        return false;
    }
    /// <summary>
    /// 
    /// </summary>
    public bool Equals(Route route)
    {
        var left = Segments;
        var right = route.Segments;

        if (left.Length != right.Length)
        {
            return false;
        }
        for (int i = 0; i < left.Length; i++)
        {
            if (!left[i].Equals(right[i]))
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// 
    /// </summary>
    public bool Equals(Route left, Route right)
    {
        return left.Equals(right);
    }
    /// <summary>
    /// 
    /// </summary>
    public int GetHashCode([DisallowNull] Route instance)
    {
        return instance.GetHashCode();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    public bool IsMatch(Path path)
    {
        return IsMatch(path, null);
    }
    /// <summary>
    /// Matches the Path to the Route
    /// </summary>
    /// <param name="path"></param>
    /// <param name="prefix">The prefix in the route to ignore.</param>
    /// <returns></returns>
    public bool IsMatch(Path path, string? prefix = null)
    {
        var route = this;

        if (!string.IsNullOrEmpty(prefix))
        {
            route = route.Value.Trim('/').Trim('\\').Replace(prefix, "");
        }

        var pSegments = path.Segments;
        var rSegments = route.Segments;

        // Ensure same segment length
        if (pSegments.Length != rSegments.Length)
        {
            return false;
        }
        for (int i = 0; i < pSegments.Length; i++)
        {
            if (!rSegments[i].IsMatch(pSegments[i]))
            {
                return false;
            }
        }
        return true;
    }


    public T GetRouteValue<T>(Path path, string paramName)
    {
        throw new NotImplementedException();
        //var segment = 
    }


    public static bool operator ==(Route left, Route right) => left.Equals(right);
    public static bool operator !=(Route left, Route right) => !left.Equals(right);

    public static implicit operator Route(string route) => new Route(route);
    public static implicit operator string(Route route) => route.ToString();
}