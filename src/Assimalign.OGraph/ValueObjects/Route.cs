using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public readonly struct Route
{

	public Route(string route)
	{
		Value = route;
	}

	public string Value { get; }

	public static implicit operator Route(string route) => new Route(route);
	public static implicit operator string(Route route) => route.Value;
    
}
