namespace Assimalign.OGraph;

public sealed class OGraphHttpGetAttribute : OGraphHttpMethodAttribute
{
    public OGraphHttpGetAttribute(string route) 
        : base("GET", route)
    {
    }
}
