using System;

namespace Assimalign.OGraph;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class OGraphHttpRouteAttribute : Attribute
{
    public OGraphHttpRouteAttribute(string route)
    {
        this.Route = route;
    }

    public Route Route { get; }
}
