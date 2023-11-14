using System;

namespace Assimalign.OGraph;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public abstract class OGraphHttpMethodAttribute : Attribute
{
    public OGraphHttpMethodAttribute(string method, string route)
    {
        Method = method;
        Route = route;
    }
    /// <summary>
    /// 
    /// </summary>
    public Method Method { get; }
    /// <summary>
    /// 
    /// </summary>
    public Route Route { get; set; }
}
