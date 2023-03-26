using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Assimalign.OGraph;

[DebuggerDisplay("Route: /{ToString()}")]
public readonly struct Route : IEquatable<Route>, IEqualityComparer<Route>
{
	public Route(string route)
	{
		if (string.IsNullOrEmpty(route))
		{
			throw new ArgumentNullException(nameof(route));
		}
		Segments = route.Trim('/').Split('/').Select(segment =>
		{
			var start = segment.IndexOf('{');
			var end = segment.IndexOf('}');

			return start == -1 ? 
				new RouteSegment(segment, isLiteral: true) : 
				new RouteSegment(segment.Substring(start + 1, end - start - 1), isParameter: true);

		}).ToArray();
	}

	public RouteSegment[] Segments { get; }

	public static implicit operator Route(string route) => new Route(route);
	public static implicit operator string(Route route) => string.Join('/', route.Segments.Select(x=>x.Value));


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
	public bool Equals(Route route)
	{
		if (route.Segments.Length != Segments.Length)
		{
			return false;
		}
		for (int i = 0; i < Segments.Length; i++)
		{
			var incoming = route.Segments[i];
			var current = Segments[i];

			if (incoming.Value != current.Value)
			{
				return false;
			}
		}
		return true;

	}
	public bool Equals(Route left, Route right)
	{
		return left.Equals(right);
	}

	public int GetHashCode([DisallowNull] Route instance)
	{
		return instance.GetHashCode();
	}

	public bool IsMatch(Path path, string prefix = null)
	{
		path = path.ToString().Replace(prefix, "");

		if (path.Segments.Length != Segments.Length)
		{
			return false;
		}
		for (int i = 0; i < Segments.Length; i++)
		{
			var routeSegment = Segments[i];
			var pathSegment = path.Segments[i];

			if (routeSegment.IsParameter)
			{
				if (!routeSegment.IsValid(pathSegment))
				{
					return false;
				}
				continue;
			}
			
			if (!routeSegment.Value.Equals(pathSegment.Value, StringComparison.CurrentCultureIgnoreCase))
			{
				return false;
			}
		}


		return true;
	}
}
