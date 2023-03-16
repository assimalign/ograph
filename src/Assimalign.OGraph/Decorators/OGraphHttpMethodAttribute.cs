using System;

namespace Assimalign.OGraph;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class OGraphHttpMethodAttribute : Attribute
{
    public OGraphHttpMethodAttribute(string method)
    {
        this.Method = method;
    }

    public Method Method { get; }
}
