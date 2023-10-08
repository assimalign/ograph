using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Assimalign.OGraph;

[DebuggerDisplay("Route: /{ToString()}")]
public readonly struct Route :
    IEquatable<Route>,
    IEqualityComparer<Route>
{
    private readonly string route;
    private readonly RouteSegment[] segments;

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

                segments[index] = new RouteSegment(segment);
                index++;
                segment = string.Empty;

                // Lets see if we reached the buffer in the array. Resize if reached.
                if (index == segments.Length)
                {
                    Array.Resize(ref segments, 5);
                }
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
        return "/" + string.Join('/', Segments.Select(x => x.Value));
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
        return IsMatch(path);
    }
    /// <summary>
    /// Matches the Path to the Route
    /// </summary>
    /// <param name="path"></param>
    /// <param name="prefix"></param>
    /// <returns></returns>
    public bool IsMatch(Path path, string prefix = null)
    {
        for (int i = 0; i < segments.Length; i++)
        {

        }
        var pathSegments = path.Segments;
        var routeSegments = Segments;

        if (pathSegments.Length != routeSegments.Length)
        {
            return false;
        }

        for (int i = 0; i < pathSegments.Length; i++)
        {

        }



        //path.Segments
        //		path = path.ToString().Replace(prefix, "");
        //
        //		if (path.Segments.Length != Segments.Length)
        //		{
        //			return false;
        //		}
        //		for (int i = 0; i < Segments.Length; i++)
        //		{
        //			var routeSegment = Segments[i];
        //			var pathSegment = path.Segments[i];
        //
        //			if (routeSegment.IsParameter)
        //			{
        //				if (!routeSegment.IsValid(pathSegment))
        //				{
        //					return false;
        //				}
        //				continue;
        //			}
        //			if (!routeSegment.Value.Equals(pathSegment.Value, StringComparison.CurrentCultureIgnoreCase))
        //			{
        //				return false;
        //			}
        //		}
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="paramName"></param>
    /// <returns></returns>
    public T GetRouteValue<T>(string paramName)
        where T : struct
    {
        for (int i = 0; i < segments.Length; i++)
        {

        }

        throw new InvalidOperationException($"No route value with parameter name: {paramName} was found");
    }

    public static implicit operator Route(string route) => new Route(route);
    public static implicit operator string(Route route) => route.ToString();
}