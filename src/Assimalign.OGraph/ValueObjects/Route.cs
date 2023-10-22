using System;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph;

[DebuggerDisplay("Route: {Value}")]
public readonly struct Route :
    IEquatable<Route>,
    IEqualityComparer<Route>,
    IComparable<Route>
{
    private readonly RouteSegment[] segments;

    private static string[] reserved => new string[]
    {
        "$query",
        "$transactions" // for sending 
    };

    public Route(string route)
    {
        if (string.IsNullOrEmpty(route))
        {
            throw new ArgumentNullException(nameof(route));
        }
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

                if (reserved.Contains(segment, StringComparer.OrdinalIgnoreCase))
                {
                    throw new Exception($"Invalid route. Using Reserved route: {segment} is not allowed.");
                }

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
    public string Value => ToString();
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
    public override string ToString()
    {
        return string.Join('/', Segments.Select(x => x.Value));
    }
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        foreach (var segment in Segments)
        {
            hashCode.Add(segment);
        }
        return hashCode.ToHashCode();
    }
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
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="paramName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public T GetRouteValue<T>(Path path, string paramName)
    {
        throw new NotImplementedException();
        //var segment = 
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Path Format(params object[] args)
    {
        var stringBuilder = new StringBuilder();
        var segments = Segments;
        var parameterCount = segments.Count(p => p.SegmentType.Equals(RouteSegmentType.Parameter));
        
        if (parameterCount.Equals(args.Length))
        {
            throw new ArgumentException($"Argument count does not match the number of route parameters. Expected {parameterCount}, received {args.Length}");
        }

        for (int i = 0; i < segments.Length; i++)
        {
            var segment = segments[i];

            switch (segment.SegmentType)
            {
                case RouteSegmentType.Literal:
                    {
                        stringBuilder.Append(segment.Value);
                        break;
                    }
                case RouteSegmentType.Parameter:
                    {
                        // TODO: 
                        break;
                    }
            }
        }

        return stringBuilder.ToString();
    }

    #region Implicit Interfaces
    bool IEquatable<Route>.Equals(Route route)
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
    bool IEqualityComparer<Route>.Equals(Route left, Route right)
    {
        return left.Equals(right);
    }
    int IEqualityComparer<Route>.GetHashCode([DisallowNull] Route instance)
    {
        return instance.GetHashCode();
    }
    int IComparable<Route>.CompareTo(Route other)
    {
        return Value.ToLowerInvariant().CompareTo(other.Value.ToLowerInvariant());
    }
    #endregion

    #region Operators
    public static bool operator ==(Route left, Route right) => left.Equals(right);
    public static bool operator !=(Route left, Route right) => !left.Equals(right);

    public static implicit operator Route(string route) => new Route(route);
    public static implicit operator string(Route route) => route.ToString();
    #endregion
}